using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System;

namespace Common.UI
{
    [Serializable]
    public class GDIPlusOptions
    {
        public GDIPlusOptions()
            : base()
        {
            this.InterpolationMode = InterpolationMode.Default;
            this.SmoothingMode = SmoothingMode.Default;
            this.PixelOffsetMode = PixelOffsetMode.Default;
            this.TextRenderingHint = TextRenderingHint.SystemDefault;
            this.CompositingQuality = CompositingQuality.Default;
        }
        public InterpolationMode InterpolationMode
        {
            get;
            set;
        }
        public SmoothingMode SmoothingMode
        {
            get;
            set;
        }
        public PixelOffsetMode PixelOffsetMode
        {
            get;
            set;
        }
        public TextRenderingHint TextRenderingHint
        {
            get;
            set;
        }
        public CompositingQuality CompositingQuality
        {
            get;
            set;
        }
    }
}
