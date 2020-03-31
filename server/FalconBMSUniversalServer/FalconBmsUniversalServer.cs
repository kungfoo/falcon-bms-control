using ENet.Managed;
using F4TexSharedMem;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Collections.Generic;
using MsgPack.Serialization;
using System.Net.Sockets;
using System;
using System.Threading.Tasks;

namespace FalconBmsUniversalServer
{
    class FalconBmsUniversalServer
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("FalconBmsUniversalServer");
        private bool _running = true;

        private readonly SerializationContext _context = new SerializationContext
        {
            SerializationMethod = SerializationMethod.Map
        };

        private static void Main(string[] args)
        {
            Logger.Info("Starting up...");
            var server = new FalconBmsUniversalServer();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => { server.Stop(); };
            server.Run();
        }

        private void Stop()
        {
            Logger.Info("Server is shutting down...");
            _running = false;
            ManagedENet.Shutdown(true);
        }

        private void Run()
        {
            StartUdpListener();
            StartEnetHost();
        }

        private void StartEnetHost()
        {
            ManagedENet.Startup();

            var endpoint = new IPEndPoint(IPAddress.Any, 9022);
            Logger.Info("Running on {0}", endpoint);

            var host = new ENetHost(endpoint, ENetHost.MaximumPeers, 1);
            host.OnConnect += Host_OnConnect;
            host.Service();
        }

        private void StartUdpListener()
        {
            var broadcastAddress = new IPEndPoint(IPAddress.Any, 9020);
            Logger.Info("Listening for broadcast packets on: {0}", broadcastAddress);
            var udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.Client.Bind(broadcastAddress);
            Task.Run(async () =>
            {
                while (_running)
                {
                    var result = await udpClient.ReceiveAsync();
                    Logger.Debug("Received {0} bytes from {1}", result.Buffer.Length, result.RemoteEndPoint);

                    var message = Unpack<Message>(result.Buffer);
                    if (message.type != "hello") continue;
                    var buffer = Pack<Message>(new Message {type = "ack"});
                    udpClient.Send(buffer, buffer.Length, result.RemoteEndPoint);
                }
            });
        }

        private void Host_OnConnect(object sender, ENetConnectEventArgs e)
        {
            Logger.Info("Peer connected from {0}", e.Peer.RemoteEndPoint);

            e.Peer.OnReceive += Peer_OnReceive;
            e.Peer.OnDisconnect += Peer_OnDisconnect;
        }

        private void Peer_OnDisconnect(object sender, uint e)
        {
            var peer = sender as ENetPeer;
            Logger.Info("Peer disconnected from {0}", peer.RemoteEndPoint);
        }

        private void Peer_OnReceive(object sender, ENetPacket e)
        {
            var peer = sender as ENetPeer;
            var message = Unpack<Message>(e);
            switch (message.type)
            {
                case string s when s.StartsWith("osb"):
                    var osbButtonMessage = Unpack<OsbButtonMessage>(e);
                    Logger.Debug("Osb message received: {0}:{1}", osbButtonMessage.mfd, osbButtonMessage.osb);
                    break;

                default:
                    Logger.Error("Rceived unhandled message type {0}", message.type);
                    break;
            }
        }

        private T Unpack<T>(byte[] buffer)
        {
            var serializer = MessagePackSerializer.Get<T>(_context);
            return serializer.Unpack(new MemoryStream(buffer));
        }

        private T Unpack<T>(ENetPacket e)
        {
            var serializer = MessagePackSerializer.Get<T>(_context);
            return serializer.Unpack(e.GetPayloadStream(false));
        }

        private byte[] Pack<T>(T thing)
        {
            var buffer = new MemoryStream();
            var serializer = MessagePackSerializer.Get<T>(_context);
            serializer.Pack(buffer, thing);
            return buffer.ToArray();
        }
    }

    /*
    * Something the client can request and we know how to get it from BMS.
    */
    interface ClientRequestable
    {
        string Identifier { get; }
    }

    interface OffersClientRequestables
    {
        bool Offers(string identifier);

        bool IsDataAvailable { get; }

        byte[] GetEncoded(string identifier);
    }

    struct SharedTextureMemory : ClientRequestable
    {
        public int X { get; }
        public int Y { get; }
        public int Height { get; }
        public int Width { get; }

        public string Identifier { get; }

        public SharedTextureMemory(string identifier, int x, int y, int width, int height)
        {
            Identifier = identifier;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public System.Drawing.Rectangle ToRect()
        {
            return new System.Drawing.Rectangle(X, Y, Width, Height);
        }
    }

    public struct Message
    {
        // lowercase property names, because message pack works automagic like this.
        public string type { get; set; }
    }

    public struct OsbButtonMessage
    {
        public string type { get; set; }
        public string mfd { get; set; }
        public string osb { get; set; }
    }

    public struct MessageWithPayload
    {
        // lowercase property names, because message pack works automagic like this.
        public string type { get; }
        public string kind { get; }
        public string identifier { get; }
        public byte[] payload { get; }

        public MessageWithPayload(string type, string kind, string identifier, byte[] payload)
        {
            this.type = type;
            this.kind = kind;
            this.identifier = identifier;
            this.payload = payload;
        }
    }

    class SharedTexttureMemoryExtractor : OffersClientRequestables
    {
        private static readonly SharedTextureMemory leftMfd =
            new SharedTextureMemory("f16/left-mfd", 753, 753, 443, 443);

        private static readonly SharedTextureMemory rightMfd =
            new SharedTextureMemory("f16/right-mfd", 753, 293, 443, 443);

        private static readonly SharedTextureMemory rwr = new SharedTextureMemory("f16/rwr", 960, 0, 240, 240);
        private static readonly SharedTextureMemory ded = new SharedTextureMemory("f16/ded", 575, 140, 400, 150);

        private readonly Dictionary<string, SharedTextureMemory> offered = new Dictionary<string, SharedTextureMemory>()
        {
            {leftMfd.Identifier, leftMfd},
            {rightMfd.Identifier, rightMfd},
            {rwr.Identifier, rwr},
            {ded.Identifier, ded}
        };

        private readonly Reader reader;

        public SharedTexttureMemoryExtractor(Reader reader)
        {
            this.reader = reader;
        }

        public bool IsDataAvailable
        {
            get => reader.IsDataAvailable;
        }

        public bool Offers(string identifier)
        {
            return offered.ContainsKey(identifier);
        }

        public byte[] GetEncoded(string identifier)
        {
            return ReadSharedTextureMemory(offered[identifier]);
        }

        private byte[] ReadSharedTextureMemory(SharedTextureMemory sharedTextureMemoryPosition)
        {
            System.Drawing.Bitmap left_mfd = reader.GetImage(sharedTextureMemoryPosition.ToRect());
            MemoryStream buffer = new MemoryStream();
            left_mfd.Save(buffer, ImageFormat.Jpeg);
            return buffer.ToArray();
        }
    }
}