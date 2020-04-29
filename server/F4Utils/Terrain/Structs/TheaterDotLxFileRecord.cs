using System;

namespace F4Utils.Terrain.Structs
{
    [Serializable]
    public class TheaterDotLxFileRecord
    {
        public UInt16 Elevation;
        public byte Pallete;
        public UInt32 TextureId;
        public byte X1;
        public byte X2;
    }
}