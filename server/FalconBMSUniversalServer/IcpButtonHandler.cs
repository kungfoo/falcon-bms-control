using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FalconBmsUniversalServer.Messages;

namespace FalconBmsUniversalServer
{
    internal class IcpButtonHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("IcpButtonHandler");

        private static readonly Dictionary<string, string> CallbacksForButtons = new Dictionary<string, string>
        {
            {"1", "SimICPTILS"},
            {"2", "SimICPALOW"},
            {"3", "SimICPTHREE"},
            {"4", "SimICPStpt"},
            {"5", "SimICPCrus"},
            {"6", "SimICPSIX"},
            {"7", "SimICPMark"},
            {"8", "SimICPEIGHT"},
            {"9", "SimICPNINE"},
            {"0", "SimICPZERO"},
            {"RCL", "SimICPCLEAR"},
            {"ENTER", "SimICPEnter"},
            {"COM1", "SimICPCom1"},
            {"COM2", "SimICPCom2"},
            {"IFF", "SimICPIFF"},
            {"LIST", "SimICPLIST"},
            {"A-A", "SimICPAA"},
            {"A-G", "SimICPAG"},
            {"icp-wpt-next", "SimICPNext"},
            {"icp-wpt-previous", "SimICPPrevious"},
            {"icp-ded-up", "SimICPDEDUP"},
            {"icp-ded-down", "SimICPDEDDOWN"},
            {"icp-ded-seq", "SimICPDEDSEQ"},
            {"icp-ded-return", "SimICPResetDED"},
        };

        private readonly CallbackSender _callbackSender;

        public IcpButtonHandler(CallbackSender callbackSender)
        {
            _callbackSender = callbackSender;
        }

        public Task Handle(IcpButtonMessage message)
        {
            CallbacksForButtons.TryGetValue(message.button, out var callback);
            Logger.Debug("icp-button: {0}:{1}", message.type, message.button);
            switch (message.type)
            {
                case "icp-pressed":
                    return _callbackSender.SendKeyPressed(callback);
                case "icp-released":
                    return _callbackSender.SendKeyReleased(callback);
                default:
                    return Task.Run(() => { });
            }
        }
    }
}