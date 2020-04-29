namespace F4Utils.Campaign.F4Structs
{
    public class PtHeaderDataType {
        public short objID;				        // ID of the objective this belongs to
        public byte type;						// The type of pt data this contains
        public byte count;						// Number of points
        public byte[] features = new byte[CampLibConstants.MAX_FEAT_DEPEND];	// Features this list depends on (# in objective's feature list)
        public short data;						// Other data (runway heading, for example)
        public float sinHeading;
        public float cosHeading;
        public short first;						// Index of first point
        public short texIdx;					// texture to apply to this runway
        public sbyte runwayNum;					// -1 if not a runway, indicates which runway this list applies to
        public sbyte ltrt;						// put base pt to rt or left
        public short nextHeader;				// Index of next header, if any
    };
}