using System;

namespace F4Utils.Campaign.F4Structs
{
    public class FeatureEntry {
        public short Index;						// Entity class index of feature
        public ushort Flags;
        public byte[] eClass = new byte[8];		// Entity class array of feature
        public byte Value;						// % loss in operational status for destruction
        public vector Offset;
        public Int16 Facing;
    };
}