namespace F4Utils.Campaign.F4Structs
{
    public struct CampObjectiveLinkDataType
    {
        public byte[] costs;	// Cost to go here, depending on movement type //init array size = MoveType.MOVEMENT_TYPES
        public VU_ID id;
    }
}