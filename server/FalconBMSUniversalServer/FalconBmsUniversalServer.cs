﻿using ENet.Managed;
using F4TexSharedMem;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Collections.Generic;
using MsgPack.Serialization;

namespace FalconBmsUniversalServer
{
    class FalconBmsUniversalServer {

        private static readonly NLog.Logger logger = NLog.LogManager.GetLogger("FalconBmsUniversalServer");

        private readonly OffersClientRequestables[] requestables = new OffersClientRequestables[] {
            new SharedTexttureMemoryExtractor(new Reader())
        };

        private static readonly SerializationContext context = new SerializationContext();
        public static readonly MessagePackSerializer<Message> serializer = MessagePackSerializer.Get<Message>(context);
        public static readonly MessagePackSerializer<MessageWithPayload> msgWPSerializer = MessagePackSerializer.Get<MessageWithPayload>(context);

        static void Main(string[] args)
        {
            logger.Info("Starting up...");
            context.SerializationMethod = SerializationMethod.Map;
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

                var message = serializer.Unpack(e.GetPayloadStream(false));
                var identifier = message.identifier;
                foreach (OffersClientRequestables offering in requestables)
                {
                    if(offering.Offers(identifier) && offering.IsDataAvailable) {
                        byte[] encoded = offering.GetEncoded(identifier);
                        MessageWithPayload reply = new MessageWithPayload("response", message.kind, message.identifier, encoded);
                        var buffer = new MemoryStream();
                        msgWPSerializer.Pack(buffer, reply);
                        peer.Send(buffer.ToArray(), 0, 0);
                    }
                }
            }
        }
    

    /*
    * Something the client can request and we know how to get it from BMS.
    */
    interface ClientRequestable {
        string Identifier { get; }
    }

    interface OffersClientRequestables {
        bool Offers(string identifier);

        bool IsDataAvailable { get; }

        byte[] GetEncoded(string identifier);
    }

    struct SharedTextureMemory: ClientRequestable
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

        public System.Drawing.Rectangle ToRect() {
            return new System.Drawing.Rectangle(X, Y, Width, Height);
        }
    }

    public struct Message {

        // lowercase property names, because message pack works automagic like this.
        public string type { get; }
        public string kind { get; }
        public string identifier { get; }

        public Message(string type, string kind, string identifier) {
            this.type = type;
            this.kind = kind;
            this.identifier = identifier;
        }
    }

    public struct MessageWithPayload {

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

    class SharedTexttureMemoryExtractor: OffersClientRequestables {
        private static readonly SharedTextureMemory leftMfd = new SharedTextureMemory( "f16/left-mfd", 753, 753, 443, 443);
        private static readonly SharedTextureMemory rightMfd = new SharedTextureMemory("f16/right-mfd", 753, 293, 443, 443);
        private static readonly SharedTextureMemory rwr = new SharedTextureMemory( "f16/rwr", 960, 0, 240, 240);
        private static readonly SharedTextureMemory ded = new SharedTextureMemory("f16/ded", 575, 140, 400, 150);

        private readonly Dictionary<string, SharedTextureMemory> offered = new Dictionary<string, SharedTextureMemory>(){
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

        public bool IsDataAvailable { get => reader.IsDataAvailable; }

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


