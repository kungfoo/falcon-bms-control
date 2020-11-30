using System;

namespace F4SharedMem.Headers
{
    // various flags
    [Flags]
    [Serializable]
    public enum MiscBits : uint
    {
        RALT_Valid = 0x1,    // indicates whether the RALT reading is valid/reliable
        Flcs_Flcc_A = 0x02,
        Flcs_Flcc_B = 0x04,
        Flcs_Flcc_C = 0x08,
        Flcs_Flcc_D = 0x10,
        SolenoidStatus = 0x20, //0 not powered or failed or WOW  , 1 is working OK
        AllLampBitsFlccOn = 0x1e,

    };
}
