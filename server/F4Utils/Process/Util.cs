using System;
using System.Text;
using Common.Win32;
using System.Linq;
namespace F4Utils.Process
{
    public static class Util
    {
        private const string WINDOW_CLASS_FALCONDISPLAY = "FalconDisplay";

        public static void ActivateFalconWindow()
        {
            var windowHandle = GetFalconWindowHandle();
            if (windowHandle != IntPtr.Zero)
            {
                NativeMethods.SetForegroundWindow(windowHandle);
            }
        }


        public static string GetFalconWindowTitle()
        {
            var txt = new StringBuilder(NativeMethods.MAX_PATH);
            var windowHandle = GetFalconWindowHandle();
            string toReturn = null;
            if (windowHandle != IntPtr.Zero)
            {
                NativeMethods.GetWindowText(windowHandle, txt, NativeMethods.MAX_PATH);
                toReturn = txt.ToString();
            }
            return toReturn;
        }

        private static DateTime _lastFalconRunningCheck = DateTime.MinValue;
        private static bool _falconWasRunningOnLastCheck;
        public static bool IsFalconRunning()
        {
            if (!(DateTime.UtcNow.Subtract(_lastFalconRunningCheck).TotalMilliseconds > 500))
                return _falconWasRunningOnLastCheck;
            var windowHandle = GetFalconWindowHandle();
            _falconWasRunningOnLastCheck = (windowHandle != IntPtr.Zero);
            _lastFalconRunningCheck = DateTime.UtcNow;
            return _falconWasRunningOnLastCheck;
        }

        public static IntPtr GetFalconWindowHandle()
        {
            var hWnd = NativeMethods.FindWindow(WINDOW_CLASS_FALCONDISPLAY, null);
            return hWnd;
        }

    }
}