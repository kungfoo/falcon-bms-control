using System;

namespace F4SharedMem.Headers
{
    [Flags]
    [Serializable]
    public enum AltBits : uint
    {
        CalType = 0x01,	// true if calibration in inches of Mercury (Hg), false if in hectoPascal (hPa)
        PneuFlag = 0x02,	// true if PNEU flag is visible
    };
}
