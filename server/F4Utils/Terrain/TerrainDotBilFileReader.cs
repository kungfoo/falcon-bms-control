using F4Utils.Terrain.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Terrain
{
    internal interface ITerrainDotBilFileReader
    {
        F4Utils.Terrain.Structs.TerrainDotBilFileInfo Read(string terrainDotBilFile);
    }

    internal class TerrainDotBilFileReader : F4Utils.Terrain.ITerrainDotBilFileReader
    {
        public TerrainDotBilFileInfo Read(string terrainDotBilFile)
        {
            var bilFile = new TerrainDotBilFileInfo();
            using (var fileStream = new FileStream(terrainDotBilFile, FileMode.Open, FileAccess.Read))
            using (var streamReader = new BinaryReader(fileStream))
            {
                var numEntries = fileStream.Length / sizeof(UInt16);
                bilFile.Elevations = new short[numEntries];
                for (var i = 0; i < numEntries; i++)
                {
                    bilFile.Elevations[i] = streamReader.ReadInt16();
                }
            }
            return bilFile;
        }
    }
}
