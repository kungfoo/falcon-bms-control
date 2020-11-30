namespace F4Utils.Campaign.F4Structs
{
    public struct TeamAirActionType
    {
        public uint actionStartTime;						// When we start.
        public uint actionStopTime;							// When we are supposed to be done by.
        public VU_ID actionObjective;						// Primary objective this is all about
        public VU_ID lastActionObjective;
        public byte actionType;
    };
}