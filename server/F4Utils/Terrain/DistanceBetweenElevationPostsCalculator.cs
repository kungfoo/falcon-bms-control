namespace F4Utils.Terrain
{
    internal interface IDistanceBetweenElevationPostsCalculator
    {
        float GetNumFeetBetweenElevationPosts(uint lod);
    }
    internal class DistanceBetweenElevationPostsCalculator:IDistanceBetweenElevationPostsCalculator
    {
        private readonly TerrainDB _terrainDB;
        public DistanceBetweenElevationPostsCalculator(TerrainDB terrainDB)
        {
            _terrainDB = terrainDB;
        }
        public float GetNumFeetBetweenElevationPosts(uint lod)
        {
            
            if (_terrainDB == null || _terrainDB.TheaterDotLxFiles == null) return 0;
            var lodInfo = _terrainDB.TheaterDotLxFiles[lod];
            var mapInfo = _terrainDB.TheaterDotMap;
            var feetBetweenPosts = mapInfo.FeetBetweenL0Posts;
            for (var i = 1; i <= lodInfo.LoDLevel; i++)
            {
                feetBetweenPosts *= 2;
            }
            return feetBetweenPosts;
        }

    }
}
