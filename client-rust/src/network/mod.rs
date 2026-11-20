pub mod textures;
pub mod worker;

use std::net::Ipv4Addr;

use bevy::prelude::*;
use crossbeam_channel::{Receiver, Sender, unbounded};

use crate::network::textures::{StreamedTextures, TextureDecoder};
use crate::network::worker::{NetworkCommand, NetworkEvent, spawn_network_worker};
use crate::protocol::{Command, ProtocolMessage};
use crate::settings::Settings;
use crate::ui::AppState;

/// High-level connection state displayed on the Connecting screen and used by UI systems.
#[derive(Resource, Debug, Clone, PartialEq, Eq, Default)]
pub enum ConnectionStatus {
    #[default]
    Discovering,
    Connecting(Ipv4Addr),
    Connected(Ipv4Addr),
    Error(String),
}

impl ConnectionStatus {
    pub fn display_text(&self) -> String {
        match self {
            Self::Discovering => "Discovering server...".to_string(),
            Self::Connecting(ip) => format!("Connecting to {}...", ip),
            Self::Connected(ip) => format!("Connected to {}", ip),
            Self::Error(msg) => format!("Connection error: {}", msg),
        }
    }
}

/// Bevy resource for sending commands to the ENet/UDP background worker.
#[derive(Resource)]
pub struct NetworkClient {
    cmd_tx: Sender<NetworkCommand>,
    event_rx: Receiver<NetworkEvent>,
    decoder: TextureDecoder,
}

impl NetworkClient {
    pub fn start_discovery(&self) {
        let _ = self.cmd_tx.send(NetworkCommand::StartDiscovery);
    }

    pub fn connect(&self, ip: Ipv4Addr) {
        let _ = self.cmd_tx.send(NetworkCommand::Connect(ip));
    }

    pub fn disconnect(&self) {
        let _ = self.cmd_tx.send(NetworkCommand::Disconnect);
    }

    pub fn send(&self, msg: ProtocolMessage) {
        let _ = self.cmd_tx.send(NetworkCommand::Send(msg));
    }

    pub fn start_stream(&self, identifier: impl Into<String>, refresh_rate: u16, quality: u16) {
        self.send(ProtocolMessage::StreamedTextureRequest {
            identifier: identifier.into(),
            command: Command::Start,
            refresh_rate: Some(refresh_rate),
            quality: Some(quality),
        });
    }

    pub fn stop_stream(&self, identifier: impl Into<String>) {
        self.send(ProtocolMessage::StreamedTextureRequest {
            identifier: identifier.into(),
            command: Command::Stop,
            refresh_rate: None,
            quality: None,
        });
    }

    pub fn connect_or_discover(&self, settings: &Settings) {
        if let Some(ip) = settings
            .ip
            .as_deref()
            .and_then(|s| s.parse::<Ipv4Addr>().ok())
        {
            self.connect(ip);
        } else {
            self.start_discovery();
        }
    }
}

impl Drop for NetworkClient {
    fn drop(&mut self) {
        let _ = self.cmd_tx.send(NetworkCommand::Shutdown);
    }
}

pub struct NetworkPlugin;

impl Plugin for NetworkPlugin {
    fn build(&self, app: &mut App) {
        let (cmd_tx, cmd_rx) = unbounded();
        let (event_tx, event_rx) = unbounded();
        let _worker = spawn_network_worker(cmd_rx, event_tx);
        let decoder = TextureDecoder::spawn();

        app.insert_resource(NetworkClient {
            cmd_tx,
            event_rx,
            decoder,
        })
        .init_resource::<ConnectionStatus>()
        .add_systems(Startup, init_streamed_textures)
        .add_systems(OnEnter(AppState::Connecting), begin_connecting)
        .add_systems(Update, (poll_network_events, apply_decoded_textures));
    }
}

fn init_streamed_textures(mut commands: Commands, mut images: ResMut<Assets<Image>>) {
    commands.insert_resource(StreamedTextures::new(&mut images));
}

fn begin_connecting(client: Res<NetworkClient>, settings: Res<Settings>) {
    client.connect_or_discover(&settings);
}

fn poll_network_events(
    client: Res<NetworkClient>,
    mut status: ResMut<ConnectionStatus>,
    current_state: Res<State<AppState>>,
    mut next_state: ResMut<NextState<AppState>>,
) {
    while let Ok(event) = client.event_rx.try_recv() {
        match event {
            NetworkEvent::Discovering => {
                *status = ConnectionStatus::Discovering;
            }
            NetworkEvent::Connecting(ip) => {
                *status = ConnectionStatus::Connecting(ip);
            }
            NetworkEvent::Connected(ip) => {
                *status = ConnectionStatus::Connected(ip);
                if *current_state.get() == AppState::Connecting {
                    next_state.set(AppState::Cockpit);
                }
            }
            NetworkEvent::Disconnected => {
                *status = ConnectionStatus::Discovering;
                if *current_state.get() == AppState::Cockpit {
                    next_state.set(AppState::Connecting);
                }
            }
            NetworkEvent::TextureFrame {
                channel,
                jpeg_bytes,
            } => {
                client.decoder.submit(channel, jpeg_bytes);
            }
            NetworkEvent::Error(msg) => {
                *status = ConnectionStatus::Error(msg);
            }
        }
    }
}

fn apply_decoded_textures(
    client: Res<NetworkClient>,
    mut textures: ResMut<StreamedTextures>,
    mut images: ResMut<Assets<Image>>,
) {
    while let Some(frame) = client.decoder.try_recv() {
        textures.apply_frame(&mut images, frame);
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn connection_status_display_text_matches_variants() {
        assert_eq!(
            ConnectionStatus::Discovering.display_text(),
            "Discovering server..."
        );
        assert_eq!(
            ConnectionStatus::Connecting(Ipv4Addr::new(192, 168, 1, 10)).display_text(),
            "Connecting to 192.168.1.10..."
        );
        assert_eq!(
            ConnectionStatus::Connected(Ipv4Addr::new(10, 0, 0, 2)).display_text(),
            "Connected to 10.0.0.2"
        );
        assert_eq!(
            ConnectionStatus::Error("boom".into()).display_text(),
            "Connection error: boom"
        );
    }
}
