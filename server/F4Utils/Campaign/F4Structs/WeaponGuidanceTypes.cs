using System;

namespace F4Utils.Campaign.F4Structs
{
    [Flags]
    public enum WeaponGuidanceTypes
    {
        WEAP_VISUALONLY = 0x0000, // Default state unless one of these flags are set
        WEAP_ANTIRADATION = 0x0001,
        WEAP_HEATSEEKER = 0x0002,
        WEAP_RADAR = 0x0004,
        WEAP_LASER = 0x0008,
        WEAP_TV = 0x0010,
        WEAP_REAR_ASPECT = 0x0020, // Really only applies to heatseakers
        WEAP_FRONT_ASPECT = 0x0040, // Really only applies to SAMs
        WEAP_DUMB_ONLY = 0x1000, // ONLY load non-guided weapons
        WEAP_NOGPS_ONLY = 0x2000, // Don't load GPS guided weapons
        WEAP_GUIDED_MASK = 0x001F
    }
}