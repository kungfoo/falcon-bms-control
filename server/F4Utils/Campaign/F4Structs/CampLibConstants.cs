namespace F4Utils.Campaign.F4Structs
{
    public class CampLibConstants
    {
        public const uint INFINITE_TIME = 4294967295; // Max value of CampaignTime
        public const uint VEHICLES_PER_UNIT = 16;
        public const uint FEATURES_PER_OBJ = 32;
        public const uint MAXIMUM_ROLES = 16;
        public const uint MAXIMUM_OBJTYPES = 32;
        public const uint MAXIMUM_WEAPTYPES = 600;
        public const uint MAX_UNIT_CHILDREN = 5;
        public const uint MAX_FEAT_DEPEND = 5;

        public const uint MAX_NUMBER_OF_OBJECTIVES = 8000;
        public const uint MAX_NUMBER_OF_UNITS = 4000; // Max # of NON volitile units only
        public const uint MAX_NUMBER_OF_VOLITILE_UNITS = 16000;
        public const uint MAX_CAMP_ENTITIES = (MAX_NUMBER_OF_OBJECTIVES + MAX_NUMBER_OF_UNITS + MAX_NUMBER_OF_VOLITILE_UNITS);
    }
}