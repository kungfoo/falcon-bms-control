using System;

namespace F4SharedMem.Headers
{
    [Serializable]
    public enum FlyStates : byte
    {
        IN_UI = 0, // UI      - in the UI
        LOADING = 1, // UI>3D   - loading the sim data
        WAITING = 2, // UI>3D   - waiting for other players
        FLYING = 3, // 3D      - flying
        DEAD = 4, // 3D>Dead - dead, waiting to respawn
        UNKNOWN = 5, // ???
    };
}
