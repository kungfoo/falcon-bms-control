using System;
using Common.Drawing;
using Common.Drawing.Drawing2D;
using Common.Drawing.Imaging;
using Common.Drawing.Text;

namespace Common.Imaging
{
    public static class GraphicsExtensions
    {
        private static ImageRenderingOptions Fastest => new ImageRenderingOptions
        {
            CompositingMode = CompositingMode.SourceOver,
            CompositingQuality = CompositingQuality.HighSpeed,
            InterpolationMode = InterpolationMode.Default,
            SmoothingMode = SmoothingMode.None,
            PixelOffsetMode = PixelOffsetMode.HighSpeed,
            TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        };

        private static ImageRenderingOptions FastestForText => new ImageRenderingOptions
        {
            CompositingMode = CompositingMode.SourceOver,
            CompositingQuality = CompositingQuality.Default,
            InterpolationMode = InterpolationMode.Default,
            SmoothingMode = SmoothingMode.AntiAlias,
            PixelOffsetMode = PixelOffsetMode.Default,
            TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        };

        private static ImageRenderingOptions FastImageRenderingOptions => Fastest;

        public static void DrawImageFast(this Graphics g, Image image, Point point)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, point));
        }

        public static void DrawImageFast(this Graphics g, Image image, Point[] destPoints)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints));
        }

        public static void DrawImageFast(this Graphics g, Image image, PointF point)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, point));
        }

        public static void DrawImageFast(this Graphics g, Image image, PointF[] destPoints)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle rect)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, rect));
        }

        public static void DrawImageFast(this Graphics g, Image image, RectangleF rect)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, rect));
        }

        public static void DrawImageFast(this Graphics g, Image image, float x, float y)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, x, y));
        }

        public static void DrawImageFast(this Graphics g, Image image, Point[] destPoints, Rectangle srcRect,
            GraphicsUnit srcUnit)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints, srcRect, srcUnit));
        }

        public static void DrawImageFast(this Graphics g, Image image, PointF[] destPoints, RectangleF srcRect,
            GraphicsUnit srcUnit)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints, srcRect, srcUnit));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle destRect, Rectangle srcRect,
            GraphicsUnit srcUnit)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcRect, srcUnit));
        }

        public static void DrawImageFast(this Graphics g, Image image, RectangleF destRect, RectangleF srcRect,
            GraphicsUnit srcUnit)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcRect, srcUnit));
        }

        public static void DrawImageFast(this Graphics g, Image image, float x, float y, float width, float height)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, x, y, width, height));
        }

        public static void DrawImageFast(this Graphics g, Image image, float x, float y, RectangleF srcRect,
            GraphicsUnit srcUnit)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, x, y, srcRect, srcUnit));
        }

        public static void DrawImageFast(this Graphics g, Image image, int x, int y, int width, int height)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, x, y, width, height));
        }

        public static void DrawImageFast(this Graphics g, Image image, int x, int y, Rectangle srcRect,
            GraphicsUnit srcUnit)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, x, y, srcRect, srcUnit));
        }

        public static void DrawImageFast(this Graphics g, Image image, Point[] destPoints, Rectangle srcRect,
            GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr));
        }

        public static void DrawImageFast(this Graphics g, Image image, PointF[] destPoints, RectangleF srcRect,
            GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr));
        }

        public static void DrawImageFast(this Graphics g, Image image, Point[] destPoints, Rectangle srcRect,
            GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback));
        }

        public static void DrawImageFast(this Graphics g, Image image, PointF[] destPoints, RectangleF srcRect,
            GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback));
        }

        public static void DrawImageFast(this Graphics g, Image image, Point[] destPoints, Rectangle srcRect,
            GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, callbackData));
        }

        public static void DrawImageFast(this Graphics g, Image image, PointF[] destPoints, RectangleF srcRect,
            GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, callbackData));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle destRect, float srcX, float srcY,
            float srcWidth, float srcHeight, GraphicsUnit srcUnit)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle destRect, int srcX, int srcY,
            int srcWidth, int srcHeight, GraphicsUnit srcUnit)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle destRect, float srcX, float srcY,
            float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle destRect, int srcX, int srcY,
            int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle destRect, float srcX, float srcY,
            float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs,
            Graphics.DrawImageAbort callback)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle destRect, int srcX, int srcY,
            int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr,
            Graphics.DrawImageAbort callback)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr, callback));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle destRect, float srcX, float srcY,
            float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs,
            Graphics.DrawImageAbort callback, IntPtr callbackData)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback,
                    callbackData));
        }

        public static void DrawImageFast(this Graphics g, Image image, Rectangle destRect, int srcX, int srcY,
            int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs,
            Graphics.DrawImageAbort callback, IntPtr callbackData)
        {
            SetImageOptionsRunMethodAndRevert(g, FastImageRenderingOptions, () =>
                g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback,
                    callbackData));
        }

        public static void DrawLineFast(this Graphics g, Pen pen, Point pt1, Point pt2)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawLine(pen, pt1, pt2));
        }

        public static void DrawLineFast(this Graphics g, Pen pen, PointF pt1, PointF pt2)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawLine(pen, pt1, pt2));
        }

        public static void DrawLineFast(this Graphics g, Pen pen, float x1, float y1, float x2, float y2)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawLine(pen, x1, y1, x2, y2));
        }

        public static void DrawLineFast(this Graphics g, Pen pen, int x1, int y1, int x2, int y2)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawLine(pen, x1, y1, x2, y2));
        }

        public static void DrawPathFast(this Graphics g, Pen pen, GraphicsPath path)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawPath(pen, path));
        }

        public static void DrawRectangleFast(this Graphics g, Pen pen, Rectangle rect)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawRectangle(pen, rect));
        }

        public static void DrawRectangleFast(this Graphics g, Pen pen, float x, float y, float width, float height)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawRectangle(pen, x, y, width, height));
        }

        public static void DrawRectangleFast(this Graphics g, Pen pen, int x, int y, int width, int height)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawRectangle(pen, x, y, width, height));
        }

        public static void DrawStringFast(this Graphics g, string s, Font font, Brush brush, PointF point)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawString(s, font, brush, point));
        }

        public static void DrawStringFast(this Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawString(s, font, brush, layoutRectangle));
        }

        public static void DrawStringFast(this Graphics g, string s, Font font, Brush brush, float x, float y)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawString(s, font, brush, x, y));
        }

        public static void DrawStringFast(this Graphics g, string s, Font font, Brush brush, PointF point,
            StringFormat format)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawString(s, font, brush, point, format));
        }

        public static void DrawStringFast(this Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle,
            StringFormat format)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawString(s, font, brush, layoutRectangle, format));
        }

        public static void DrawStringFast(this Graphics g, string s, Font font, Brush brush, float x, float y,
            StringFormat format)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.DrawString(s, font, brush, x, y, format));
        }

        public static void FillPathFast(this Graphics g, Brush brush, GraphicsPath path)
        {
            SetImageOptionsRunMethodAndRevert(g, FastestForText, () =>
                g.FillPath(brush, path));
        }

        private static ImageRenderingOptions GetImageRenderingOptions(Graphics g)
        {
            return new ImageRenderingOptions
            {
                InterpolationMode = g.InterpolationMode,
                SmoothingMode = g.SmoothingMode,
                PixelOffsetMode = g.PixelOffsetMode,
                CompositingQuality = g.CompositingQuality,
                TextRenderingHint = g.TextRenderingHint,
                CompositingMode = g.CompositingMode
            };
        }

        private static void SetImageOptionsRunMethodAndRevert(Graphics g,
            ImageRenderingOptions newImageRenderingOptions, Action methodToRun)
        {
            var originalRenderingOptions = GetImageRenderingOptions(g);
            SetImageRenderingOptions(g, newImageRenderingOptions);
            methodToRun?.Invoke();
            SetImageRenderingOptions(g, originalRenderingOptions);
        }

        private static void SetImageRenderingOptions(Graphics g, ImageRenderingOptions imageRenderingOptions)
        {
            g.InterpolationMode = imageRenderingOptions.InterpolationMode;
            g.SmoothingMode = imageRenderingOptions.SmoothingMode;
            g.PixelOffsetMode = imageRenderingOptions.PixelOffsetMode;
            g.CompositingQuality = imageRenderingOptions.CompositingQuality;
            g.TextRenderingHint = imageRenderingOptions.TextRenderingHint;
            g.CompositingMode = imageRenderingOptions.CompositingMode;
        }

        private struct ImageRenderingOptions
        {
            public InterpolationMode InterpolationMode { get; set; }
            public SmoothingMode SmoothingMode { get; set; }
            public PixelOffsetMode PixelOffsetMode { get; set; }
            public CompositingQuality CompositingQuality { get; set; }
            public TextRenderingHint TextRenderingHint { get; set; }
            public CompositingMode CompositingMode { get; set; }
        }
    }
}