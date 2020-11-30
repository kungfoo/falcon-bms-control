using System;
using Common.Drawing.Drawing2D;
using Common.Drawing.Text;

namespace Common.UI
{
    [Serializable]
    public class GdiPlusOptions
    {
        public GdiPlusOptions()
        {
            InterpolationMode = InterpolationMode.Default;
            SmoothingMode = SmoothingMode.Default;
            PixelOffsetMode = PixelOffsetMode.Default;
            TextRenderingHint = TextRenderingHint.SystemDefault;
            CompositingQuality = CompositingQuality.Default;
        }

        public CompositingQuality CompositingQuality { get; set; }

        public InterpolationMode InterpolationMode { get; set; }
        public PixelOffsetMode PixelOffsetMode { get; set; }
        public SmoothingMode SmoothingMode { get; set; }
        public TextRenderingHint TextRenderingHint { get; set; }
    }
}