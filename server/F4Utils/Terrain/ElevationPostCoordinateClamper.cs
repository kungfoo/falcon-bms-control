namespace F4Utils.Terrain
{
    internal interface IElevationPostCoordinateClamper
    {
        void ClampElevationPostCoordinates(ref int postColumn, ref int postRow, uint lod);
    }
    internal class ElevationPostCoordinateClamper:IElevationPostCoordinateClamper
    {
        private readonly TerrainDB _terrainDB;
        public ElevationPostCoordinateClamper(TerrainDB terrainDB)
        {
            _terrainDB = terrainDB;
        }
        public void ClampElevationPostCoordinates(ref int postColumn, ref int postRow, uint lod)
        {
            if (_terrainDB == null ||  _terrainDB.TheaterDotLxFiles == null)
            {
                postColumn = 0;
                postRow = 0;
                return;
            }
            var mapInfo = _terrainDB.TheaterDotMap;
            var lodInfo = _terrainDB.TheaterDotLxFiles[lod];

            const int postsAcross = Constants.NUM_ELEVATION_POSTS_ACROSS_SINGLE_LOD_SEGMENT;
            if (postColumn < 0) postColumn = 0;
            if (postRow < 0) postRow = 0;
            if (postColumn > (mapInfo.LODMapWidths[lodInfo.LoDLevel] * postsAcross) - 1)
                postColumn = (int)(mapInfo.LODMapWidths[lodInfo.LoDLevel] * postsAcross) - 1;
            if (postRow > (mapInfo.LODMapHeights[lodInfo.LoDLevel] * postsAcross) - 1)
                postRow = (int)(mapInfo.LODMapHeights[lodInfo.LoDLevel] * postsAcross) - 1;
        }
    }
}
