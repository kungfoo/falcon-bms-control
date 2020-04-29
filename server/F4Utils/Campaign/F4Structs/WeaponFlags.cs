using System;

namespace F4Utils.Campaign.F4Structs
{
    [Flags]
    public enum WeaponFlags
    {
        WEAP_RECON = 0x00000001, // Used for recon
        WEAP_FUEL = 0x00000002, // Fuel tank
        WEAP_ECM = 0x00000004, // Used for ECM
        WEAP_AREA = 0x00000008, // Can damage multiple vehicles
        WEAP_CLUSTER = 0x00000010, // CBU -- cluster bomb
        WEAP_TRACER = 0x00000020, // Use tracers when drawing weapon fire
        WEAP_ALWAYSRACK = 0x00000040, // this weapon has no rack
        WEAP_LGB_3RD_GEN = 0x00000080, // 3rd generation LGB's
        WEAP_BOMBWARHEAD = 0x00000100,
        // this is for example a missile with a bomb war head. MissileEnd when ground impact, not when lethalradius reached
        WEAP_BAI_LOADOUT = 0x00000200, // special loadout for BAI type missions
        WEAP_BOMBDROPSOUND = 0x00000400, // play the bomb drop sound instead of missile launch
        WEAP_BOMBGPS = 0x00000800,
        // we use this for JDAM "missile" bombs to have them always CanDetectObject and CanSeeObject true
        WEAP_BOMBWCMD = 0x00001000, //  WCMD bombs
        WEAP_FORCE_ON_ONE = 0x00002000, // Put all requested weapons on one/two hardpoints
        WEAP_GUN = 0x00004000, // Used by LoadWeapons only - to specify guns only
        WEAP_ONETENTH = 0x00008000, // # listed is actually 1/10th the # of shots
        WEAP_TGP = 0x00010000, // For loadind TGT pods
        WEAP_DLP = 0x00020000, // For loading data link pods
        WEAP_CMP = 0x00040000, // For loading chaff/flare pods
        WEAP_INFINITE_MASK = 0x07// Things which meet this mask never run out.
    }
}