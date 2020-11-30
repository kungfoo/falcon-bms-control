

using System;
namespace F4Utils.Campaign.F4Structs
{
    // Returned by TargetThreats
    // Flags for what we really need
    [Flags]
    public enum TargetThreatReturnFlagEnum
    {
        NEED_SEAD = 0x01,
        NEED_ECM = 0x02,

        // Specifics as to the threat types
        THREAT_LALT_SAM = 0x10,
        THREAT_HALT_SAM = 0x20,
        THREAT_AAA = 0x40,

        // Specifics as to the threat location
        THREAT_ENROUTE = 0x100,
        THREAT_TARGET = 0x200,
    };
}
