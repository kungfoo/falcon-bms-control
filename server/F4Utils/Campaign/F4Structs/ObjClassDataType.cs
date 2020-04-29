namespace F4Utils.Campaign.F4Structs
{
    public class ObjClassDataType {
        public short Index;						// descriptionIndex pointing here
        public byte[] Name=new byte[20];
        public short DataRate;					// Sorte Rate and other cool data
        public short DeagDistance;				// Distance to deaggregate at.
        public short PtDataIndex;				// Index into pt header data table
        public byte[] Detection=new byte[(int)MoveType.MOVEMENT_TYPES];	// Detection ranges
        public byte[] DamageMod=new byte[(int)DamageDataType.OtherDam+1];		// How much each type will hurt me (% of strength applied)
        public short IconIndex;					// Index to this objective's icon type
        public byte	Features;					// Number of features in this objective
        public byte	RadarFeature;				// ID of the radar feature for this objective
        public short FirstFeature;				// Index of first feature entry
    };
}