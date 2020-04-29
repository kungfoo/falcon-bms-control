using System;
using Common.Drawing;

namespace F4Utils.Terrain.Structs
{
    [Serializable]
    public struct TheaterDotMapFileInfo
    {
        public float FeetBetweenL0Posts;
        public float FeetToMeaCellConversionFactor;
        public Color[] GreenPallete;
        public UInt32[] LODMapHeights;
        public UInt32[] LODMapWidths;
        public UInt32 LastFarTiledLOD;
        public UInt32 LastNearTiledLOD;
        public UInt32 MEAMapHeight;
        public UInt32 MEAMapWidth;
        public UInt32 NumLODs;
        public Color[] Pallete;
        public float baseLat;
        public float baseLong;
        public UInt32 flags;
    }
}