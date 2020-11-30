namespace F4Utils.Campaign.F4Structs
{
    public enum MissionTypeEnum
    {                                   // NOTE: This must fit int a uchar!
        AMIS_NONE = 0,
        AMIS_BARCAP1 = 1,			    // BARCAP missions to protect a target area
        AMIS_BARCAP2 = 2,			    // BARCAP missions to defend a border
        AMIS_HAVCAP = 3,
        AMIS_TARCAP = 4,
        AMIS_RESCAP = 5,
        AMIS_AMBUSHCAP = 6,
        AMIS_SWEEP = 7,
        AMIS_ALERT = 8,
        AMIS_INTERCEPT = 9,
        AMIS_ESCORT = 10,
        AMIS_SEADSTRIKE = 11,
        AMIS_SEADESCORT = 12,
        AMIS_OCASTRIKE = 13,			// OCA strike (direct w/escort & sead)
        AMIS_INTSTRIKE = 14,			// Interdiction (direct, alone)
        AMIS_STRIKE = 15,			    // 
        AMIS_DEEPSTRIKE = 16,			// Deep strike (safe path, w/escort & sead)
        AMIS_STSTRIKE = 17,			    // Stealth strike (safe path, alone)
        AMIS_STRATBOMB = 18,
        AMIS_FAC = 19,
        AMIS_ONCALLCAS = 20,			// On call CAS
        AMIS_PRPLANCAS = 21,			// Pre planned CAS
        AMIS_CAS = 22,			        // Immediate CAS
        AMIS_SAD = 23,			        // Search and destroy interdiction
        AMIS_INT = 24,			        // Interdiction (vs supply/fuel lines only)
        AMIS_BAI = 25,			        // Battlefield area interdiction
        AMIS_AWACS = 26,
        AMIS_JSTAR = 27,
        AMIS_ELINT = 27,
        AMIS_TANKER = 28,
        AMIS_RECON = 29,
        AMIS_BDA = 30,
        AMIS_ECM = 31,
        AMIS_AIRCAV = 32,
        AMIS_AIRLIFT = 33,
        AMIS_SAR = 34,
        AMIS_ASW = 35,
        AMIS_ASHIP = 36,
        AMIS_PATROL = 37,
        AMIS_RECONPATROL = 38,			// Recon for enemy ground vehicles
        AMIS_ABORT = 39,
        AMIS_TRAINING = 40,
        AMIS_OTHER = 41,
    };
}
