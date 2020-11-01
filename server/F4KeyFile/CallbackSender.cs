using Common.Win32;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F4KeyFile
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public static class CallbackSender
    {
        
        private const string WINDOW_CLASS_FALCONDISPLAY = "FalconDisplay"; //HACK: uses Falcon window class 

        private static readonly ILog Log = LogManager.GetLogger(typeof(CallbackSender));
        private static readonly object KeySenderLock = new object();

        public static void SendKeystrokesForCallback(Callbacks callback, KeyFile keyFile)
        {
            SendKeystrokesForCallbackName(callback.ToString(), keyFile);
        }
        public static void SendKeystrokesForCallbackName(string callbackName, KeyFile keyFile)
        {
            var keyBinding = keyFile.GetBindingForCallback(callbackName) as KeyBinding;
            if (keyBinding == null) return;
            SendKeystrokesForKeyBinding(keyBinding);
        }
        public static void SendKeystrokesForKeyBinding(KeyBinding keyBinding)
        {
            ActivateFalconWindow();
            lock (KeySenderLock)
            {
                var primaryKeyWithModifiers = keyBinding.Key;
                var comboKeyWithModifiers = keyBinding.ComboKey; 
                SendClearingKeystrokes();
                WaitToSendNextKeystrokes();

                SendUpKeystrokes(primaryKeyWithModifiers);
                WaitToSendNextKeystrokes();
                SendDownKeystrokes(primaryKeyWithModifiers);
                WaitToSendNextKeystrokes();
                SendUpKeystrokes(primaryKeyWithModifiers);
                WaitToSendNextKeystrokes();

                SendUpKeystrokes(comboKeyWithModifiers);
                WaitToSendNextKeystrokes();
                SendDownKeystrokes(comboKeyWithModifiers);
                WaitToSendNextKeystrokes();
                SendUpKeystrokes(comboKeyWithModifiers);
                WaitToSendNextKeystrokes();

                SendClearingKeystrokes();
                WaitToSendNextKeystrokes();
            }

        }
        private static int KeyDelayMsecs = 25;
        public static int KeyDelay
        {
            get { return KeyDelayMsecs; }
            set
            {
                if (KeyDelayMsecs < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                KeyDelayMsecs = value;
            }
        }
        private static void WaitToSendNextKeystrokes()
        {
            Thread.Sleep(KeyDelayMsecs);
        }
        private static void SendClearingKeystrokes()
        {
            KeyAndMouseFunctions.SendKey((ushort)ScanCodes.LShift, false, true);
            KeyAndMouseFunctions.SendKey((ushort)ScanCodes.LMenu, false, true);
            KeyAndMouseFunctions.SendKey((ushort)ScanCodes.LControl, false, true);
            KeyAndMouseFunctions.SendKey((ushort)ScanCodes.RShift, false, true);
            KeyAndMouseFunctions.SendKey((ushort)ScanCodes.RMenu, false, true);
            KeyAndMouseFunctions.SendKey((ushort)ScanCodes.RControl, false, true);
        }

        private static void SendDownKeystrokes(KeyWithModifiers keyWithModifiers)
        {
            //send down keystrokes
            if ((keyWithModifiers.Modifiers & KeyModifiers.Shift) == KeyModifiers.Shift)
            {
                KeyAndMouseFunctions.SendKey((ushort)ScanCodes.LShift, true, false);
            }
            if ((keyWithModifiers.Modifiers & KeyModifiers.Alt) == KeyModifiers.Alt)
            {
                KeyAndMouseFunctions.SendKey((ushort)ScanCodes.LMenu, true, false);
            }
            if ((keyWithModifiers.Modifiers & KeyModifiers.Ctrl) == KeyModifiers.Ctrl)
            {
                KeyAndMouseFunctions.SendKey((ushort)ScanCodes.LControl, true, false);
            }
            KeyAndMouseFunctions.SendKey((ushort)keyWithModifiers.ScanCode, true, false);
        }

        private static void SendUpKeystrokes(KeyWithModifiers keyWithModifiers)
        {
            //send up keystrokes
            KeyAndMouseFunctions.SendKey((ushort)keyWithModifiers.ScanCode, false, true);
            if ((keyWithModifiers.Modifiers & KeyModifiers.Ctrl) == KeyModifiers.Ctrl)
            {
                KeyAndMouseFunctions.SendKey((ushort)ScanCodes.LControl, false, true);
            }
            if ((keyWithModifiers.Modifiers & KeyModifiers.Alt) == KeyModifiers.Alt)
            {
                KeyAndMouseFunctions.SendKey((ushort)ScanCodes.LMenu, false, true);
            }
            if ((keyWithModifiers.Modifiers & KeyModifiers.Shift) == KeyModifiers.Shift)
            {
                KeyAndMouseFunctions.SendKey((ushort)ScanCodes.LShift, false, true);
            }
        }
        private static void ActivateFalconWindow()
        {
            var windowHandle = GetFalconWindowHandle();
            if (windowHandle != IntPtr.Zero)
            {
                NativeMethods.SetForegroundWindow(windowHandle);
            }
        }
        private static IntPtr GetFalconWindowHandle()
        {
            var hWnd = NativeMethods.FindWindow(WINDOW_CLASS_FALCONDISPLAY, null);
            return hWnd;
        }


    }
}
