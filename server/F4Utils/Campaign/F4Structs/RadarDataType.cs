namespace F4Utils.Campaign.F4Structs
{
    public class RadarDataType
    {
        public int RWRsound;			            // Which sound plays in the RWR
        public short RWRsymbol;			            // Which symbol shows up on the RWR
        public short RDRDataInd;		            // Index into radar data files
        public float[] Lethality = new float[(int)AltLethality.NUM_ALT_LETHALITY];	// Lethality against low altitude targets
        public float NominalRange;					// Detection range against F16 sized target
        public float BeamHalfAngle;					// radians (degrees in file)
        public float ScanHalfAngle;					// radians (degrees in file)
        public float SweepRate;						// radians/sec (degrees in file)
        public uint CoastTime;						// ms to hold lock on faded target (seconds in file)
        public float LookDownPenalty;				// degrades SN ratio
        public float JammingPenalty;			    // degrades SN ratio
        public float NotchPenalty;					// degrades SN ratio
        public float NotchSpeed;					// ft/sec (kts in file)
        public float ChaffChance;					// Base probability a bundle of chaff will decoy this radar
        public short flag;							// 0x01 = NCTR capable
    }
}