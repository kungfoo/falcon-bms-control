namespace F4Utils.Campaign.F4Structs
{
    public struct VuEntityType
    {
        public ushort id_;
        public ushort collisionType_;
        public float collisionRadius_;
        public byte[] classInfo_;//init size=8;
        public uint updateRate_;
        public uint updateTolerance_;
        public float fineUpdateRange_;	// max distance to send position updates
        public float fineUpdateForceRange_; // distance to force position updates
        public float fineUpdateMultiplier_; // multiplier for noticing position updates
        public uint damageSeed_;
        public int hitpoints_;
        public ushort majorRevisionNumber_;
        public ushort minorRevisionNumber_;
        public ushort createPriority_;
        public byte managementDomain_;
        public byte transferable_;
        public byte private_;
        public byte tangible_;
        public byte collidable_;
        public byte global_;
        public byte persistent_;
    }
}