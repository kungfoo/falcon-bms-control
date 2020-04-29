using System;

namespace F4Utils.Terrain.Structs
{
    [Serializable]
    public struct TextureBinTileRecord
    {
        public TextureBinAreaRecord[] areaRecords;
        public uint numAreas;
        public uint numPaths;
        public TextureBinPathRecord[] pathRecords;
        public string tileName;
    }
}