using Common.Drawing;
using Common.Imaging;
using System.Collections.Generic;

namespace F4Utils.Terrain
{
    internal interface IDetailTextureForElevationPostRetriever
    {
        Bitmap GetDetailTextureForElevationPost(int postCol, int postRow, uint lod);
    }
    internal class DetailTextureForElevationPostRetriever:IDetailTextureForElevationPostRetriever
    {
        private readonly IElevationPostCoordinateClamper _elevationPostCoordinateClamper;
        private readonly ITerrainTextureByTextureIdRetriever _terrainTextureByTextureIdRetriever;
        private readonly IColumnAndRowElevationPostRecordRetriever _columnAndRowElevationPostRetriever;
        private readonly TerrainDB _terrainDB;
        public DetailTextureForElevationPostRetriever(
            TerrainDB terrainDB,
            IElevationPostCoordinateClamper elevationPostCoordinateClamper = null,
            ITerrainTextureByTextureIdRetriever terrainTextureByTextureIdRetriever = null,
            IColumnAndRowElevationPostRecordRetriever columnAndRowElevationPostRetriever = null
            )
        {
            _terrainDB = terrainDB;
            _elevationPostCoordinateClamper = elevationPostCoordinateClamper ?? new ElevationPostCoordinateClamper(_terrainDB);
            _terrainTextureByTextureIdRetriever = terrainTextureByTextureIdRetriever ?? new TerrainTextureByTextureIdRetriever(_terrainDB);
            _columnAndRowElevationPostRetriever = columnAndRowElevationPostRetriever ?? new ColumnAndRowElevationPostRecordRetriever(_terrainDB, _elevationPostCoordinateClamper);
        }
        public Bitmap GetDetailTextureForElevationPost(int postCol, int postRow, uint lod)
        {
           
            var col = postCol;
            var row = postRow;

            _elevationPostCoordinateClamper.ClampElevationPostCoordinates(ref col, ref row, lod);
            if (postCol != col || postRow != row)
            {
                col = 0;
                row = 0;
            }


            var lRecord = _columnAndRowElevationPostRetriever.GetElevationPostRecordByColumnAndRow(col, row, lod);
            if (lRecord == null) return null;

            var textureId = lRecord.TextureId;
            var bigTexture = _terrainTextureByTextureIdRetriever.GetTerrainTextureByTextureId(textureId, lod);

            Bitmap toReturn;
            if (lod <= _terrainDB.TheaterDotMap.LastNearTiledLOD)
            {
                var chunksWide = 4 >> (int)lod;
                var thisChunkXIndex = (uint)(col % chunksWide);
                var thisChunkYIndex = (uint)(row % chunksWide);

                var leftX = (int)(thisChunkXIndex * (bigTexture.Width / chunksWide));
                var rightX = (int)((thisChunkXIndex + 1) * (bigTexture.Width / chunksWide)) - 1;
                var topY = (int)(bigTexture.Height - (thisChunkYIndex + 1) * (bigTexture.Height / chunksWide));
                var bottomY = (int)(bigTexture.Height - thisChunkYIndex * (bigTexture.Height / chunksWide)) - 1;

                var sourceRect = new Rectangle(leftX, topY, (rightX - leftX) + 1, (bottomY - topY) + 1);

                toReturn = (Bitmap)Util.CropBitmap(bigTexture, sourceRect);
                bigTexture.Dispose();
            }
            else
            {
                toReturn = bigTexture;
            }
            return toReturn;
        }
    }
}
