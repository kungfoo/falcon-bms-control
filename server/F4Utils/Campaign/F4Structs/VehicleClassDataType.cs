namespace F4Utils.Campaign.F4Structs
{
    public class VehicleClassDataType {
        public short Index;						// descriptionIndex pointing here
        public short HitPoints;					// Damage this thing can take
        public uint Flags;						// see VEH_ flags in vehicle.h
        public byte[] Name = new byte[15];
        public byte[] NCTR = new byte[5];
        public float RCSfactor;					// log2( 1 + RCS relative to an F16 )
        public int MaxWt;						// Max loaded weight in lbs.
        public int EmptyWt;					// Empty weight in lbs.	
        public int FuelWt;						// Weight of max fuel in lbs.
        public short FuelEcon;					// Fuel usage in lbs./min.
        public short EngineSound;				// SoundFX sample index of corresponding engine sound
        internal short Union1 { get;  set; }
        public short HighAlt { get { return Union1; } set { Union1 = value; } }				       // in hundreds of feet
        public short CarrierObjectiveCTIndex { get { return Union1; } set { Union1 = value; } }   // CTIndex of a carriers objective data 
        public short LowAlt;						// in hundreds of feet
        public short CruiseAlt;					// in hundreds of feet
        public short MaxSpeed;					// Maximum vehicle speed, in kph
        public short RadarType;					// Index into RadarDataTable
        public short NumberOfPilots;				// # of pilots (for eject)
        internal ushort Union2 { get; set; }
        public ushort RackFlags { get { return Union2; } set { Union2 = value; } }					//0x01 means hardpoint 0 needs a rack, 0x02 -> hdpt 1, etc
        public ushort GunIsAaCapable { get { return Union2; } set { Union2 = value; } }				//0x01 means gun on this HP can shoot against airborne targets
        public ushort IsVerticalLaunchingSystem { get { return Union2; } set { Union2 = value; } } 	//0x01 means this slot is a VLS type
        public ushort VisibleFlags;				//0x01 means hardpoint 0 is visible, 0x02 -> hdpt 1, etc
        public byte CallsignIndex;
        public byte CallsignSlots;
        public byte[] HitChance = new byte[(int)MoveType.MOVEMENT_TYPES];	    // Vehicle hit chances (best hitchance & bonus)
        public byte[] Strength = new byte[(int)MoveType.MOVEMENT_TYPES];	    // Combat strengths (full strength only) (calculated)
        public byte[] Range = new byte[(int)MoveType.MOVEMENT_TYPES];		    // Firing ranges (full strength only) (calculated)
        public byte[] Detection = new byte[(int)MoveType.MOVEMENT_TYPES];	    // Electronic detection ranges
        public short[] Weapon = new short[WeaponsConstants.HARDPOINT_MAX];		// Weapon id of weapons (or weapon list)
        public byte[] Weapons = new byte[WeaponsConstants.HARDPOINT_MAX];		    // Number of shots each (fully supplied)
        public byte[] DamageMod = new byte[(int)DamageDataType.OtherDam + 1];	// How much each type will hurt me (% of strength applied)
        public byte visSignature;
        public byte irSignature;
        public byte pad1;
    };
}