using F4Utils.Terrain.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Terrain
{
    internal interface ITerrainDotTidFileReader
    {
        F4Utils.Terrain.Structs.TerrainDotTidFileInfo Read(string terrainDotTidFile);
    }

    internal class TerrainDotTidFileReader : F4Utils.Terrain.ITerrainDotTidFileReader
    {
        public TerrainDotTidFileInfo Read(string terrainDotTidFile)
        {
            var tidFile = new TerrainDotTidFileInfo();
            using (var fileStream = new FileStream(terrainDotTidFile, FileMode.Open, FileAccess.Read))
            using (var streamReader = new BinaryReader(fileStream))
            {
                var numEntries = fileStream.Length / sizeof(UInt16);
                tidFile.TextureIDs = new short[numEntries];
                for (var i = 0; i < numEntries; i++)
                {
                    tidFile.TextureIDs[i] = streamReader.ReadInt16();
                }
            }
            return tidFile;
        }
    }
}
