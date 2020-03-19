using System;

namespace F4SharedMem.Headers
{
    [Serializable]
    public enum TacanSources : byte
    {
        UFC = 0,
        AUX = 1,
        NUMBER_OF_SOURCES = 2,
    };
}
