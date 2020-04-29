namespace F4Utils.Campaign.F4Structs
{
    public struct ObjectiveDelta
    {
        public VU_ID id;
        public uint last_repair;
        public byte owner;
        public byte supply;
        public byte fuel;
        public byte losses;
        public byte[] fStatus;
    }
}