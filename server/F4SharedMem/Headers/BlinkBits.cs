using System;

namespace F4SharedMem.Headers
{	// BLINKING LIGHTS - only indicating *IF* a lamp is blinking, not implementing the actual on/off/blinking pattern logic!
    [Flags]
    [Serializable]
    public enum BlinkBits : uint
    {
        // currently working
        OuterMarker = 0x01,	// defined in HsiBits    - slow flashing for outer marker
        MiddleMarker = 0x02,	// defined in HsiBits    - fast flashing for middle marker
        PROBEHEAT = 0x04,	// defined in LightBits2 - probeheat system is tested
        AuxSrch = 0x08,	// defined in LightBits2 - search function in NOT activated and a search radar is painting ownship
        Launch = 0x10,	// defined in LightBits2 - missile is fired at ownship
        PriMode = 0x20,	// defined in LightBits2 - priority mode is enabled but more than 5 threat emitters are detected
        Unk = 0x40,	// defined in LightBits2 - unknown is not active but EWS detects unknown radar

        // not working yet, defined for future use
        Elec_Fault = 0x80,	// defined in LightBits3 - non-resetting fault
        OXY_BROW = 0x100,	// defined in LightBits  - monitor fault during Obogs
        EPUOn = 0x200,	// defined in LightBits3 - abnormal EPU operation
        JFSOn_Slow = 0x400,	// defined in LightBits3 - slow blinking: non-critical failure
        JFSOn_Fast = 0x800,	// defined in LightBits3 - fast blinking: critical failure
    };
}
