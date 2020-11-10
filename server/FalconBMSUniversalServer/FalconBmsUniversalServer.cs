using ENet.Managed;
using F4TexSharedMem;
using System.IO;
using System.Net;
using System.Collections.Generic;
using MsgPack.Serialization;
using System.Net.Sockets;
using System;
using System.Data.HashFunction;
using System.Data.HashFunction.xxHash;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;
using FalconBmsUniversalServer.Messages;
using FalconBmsUniversalServer.SharedTextureMemory;


namespace FalconBmsUniversalServer
{
    internal class FalconBmsUniversalServer
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("FalconBmsUniversalServer");
        private bool _running = true;
        private readonly Dictionary<StreamKey, CancellationTokenSource> _runningStreams = new Dictionary<StreamKey, CancellationTokenSource>();
        private readonly IcpButtonHandler _icpButtonHandler;
        private readonly OsbButtonHandler _osbButtonHandler;

        private readonly SerializationContext _context = new SerializationContext
        {
            SerializationMethod = SerializationMethod.Map
        };

        private readonly SharedTextureMemoryExtractor _extractor = new SharedTextureMemoryExtractor(new Reader());

        private FalconBmsUniversalServer()
        {
            var sender  = new CallbackSender();
            _icpButtonHandler = new IcpButtonHandler(sender);
            _osbButtonHandler = new OsbButtonHandler(sender);
        }

