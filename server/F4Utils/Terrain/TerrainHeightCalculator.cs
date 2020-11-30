namespace F4Utils.Terrain
{
    internal interface ITerrainHeightCalculator
    {
        float CalculateTerrainHeight(float feetNorth, float feetEast, uint lod);
    }
    internal class TerrainHeightCalculator:ITerrainHeightCalculator
    {
        private readonly TerrainDB _terrainDB;
        private readonly IColumnAndRowElevationPostRecordRetriever _columnAndRowElevationPostRecordRetriever;
        private readonly IDistanceBetweenElevationPostsCalculator _distanceBetweenElevationPostsCalculator;
        private readonly INearestElevationPostColumnAndRowCalculator _nearestElevationPostColumnAndRowCalculator;
        internal TerrainHeightCalculator(
            TerrainDB terrainDB,
            IColumnAndRowElevationPostRecordRetriever columnAndRowElevationPostRecordRetriever = null,
            IDistanceBetweenElevationPostsCalculator distanceBetweenElevationPostsCalculator = null,
            INearestElevationPostColumnAndRowCalculator nearestElevationPostColumnAndRowCalculator=null)
        {
            _terrainDB = terrainDB;
            _columnAndRowElevationPostRecordRetriever = columnAndRowElevationPostRecordRetriever ?? new ColumnAndRowElevationPostRecordRetriever(_terrainDB);
            _distanceBetweenElevationPostsCalculator = distanceBetweenElevationPostsCalculator ?? new DistanceBetweenElevationPostsCalculator(_terrainDB);
            _nearestElevationPostColumnAndRowCalculator = nearestElevationPostColumnAndRowCalculator ?? new NearestElevationPostColumnAndRowCalculator(_terrainDB);
        }
        public TerrainHeightCalculator()
            : this(null, null, null)
        {

        }
        public float CalculateTerrainHeight(float feetNorth, float feetEast, uint lod)
        {
            int col;
            int row;
            var feetAcross = _distanceBetweenElevationPostsCalculator.GetNumFeetBetweenElevationPosts(lod);

            //determine the column and row in the DTED matrix where the nearest elevation post can be found
            _nearestElevationPostColumnAndRowCalculator.GetNearestElevationPostColumnAndRowForNorthEastCoordinates(feetNorth, feetEast, lod, out col, out row);

            //retrieve the 4 elevation posts which form a box around our current position (origin point x=0,y=0 is in lower left)
            var Q11 = _columnAndRowElevationPostRecordRetriever.GetElevationPostRecordByColumnAndRow(col, row, lod);
            var Q21 = _columnAndRowElevationPostRecordRetriever.GetElevationPostRecordByColumnAndRow(col + 1, row, lod);
            var Q22 = _columnAndRowElevationPostRecordRetriever.GetElevationPostRecordByColumnAndRow(col + 1, row + 1, lod);
            var Q12 = _columnAndRowElevationPostRecordRetriever.GetElevationPostRecordByColumnAndRow(col, row + 1, lod);
            
            if (Q11 == null || Q21 == null || Q22 == null || Q12 == null)
            {
                return 0;
            }
            //determine the North/East coordinates of these 4 posts, respectively
            var Q11North = row * feetAcross;
            var Q11East = col * feetAcross;
            float FQ11 = Q11.Elevation;

            var Q21East = (col + 1) * feetAcross;
            float FQ21 = Q21.Elevation;

            float FQ22 = Q22.Elevation;

            var Q12North = (row + 1) * feetAcross;
            float FQ12 = Q12.Elevation;

            //perform bilinear interpolation on the 4 outer elevation posts relative to our actual center post
            //see: http://en.wikipedia.org/wiki/Bilinear_interpolation

            var x = feetEast;
            var y = feetNorth;

            var x1 = Q11East;
            var x2 = Q21East;
            var y1 = Q11North;
            var y2 = Q12North;

            var result =
                (
                    ((FQ11 / ((x2 - x1) * (y2 - y1))) * (x2 - x) * (y2 - y))
                    +
                    ((FQ21 / ((x2 - x1) * (y2 - y1))) * (x - x1) * (y2 - y))
                    +
                    ((FQ12 / ((x2 - x1) * (y2 - y1))) * (x2 - x) * (y - y1))
                    +
                    ((FQ22 / ((x2 - x1) * (y2 - y1))) * (x - x1) * (y - y1))
                );

            return result;
        }
    }
}
