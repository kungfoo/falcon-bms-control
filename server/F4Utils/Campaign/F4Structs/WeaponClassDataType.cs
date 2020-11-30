namespace F4Utils.Campaign.F4Structs
{
    public class WeaponClassDataType {
        public short Index;						                        // descriptionIndex pointing here
        internal ushort Union1 { get; set; }
        public ushort Strength { get { return Union1; } set { Union1 = value; }}		// How much damage it'll do.
        public ushort FuelCapacity { get { return Union1; } set { Union1 = value; }}	// How much fuel it can take
        public ushort NumChaffs { get { return Union1; } set { Union1 = value; }}	// How many chaffs if it's a chaff/flare dispenser
        public ushort EcmStrength { get { return Union1; } set { Union1 = value; }}	// ECM strength
        public DamageDataType DamageType;					            // What type of damage it does.
        public short Range;						                        // Range, in km.
        public ushort Flags;
        public byte[] Name = new byte[18];
        public byte MinAlt;
        public byte BulletDispersion;
        public byte[] HitChance = new byte[(int)MoveType.MOVEMENT_TYPES];
        public byte FireRate;					                        // # of shots fired per barrage
        public byte Rariety;					                        // % of full supply which is actually provided
        public ushort GuidanceFlags;
        public byte Collective;
        internal byte BulletInfo { get; set; }
        public byte BulletTTL { get { return (byte) (BulletInfo & 0x1F); }}
        public byte BulletVelocity { get { return (byte)(BulletInfo & 0xE0); } }
        public short RackGroup;					                        // BMS - Biker - This is the rack group variable
        public ushort Weight;						                    // Weight in lbs.
        public short DragIndex;
        internal ushort Union2 { get; set; }
        public ushort BlastRadius { get { return Union2; } set { Union2 = value; }}	// radius in ft.
        public ushort NumFlares { get { return Union2; } set { Union2 = value; }}	// How many flares if it's a chaff/flare dispenser
        public short RadarType;					                        // Index into RadarDataTable
        public short SimDataIdx;					                    // Index int SimWeaponDataTable
        public sbyte MaxAlt;						                        // Maximum altitude it can hit in thousands of feet
        public byte BulletRoundsPerSec;			
    };
}