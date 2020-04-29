using System;

namespace F4Utils.Terrain
{
    internal interface INearestElevationPostColumnAndRowCalculator
    {
        void GetNearestElevationPostColumnAndRowForNorthEastCoordinates(float feetNorth, float feetEast, uint lod, out int col, out int row);
    }
    internal class NearestElevationPostColumnAndRowCalculator:INearestElevationPostColumnAndRowCalculator
    {
        private readonly TerrainDB _terrainDB;
        private readonly IDistanceBetweenElevationPostsCalculator _distanceBetweenElevationPostsCalculator;
        private readonly IElevationPostCoordinateClamper _elevationPostCoordinateClamper;
        public NearestElevationPostColumnAndRowCalculator(
            TerrainDB terrainDB,
            IDistanceBetweenElevationPostsCalculator distanceBetweenElevationPostsCalculator = null,
            IElevationPostCoordinateClamper elevationPostCoordinateClamper = null) {
                _terrainDB = terrainDB;
            _distanceBetweenElevationPostsCalculator = distanceBetweenElevationPostsCalculator ?? new DistanceBetweenElevationPostsCalculator(_terrainDB);
            _elevationPostCoordinateClamper = elevationPostCoordinateClamper ?? new ElevationPostCoordinateClamper(_terrainDB);
        }
        public void GetNearestElevationPostColumnAndRowForNorthEastCoordinates(float feetNorth, float feetEast, uint lod, out int col, out int row)
        {
            var feetBetweenElevationPosts = _distanceBetweenElevationPostsCalculator.GetNumFeetBetweenElevationPosts(lod);
            col = (int)Math.Floor(feetEast / feetBetweenElevationPosts);
            row = (int)Math.Floor(feetNorth / feetBetweenElevationPosts);
            _elevationPostCoordinateClamper.ClampElevationPostCoordinates(ref row, ref col, lod);
        }
    }
}
