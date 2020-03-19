using System;

namespace F4SharedMem.Headers
{
    [Serializable]
    // RTT area indices
    public enum RTT_areas : byte
    {
        RTT_HUD = 0,
        RTT_PFL,
        RTT_DED,
        RTT_RWR,
        RTT_MFDLEFT,
        RTT_MFDRIGHT,
        RTT_HMS,
        RTT_noOfAreas
    };
}
