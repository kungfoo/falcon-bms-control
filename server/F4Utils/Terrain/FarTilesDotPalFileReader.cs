using System;
using Common.Drawing;
using System.IO;
using F4Utils.Terrain.Structs;

namespace F4Utils.Terrain
{
    internal interface IFarTilesDotPalFileReader
    {
        FarTilesDotPalFileInfo ReadFarTilesDotPalFile(string farTilesDotPalFilePath);
    }
    internal class FarTilesDotPalFileReader:IFarTilesDotPalFileReader
    {
        public FarTilesDotPalFileInfo ReadFarTilesDotPalFile(string farTilesDotPalFilePath)
        {
            if (String.IsNullOrEmpty(farTilesDotPalFilePath)) throw new ArgumentNullException("farTilesPalletePath");
            var fileInfo = new FileInfo(farTilesDotPalFilePath);
            if (!fileInfo.Exists) throw new FileNotFoundException(farTilesDotPalFilePath);

            var bytesToRead = (int)fileInfo.Length;
            var bytesRead = new byte[bytesToRead];

            using (var stream = File.OpenRead(farTilesDotPalFilePath))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(bytesRead, 0, bytesToRead);
                stream.Close();
            }
            var pallete = new Color[256];
            var curByte = 0;
            for (var i = 0; i < pallete.Length; i++)
            {
                var thisPalleteEntry = BitConverter.ToUInt32(bytesRead, curByte);

                var cR = (byte)(thisPalleteEntry & 0xFF);
                var cG = (byte)((thisPalleteEntry >> 8) & 0xFF);
                var cB = (byte)((thisPalleteEntry >> 16) & 0xFF);
                //byte cA = (byte)((thisPalleteEntry >> 24) & 0xFF);
                const byte cA = 0xFF;
                var thisColor = Color.FromArgb(cA, cR, cG, cB);
                pallete[i] = thisColor;

                curByte += 4;
            }
            uint texCount = 0;
            uint tilesAtLod = 0;
            do
            {
                texCount += tilesAtLod;
                tilesAtLod = BitConverter.ToUInt32(bytesRead, curByte);
                curByte += 4;
            } while (curByte < bytesToRead);
            var toReturn = new FarTilesDotPalFileInfo { numTextures = texCount, pallete = pallete };
            return toReturn;
        }
    }
}
