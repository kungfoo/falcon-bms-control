using System;
using Common.Drawing;
using System.IO;
using F4Utils.Terrain.Structs;

namespace F4Utils.Terrain
{
    internal interface ITheaterDotMapFileReader
    {
        TheaterDotMapFileInfo ReadTheaterDotMapFile(string theaterDotMapFilePath);
    }
    internal class TheaterDotMapFileReader:ITheaterDotMapFileReader
    {
        public TheaterDotMapFileInfo ReadTheaterDotMapFile(string theaterDotMapFilePath)
        {
            if (String.IsNullOrEmpty(theaterDotMapFilePath)) throw new ArgumentNullException("theaterMapFilePath");
            var fileInfo = new FileInfo(theaterDotMapFilePath);
            if (!fileInfo.Exists) throw new FileNotFoundException(theaterDotMapFilePath);

            var mapInfo = new TheaterDotMapFileInfo { Pallete = new Color[256], GreenPallete = new Color[256] };
            var bytesToRead = (int)fileInfo.Length;
            var bytesRead = new byte[bytesToRead];

            using (var stream = File.OpenRead(theaterDotMapFilePath))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(bytesRead, 0, bytesToRead);
                stream.Close();
            }
            mapInfo.FeetBetweenL0Posts = BitConverter.ToSingle(bytesRead, 0);
            mapInfo.MEAMapWidth = BitConverter.ToUInt32(bytesRead, 4);
            mapInfo.MEAMapHeight = BitConverter.ToUInt32(bytesRead, 8);
            mapInfo.FeetToMeaCellConversionFactor = BitConverter.ToSingle(bytesRead, 12);
            mapInfo.NumLODs = BitConverter.ToUInt32(bytesRead, 16);
            mapInfo.LastNearTiledLOD = BitConverter.ToUInt32(bytesRead, 20);
            mapInfo.LastFarTiledLOD = BitConverter.ToUInt32(bytesRead, 24);

            var pallete = new UInt32[256];
            for (var i = 0; i < pallete.Length; i++)
            {
                pallete[i] = BitConverter.ToUInt32(bytesRead, 28 + (i * 4));
            }
            for (var i = 0; i < mapInfo.Pallete.Length; i++)
            {
                var cR = (byte)(pallete[i] & 0xFF);
                var cG = (byte)((pallete[i] >> 8) & 0xFF);
                var cB = (byte)((pallete[i] >> 16) & 0xFF);
                var cA = (byte)((pallete[i] >> 24) & 0xFF);
                var thisColor = Color.FromArgb(cA, cR, cG, cB);
                mapInfo.Pallete[i] = thisColor;

                const byte gR = 0x00;
                var gG = (byte)((thisColor.R * 0.25f) + (thisColor.G * 0.5f) + (thisColor.B * 0.25f));
                const byte gB = 0x00;
                var gA = (byte)((pallete[i] >> 24) & 0xFF);
                var greenColor = Color.FromArgb(gA, gR, gG, gB);
                mapInfo.GreenPallete[i] = greenColor;
            }
            mapInfo.LODMapWidths = new UInt32[mapInfo.NumLODs];
            mapInfo.LODMapHeights = new UInt32[mapInfo.NumLODs];
            for (var i = 0; i < mapInfo.NumLODs; i++)
            {
                mapInfo.LODMapWidths[i] = BitConverter.ToUInt32(bytesRead, 1052 + (i * 8));
                mapInfo.LODMapHeights[i] = BitConverter.ToUInt32(bytesRead, 1056 + (i * 8));
            }
            mapInfo.flags = 0;
            mapInfo.baseLong = 119.1148778F;
            mapInfo.baseLat = 33.775918333F;
            if (bytesRead.Length >= (1056 + ((mapInfo.NumLODs - 1) * 8) + 4))
            {
                mapInfo.flags = BitConverter.ToUInt32(bytesRead, (int)(1056 + ((mapInfo.NumLODs - 1) * 8) + 4));
            }
            if (bytesRead.Length >= (1056 + ((mapInfo.NumLODs - 1) * 8) + 8))
            {
                mapInfo.baseLong = BitConverter.ToSingle(bytesRead, (int)(1056 + ((mapInfo.NumLODs - 1) * 8) + 8));
            }
            if (bytesRead.Length >= (1056 + ((mapInfo.NumLODs - 1) * 8) + 12))
            {
                mapInfo.baseLat = BitConverter.ToSingle(bytesRead, (int)(1056 + ((mapInfo.NumLODs - 1) * 8) + 12));
            }
            return mapInfo;
        }
    }
}
