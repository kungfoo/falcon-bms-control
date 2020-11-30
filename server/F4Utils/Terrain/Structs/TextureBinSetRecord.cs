using System;

namespace F4Utils.Terrain.Structs
{
    [Serializable]
    public struct TextureBinSetRecord
    {
        public uint numTiles;
        public byte terrainType;
        public TextureBinTileRecord[] tileRecords;
    }
}