using System;

namespace F4Utils.Terrain.Structs
{
    [Serializable]
    public struct TheaterDotLxFileInfo
    {
        public TheaterDotLxFileRecord[] L;
        public uint LRecordSizeBytes;
        public uint LoDLevel;
        public UInt16 MaxElevation;
        public UInt16 MinElevation;
        public TheaterDotOxFileRecord[] O;
        public uint maxTexOffset;
        public uint minTexOffset;
    }
}