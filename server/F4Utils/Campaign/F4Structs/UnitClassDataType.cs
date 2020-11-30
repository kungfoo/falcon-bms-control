namespace F4Utils.Campaign.F4Structs
{
    public class UnitClassDataType {
        public UnitClassDataType()
        {
            for (var i = 0; i < VehicleClass.Length; i++)
            {
                VehicleClass[i]=new byte[8];
            }
        }
        public short Index;						                                        // descriptionIndex pointing here
        public int[] NumElements = new int[CampLibConstants.VEHICLES_PER_UNIT];
        public short[] VehicleType = new short[CampLibConstants.VEHICLES_PER_UNIT];	    // Class table description index
        public byte[][] VehicleClass = new byte[CampLibConstants.VEHICLES_PER_UNIT][];  // 9 byte class description array
        public ushort Flags;						                                    // Unit capibility flags (see VEH_ flags in vehicle.h)
        public byte[] Name = new byte[20];                                              // Unit name 'Infantry', 'Armor'
        public MoveType MovementType;
        public short MovementSpeed;
        public short MaxRange;					                                        // Movement/flight range with full supply
        public int Fuel;						                                        // Fuel (internal)
        public short Rate;						                                        // Fuel usage- in lbs per minute (cruise speed)
        public short PtDataIndex;				                                        // Index into pt header data table
        public byte[] Scores = new byte[CampLibConstants.MAXIMUM_ROLES];		            // Scores for each type of mission or role
        public byte Role;						                                        // What sort of mission role is standard
        public byte[] HitChance = new byte[(int)MoveType.MOVEMENT_TYPES];	            // Unit hit chances (best hitchance)
        public byte[] Strength = new byte[(int)MoveType.MOVEMENT_TYPES];	            // Unit strengths (full strength only)
        public byte[] Range = new byte[(int)MoveType.MOVEMENT_TYPES];	                // Firing ranges (maximum range)
        public byte[] Detection = new byte[(int)MoveType.MOVEMENT_TYPES];	            // Electronic detection ranges at full strength
        public byte[] DamageMod = new byte[(int)DamageDataType.OtherDam+1];		        // How much each type will hurt me (% of strength applied)
        public byte RadarVehicle;				                                        // ID of the radar vehicle for this unit
        public short SpecialIndex;				                                        // Index into yet another table (max stores for squadrons)
        public short IconIndex;					                                        // Index to this unit's icon type
    };
}