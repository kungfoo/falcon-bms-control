using System;
using Common.Drawing;
using Common.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace F4Utils.Campaign.Save
{
    public class PakFile
    {
        public Bitmap PakMap { get; private set; }
        public byte[] MapData { get; private set; }
        public PakFile(string fileName)
        {
            LoadPakFile(fileName);
        }
        private void LoadPakFile(string fileName)
        {
            //reads PAK file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                MapData = new byte[stream.Length];
                stream.Read(MapData, 0, (int)stream.Length);
             }
            var dims = (int)Math.Sqrt(MapData.Length);
            Common.Drawing.Bitmap mapImage = new Common.Drawing.Bitmap(width:dims, height:dims, format: PixelFormat.Format8bppIndexed);
            var bitmapData = mapImage.LockBits(new Common.Drawing.Rectangle(0, 0, dims, dims), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            Marshal.Copy(MapData, 0, bitmapData.Scan0, MapData.Length);
            mapImage.UnlockBits(bitmapData);
            PakMap = mapImage;
        }
        public void Save(string fileName)
        {
            //writes PAK file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                stream.Write(MapData, 0, (int)MapData.Length);
            }
        }
    }
}
