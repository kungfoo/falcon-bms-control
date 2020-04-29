namespace F4Utils.Campaign.F4Structs
{
    public enum DamageDataType
    {
        NoDamage=0,
        PenetrationDam,						// Hardened structures, tanks, ships
        HighExplosiveDam,					// Soft targets, area targets
        HeaveDam,							// Runways
        IncendairyDam,						// Burn baby, burn!
        ProximityDam,						// AA missiles, etc.
        KineticDam,							// Guns, small arms fire
        HydrostaticDam,						// Submarines
        ChemicalDam,
        NuclearDam,					
        OtherDam
    }
}