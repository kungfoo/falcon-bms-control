namespace F4Utils.Campaign.F4Structs
{
    public enum Dirtyness
    {
        SEND_EVENTUALLY = 0x00000001,	// 1
        SEND_SOMETIME = 0x00000010,	// 16
        SEND_LATER = 0x00000100,	// 256
        SEND_SOON = 0x00001000,	// 4096
        SEND_NOW = 0x00010000,	// 65536
        SEND_RELIABLE = 0x00100000,	// 1048576
        SEND_OOB = 0x01000000,	// 16777216
        SEND_RELIABLEANDOOB = 0x10000000	// 268435456
    };
}