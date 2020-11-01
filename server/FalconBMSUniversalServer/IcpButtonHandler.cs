using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FalconBMSUniversalServer;
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
            {"IFF", "SimICPIFF"},
            {"LIST", "SimICPLIST"},
            {"A-A", "SimICPAA"},
            {"A-G", "SimICPAG"}
        };

        private static readonly string FALCON_BMS_PROCESS_NAME = "Falcon BMS";
        
        private FalconKeyFile _keyFile;

        private FalconKeyFile OpenKeyFile()
        {
            string falconKeyFilePath = null;
            using (var reader = new F4SharedMem.Reader())
            {
                falconKeyFilePath = reader.GetCurrentData()?.StringData?.data?
                    .Where(x => x.strId == (uint) F4SharedMem.Headers.StringIdentifier.KeyFile)
                    .First()
                    .value;
            }

            if (string.IsNullOrEmpty(falconKeyFilePath)) return null;
            var keyFileInfo = new FileInfo(falconKeyFilePath);

            if (!keyFileInfo.Exists) return null;
            return new FalconKeyFile(falconKeyFilePath);
        }

        public Task Handle(IcpButtonMessage message)
        {
            if (_keyFile == null)
            {
                _keyFile = OpenKeyFile();
                if (_keyFile == null)
                {
                    return Task.Run(() => Logger.Warn("Invoked handler, but could not open current key file."));
                }
            }

            return Task.Run(() =>
            {
                Logger.Debug("MFD OSB message received: {0}:{1}", message.type, message.button);
                switch (message.type)
                {
                    case "icp-pressed":
                        SendKeyPressed(message.button);
                        break;
                    case "icp-released":
                        SendKeyReleased(message.button);
                        break;
                    default:
                        return;
                }
            });
        }

        private void SendKeyPressed(string button)
        {
            CallbacksForButtons.TryGetValue(button, out var callback);
            InvokeCallbackForButton(button, kf =>
            {
                Logger.Debug("Down {}", kf[callback]);
                kf[callback].Down();
            });
        }

        private void SendKeyReleased(string button)
        {
            CallbacksForButtons.TryGetValue(button, out var callback);
            InvokeCallbackForButton(button, kf =>
            {
                Logger.Debug("Released {}", kf[callback]);
                kf[callback].Up();
            });
        }

        private void InvokeCallbackForButton(string button, Action<FalconKeyFile> action)
        {
            var p = Process.GetProcessesByName(FALCON_BMS_PROCESS_NAME).FirstOrDefault();
            if (p != null)
            {
                var h = p.MainWindowHandle;
                SetForegroundWindow(h);
                try
                {
                    action.Invoke(_keyFile);
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Failed to invoke callback for {} due to {}", button, e.Message);
                }
            }
            else
            {
                Logger.Error("Could not find process named {0}", FALCON_BMS_PROCESS_NAME);
            }
        }
        
        [DllImport("User32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);
    }
}