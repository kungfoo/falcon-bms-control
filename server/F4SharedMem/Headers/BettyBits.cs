using System;

namespace F4SharedMem.Headers
{
    // Bitching Betty VMS sounds playing
    [Flags]
    [Serializable]
    public enum BettyBits : uint
	{
		Betty_Allwords       = 0x00001,
		Betty_Pullup         = 0x00002,
		Betty_Altitude       = 0x00004,
		Betty_Warning        = 0x00008,
		Betty_Jammer         = 0x00010,
		Betty_Counter        = 0x00020,
		Betty_ChaffFlare     = 0x00040,
		Betty_ChaffFlare_Low = 0x00080,
		Betty_ChaffFlare_Out = 0x00100,
		Betty_Lock           = 0x00200,
		Betty_Caution        = 0x00400,
		Betty_Bingo          = 0x00800,
		Betty_Data           = 0x01000,
		Betty_IFF            = 0x02000,
		Betty_Lowspeed       = 0x04000,
		Betty_Beeps          = 0x08000,
		Betty_AOA            = 0x10000,
		Betty_MaxG           = 0x20000,
	};
}
