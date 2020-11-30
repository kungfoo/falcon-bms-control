namespace F4Utils.Campaign.F4Structs
{
    // Target desctiption types (when to use target actions)
    public enum TargetDescriptionType
    {
        TDESC_NONE = 0,
        TDESC_TTL = 1,					// Takeoff To Landing (mission actions during whole route)
        TDESC_ATA = 2,					// Assembly To Assembly
        TDESC_TAO = 3,					// Target Area Only
    };
}
