namespace F4Utils.Campaign.F4Structs
{
    public class SimACDefType
    {
        public int  combatClass;                      // What type of combat does it do?
        public int  airframeIdx;                      // Index into airframe tables
        public int  signatureIdx;                     // Index into signature tables (IR only for now)
        public int[]  sensorType=new int[5];          // Sensor Types
        public int[]  sensorIdx=new int[5];           // Index into sensor data tables
    }
}