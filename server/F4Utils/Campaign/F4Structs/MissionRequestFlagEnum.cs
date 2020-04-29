using System;
namespace F4Utils.Campaign.F4Structs
{
    // Flags for mission requests
    [Flags]
    public enum MissionRequestFlagEnum
    {
        REQF_USERESERVES = 0x01,			// Use reserve aircraft for this request, if nessesary (mostly for support)
        REQF_CHECKED = 0x02,				// This request has already been checked at least once
        REQF_NEEDRESPONSE = 0x04,			// This needs to be dealt with immediately, and returns a response
        REQF_MET = 0x08,				    // This has been met already (in planned list)
        REQF_ONETRY = 0x100,				// We get one try at building this, otherwise cancel
        REQF_USE_REQ_SQUAD = 0x200,			// Use the squadron which requested the mission
        REQF_PART_OF_ACTION = 0x400,		// This target is part of a larger action by this team
        REQF_ALLOW_ERRORS = 0x800,			// Allow missions to be planned with errors (for tactical engagement)
        REQF_TE_MISSION = 0x1000,		    // This is a TE mission, correct mission length calculation mission.cpp
    };


}
