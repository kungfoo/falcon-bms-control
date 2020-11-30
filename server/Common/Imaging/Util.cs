using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Common.Drawing;
using Common.Drawing.Imaging;
using Common.Win32;
using log4net;

namespace Common.Imaging
{
    public static class Util
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Util));

        public static ColorMatrix GreyscaleColorMatrix => new ColorMatrix(new[]
        {
            new[] {0.33f, 0.33f, 0.33f, 0, 0},
            new[] {0.33f, 0.33f, 0.33f, 0, 0},
            new[] {0.33f, 0.33f, 0.33f, 0, 0},
            new float[] {0, 0, 0, 1, 0, 0},
            new float[] {0, 0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 0, 1}
        });

        public static Image BitmapFromBytes(byte[] bitmapBytes)
        {
            Image toReturn = null;
            if (bitmapBytes != null && bitmapBytes.Length > 0)
            {
                using (var ms = new MemoryStream(bitmapBytes))
                {
                    toReturn = Image.FromStream(ms);
                }
            }
            return toReturn;
        }

        public static byte[] BytesFromBitmap(Image image, string compressionType, string imageFormat)
        {
            byte[] toReturn = null;
            if (image == null) return null;
            try
            {
                var x = image.Width;
            }
            catch (Exception e)
            {
                Log.Debug(e.Message, e);
                return null;
            }
            using (var ms = new MemoryStream())
            {
                int encoderValue;
                string codecMimeType;
                switch (compressionType)
                {
                    case "LZW":
                        encoderValue = (int) EncoderValue.CompressionLZW;
                        break;
                    case "RLE":
                        encoderValue = (int) EncoderValue.CompressionRle;
                        break;
                    default:
                        encoderValue = (int) EncoderValue.CompressionNone;
                        break;
                }
                switch (imageFormat)
                {
                    case "BMP":
                        codecMimeType = "image/bmp";
                        break;
                    case "GIF":
                        codecMimeType = "image/gif";
                        break;
                    case "JPEG":
                        codecMimeType = "image/jpeg";
                        break;
                    case "PNG":
                        codecMimeType = "image/png";
                        break;
                    default:
                        codecMimeType = "image/tiff";
                        break;
                }

                var encoder = Encoder.Compression;
                var codecParams = new EncoderParameters(1);
                var codecs = ImageCodecInfo.GetImageEncoders();
                using (var encoderParam = new EncoderParameter(encoder, encoderValue))
                {
                    codecParams.Param = new[] {encoderParam};
                    var codecToUse = codecs.FirstOrDefault(codec => codec.MimeType == codecMimeType);
                    try
                    {
                        image.Save(ms, codecToUse, codecParams);
                        toReturn = ms.ToArray();
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e.Message, e);
                    }
                }
            }
            return toReturn;
        }

        public static Image CloneBitmap(Image image)
        {
            if (image == null)
            {
                return null;
            }
            Image toReturn;
            try
            {
                toReturn = (Image) image.Clone();
            }
            catch (Exception e)
            {
                Log.Debug(e.Message, e);
                toReturn = null;
            }
            return toReturn;
        }

        /// <summary>
        ///     Given an Image object, returns a greyscale equivalent Image object
        /// </summary>
        /// <param name="source">a Bitmap object to conver to greyscale</param>
        /// <returns>an Image object based on the input Bitmap, but converted to greyscale</returns>
        public static Image ConvertImageToGreyscale(Bitmap source)
        {
            var bitmap = new Bitmap(source.Width, source.Height);
            for (var y = 0; y < bitmap.Height; y++)
            for (var x = 0; x < bitmap.Width; x++)
            {
                var c = source.GetPixel(x, y);
                var luma = (int) (c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                bitmap.SetPixel(x, y, Color.FromArgb(luma, luma, luma));
            }
            return bitmap;
        }

        /// <summary>
        ///     Convert's a <see cref="Bitmap" /> to a different pixel format
        /// </summary>
        /// <param name="img">a <see cref="Bitmap" /> Bitmap to convert</param>
        /// <param name="format"></param>
        public static void ConvertPixelFormat(ref Image img, PixelFormat format)
        {
            if (img == null) return;
            var areSame = false;
            try
            {
                areSame |= format == img.PixelFormat;
            }
            catch (Exception e)
            {
                Log.Debug(e.Message, e);
            }
            if (areSame) return;

            var originalImg = img;
            Image converted = null;
            var success = false;
            try
            {
                converted = new Bitmap(img.Width, img.Height, format);
                Graphics graphics;
                using (graphics = Graphics.FromImage(converted))
                using (var imageAttributes = new ImageAttributes())
                {
                    graphics.DrawImageFast(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, converted.Width,
                        converted.Height, GraphicsUnit.Pixel, imageAttributes);
                }
                Interlocked.Exchange(ref img, converted);
                success = true;
            }
            catch (Exception e)
            {
                Log.Debug(e.Message, e);
                Common.Util.DisposeObject(converted);
            }
            finally
            {
                if (success)
                {
                    Common.Util.DisposeObject(originalImg);
                }
            }
        }

        public static Image CopyBitmap(Image toCopy)
        {
            if (toCopy == null) return null;
            Image toReturn = new Bitmap(toCopy.Width, toCopy.Height, toCopy.PixelFormat);
            using (var g = Graphics.FromImage(toReturn))
            {
                g.DrawImageUnscaled(toCopy, 0, 0, toCopy.Width, toCopy.Height);
            }
            return toReturn;
        }

        /// <summary>
        ///     Copies a bitmap into a 1bpp/8bpp bitmap of the same dimensions, fast
        /// </summary>
        /// <param name="b">original bitmap</param>
        /// <param name="bpp">1 or 8, target bpp</param>
        /// <returns>a 1bpp copy of the bitmap</returns>
        public static Image CopyToBpp(Bitmap b, int bpp)
        {
            if (bpp != 1 && bpp != 8) throw new ArgumentException("1 or 8", nameof(bpp));
            var w = b.Width;
            var h = b.Height;
            var hbm = b.GetHbitmap();
            var bmi = new NativeMethods.BITMAPINFO
            {
                biSize = 40,
                biWidth = w,
                biHeight = h,
                biPlanes = 1,
                biBitCount = (short) bpp,
                biCompression = NativeMethods.BI_RGB,
                biSizeBitmap = (uint) (((w + 7) & 0xFFFFFFF8) * h / 8),
                biXPelsPerMeter = 1000000,
                biYPelsPerMeter = 1000000
            };
            var ncols = (uint) 1 << bpp;
            bmi.biClrUsed = ncols;
            bmi.biClrImportant = ncols;
            bmi.cols = new uint[256];
            if (bpp == 1)
            {
                bmi.cols[0] = MAKERGB(0, 0, 0);
                bmi.cols[1] = MAKERGB(255, 255, 255);
            }
            else
            {
                for (var i = 0; i < ncols; i++) bmi.cols[i] = MAKERGB(i, i, i);
            }
            var hbm0 = NativeMethods.CreateDIBSection(IntPtr.Zero, ref bmi, NativeMethods.DIB_RGB_COLORS, out var bits0,
                IntPtr.Zero, 0);
            var sdc = NativeMethods.GetDC(IntPtr.Zero);
            var hdc = NativeMethods.CreateCompatibleDC(sdc);
            NativeMethods.SelectObject(hdc, hbm);
            var hdc0 = NativeMethods.CreateCompatibleDC(sdc);
            NativeMethods.SelectObject(hdc0, hbm0);
            NativeMethods.BitBlt(hdc0, 0, 0, w, h, hdc, 0, 0, NativeMethods.SRCCOPY);
            var b0 = Image.FromHbitmap(hbm0);
            NativeMethods.DeleteDC(hdc);
            NativeMethods.DeleteDC(hdc0);
            NativeMethods.ReleaseDC(IntPtr.Zero, sdc);
            NativeMethods.DeleteObject(hbm);
            NativeMethods.DeleteObject(hbm0);
            return b0;
        }

        public static Image CropBitmap(Image img, Rectangle cropArea)
        {
            Image bmpCrop = ((Bitmap) img).Clone(cropArea,
                img.PixelFormat);
            return bmpCrop;
        }

        public static Bitmap CropToContent(Bitmap bitmap)
        {
            Image asImage = bitmap;
            ConvertPixelFormat(ref asImage, PixelFormat.Format32bppPArgb);
            bitmap = (Bitmap) asImage;

            var minXNonBlackPixel = 0;
            var maxXNonBlackPixel = 0;
            var minYNonBlackPixel = 0;
            var maxYNonBlackPixel = 0;
            BitmapData sourceImageLock = null;
            try
            {
                sourceImageLock = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly, bitmap.PixelFormat);
                var bytes = new byte[bitmap.Width * bitmap.Height * 4];
                Marshal.Copy(sourceImageLock.Scan0, bytes, 0, bytes.Length);
                for (var y = 0; y < bitmap.Height; y++)
                for (var x = 0; x < bitmap.Width; x++)
                    if
                    (
                        bytes[y * bitmap.Width * 4 + x * 4] != 0
                        ||
                        bytes[y * bitmap.Width * 4 + x * 4 + 1] != 0
                        ||
                        bytes[y * bitmap.Width * 4 + x * 4 + 2] != 0
                    )
                    {
                        if (minXNonBlackPixel == 0) minXNonBlackPixel = x;
                        if (x > maxXNonBlackPixel) maxXNonBlackPixel = x;
                        if (minYNonBlackPixel == 0) minYNonBlackPixel = y;
                        if (y > maxYNonBlackPixel) maxYNonBlackPixel = y;
                    }
            }
            finally
            {
                if (sourceImageLock != null)
                {
                    bitmap.UnlockBits(sourceImageLock);
                }
            }
            var cropRectangle = new Rectangle(minXNonBlackPixel, minYNonBlackPixel,
                maxXNonBlackPixel - minXNonBlackPixel,
                maxYNonBlackPixel - minYNonBlackPixel);
            var toReturn = new Bitmap(cropRectangle.Width, cropRectangle.Height, bitmap.PixelFormat);
            using (var g = Graphics.FromImage(toReturn))
            {
                g.DrawImageFast(bitmap, new Rectangle(0, 0, cropRectangle.Width, cropRectangle.Height), cropRectangle,
                    GraphicsUnit.Pixel);
            }
            return toReturn;
        }

        public static void CropToContentAndDisposeOriginal(ref Bitmap image)
        {
            var croppped = CropToContent(image);
            Common.Util.DisposeObject(image);
            image = croppped;
        }

        public static Image GetDimmerImage(Image toProcess, float percentLuminanceRetained)
        {
            if (toProcess == null) return null;
            Image toReturn = new Bitmap(toProcess.Width, toProcess.Height);
            var ia = new ImageAttributes();
            var cm = GetDimmingColorMatrix(percentLuminanceRetained);
            ia.SetColorMatrix(cm);

            using (var g = Graphics.FromImage(toReturn))
            {
                g.DrawImageFast(toProcess, new Rectangle(0, 0, toProcess.Width, toProcess.Height), 0, 0,
                    toProcess.Width,
                    toProcess.Height, GraphicsUnit.Pixel, ia);
            }
            return toReturn;
        }

        public static Image GetDimmerImage(Image toProcess)
        {
            if (toProcess == null) return null;
            Image toReturn = new Bitmap(toProcess.Width, toProcess.Height);
            var ia = new ImageAttributes();
            var cm = GetDimmingColorMatrix(0.8f);
            ia.SetColorMatrix(cm);
            using (var g = Graphics.FromImage(toReturn))
            {
                g.DrawImageFast(toProcess, new Rectangle(0, 0, toProcess.Width, toProcess.Height), 0, 0,
                    toProcess.Width,
                    toProcess.Height, GraphicsUnit.Pixel, ia);
            }
            return toReturn;
        }

        public static ColorMatrix GetDimmingColorMatrix(float percentLuminanceRetained)
        {
            var cm = new ColorMatrix();
            cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = percentLuminanceRetained;
            return cm;
        }

        public static Image GetNegativeImage(Image toProcess)
        {
            if (toProcess == null) return null;
            Image toReturn = new Bitmap(toProcess.Width, toProcess.Height);
            var ia = new ImageAttributes();
            var cm = new ColorMatrix();
            cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = -1;
            ia.SetColorMatrix(cm);
            using (var g = Graphics.FromImage(toReturn))
            {
                g.DrawImageFast(toProcess, new Rectangle(0, 0, toProcess.Width, toProcess.Height), 0, 0,
                    toProcess.Width,
                    toProcess.Height, GraphicsUnit.Pixel, ia);
            }
            return toReturn;
        }

        public static ColorMatrix GetNVISColorMatrix(int brightnessLevel, int maxBrightnessLevel)
        {
            var cm = new ColorMatrix
            (
                new[]
                {
                    new float[] {0, 0, 0, 0, 0}, //red %
                    new[]
                    {
                        0,
                        brightnessLevel / (float) maxBrightnessLevel,
                        0, 0, 0
                    }, //green
                    new float[] {0, 0, 0, 0, 0}, //blue %
                    new float[] {0, 0, 0, 1, 0}, //alpha %
                    new float[] {-1, 0, -1, 0, 1} //add
                }
            );
            return cm;
        }

        public static Icon IconFromBitmap(Bitmap bitmap)
        {
            if (bitmap == null) return null;

            var toIconify = bitmap;
            Icon toReturn;
            var hIcon = toIconify.GetHicon();
            var temp = Icon.FromHandle(hIcon);
            using (var ms = new MemoryStream())
            {
                temp.Save(ms);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                toReturn = new Icon(ms);
            }
            NativeMethods.DestroyIcon(hIcon);
            return toReturn;
        }

        public static void InvertImage(ref Image bitmap)
        {
            var inverted = GetNegativeImage(bitmap);
            Common.Util.DisposeObject(bitmap);
            bitmap = inverted;
        }

        public static Image LoadBitmapFromFile(string filename)
        {
            var temp = Image.FromFile(filename);
            ConvertPixelFormat(ref temp, PixelFormat.Format32bppPArgb);
            return temp;
        }

        public static Image ResizeBitmap(Image imgToResize, Size size)
        {
            var sourceWidth = imgToResize.Width;
            var sourceHeight = imgToResize.Height;

            var nPercentW = size.Width / (float) sourceWidth;
            var nPercentH = size.Height / (float) sourceHeight;

            var nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            var destWidth = (int) (sourceWidth * nPercent);
            var destHeight = (int) (sourceHeight * nPercent);

            var bitmap = new Bitmap(destWidth, destHeight, imgToResize.PixelFormat);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImageFast(imgToResize, 0, 0, destWidth, destHeight);
            }

            return bitmap;
        }

        public static Image RotateBitmap(Image image, float angle)
        {
            angle = -angle;
            //create a new empty bitmap to hold rotated Bitmap
            Image returnBitmap = new Bitmap(System.Math.Max(image.Width, image.Height),
                System.Math.Max(image.Width, image.Height), image.PixelFormat);
            //make a graphics object from the empty bitmap
            var g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of Bitmap
            g.TranslateTransform((float) image.Width / 2, (float) image.Height / 2);
            //rotate
            g.RotateTransform(angle);
            //move Bitmap back
            g.TranslateTransform(-(float) image.Width / 2, -(float) image.Height / 2);
            //draw passed in Bitmap onto graphics object
            g.DrawImageFast(image, new Point(0, 0));
            return returnBitmap;
        }

        private static uint MAKERGB(int r, int g, int b)
        {
            return (uint) (b & 255) | (uint) ((g & 255) << 8) | (uint) ((r & 255) << 16);
        }
    }
}