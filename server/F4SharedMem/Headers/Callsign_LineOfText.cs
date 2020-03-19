using System;
using System.Runtime.InteropServices;

namespace F4SharedMem.Headers
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Callsign_LineOfText
    {
        public const int CALLSIGN_LEN = 12;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CALLSIGN_LEN)]
        public byte[] chars;
    }
}
