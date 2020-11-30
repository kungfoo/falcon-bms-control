namespace F4Utils.Campaign.F4Structs
{
    public struct RwrDataType
    {
        public float nominalRange;              // Nominal detection range
        public float top;                       // Scan volume top (Degrees in text file)
        public float bottom;                    // Scan volume bottom (Degrees in text file)
        public float left;                      // Scan volume left (Degrees in text file)
        public float right;                     // Scan volume right (Degrees in text file)
        public short flag;					   /* 0x01 = can get exact heading
											  0x02 = can only get vague direction
											  0x04 = can detect exact radar type
											  0x08 = can only detect group of radar types */
    }
}