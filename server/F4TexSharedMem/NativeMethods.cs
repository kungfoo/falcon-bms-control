using System;
using System.Runtime.InteropServices;

namespace F4TexSharedMem.Win32
{
    internal static class NativeMethods
    {
        public const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        public const UInt32 SECTION_QUERY = 0x0001;
        public const UInt32 SECTION_MAP_WRITE = 0x0002;
        public const UInt32 SECTION_MAP_READ = 0x0004;
        public const UInt32 SECTION_MAP_EXECUTE = 0x0008;
        public const UInt32 SECTION_EXTEND_SIZE = 0x0010;

        public const UInt32 SECTION_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SECTION_QUERY |
                                                  SECTION_MAP_WRITE |
                                                  SECTION_MAP_READ |
                                                  SECTION_MAP_EXECUTE |
                                                  SECTION_EXTEND_SIZE);

        public const UInt32 FILE_MAP_ALL_ACCESS = SECTION_ALL_ACCESS;

        //size=32 bytes

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr OpenFileMapping(uint dwDesiredAccess,
                                                      bool bInheritHandle,
                                                      string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint
                                                                                   dwDesiredAccess,
                                                    uint dwFileOffsetHigh, uint dwFileOffsetLow,
                                                    IntPtr dwNumberOfBytesToMap);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        #region Nested type: DDCOLORKEY

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DDCOLORKEY
        {
            public int dwColorSpaceLowValue;
            public int dwColorSpaceHighValue;
        }

        #endregion

        #region Nested type: DDPIXELFORMAT

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct DDPIXELFORMAT
        {
            [FieldOffset(0)] public int dwSize;
            [FieldOffset(4)] public int dwFlags;
            [FieldOffset(8)] public int dwFourCC;
            [FieldOffset(12)] public int dwRGBBitCount;
            [FieldOffset(12)] public int dwYUVBitCount;
            [FieldOffset(12)] public int dwZBufferBitDepth;
            [FieldOffset(12)] public int dwAlphaBitDepth;
            [FieldOffset(12)] public int dwLuminanceBitCount;
            [FieldOffset(12)] public int dwBumpBitCount;
            [FieldOffset(16)] public int dwRBitMask;
            [FieldOffset(16)] public int dwYBitMask;
            [FieldOffset(16)] public int dwStencilBitDepth;
            [FieldOffset(16)] public int dwLuminanceBitMask;
            [FieldOffset(16)] public int dwBumpDuBitMask;
            [FieldOffset(20)] public int dwGBitMask;
            [FieldOffset(20)] public int dwUBitMask;
            [FieldOffset(20)] public int dwZBitMask;
            [FieldOffset(20)] public int dwBumpDvBitMask;
            [FieldOffset(24)] public int dwBBitMask;
            [FieldOffset(24)] public int dwVBitMask;
            [FieldOffset(24)] public int dwStencilBitMask;
            [FieldOffset(24)] public int dwBumpLuminanceBitMask;
            [FieldOffset(28)] public int dwRGBAlphaBitMask;
            [FieldOffset(28)] public int dwYUVAlphaBitMask;
            [FieldOffset(28)] public int dwLuminanceAlphaBitMask;
            [FieldOffset(28)] public int dwRGBZBitMask;
            [FieldOffset(28)] public int dwYUVZBitMask;
        }

        #endregion

        //size=16 bytes

        #region Nested type: DDSCAPS2

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DDSCAPS2
        {
            public int dwCaps;
            public int dwCaps2;
            public int dwCaps3;
            public int dwCaps4;
        }

        #endregion

        #region Nested type: DDSURFACEDESC2

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct DDSURFACEDESC2
        {
            [FieldOffset(0)] public int dwSize;
            [FieldOffset(4)] public int dwFlags;
            [FieldOffset(8)] public int dwHeight;
            [FieldOffset(12)] public int dwWidth;
            [FieldOffset(16)] public int lPitch;
            [FieldOffset(16)] public int dwLinearSize;
            [FieldOffset(20)] public int dwBackBufferCount;
            [FieldOffset(24)] public int dwMipMapCount;
            [FieldOffset(24)] public int dwRefreshRate;
            [FieldOffset(28)] public int dwAlphaBitDepth;
            [FieldOffset(32)] public int dwReserved;
            [FieldOffset(36)] public int lpSurface;
            [FieldOffset(40)] public DDCOLORKEY ddckCKDestOverlay;
            [FieldOffset(40)] public int dwEmptyFaceColor;
            [FieldOffset(48)] public DDCOLORKEY ddckCKDestBlt;
            [FieldOffset(56)] public DDCOLORKEY ddckCKSrcOverlay;
            [FieldOffset(56)] public DDCOLORKEY ddckCKSrcBlt;
            [FieldOffset(72)] public DDPIXELFORMAT ddpfPixelFormat;
            [FieldOffset(104)] public DDSCAPS2 ddsCaps;
            [FieldOffset(120)] public int dwTextureStage;
        }

        #endregion
    }
}