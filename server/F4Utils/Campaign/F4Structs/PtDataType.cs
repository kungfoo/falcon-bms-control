namespace F4Utils.Campaign.F4Structs
{
    public struct PtDataType
    {
        public float xOffset;					// X and Y offsets of this point (from center of objective tile)
        public float yOffset;
        public float zOffset;					// Offset in height
        public float height;					// Max height allowend on this PT
        public float width;						// Max width allowend on this PT
        public float length;					//Max length allowend on this PT
        public byte type;						// The type of point this is
        public byte flags;
        public byte rootIdx;					// Make it a double linked list for taxiway branches
        public byte branchIdx;					// Make it a double linked list for taxiway branches
    };
}