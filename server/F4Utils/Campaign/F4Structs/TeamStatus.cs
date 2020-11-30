namespace F4Utils.Campaign.F4Structs
{
    public struct TeamStatus
    {
        public ushort airDefenseVehs;
        public ushort aircraft;
        public ushort groundVehs;
        public ushort ships;
        public ushort supply;
        public ushort fuel;
        public ushort airbases;
        public byte supplyLevel;							// Supply in terms of pecentage
        public byte fuelLevel;								// fuel in terms of pecentage
    };
}