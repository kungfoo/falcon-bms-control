﻿//  Copyright 2014 Craig Courtney
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Helios is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

 using System;
 using System.ComponentModel;
 using System.Runtime.InteropServices;

 namespace FalconBMSUniversalServer
{
    class NativeMethods
    {
        private NativeMethods()
        {
        }

        #region Shared Structures

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        [TypeConverter(typeof(RectConverter))]
        internal struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public Rect(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }

            public int Width
            {
                get { return Right - Left; }
            }

            public int Height
            {
                get { return Bottom - Top; }
            }
        }

        internal class RectConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                {
                    string[] v = ((string)value).Split(',');
                    return new Rect(int.Parse(v[0]), int.Parse(v[1]), int.Parse(v[2]), int.Parse(v[3]));
                }
                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    Rect rectValue = (Rect)value;
                    return rectValue.Left + "," + rectValue.Top + "," + rectValue.Right + "," + rectValue.Bottom;
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        #endregion

        #region Common Functions

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(Int32 ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

        #endregion

        #region Screen Caputre APIs

        public const int SRCCOPY = 13369376;
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest,
                                         int yDest, int wDest,
                                         int hDest, IntPtr hdcSource,
                                         int xSrc, int ySrc, int RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap
                                    (IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

        #endregion

        #region DirectX Structures

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct DDPIXELFORMAT
        {
            [FieldOffset(0)]
            public int dwSize;
            [FieldOffset(4)]
            public int dwFlags;
            [FieldOffset(8)]
            public int dwFourCC;
            [FieldOffset(12)]
            public int dwRGBBitCount;
            [FieldOffset(12)]
            public int dwYUVBitCount;
            [FieldOffset(12)]
            public int dwZBufferBitDepth;
            [FieldOffset(12)]
            public int dwAlphaBitDepth;
            [FieldOffset(12)]
            public int dwLuminanceBitCount;
            [FieldOffset(12)]
            public int dwBumpBitCount;
            [FieldOffset(16)]
            public int dwRBitMask;
            [FieldOffset(16)]
            public int dwYBitMask;
            [FieldOffset(16)]
            public int dwStencilBitDepth;
            [FieldOffset(16)]
            public int dwLuminanceBitMask;
            [FieldOffset(16)]
            public int dwBumpDuBitMask;
            [FieldOffset(20)]
            public int dwGBitMask;
            [FieldOffset(20)]
            public int dwUBitMask;
            [FieldOffset(20)]
            public int dwZBitMask;
            [FieldOffset(20)]
            public int dwBumpDvBitMask;
            [FieldOffset(24)]
            public int dwBBitMask;
            [FieldOffset(24)]
            public int dwVBitMask;
            [FieldOffset(24)]
            public int dwStencilBitMask;
            [FieldOffset(24)]
            public int dwBumpLuminanceBitMask;
            [FieldOffset(28)]
            public int dwRGBAlphaBitMask;
            [FieldOffset(28)]
            public int dwYUVAlphaBitMask;
            [FieldOffset(28)]
            public int dwLuminanceAlphaBitMask;
            [FieldOffset(28)]
            public int dwRGBZBitMask;
            [FieldOffset(28)]
            public int dwYUVZBitMask;
        }

        //size=16 bytes
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DDSCAPS2
        {
            public int dwCaps;
            public int dwCaps2;
            public int dwCaps3;
            public int dwCaps4;
        }
        //size=8 bytes
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DDCOLORKEY
        {
            public int dwColorSpaceLowValue;
            public int dwColorSpaceHighValue;
        }
        //size=124 bytes
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct DDSURFACEDESC2
        {
            [FieldOffset(0)]
            public int dwSize;
            [FieldOffset(4)]
            public int dwFlags;
            [FieldOffset(8)]
            public int dwHeight;
            [FieldOffset(12)]
            public int dwWidth;
            [FieldOffset(16)]
            public int lPitch;
            [FieldOffset(16)]
            public int dwLinearSize;
            [FieldOffset(20)]
            public int dwBackBufferCount;
            [FieldOffset(24)]
            public int dwMipMapCount;
            [FieldOffset(24)]
            public int dwRefreshRate;
            [FieldOffset(28)]
            public int dwAlphaBitDepth;
            [FieldOffset(32)]
            public int dwReserved;
            [FieldOffset(36)]
            public int lpSurface;
            [FieldOffset(40)]
            public DDCOLORKEY ddckCKDestOverlay;
            [FieldOffset(40)]
            public int dwEmptyFaceColor;
            [FieldOffset(48)]
            public DDCOLORKEY ddckCKDestBlt;
            [FieldOffset(56)]
            public DDCOLORKEY ddckCKSrcOverlay;
            [FieldOffset(56)]
            public DDCOLORKEY ddckCKSrcBlt;
            [FieldOffset(72)]
            public DDPIXELFORMAT ddpfPixelFormat;
            [FieldOffset(104)]
            public DDSCAPS2 ddsCaps;
            [FieldOffset(120)]
            public int dwTextureStage;
        }

        [Flags]
        public enum FileMapAccess : uint
        {
            FileMapCopy = 0x0001,
            FileMapWrite = 0x0002,
            FileMapRead = 0x0004,
            FileMapAllAccess = 0x001f,
            fileMapExecute = 0x0020,
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenFileMapping(
            FileMapAccess dwDesiredAccess,
            bool bInheritHandle,
            string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr MapViewOfFile(
            IntPtr hFileMappingObject,
            FileMapAccess dwDesiredAccess,
            uint dwFileOffsetHigh,
            uint dwFileOffsetLow,
            uint dwNumberOfBytesToMap);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        #endregion

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

        #region Keyboard Emulation

        internal const int INPUT_MOUSE = 0;
        internal const int INPUT_KEYBOARD = 1;
        internal const int INPUT_HARDWARE = 2;
        internal const uint KEY_EXTENDED = 0x0001;
        internal const uint KEY_UP = 0x0002;
        internal const uint KEY_UNICODE = 0x0004;
        internal const uint KEY_SCANCODE = 0x0008;

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        internal struct INPUT
        {
            public int type;
            public MOUSEKEYBDHARDWAREINPUT mkhi;

            public INPUT(ushort wVk, ushort wScan, uint dwFlags, uint time, IntPtr dwExtraInfo)
            {
                type = INPUT_KEYBOARD;
                mkhi = new MOUSEKEYBDHARDWAREINPUT();
                mkhi.ki.wVk = wVk;
                mkhi.ki.wScan = wScan;
                mkhi.ki.dwFlags = dwFlags;
                mkhi.ki.time = time;
                mkhi.ki.dwExtraInfo = dwExtraInfo;
            }
        }

        [DllImport("User32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        internal static extern short VkKeyScanEx(char ch, IntPtr dwhkl);

        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);

        internal delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);

        #endregion

        #region Monitor Layout Commands

        [StructLayout(LayoutKind.Sequential)]
        internal struct MonitorInfo
        {
            /// <summary>
            /// The size, in bytes, of the structure. Set this member to sizeof(MONITORINFO) (40) before calling the GetMonitorInfo function.
            /// Doing so lets the function determine the type of structure you are passing to it.
            /// </summary>
            public uint Size;

            /// <summary>
            /// A RECT structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public Rect Monitor;

            /// <summary>
            /// A RECT structure that specifies the work area rectangle of the display monitor that can be used by applications,
            /// expressed in virtual-screen coordinates. Windows uses this rectangle to maximize an application on the monitor.
            /// The rest of the area in rcMonitor contains system windows such as the task bar and side bars.
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public Rect WorkArea;

            /// <summary>
            /// The attributes of the display monitor.
            ///
            /// This member can be the following value:
            ///   1 : MONITORINFOF_PRIMARY
            /// </summary>
            public uint Flags;

            public void Init()
            {
                this.Size = 40;
            }
        }

        // size of a device name string
        private const int CCHDEVICENAME = 32;

        /// <summary>
        /// The MONITORINFOEX structure contains information about a display monitor.
        /// The GetMonitorInfo function stores information into a MONITORINFOEX structure or a MONITORINFO structure.
        /// The MONITORINFOEX structure is a superset of the MONITORINFO structure. The MONITORINFOEX structure adds a string member to contain a name
        /// for the display monitor.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct MonitorInfoEx
        {
            /// <summary>
            /// The size, in bytes, of the structure. Set this member to sizeof(MONITORINFOEX) (72) before calling the GetMonitorInfo function.
            /// Doing so lets the function determine the type of structure you are passing to it.
            /// </summary>
            public uint Size;

            /// <summary>
            /// A RECT structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public Rect Monitor;

            /// <summary>
            /// A RECT structure that specifies the work area rectangle of the display monitor that can be used by applications,
            /// expressed in virtual-screen coordinates. Windows uses this rectangle to maximize an application on the monitor.
            /// The rest of the area in rcMonitor contains system windows such as the task bar and side bars.
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public Rect WorkArea;

            /// <summary>
            /// The attributes of the display monitor.
            ///
            /// This member can be the following value:
            ///   1 : MONITORINFOF_PRIMARY
            /// </summary>
            public uint Flags;

            /// <summary>
            /// A string that specifies the device name of the monitor being used. Most applications have no use for a display monitor name,
            /// and so can save some bytes by using a MONITORINFO structure.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string DeviceName;

            public void Init()
            {
                this.Size = 72;
                this.DeviceName = string.Empty;
            }
        }

        [DllImport("user32.dll")]
        internal static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip,
           MonitorEnumDelegate lpfnEnum, IntPtr dwData);

        [DllImport("user32.dll")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

        [DllImport("user32.dll")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfoEx lpmi);


        [Flags()]
        public enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x16,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        #endregion
    }
}
