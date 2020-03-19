using System;

namespace F4SharedMem.Headers
{
    [Serializable]
    public enum TacanBits : byte
    {
        band = 0x01,   // true in this bit position if band is X
        mode = 0x02,   // true in this bit position if domain is air to air
    };
}
