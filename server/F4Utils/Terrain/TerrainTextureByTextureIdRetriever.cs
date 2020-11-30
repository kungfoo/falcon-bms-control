using System.Collections.Generic;
using Common.Drawing;

namespace F4Utils.Terrain
{
    internal interface ITerrainTextureByTextureIdRetriever
    {
        Bitmap GetTerrainTextureByTextureId(uint textureId, uint lod);
    }
    internal class TerrainTextureByTextureIdRetriever:ITerrainTextureByTextureIdRetriever
    {
        private readonly INearTileTextureLoader _nearTileTextureLoader;
        private readonly IFarTileTextureRetriever _farTileTextureRetriever;
        private readonly TerrainDB _terrainDB;
        public TerrainTextureByTextureIdRetriever(
            TerrainDB terrainDB, 
            INearTileTextureLoader nearTileTextureLoader = null, 
            IFarTileTextureRetriever farTileTextureRetriever = null)
        {
            _terrainDB = terrainDB;
            _nearTileTextureLoader = nearTileTextureLoader ?? new NearTileTextureLoader(_terrainDB);
            _farTileTextureRetriever = farTileTextureRetriever ?? new FarTileTextureRetriever(_terrainDB);
        }
        public Bitmap GetTerrainTextureByTextureId(uint textureId, uint lod)
        {
            var lodInfo = _terrainDB.TheaterDotLxFiles[lod];
            Bitmap toReturn = null;

            if (lod <= _terrainDB.TheaterDotMap.LastNearTiledLOD)
            {
                var textureBinInfo = _terrainDB.TextureDotBin;
                textureId -= lodInfo.minTexOffset;

                var setNum = textureId / Constants.NUM_TEXTURES_PER_SET;
                var tileNum = textureId % Constants.NUM_TEXTURES_PER_SET;
                var thisSet = textureBinInfo.setRecords[setNum];
                var tileName = thisSet.tileRecords[tileNum].tileName;
                toReturn = _nearTileTextureLoader.LoadNearTileTexture(tileName);
            }
            else if (lod <= _terrainDB.TheaterDotMap.LastFarTiledLOD)
            {
                toReturn = _farTileTextureRetriever.GetFarTileTexture(textureId);
            }
            return toReturn;
        }
    }
}
