namespace F4Utils.Campaign.F4Structs
{
    public class SimWeaponDataType
    {
        public int  flags;                            // Flags for the SMS
        public float cd;                              // Drag coefficient
        public float weight;                          // Weight
        public float area;                            // sirface area for drag calc
        public float xEjection;                       // Body X axis ejection velocity
        public float yEjection;                       // Body Y axis ejection velocity
        public float zEjection;                       // Body Z axis ejection velocity
        public sbyte[] mnemonic=new sbyte[8];         // SMS Mnemonic
        public int weaponClass;                       // SMS Weapon Class
        public int domain;                            // SMS Weapon Domain
        public int weaponType;                        // SMS Weapon Type
        public int dataIdx;                           // Aditional characteristics data file
    }
}