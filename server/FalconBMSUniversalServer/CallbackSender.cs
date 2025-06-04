using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FalconBMSUniversalServer;
using NLog;

namespace FalconBmsUniversalServer
{
    internal class CallbackSender
    {
        private static readonly Logger Logger = LogManager.GetLogger("CallbackSender");
        private static readonly string FALCON_BMS_PROCESS_NAME = "Falcon BMS";

        private FalconKeyFile _keyFile;

        private FalconKeyFile OpenKeyFile()
        {
            if (_keyFile != null)
            {
                return _keyFile;
            }

            string falconKeyFilePath = null;
            using (var reader = new F4SharedMem.Reader())
            {
                falconKeyFilePath = reader.GetCurrentData()?.StringData?.data?
                    .Where(x => x.strId == (uint)F4SharedMem.Headers.StringIdentifier.KeyFile)
                    .First()
                    .value;
            }

            if (string.IsNullOrEmpty(falconKeyFilePath)) return null;
            var keyFileInfo = new FileInfo(falconKeyFilePath);

            if (!keyFileInfo.Exists) return null;
            return new FalconKeyFile(falconKeyFilePath);
        }

        public Task SendKeyPressed(string callback)
        {
            return Task.Run(() => InvokeCallbackForButton(callback, kf =>
            {
                _keyFile = OpenKeyFile();
                if (_keyFile == null) return;
                Logger.Debug("Down {}", _keyFile[callback]);
                if (_keyFile.HasCallback(callback))
                {
                    _keyFile[callback].Down();
                }
                else
                {
                    Logger.Error("No callback for {} present in keyfile {}, some functionality will not work!", callback, kf.FileName);
                }
            }));
        }

        public Task SendKeyReleased(string callback)
        {
            return Task.Run(() => InvokeCallbackForButton(callback, kf =>
            {
                _keyFile = OpenKeyFile();
                if (_keyFile == null) return;
                Logger.Debug("Released {}", _keyFile[callback]);
                if (_keyFile.HasCallback(callback))
                {
                    _keyFile[callback].Up();
                }
                else
                {
                    Logger.Error("No callback for {} present in keyfile {}, some functionality will not work!", callback, kf.FileName);
                }
            }));
        }

        private void InvokeCallbackForButton(string callback, Action<FalconKeyFile> action)
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
                    Logger.Error(e, "Failed to invoke callback for {} due to {}", callback, e.Message);
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