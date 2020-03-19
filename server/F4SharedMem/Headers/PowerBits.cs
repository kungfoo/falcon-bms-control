using System;

namespace F4SharedMem.Headers
{
    [Flags]
    [Serializable]
    public enum PowerBits : uint
    {
        BusPowerBattery = 0x01,	// true if at least the battery bus is powered
        BusPowerEmergency = 0x02,	// true if at least the emergency bus is powered
        BusPowerEssential = 0x04,	// true if at least the essential bus is powered
        BusPowerNonEssential = 0x08,	// true if at least the non-essential bus is powered
        MainGenerator = 0x10,	// true if the main generator is online
        StandbyGenerator = 0x20,	// true if the standby generator is online
        JetFuelStarter = 0x40,	// true if JFS is running, can be used for magswitch
    };
}
