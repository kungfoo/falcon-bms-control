using System;

namespace F4Utils.Campaign.F4Structs
{
    [Flags]
    public enum VuFlagBits : ushort
    {
        private_ = 0x01,	// 1 --> not public
        transfer_ = 0x02,// 1 --> can be transferred
        tangible_ = 0x04,	// 1 --> can be seen/touched with
        collidable_ = 0x08,	// 1 --> put in auto collision table
        global_ = 0x10,	// 1 --> visible to all groups
        persistent_ = 0x20,	// 1 --> keep ent local across group joins
        pad_ = 0xFFC0,	// unused
    }
}