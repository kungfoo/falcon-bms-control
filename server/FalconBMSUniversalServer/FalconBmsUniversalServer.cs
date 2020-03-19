using ENet.Managed;
using F4TexSharedMem;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace FalconBmsUniversalServer
{

    struct SharedTextureMemoryPosition
    {

        public int X { get; }
        public int Y { get; }
        public int Height { get; }
        public int Width { get; }

        public SharedTextureMemoryPosition(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public System.Drawing.Rectangle ToRect() {
            return new System.Drawing.Rectangle(X, Y, Width, Height);
        }
    }



    class FalconBmsUniversalServer
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetLogger("FalconBmsUniversalServer");

        private readonly SharedTextureMemoryPosition leftMfd = new SharedTextureMemoryPosition(753, 753, 443, 443);
        private readonly SharedTextureMemoryPosition rightMfd = new SharedTextureMemoryPosition(753, 293, 443, 443);
        private readonly SharedTextureMemoryPosition rwr = new SharedTextureMemoryPosition(960, 0, 240, 240);
        private readonly SharedTextureMemoryPosition ded = new SharedTextureMemoryPosition(575, 140, 400, 150);

        private readonly Reader reader = new Reader();

        static void Main(string[] args)
        {
            logger.Info("Starting up...");
            new FalconBmsUniversalServer().Run();
        }

        private void Run()
        {
            ManagedENet.Startup();

            var endpoint = new IPEndPoint(IPAddress.Any, 6789);
            logger.Info("Running on {0}", endpoint);

            ENetHost host = new ENetHost(endpoint,  ENetHost.MaximumPeers, 1);
            host.CompressWithRangeCoder();
            host.OnConnect += Host_OnConnect;
            host.Service();
        }

        private void Host_OnConnect(object sender, ENetConnectEventArgs e)
        {
            logger.Info("Peer connected from {0}", e.Peer.RemoteEndPoint);

            e.Peer.OnReceive += Peer_OnReceive;
            e.Peer.OnDisconnect += Peer_OnDisconnect;
        }

        private void Peer_OnDisconnect(object sender, uint e)
        {
            var peer = sender as ENetPeer;
            logger.Info("Peer disconnected from {0}", peer.RemoteEndPoint);
        }

        private void Peer_OnReceive(object sender, ENetPacket e)
        {
            var peer = sender as ENetPeer;

            using (var reader = new StreamReader(e.GetPayloadStream(false))) {
                var message = reader.ReadLine();
                switch (message)
                {
                    case "f16/left-mfd":
                        SendSharedMemoryTexture(peer, leftMfd);
                        break;
                    case "f16/right-mfd":
                        SendSharedMemoryTexture(peer, rightMfd);
                        break;
                    case "f16/ded":
                        SendSharedMemoryTexture(peer, ded);
                        break;
                    case "f16/rwr":
                        SendSharedMemoryTexture(peer, rwr);
                        break;
                    default:
                        logger.Error("Received unknown message {0}", message);
                        break;
                }
            }
        }

        private void SendSharedMemoryTexture(ENetPeer peer, SharedTextureMemoryPosition sharedTextureMemoryPosition)
        {
            if (!reader.IsDataAvailable)
            {
                return;
            }

            byte[] buffer = ReadSharedTextureMemory(reader, sharedTextureMemoryPosition);
            peer.Send(buffer, 0, 0);
        }

        private byte[] ReadSharedTextureMemory(Reader reader, SharedTextureMemoryPosition sharedTextureMemoryPosition)
        {
            System.Drawing.Bitmap left_mfd = reader.GetImage(sharedTextureMemoryPosition.ToRect());
            MemoryStream buffer = new MemoryStream();
            left_mfd.Save(buffer, ImageFormat.Jpeg);
            return buffer.ToArray();
        }
    }
}
