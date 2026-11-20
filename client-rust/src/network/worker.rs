use std::net::{Ipv4Addr, SocketAddrV4, UdpSocket};
use std::thread;
use std::time::{Duration, Instant};

use bevy::log::{debug, error, info, warn};
use crossbeam_channel::{Receiver, Sender};
use enet::{Address, BandwidthLimit, ChannelLimit, Enet, EventKind, Host, Packet, PacketMode, PeerID};

use crate::protocol::{ProtocolMessage, TextureChannel};

pub const DISCOVERY_PORT: u16 = 9020;
pub const ENET_PORT: u16 = 9022;
pub const ENET_CHANNEL_COUNT: usize = 10;
const DISCOVERY_INTERVAL: Duration = Duration::from_millis(900);

#[derive(Debug, Clone)]
pub enum NetworkCommand {
    /// Start UDP broadcast discovery until a server responds with `Ack`, then connect.
    StartDiscovery,
    /// Connect directly to a known server IPv4 address.
    Connect(Ipv4Addr),
    /// Disconnect current peer (if any) and stop discovery.
    Disconnect,
    /// Send a reliable `ProtocolMessage` on channel 0.
    Send(ProtocolMessage),
    /// Shut down the worker thread cleanly.
    Shutdown,
}

#[derive(Debug, Clone)]
pub enum NetworkEvent {
    Discovering,
    Connecting(Ipv4Addr),
    Connected(Ipv4Addr),
    Disconnected,
    TextureFrame {
        channel: TextureChannel,
        jpeg_bytes: Vec<u8>,
    },
    Error(String),
}

struct PeerData;

struct DiscoveryState {
    socket: UdpSocket,
    last_hello: Instant,
}

impl DiscoveryState {
    fn start() -> std::io::Result<Self> {
        let socket = UdpSocket::bind(SocketAddrV4::new(Ipv4Addr::UNSPECIFIED, 0))?;
        socket.set_nonblocking(true)?;
        socket.set_broadcast(true)?;
        Ok(Self {
            socket,
            last_hello: Instant::now() - DISCOVERY_INTERVAL,
        })
    }

    fn poll(&mut self) -> Option<Ipv4Addr> {
        if self.last_hello.elapsed() >= DISCOVERY_INTERVAL {
            let hello = ProtocolMessage::Hello {};
            if let Ok(bytes) = hello.to_msgpack() {
                let target = SocketAddrV4::new(Ipv4Addr::BROADCAST, DISCOVERY_PORT);
                if let Err(e) = self.socket.send_to(&bytes, target) {
                    warn!("UDP discovery broadcast failed: {}", e);
                }
            }
            self.last_hello = Instant::now();
        }

        let mut buf = [0u8; 512];
        while let Ok((len, addr)) = self.socket.recv_from(&mut buf) {
            if let Ok(ProtocolMessage::Ack {}) = ProtocolMessage::from_msgpack(&buf[..len])
                && let std::net::SocketAddr::V4(v4) = addr
            {
                return Some(*v4.ip());
            }
        }
        None
    }
}

pub fn spawn_network_worker(
    cmd_rx: Receiver<NetworkCommand>,
    event_tx: Sender<NetworkEvent>,
) -> thread::JoinHandle<()> {
    thread::Builder::new()
        .name("enet-client-worker".into())
        .spawn(move || run_worker(cmd_rx, event_tx))
        .expect("failed to spawn enet-client-worker thread")
}

