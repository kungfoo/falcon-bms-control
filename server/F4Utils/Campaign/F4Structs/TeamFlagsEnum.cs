using System;

namespace F4Utils.Campaign.F4Structs
{
    // Team flags
    [Flags]
    public enum TeamFlagsEnum
    {
        TEAM_ACTIVE = 0x01,	// Set if team is being used
        TEAM_HASSATS = 0x02,	// Has satelites
        TEAM_UPDATED = 0x04,	// We've gotten remote data for this team
        TEAM_PLAYERPRIO = 0x08,	// Dunc 20111107: Team priorities managed by player, not HQ (i.e "set by HQ" is OFF)
        TEAM_MEMBER_HASSATS = 0x10,	// BMS - Biker - If team member (who has joind the team) has access to satellite data
    };
}
