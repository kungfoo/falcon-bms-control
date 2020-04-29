namespace F4Utils.Campaign.F4Structs
{
    public struct MissionRequest
    {
        public VU_ID requesterID;
        public VU_ID targetID;
        public VU_ID secondaryID;
        public VU_ID pakID;
        //32 bytes
        public byte who;
        public byte vs;
        //4 bytes to align on int32 boundary
        public uint tot;
        //4 bytes
        public short tx;
        public short ty;
        //4 bytes
        public uint flags;
        //4 bytes
        public short caps;
        public short target_num;
        //4 bytes
        public short speed;
        public short match_strength;				// How much Air to Air strength we should try to match
        //4 bytes
        public short priority;
        public byte tot_type;
        public byte action_type;				// Type of action we're associated with
        //4 bytes
        public byte mission;
        public byte aircraft;
        public byte context;					// Context code (why this was requested)
        public byte roe_check;
        //4 bytes
        public byte delayed;					// number of times it's been pushed back
        public byte start_block;				// time block we're taking off during
        public byte final_block;				// time block we're landing during
        //4 bytes to align on int32 boundary 
        public byte[] slots;//init size=4    // squadron slots we're using.
        //4 bytes
        public sbyte min_to;						// minimum block we found planes for
        public sbyte max_to;						// maximum block we found planes for
        //2 bytes
    }
}