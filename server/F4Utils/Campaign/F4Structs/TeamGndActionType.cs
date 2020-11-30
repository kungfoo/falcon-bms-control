namespace F4Utils.Campaign.F4Structs
{
    public struct TeamGndActionType
    {
        public uint actionTime;							// When we start.
        public uint actionTimeout;							// Our action will fail if not completed by this time
        public VU_ID actionObjective;						// Primary objective this is all about
        public byte actionType;
        public byte actionTempo;							// How "active" we want the action to be
        public byte actionPoints;							// Countdown of how much longer it will go on
    };
}