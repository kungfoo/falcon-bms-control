using System;
using System.Runtime.InteropServices;

namespace FalconBMSUniversalServer
{
    internal static class NativeMethods
    {
        internal const int InputKeyboard = 1;
        internal const uint KeyExtended = 0x0001;
        internal const uint KeyUp = 0x0002;
        internal const uint KeyUnicode = 0x0004;
        internal const uint KeyScanCode = 0x0008;

        [StructLayout(LayoutKind.Sequential)]
        internal struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct Inputs
        {
            [FieldOffset(0)] public MouseInput mi;

            [FieldOffset(0)] public KeyboardInput ki;

            [FieldOffset(0)] public HardwareInput hi;
        }

        internal struct Input
        {
            public int Type;
            public Inputs inputs;
        }

        [DllImport("User32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint numberOfInputs, Input[] inputs, int size);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetKeyboardLayout(uint id);

        [DllImport("user32.dll")]
        internal static extern short VkKeyScanEx(char ch, IntPtr layout);
        
        
        [DllImport("user32.dll")]
        internal static extern Int32 GetKeyboardLayoutList(Int32 bufferSize, IntPtr[] buffer);

        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKeyEx(uint code, uint type, IntPtr layout);
    }
}