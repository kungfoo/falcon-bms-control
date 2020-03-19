using System;

namespace F4SharedMem.Headers
{
    [Serializable]
    // instrument backlight brightness
    public enum InstrLight : byte
    {
        INSTR_LIGHT_OFF = 0,
        INSTR_LIGHT_DIM = 1,
        INSTR_LIGHT_BRT = 2,
    };
}
