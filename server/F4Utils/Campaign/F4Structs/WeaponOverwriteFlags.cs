using System;

namespace F4Utils.Campaign.F4Structs
{
    [Flags]
    public enum WeaponOverwriteFlags 
    {
        OVERWRITE_NOTHING=0x0000,
        OVERWRITE_WEAPONS=0x0001, // All types of weapons
        OVERWRITE_FUELTANKS=0x0002, // Fuel tanks
        OVERWRITE_PODS=0x0004 // ATM all types of pods (ECM, RECCE, TGT, etc.)
    }
}