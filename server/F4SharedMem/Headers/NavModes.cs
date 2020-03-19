using System;

namespace F4SharedMem.Headers
{
    [Serializable]
    public enum NavModes : byte
    {
        ILS_TACAN = 0,
        TACAN = 1,
        NAV = 2,
        ILS_NAV = 3,
    };
}
