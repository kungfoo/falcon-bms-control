using System;

namespace F4SharedMem.Headers
{
    // various flags
    [Flags]
    [Serializable]
    public enum MiscBits : uint
    {
        RALT_Valid = 0x1    // indicates whether the RALT reading is valid/reliable
    };
}
