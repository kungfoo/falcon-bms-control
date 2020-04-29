using System;
using Common.Drawing;
using System.IO;
using Common.Imaging;
using ICSharpCode.SharpZipLib.Zip;
using log4net;
using Util = Common.Compression.Zip.Util;

namespace F4Utils.Terrain
{
    internal interface INearTileTextureLoader
    {
        Bitmap LoadNearTileTexture(string tileName);
    }
    internal class NearTileTextureLoader:INearTileTextureLoader
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NearTileTextureLoader));
        private readonly TerrainDB _terrainDB;
        public NearTileTextureLoader(TerrainDB terrainDB)
        {
            _terrainDB = terrainDB;
        }
        public Bitmap LoadNearTileTexture(string tileName)
        {
            Bitmap toReturn;
            var tileFullPath = Path.Combine(_terrainDB.CurrentTheaterTextureBaseFolderPath, tileName);

            var tileInfo = new FileInfo(tileFullPath);
            if (string.Equals(tileInfo.Extension, ".PCX", StringComparison.InvariantCultureIgnoreCase))
            {
                var textureSubfolder = !string.IsNullOrWhiteSpace(_terrainDB.TileSet) ? "TEXTURE_" + _terrainDB.TileSet: "TEXTURE"; 
                tileFullPath = Path.Combine(Path.Combine(_terrainDB.CurrentTheaterTextureBaseFolderPath,textureSubfolder), Path.GetFileNameWithoutExtension(tileInfo.Name) + ".DDS");
                tileInfo = new FileInfo(tileFullPath);
            }
            if (tileInfo.Exists)
            {
                try
                {
                    toReturn = DDS.Load(tileFullPath);
                    return toReturn;
                }
                catch (Exception e)
                {
                    Log.Debug(e.Message, e);
                }
            }

            if (!_terrainDB.TextureDotZipFileEntries.ContainsKey(tileName.ToLowerInvariant())) return null;
            var thisEntry = _terrainDB.TextureDotZipFileEntries[tileName.ToLowerInvariant()];
            using (var zipStream = _terrainDB.TextureZipFile.GetInputStream(thisEntry))
            {
                var rawBytes = new byte[zipStream.Length];
                zipStream.Read(rawBytes, 0, rawBytes.Length);
                toReturn = PCX.LoadFromBytes(rawBytes);
            }
            return toReturn;
        }

    }
}
