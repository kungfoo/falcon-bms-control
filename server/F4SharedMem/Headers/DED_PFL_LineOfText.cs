using System;
using System.Runtime.InteropServices;

namespace F4SharedMem.Headers
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DED_PFL_LineOfText
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
        public byte[] chars;
    }
}
