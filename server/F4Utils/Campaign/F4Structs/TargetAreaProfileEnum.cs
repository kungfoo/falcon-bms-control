

namespace F4Utils.Campaign.F4Structs
{
    // Target area profiles
    public enum TargetAreaProfileEnum
    {
        TPROF_NONE = 0,					// No target WP
        TPROF_ATTACK = 1,					// IP, Target, Turn point  (Assembly, Break point)
        TPROF_HPATTACK = 2,					// IP, Target (Assembly, Break point)
        TPROF_LOITER = 3,					// 2 Turn points (Assembly)
        TPROF_TARGET = 4,					// Target only (Assembly, Break point)
        TPROF_AVOID = 5,					// Break point, Turn point (Assembly)
        TPROF_SWEEP = 6,					// 3 circular Turn points
        TPROF_FLYBY = 7,					// Pass over target, at lowest possible threat
        TPROF_SEARCH = 8,					// Like loiter, but at full speed
        TPROF_LAND = 9,					// Land at target for station time, then takeoff again
    };
}
