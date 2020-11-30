using System;
using System.Collections.Generic;
using System.IO;
using F4Utils.Terrain.Structs;

namespace F4Utils.Terrain
{
    internal interface ITheaterDotLxFileReader
    {
        TheaterDotLxFileInfo LoadTheaterDotLxFile(uint lodLevel, string theaterDotMapFilePath, string tileset=null);
    }
    internal class TheaterDotLxFileReader:ITheaterDotLxFileReader
    {
        public TheaterDotLxFileInfo LoadTheaterDotLxFile(uint lodLevel, string theaterDotMapFilePath, string tileset=null)
        {

            if (String.IsNullOrEmpty(theaterDotMapFilePath)) throw new ArgumentNullException("theaterMapFilePath");

            var lFileInfo =
                new FileInfo(Path.GetDirectoryName(theaterDotMapFilePath) + Path.DirectorySeparatorChar 
                    + "theater"
                    + (tileset != null ? "_" + tileset : "")
                    + ".L" + lodLevel);
            var oFileInfo =
                new FileInfo(Path.GetDirectoryName(theaterDotMapFilePath) + Path.DirectorySeparatorChar
                    + "theater"
                    + (tileset != null ? "_" + tileset : "")
                    + ".O" + lodLevel);

            var toReturn = new TheaterDotLxFileInfo
            {
                MinElevation = UInt16.MaxValue,
                MaxElevation = 0,
                LoDLevel = lodLevel
            };
            if (!oFileInfo.Exists)
            {
                return toReturn;
            }

            var bytesToRead = oFileInfo.Length;
            var bytesRead = new byte[bytesToRead];
            using (var stream = File.OpenRead(oFileInfo.FullName))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(bytesRead, 0, (int)bytesToRead);
                stream.Close();
            }
            var isFourByte = true;

            var oFileRecords = new List<TheaterDotOxFileRecord>();
            var curByte = 0;
            while (curByte < bytesToRead)
            {
                var thisDword = BitConverter.ToUInt32(bytesRead, curByte);
                if (thisDword % 2304 != 0)
                {
                    //not a 4-byte file
                    isFourByte = false;
                    break;
                }
                var record = new TheaterDotOxFileRecord { LRecordStartingOffset = thisDword };
                oFileRecords.Add(record);
                curByte += 4;
            }

            curByte = 0;
            if (!isFourByte)
            {
                oFileRecords.Clear();
                while (curByte < bytesToRead)
                {
                    UInt32 thisWord = BitConverter.ToUInt16(bytesRead, curByte);
                    var record = new TheaterDotOxFileRecord { LRecordStartingOffset = thisWord };
                    oFileRecords.Add(record);
                    curByte += 2;
                }
            }

            curByte = 0;
            bytesToRead = lFileInfo.Length;
            bytesRead = new byte[bytesToRead];
            using (var stream = File.OpenRead(lFileInfo.FullName))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(bytesRead, 0, (int)bytesToRead);
                stream.Close();
            }
            uint maxTextureOffset = 0;
            var minTextureOffset = uint.MaxValue;
            var lFileRecords = new List<TheaterDotLxFileRecord>();
            if (isFourByte)
            {
                toReturn.LRecordSizeBytes = 9;
                while (curByte < bytesToRead)
                {
                    var record = new TheaterDotLxFileRecord
                    {
                        TextureId = BitConverter.ToUInt32(bytesRead, curByte)
                    };
                    if (record.TextureId > maxTextureOffset) maxTextureOffset = record.TextureId;
                    if (record.TextureId < minTextureOffset) minTextureOffset = record.TextureId;
                    record.Elevation = BitConverter.ToUInt16(bytesRead, curByte + 4);
                    if (record.Elevation < toReturn.MinElevation) toReturn.MinElevation = record.Elevation;
                    if (record.Elevation > toReturn.MaxElevation) toReturn.MaxElevation = record.Elevation;
                    record.Pallete = bytesRead[curByte + 6];
                    record.X1 = bytesRead[curByte + 7];
                    record.X2 = bytesRead[curByte + 8];
                    lFileRecords.Add(record);
                    curByte += 9;
                }
            }
            else
            {
                toReturn.LRecordSizeBytes = 7;
                while (curByte < bytesToRead)
                {
                    var record = new TheaterDotLxFileRecord
                    {
                        TextureId = BitConverter.ToUInt16(bytesRead, curByte)
                    };
                    if (record.TextureId > maxTextureOffset) maxTextureOffset = record.TextureId;
                    if (record.TextureId < minTextureOffset) minTextureOffset = record.TextureId;
                    record.Elevation = BitConverter.ToUInt16(bytesRead, curByte + 2);
                    if (record.Elevation < toReturn.MinElevation) toReturn.MinElevation = record.Elevation;
                    if (record.Elevation > toReturn.MaxElevation) toReturn.MaxElevation = record.Elevation;
                    record.Pallete = bytesRead[curByte + 4];
                    record.X1 = bytesRead[curByte + 5];
                    record.X2 = bytesRead[curByte + 6];
                    lFileRecords.Add(record);
                    curByte += 7;
                }
            }

            toReturn.minTexOffset = minTextureOffset;
            toReturn.maxTexOffset = maxTextureOffset;
            toReturn.O = oFileRecords.ToArray();
            toReturn.L = lFileRecords.ToArray();
            oFileRecords.Clear();
            lFileRecords.Clear();
            GC.Collect();
            return toReturn;
        }
    }
}
