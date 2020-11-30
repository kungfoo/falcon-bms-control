using System.IO;
using Common.Drawing;
using FreeImageAPI;

namespace Common.Imaging
{
    public static class DDS
    {
        public static Bitmap GetBitmapFromDDSFileBytes(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return GetBitmapFromDDSFileStream(ms);
            }
        }

        public static Bitmap GetBitmapFromDDSFileStream(Stream s)
        {
            var format = FREE_IMAGE_FORMAT.FIF_DDS;
            var fBitmap = FreeImage.LoadFromStream(s, FREE_IMAGE_LOAD_FLAGS.DEFAULT, ref format);
            var toReturn = FreeImage.GetBitmap(fBitmap);
            FreeImage.Unload(fBitmap);
            return toReturn;
        }

        public static Bitmap Load(string filePath)
        {
            var fBitmap = FreeImage.Load(FREE_IMAGE_FORMAT.FIF_DDS, filePath, FREE_IMAGE_LOAD_FLAGS.DEFAULT);
            var toReturn = FreeImage.GetBitmap(fBitmap);
            FreeImage.Unload(fBitmap);
            return toReturn;
        }
    }
}