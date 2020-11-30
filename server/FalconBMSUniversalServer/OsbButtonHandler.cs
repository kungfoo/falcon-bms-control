using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FalconBmsUniversalServer.Messages;

namespace FalconBmsUniversalServer
{
    internal class OsbButtonHandler
    {
        private readonly CallbackSender _sender;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("OsbButtonHandler");

        public OsbButtonHandler(CallbackSender sender)
        {
            _sender = sender;
        }

        public Task Handle(OsbButtonMessage message)
        {
            Logger.Debug("MFD OSB message received: {0}:{1}:{2}", message.type, message.mfd, message.osb);
            return Task.Run(() =>
            {
                var mfdSuffix = MfdSuffix(message.mfd);
                var callback = $"SimCBE{message.osb}{mfdSuffix}";
                switch (message.type)
                {
                    case "osb-pressed":
                        _sender.SendKeyPressed(callback);
                        break;
                    case "osb-released":
                        _sender.SendKeyReleased(callback);
                        break;
                }
            });
        }

        private string MfdSuffix(string mfd)
        {
            switch (mfd)
            {
                case "f16/left-mfd":
                    return "L";
                case "f16/right-mfd":
                    return "R";
                default:
                    return "_UNKNOWN_MFD";
            }
        }
    }
}