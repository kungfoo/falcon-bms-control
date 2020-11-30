using System.IO;
using System.Linq;
using F4KeyFile;
using log4net;

namespace F4Utils.Process
{
    public static class KeyFileUtils
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (KeyFileUtils));
        private static KeyFile _keyFile;
        private static readonly object KeySenderLock = new object();

        public static void ResetCurrentKeyFile()
        {
            _keyFile = null;
        }

        public static KeyBinding FindKeyBinding(string callback)
        {
            if (_keyFile == null) _keyFile = GetCurrentKeyFile();
            if (_keyFile == null) return null;
           return _keyFile.GetBindingForCallback(callback) as KeyBinding;
        }

        public static void SendCallbackToFalcon(string callback)
        {
            var keyBinding = FindKeyBinding(callback);
            if (keyBinding == null) return;
            Util.ActivateFalconWindow();
            lock (KeySenderLock)
            {
                keyBinding.SendCallback();
            }
        }

        public static KeyFile GetCurrentKeyFile()
        {
            KeyFile toReturn = null;
            string falconKeyFilePath = null;
            using (var reader = new F4SharedMem.Reader())
            {
                falconKeyFilePath = reader.GetCurrentData()?.StringData?.data?.Where(x => x.strId == (uint)F4SharedMem.Headers.StringIdentifier.KeyFile).First().value;
            }
            if (string.IsNullOrEmpty(falconKeyFilePath)) return null;
            var keyFileInfo = new FileInfo(falconKeyFilePath);

            if (!keyFileInfo.Exists) return null;
            try
            {
                toReturn = KeyFile.Load(falconKeyFilePath);
            }
            catch (IOException e)
            {
                Log.Error(e.Message, e);
            }
            return toReturn;
        }

    }
}