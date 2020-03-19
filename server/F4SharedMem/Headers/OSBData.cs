using System;
using System.Runtime.InteropServices;

namespace F4SharedMem.Headers
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OSBData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public OSBLabel[] leftMFD;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public OSBLabel[] rightMFD;
    }
}
