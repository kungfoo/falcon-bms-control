using System;
using System.IO;
using System.Text;
using F4Utils.Terrain.Structs;
using Common.Strings;

namespace F4Utils.Terrain
{
    internal interface ITextureDotBinFileReader
    {
        TextureDotBinFileInfo ReadTextureDotBinFile(string textureDotBinFilePath);
    }
    internal class TextureDotBinFileReader:ITextureDotBinFileReader
    {
        public TextureDotBinFileInfo ReadTextureDotBinFile(string textureDotBinFilePath)
        {
            if (String.IsNullOrEmpty(textureDotBinFilePath)) throw new ArgumentNullException("textureBinFilePath");
            var fileInfo = new FileInfo(textureDotBinFilePath);
            if (!fileInfo.Exists) throw new FileNotFoundException(textureDotBinFilePath);

            var bytesToRead = (int)fileInfo.Length;
            var bytesRead = new byte[bytesToRead];

            using (var stream = File.OpenRead(textureDotBinFilePath))
            using (var reader = new BinaryReader(stream))
            {
                var textureBinFileInfo = new TextureDotBinFileInfo();

                textureBinFileInfo.numSets = reader.ReadUInt32();
                textureBinFileInfo.setRecords = new TextureBinSetRecord[textureBinFileInfo.numSets];
                textureBinFileInfo.totalTiles = reader.ReadUInt32();

                for (var h = 0; h < textureBinFileInfo.numSets; h++)
                {
                    var thisSetRecord = new TextureBinSetRecord
                    {
                        numTiles = reader.ReadUInt32()
                    };

                    thisSetRecord.terrainType = reader.ReadByte();

                    thisSetRecord.tileRecords = new TextureBinTileRecord[thisSetRecord.numTiles];
                    for (var i = 0; i < thisSetRecord.numTiles; i++)
                    {
                        var tileRecord = new TextureBinTileRecord
                        {
                            tileName = Encoding.ASCII.GetString(reader.ReadBytes(20), 0, 20).TrimAtNull()
                        };
                        
                        tileRecord.numAreas = reader.ReadUInt32();
                        tileRecord.areaRecords = new TextureBinAreaRecord[tileRecord.numAreas];
                        tileRecord.numPaths = reader.ReadUInt32();
                        tileRecord.pathRecords = new TextureBinPathRecord[tileRecord.numPaths];

                        for (var j = 0; j < tileRecord.numAreas; j++)
                        {
                            tileRecord.areaRecords[j] = new TextureBinAreaRecord(stream); 
                        }
                        for (var k = 0; k < tileRecord.numPaths; k++)
                        {
                            tileRecord.pathRecords[k] = new TextureBinPathRecord(stream); 
                        }
                        thisSetRecord.tileRecords[i] = tileRecord;
                    }
                    textureBinFileInfo.setRecords[h] = thisSetRecord;
                }
                return textureBinFileInfo;
            }
        }

    }
}
