using System;
using System.Collections.Generic;
using System.ServiceModel;
using F4Utils.Process;
using FalconBmsUniversalServer.Messages;

namespace FalconBmsUniversalServer
{
    internal class IcpButtonHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("IcpButtonHandler");
        
        private static readonly Dictionary<String, String> CallbacksForButtons = new Dictionary<string, string>
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
            {"IFF","SimICPIFF"},
            {"LIST","SimICPLIST"},
            {"A-A", "SimICPAA"},
            {"A-G", "SimICPAG"}
        };
        
        public void handle(IcpButtonMessage message)
        {
            Logger.Debug("MFD OSB message received: {0}:{1}", message.type, message.button);
            if (message.type != "icp-pressed") return;
            CallbacksForButtons.TryGetValue(message.button, out var identifier);
            if (identifier != null)
            {
                KeyFileUtils.SendCallbackToFalcon(identifier);
                Logger.Debug("Sent callback {0}", identifier);
            }
            else
            {
                Logger.Error("Unresolved callback for button press {0}", message.button);
            }
        }
    }
}