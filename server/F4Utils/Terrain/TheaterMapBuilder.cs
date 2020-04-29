using System.Collections.Generic;
using Common.Drawing;
using Common.Drawing.Imaging;

namespace F4Utils.Terrain
{
    internal interface ITheaterMapBuilder
    {
        Bitmap GetTheaterMap(uint lod);
    }
    internal class TheaterMapBuilder:ITheaterMapBuilder
    {
        private readonly TerrainDB _terrainDB;
        private readonly Dictionary<uint, Bitmap> _theaterMaps = new Dictionary<uint, Bitmap>();
        public TheaterMapBuilder(TerrainDB terrainDB)
        {
            _terrainDB = terrainDB;
        }
        public unsafe Bitmap GetTheaterMap(uint lod)
        {
            if (!_theaterMaps.ContainsKey(lod))
            {
                var thisMap = BuildTheaterMap(lod);
                _theaterMaps[lod] = thisMap;
            }
            return _theaterMaps[lod];
        }
        public unsafe Bitmap BuildTheaterMap(uint lod)
        {
            var lodInfo = _terrainDB.TheaterDotLxFiles[lod];
            var mapInfo = _terrainDB.TheaterDotMap;
            const int postsAcross = Constants.NUM_ELEVATION_POSTS_ACROSS_SINGLE_LOD_SEGMENT;
            var bmp = new Bitmap((int)mapInfo.LODMapWidths[lodInfo.LoDLevel] * postsAcross,
                                 (int)mapInfo.LODMapHeights[lodInfo.LoDLevel] * postsAcross,
                                 PixelFormat.Format8bppIndexed);
            var palette = bmp.Palette;
            for (var i = 0; i < 256; i++)
            {
                palette.Entries[i] = mapInfo.Pallete[i];
            }
            bmp.Palette = palette;
            if (lodInfo.O == null || lodInfo.L == null) return bmp;
            var bmpLock = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly,
                                       bmp.PixelFormat);
            var scan0 = bmpLock.Scan0;
            var startPtr = scan0.ToPointer();
            var height = bmp.Height;
            var width = bmp.Width;
            for (var blockRow = 0; blockRow < ((int)mapInfo.LODMapHeights[lodInfo.LoDLevel]); blockRow++)
            {
                for (var blockCol = 0; blockCol < (mapInfo.LODMapWidths[lodInfo.LoDLevel]); blockCol++)
                {
                    var oIndex = (int)(blockRow * mapInfo.LODMapHeights[lodInfo.LoDLevel]) + blockCol;
                    var block = lodInfo.O[oIndex];
                    for (var postRow = 0; postRow < postsAcross; postRow++)
                    {
                        for (var postCol = 0; postCol < postsAcross; postCol++)
                        {
                            var lIndex =
                                (int)
                                (((block.LRecordStartingOffset / (lodInfo.LRecordSizeBytes * postsAcross * postsAcross)) *
                                  postsAcross * postsAcross) + ((postRow * postsAcross) + postCol));
                            var lRecord = lodInfo.L[lIndex];
                            var xCoord = (blockCol * postsAcross) + postCol;
                            var yCoord = height - 1 - (blockRow * postsAcross) - postRow;
                            *((byte*)startPtr + (yCoord * width) + xCoord) = lRecord.Pallete;
                        }
                    }
                }
            }
            bmp.UnlockBits(bmpLock);
            return bmp;
        }
    }
}
