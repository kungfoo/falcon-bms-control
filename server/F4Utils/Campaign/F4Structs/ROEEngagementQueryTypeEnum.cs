using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Campaign.F4Structs
{
    // Rules of engagement query types
    public enum ROEEngagementQueryTypeEnum
    {
        ROE_GROUND_FIRE = 1,		// Fire on their ground troops?
        ROE_GROUND_MOVE = 2,		// Move through their territory?
        ROE_GROUND_CAPTURE = 3,		// Capture their territory?
        ROE_AIR_ENGAGE = 4,		// Maneuver against their aircraft?
        ROE_AIR_FIRE = 5,		// Fire at their aircraft? (any range)
        ROE_AIR_FIRE_BVR = 6,		// Fire at their aircraft BVR
        ROE_AIR_OVERFLY = 7,		// Fly over their territory?
        ROE_AIR_ATTACK = 8,		// Bomb/attack their territory?
        ROE_AIR_USE_BASES = 9,		// Can we based aircraft at their airbases?
        ROE_NAVAL_FIRE = 10,		// Attack their shipping?
        ROE_NAVAL_MOVE = 11,		// Move into/through their harbors/straights?
        ROE_NAVAL_BOMBARD = 12,		// Bombard them messily?
    };
}