fn run_worker(cmd_rx: Receiver<NetworkCommand>, event_tx: Sender<NetworkEvent>) {
    let enet = match Enet::new() {
        Ok(e) => e,
        Err(e) => {
            error!("Failed to initialize ENet: {}", e);
            let _ = event_tx.send(NetworkEvent::Error(format!("ENet init failed: {}", e)));
            return;
        }
    };

    let mut host: Host<PeerData> = match enet.create_host(
        None,
        1,
        ChannelLimit::Limited(ENET_CHANNEL_COUNT),
        BandwidthLimit::Unlimited,
        BandwidthLimit::Unlimited,
    ) {
        Ok(h) => h,
        Err(e) => {
            error!("Failed to create ENet client host: {}", e);
            let _ = event_tx.send(NetworkEvent::Error(format!(
                "ENet host creation failed: {}",
                e
            )));
            return;
        }
    };

    let mut discovery: Option<DiscoveryState> = None;
    let mut active_peer: Option<PeerID> = None;
    let mut target_ip: Option<Ipv4Addr> = None;

    loop {
        // 1. Drain all pending commands from Bevy
        while let Ok(cmd) = cmd_rx.try_recv() {
            match cmd {
                NetworkCommand::Shutdown => {
                    disconnect_peer(&mut host, &mut active_peer);
                    info!("ENet client worker shutting down");
                    return;
                }
                NetworkCommand::Disconnect => {
                    discovery = None;
                    target_ip = None;
                    disconnect_peer(&mut host, &mut active_peer);
                }
                NetworkCommand::StartDiscovery => {
                    disconnect_peer(&mut host, &mut active_peer);
                    target_ip = None;
                    match DiscoveryState::start() {
                        Ok(d) => {
                            info!("Started UDP server discovery on port {}", DISCOVERY_PORT);
                            discovery = Some(d);
                            let _ = event_tx.send(NetworkEvent::Discovering);
                        }
                        Err(e) => {
                            error!("Failed to start UDP discovery: {}", e);
                            let _ = event_tx
                                .send(NetworkEvent::Error(format!("Discovery failed: {}", e)));
                        }
                    }
                }
                NetworkCommand::Connect(ip) => {
                    discovery = None;
                    disconnect_peer(&mut host, &mut active_peer);
                    connect_to(&mut host, ip, &mut active_peer, &mut target_ip, &event_tx);
                }
                NetworkCommand::Send(msg) => {
                    if let Some(peer_id) = active_peer
                        && let Some(peer) = host.peer_mut(peer_id)
                    {
                        match msg.to_msgpack() {
                            Ok(bytes) => {
                                if let Ok(packet) = Packet::new(bytes, PacketMode::ReliableSequenced)
                                    && let Err(e) = peer.send_packet(packet, 0)
                                {
                                    warn!("Failed to queue ENet packet: {}", e);
                                }
                            }
                            Err(e) => error!("Failed to serialize ProtocolMessage: {}", e),
                        }
                    }
                }
            }
        }

        // 2. Poll UDP discovery if active
        if let Some(ref mut disc) = discovery
            && let Some(ip) = disc.poll()
        {
            info!("Discovered server at {}", ip);
            discovery = None;
            connect_to(&mut host, ip, &mut active_peer, &mut target_ip, &event_tx);
        }

        // 3. Drain ALL pending ENet events so the receive queue never backs up
        loop {
            match host.service(Duration::from_millis(0)) {
                Ok(Some(event)) => match event.kind() {
                    EventKind::Connect => {
                        let peer_id = event.peer_id();
                        active_peer = Some(peer_id);
                        let ip = target_ip.unwrap_or(Ipv4Addr::UNSPECIFIED);
                        info!("Connected to server {} (peer {:?})", ip, peer_id);
                        let _ = event_tx.send(NetworkEvent::Connected(ip));
                    }
                    EventKind::Disconnect { .. } => {
                        info!("Disconnected from server");
                        active_peer = None;
                        let _ = event_tx.send(NetworkEvent::Disconnected);
                    }
                    EventKind::Receive { channel_id, packet } => {
                        if let Some(channel) = TextureChannel::from_channel_id(*channel_id) {
                            let _ = event_tx.send(NetworkEvent::TextureFrame {
                                channel,
                                jpeg_bytes: packet.data().to_vec(),
                            });
                        } else {
                            debug!("Ignoring packet on unknown channel {}", channel_id);
                        }
                    }
                },
                Ok(None) => break,
                Err(e) => {
                    error!("ENet host.service() error: {}", e);
                    break;
                }
            }
        }

        thread::sleep(Duration::from_millis(2));
    }
}

fn disconnect_peer(host: &mut Host<PeerData>, active_peer: &mut Option<PeerID>) {
    if let Some(peer_id) = active_peer.take()
        && let Some(peer) = host.peer_mut(peer_id)
    {
        peer.disconnect(0);
        host.flush();
    }
}

fn connect_to(
    host: &mut Host<PeerData>,
    ip: Ipv4Addr,
    active_peer: &mut Option<PeerID>,
    target_ip: &mut Option<Ipv4Addr>,
    event_tx: &Sender<NetworkEvent>,
) {
    let address = Address::new(ip, ENET_PORT);
    match host.connect(&address, ENET_CHANNEL_COUNT, 0) {
        Ok((_peer, peer_id)) => {
            *active_peer = Some(peer_id);
            *target_ip = Some(ip);
            info!("Connecting to {}:{}...", ip, ENET_PORT);
            let _ = event_tx.send(NetworkEvent::Connecting(ip));
        }
        Err(e) => {
            error!("ENet connect to {}:{} failed: {}", ip, ENET_PORT, e);
            let _ = event_tx.send(NetworkEvent::Error(format!("Connect failed: {}", e)));
        }
    }
}
