namespace F4Utils.Campaign.F4Structs
{
    public class FeatureClassDataType {
        public short Index;						// descriptionIndex pointing here
        public short RepairTime;			    // How long it takes to repair
        public byte Priority;					// Display priority
        public ushort Flags;					// See FEAT_ flags in feature.h
        public byte[] Name=new byte[20];		// 'Control Tower'
        public short HitPoints;					// Damage this thing can take
        public short Height;					// Height of vehicle ramp, if any
        public float Angle;						// Angle of vehicle ramp, if any
        public short RadarType;					// Index into RadarDataTable
        public byte[] Detection= new byte[(int)MoveType.MOVEMENT_TYPES];	// Electronic detection ranges
        public byte[] DamageMod=new byte[(int)DamageDataType.OtherDam+1];  // How much each type will hurt me (% of strength applied)
    };
}