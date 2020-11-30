using System;
using F4Utils.Terrain.Structs;

namespace F4Utils.Terrain
{
    internal interface IColumnAndRowElevationPostRecordRetriever
    {
        TheaterDotLxFileRecord GetElevationPostRecordByColumnAndRow(int postColumn, int postRow, uint lod);
    }
    internal class ColumnAndRowElevationPostRecordRetriever:IColumnAndRowElevationPostRecordRetriever
    {
        private readonly TerrainDB _terrainDB;
        private readonly IElevationPostCoordinateClamper _elevationPostCoordinateClamper;

        public ColumnAndRowElevationPostRecordRetriever(
            TerrainDB terrainDB, 
            IElevationPostCoordinateClamper elevationPostCoordinateClamper = null)
        {
            _terrainDB = terrainDB;
            _elevationPostCoordinateClamper = elevationPostCoordinateClamper ?? new ElevationPostCoordinateClamper(_terrainDB);
        }
        public TheaterDotLxFileRecord GetElevationPostRecordByColumnAndRow(int postColumn, int postRow, uint lod)
        {
            if (_terrainDB == null || _terrainDB.TheaterDotLxFiles == null)
            {
                return null;
            }
            var lodInfo = _terrainDB.TheaterDotLxFiles[lod];
            if (lodInfo.O == null || lodInfo.L == null)
            {
                return null;
            }
            var mapInfo = _terrainDB.TheaterDotMap;
            const int postsAcross = Constants.NUM_ELEVATION_POSTS_ACROSS_SINGLE_LOD_SEGMENT;
            _elevationPostCoordinateClamper.ClampElevationPostCoordinates(ref postColumn, ref postRow, lodInfo.LoDLevel);
            var blockRow = (int)Math.Floor((postRow / (float)postsAcross));
            var blockCol = (int)Math.Floor((postColumn / (float)postsAcross));
            var oIndex = (int)(blockRow * mapInfo.LODMapHeights[lodInfo.LoDLevel]) + blockCol;
            var block = lodInfo.O[oIndex];
            var col = (postColumn % postsAcross);
            var row = (postRow % postsAcross);
            var lIndex =
                (int)
                (((block.LRecordStartingOffset / (lodInfo.LRecordSizeBytes * postsAcross * postsAcross)) * postsAcross *
                  postsAcross) + ((row * postsAcross) + col));
            var lRecord = lodInfo.L[lIndex];
            return lRecord;
        }
    }
}
