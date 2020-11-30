using System;

namespace F4Utils.Terrain.Structs
{
    [Serializable]
    public struct TextureDotBinFileInfo
    {
        public uint numSets;
        public TextureBinSetRecord[] setRecords;
        public uint totalTiles;
    }
}