        private static void Main()
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
            StartENetHost();
        }

        private void StartENetHost()
        {
            ManagedENet.Startup();

            var endpoint = new IPEndPoint(IPAddress.Any, 9022);
            Logger.Info("Running on {0}", endpoint);

            var host = new ENetHost(endpoint, ENetHost.MaximumPeers, 10);
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
                    var buffer = Pack(new Message {type = "ack"});
                    await udpClient.SendAsync(buffer, buffer.Length, result.RemoteEndPoint);
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
            if (sender is ENetPeer peer)
            {
                Logger.Info("Peer disconnected from {0}", peer.RemoteEndPoint);
                foreach (var key in _runningStreams.Keys.Where(key => key.Peer == peer))
                {
                    _runningStreams.TryGetValue(key, out var cancellationTokenSource);
                    cancellationTokenSource?.Cancel();
                }
            }
        }

        private void Peer_OnReceive(object sender, ENetPacket e)
        {
            var peer = sender as ENetPeer;
            var message = Unpack<Message>(e);
            switch (message.type)
            {
                case string type when OsbButtonMessage.IsType(type):
                    Task.Run(async () => await _osbButtonHandler.Handle(Unpack<OsbButtonMessage>(e)));
                    break;
                case string type when IcpButtonMessage.IsType(type): 
                    Task.Run(async () => await _icpButtonHandler.Handle(Unpack<IcpButtonMessage>(e)));
                    break;
                case string type when StreamedTextureRequest.IsType(type):
                    HandleStreamedTextureRequest(Unpack<StreamedTextureRequest>(e), peer);
                    break;
                default:
                    Logger.Error("Received unhandled message type {0}", message.type);
                    break;
            }
        }

        private void HandleStreamedTextureRequest(StreamedTextureRequest streamedTextureRequest, ENetPeer peer)
        {
            switch (streamedTextureRequest.command)
            {
                case "start":
                    StartStreamingTexture(streamedTextureRequest, peer);
                    break;
                case "stop":
                    StopStreamingTexture(streamedTextureRequest, peer);
                    break;
                default:
                    Logger.Error("Unhandled streamed texture command {0}", streamedTextureRequest.command);
                    break;
            }
        }

        private readonly Mutex _mutex = new Mutex();

        private void StartStreamingTexture(StreamedTextureRequest streamedTextureRequest, ENetPeer peer)
        {
            try
            {
                _mutex.WaitOne();
                Logger.Debug("Starting to stream {0} to {1}", streamedTextureRequest.identifier, peer.RemoteEndPoint);
                var cancellationToken = new CancellationTokenSource();
                var streamer = new StreamedTextureThread(streamedTextureRequest, peer, _extractor, cancellationToken);
                // TODO: should probably use a thread pool here and just submit some work instead of starting and stopping threads.
                var thread = new Thread(streamer.Run);

                var streamKey = new StreamKey {Identifier = streamedTextureRequest.identifier, Peer = peer};
                if (!_runningStreams.ContainsKey(streamKey))
                {
                    _runningStreams.Add(streamKey, cancellationToken);
                }

                thread.Start();
            }
            finally
            {
                _mutex.ReleaseMutex();   
            }
        }
        
        private void StopStreamingTexture(StreamedTextureRequest streamedTextureRequest, ENetPeer peer)
        {
            try
            {
                _mutex.WaitOne();
                Logger.Debug("Stopping to stream {0} to {1}", streamedTextureRequest.identifier, peer.RemoteEndPoint);
                var streamKey = new StreamKey {Identifier = streamedTextureRequest.identifier, Peer = peer};
                if (!_runningStreams.TryGetValue(streamKey, out var cancellationTokenSource)) return;
                cancellationTokenSource.Cancel();
                _runningStreams.Remove(streamKey);
            }
            finally
            {
                _mutex.ReleaseMutex();
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

    internal class StreamedTextureThread
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("StreamedTextureThread");
        private readonly IxxHash _hasher = xxHashFactory.Instance.Create();
        private readonly StreamedTextureRequest _request;
        private readonly ENetPeer _peer;
        private readonly SharedTextureMemoryExtractor _extractor;
        private readonly CancellationTokenSource _cancellationToken;
        private IHashValue _oldHash = null;
        private int _failedChunks = 0;

        public StreamedTextureThread(StreamedTextureRequest request, ENetPeer peer,
            SharedTextureMemoryExtractor extractor, CancellationTokenSource cancellationToken)
        {
            _request = request;
            _peer = peer;
            _extractor = extractor;
            _cancellationToken = cancellationToken;
        }

        public void Run()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                SendOneChunk();
                Thread.Sleep(33);
            }
        }

        private void SendOneChunk()
        {
            if (!_extractor.Offers(_request.identifier) || !_extractor.IsDataAvailable) return;
            var encoded = _extractor.GetEncoded(_request.identifier);
            var channel = ChannelFor(_request);
            var newHash = _hasher.ComputeHash(encoded);
            if (!newHash.Equals(_oldHash))
            {
                try
                {
                    _peer.Send(encoded, channel, ENetPacketFlags.UnreliableFragment);
                    _oldHash = newHash;
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Failed to send one chunk.");
                    _failedChunks++;
                    if (_failedChunks > 60)
                    {
                        // probably this peer has died.
                        _cancellationToken.Cancel();
                    }
                }
            }
        }
        
        private static byte ChannelFor(StreamedTextureRequest request)
        {
            switch (request.identifier)
            {
                case "f16/left-mfd":
                    return 1;
                case "f16/right-mfd":
                    return 2;
                case "f16/ded":
                    return 3;
                case "f16/rwr":
                    return 4;
                default:
                    Logger.Error("Request {0} has no mapped channel!", request.identifier);
                    // assume this can be sent on channel 0
                    return 0;
            }
        }
    }

    internal struct StreamKey
    {
        public string Identifier;
        public ENetPeer Peer;
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

    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // lowercase property names, because message pack works automagic like this.
    namespace Messages
    {

        public interface IMessage
        {
            string type { get; }
        }

        public struct Message: IMessage
        {
            public string type { get; set; }
        }

        public struct OsbButtonMessage: IMessage
        {
            public string type { get; set; }
            public string mfd { get; set; }
            public string osb { get; set; }

            public static bool IsType(string type)
            {
                return type.StartsWith("osb");
            }
        }

        public struct IcpButtonMessage : IMessage
        {
            public string type { get; set; }
            public string button { get; set; }

            public static bool IsType(string type)
            {
                return type.StartsWith("icp");
            }
        }

        public struct StreamedTextureRequest: IMessage
        {
            public string type { get; set; }
            public string command { get; set; }
            public string identifier { get; set; }
            
            public static bool IsType(string type)
            {
                return type == "streamed-texture";
            }
        }
    }

    namespace SharedTextureMemory
    {
        struct SharedTextureMemory : ClientRequestable
        {
            private int X { get; }
            private int Y { get; }
            private int Height { get; }
            private int Width { get; }

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

        internal class SharedTextureMemoryExtractor : OffersClientRequestables
        {
            private static readonly SharedTextureMemory LeftMfd =
                new SharedTextureMemory("f16/left-mfd", 753, 753, 443, 443);

            private static readonly SharedTextureMemory RightMfd =
                new SharedTextureMemory("f16/right-mfd", 753, 293, 443, 443);

            private static readonly SharedTextureMemory Rwr = new SharedTextureMemory("f16/rwr", 960, 0, 240, 240);
            private static readonly SharedTextureMemory Ded = new SharedTextureMemory("f16/ded", 575, 140, 400, 150);

            private readonly Dictionary<string, SharedTextureMemory> _offered =
                new Dictionary<string, SharedTextureMemory>()
                {
                    {LeftMfd.Identifier, LeftMfd},
                    {RightMfd.Identifier, RightMfd},
                    {Rwr.Identifier, Rwr},
                    {Ded.Identifier, Ded}
                };

            private readonly Reader _reader;

            public SharedTextureMemoryExtractor(Reader reader)
            {
                _reader = reader;
            }

            public bool IsDataAvailable => _reader.IsDataAvailable;

            public bool Offers(string identifier)
            {
                return _offered.ContainsKey(identifier);
            }

            public byte[] GetEncoded(string identifier)
            {
                return ReadSharedTextureMemory(_offered[identifier]);
            }

            private byte[] ReadSharedTextureMemory(SharedTextureMemory sharedTextureMemoryPosition)
            {
                var image = _reader.GetImage(sharedTextureMemoryPosition.ToRect());
                return ToJpeg(image, 93L);
            }
            private static byte[] ToJpeg(Bitmap image, long quality)
            {
                using (var encoderParameters = new EncoderParameters(1))
                using (var encoderParameter = new EncoderParameter(Encoder.Quality, quality))
                {
                    var buffer = new MemoryStream();
                    var codecInfo = ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                    encoderParameters.Param[0] = encoderParameter;
                    image.Save(buffer, codecInfo, encoderParameters);
                    return buffer.ToArray();
                }
            }
        }
    }
}