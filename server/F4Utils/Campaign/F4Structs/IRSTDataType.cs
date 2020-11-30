namespace F4Utils.Campaign.F4Structs
{
    public struct IRSTDataType
    {
        public float NominalRange;					// Detection range against F16 sized target
        public float FOVHalfAngle;					// radians (degrees in file)
        public float GimbalLimitHalfAngle;			// radians (degrees in file)
        public float GroundFactor;					// Range multiplier applied for ground targets
        public float FlareChance;					// Base probability a flare will work
    }
}