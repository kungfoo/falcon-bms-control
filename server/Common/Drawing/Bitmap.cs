using System;
using System.IO;
using Common.Drawing.Imaging;

namespace Common.Drawing
{
    public class Bitmap : Image
    {
        public Bitmap(string filename)
        {
            WrappedBitmap = new System.Drawing.Bitmap(filename);
        }

        public Bitmap(string filename, bool useIcm)
        {
            WrappedBitmap = new System.Drawing.Bitmap(filename, useIcm);
        }

        public Bitmap(Type type, string resource)
        {
            WrappedBitmap = new System.Drawing.Bitmap(type, resource);
        }

        public Bitmap(Stream stream)
        {
            WrappedBitmap = new System.Drawing.Bitmap(stream);
        }

        public Bitmap(Stream stream, bool useIcm)
        {
            WrappedBitmap = new System.Drawing.Bitmap(stream, useIcm);
        }

        public Bitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0)
        {
            WrappedBitmap = new System.Drawing.Bitmap(width, height, stride,
                (System.Drawing.Imaging.PixelFormat) format, scan0);
        }

        public Bitmap(int width, int height, PixelFormat format)
        {
            WrappedBitmap = new System.Drawing.Bitmap(width, height, (System.Drawing.Imaging.PixelFormat) format);
        }

        public Bitmap(int width, int height)
        {
            WrappedBitmap = new System.Drawing.Bitmap(width, height);
        }

        public Bitmap(int width, int height, System.Drawing.Graphics g)
        {
            WrappedBitmap = new System.Drawing.Bitmap(width, height, g);
        }

        public Bitmap(Image original)
        {
            WrappedBitmap = new System.Drawing.Bitmap(original);
        }

        public Bitmap(Image original, int width, int height)
        {
            WrappedBitmap = new System.Drawing.Bitmap(original, width, height);
        }

        public Bitmap(Image original, Size newSize)
        {
            WrappedBitmap = new System.Drawing.Bitmap(original, newSize);
        }

        private Bitmap()
        {
        }

        private System.Drawing.Bitmap WrappedBitmap
        {
            get => WrappedImage as System.Drawing.Bitmap;
            set => WrappedImage = value;
        }

        public virtual Bitmap Clone(Rectangle rect, PixelFormat format)
        {
            return new Bitmap(WrappedBitmap.Clone(rect, (System.Drawing.Imaging.PixelFormat) format));
        }

        public virtual Bitmap Clone(RectangleF rect, PixelFormat format)
        {
            return new Bitmap(WrappedBitmap.Clone(rect, (System.Drawing.Imaging.PixelFormat) format));
        }

        public override object Clone()
        {
            return new Bitmap((System.Drawing.Bitmap) WrappedBitmap.Clone());
        }

        public override bool Equals(object obj)
        {
            return WrappedBitmap.Equals(obj);
        }

        public virtual Bitmap FromHicon(IntPtr hicon)
        {
            return new Bitmap(System.Drawing.Bitmap.FromHicon(hicon));
        }

        public virtual Bitmap FromResource(IntPtr hinstance, string bitmapName)
        {
            return new Bitmap(System.Drawing.Bitmap.FromResource(hinstance, bitmapName));
        }

        public override int GetHashCode()
        {
            return WrappedBitmap.GetHashCode();
        }

        public IntPtr GetHbitmap()
        {
            return WrappedBitmap.GetHbitmap();
        }

        public virtual IntPtr GetHbitmap(Color background)
        {
            return WrappedBitmap.GetHbitmap(background);
        }

        public IntPtr GetHicon()
        {
            return WrappedBitmap.GetHicon();
        }

        public virtual Color GetPixel(int x, int y)
        {
            return WrappedBitmap.GetPixel(x, y);
        }

        public virtual BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
        {
            return WrappedBitmap.LockBits(rect, (System.Drawing.Imaging.ImageLockMode) flags,
                (System.Drawing.Imaging.PixelFormat) format);
        }

        public virtual BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format,
            BitmapData bitmapData)
        {
            return WrappedBitmap.LockBits(rect, (System.Drawing.Imaging.ImageLockMode) flags,
                (System.Drawing.Imaging.PixelFormat) format, bitmapData);
        }

        public void MakeTransparent()
        {
            WrappedBitmap.MakeTransparent();
        }

        public virtual void MakeTransparent(Color transparentColor)
        {
            WrappedBitmap.MakeTransparent(transparentColor);
        }

        public virtual void SetPixel(int x, int y, Color color)
        {
            WrappedBitmap.SetPixel(x, y, color);
        }

        public virtual void SetResolution(float xDpi, float yDpi)
        {
            WrappedBitmap.SetResolution(xDpi, yDpi);
        }

        public override string ToString()
        {
            return WrappedBitmap.ToString();
        }

        public virtual void UnlockBits(BitmapData bitmapdata)
        {
            WrappedBitmap.UnlockBits(bitmapdata);
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Bitmap" /> to a <see cref="T:Common.Drawing.Bitmap" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Bitmap" /> that results from the conversion.</returns>
        /// <param name="bitmap">The <see cref="T:System.Drawing.Bitmap" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Bitmap(System.Drawing.Bitmap bitmap)
        {
            return bitmap == null ? null : new Bitmap(bitmap);
        }

        /// <summary>Converts the specified <see cref="T:Common.Drawing.Bitmap" /> to a <see cref="T:System.Drawing.Bitmap" />.</summary>
        /// <returns>The <see cref="T:System.Drawing.Bitmap" /> that results from the conversion.</returns>
        /// <param name="bitmap">The <see cref="T:Common.Drawing.Bitmap" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Bitmap(Bitmap bitmap)
        {
            return bitmap?.WrappedBitmap;
        }
    }
}