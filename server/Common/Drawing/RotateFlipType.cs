namespace Common.Drawing
{
    /// <summary>Specifies how much an image is rotated and the axis used to flip the image.</summary>
    /// <filterpriority>2</filterpriority>
    public enum RotateFlipType
    {
        /// <summary>Specifies no clockwise rotation and no flipping.</summary>
        RotateNoneFlipNone,

        /// <summary>Specifies a 90-degree clockwise rotation without flipping.</summary>
        Rotate90FlipNone,

        /// <summary>Specifies a 180-degree clockwise rotation without flipping.</summary>
        Rotate180FlipNone,

        /// <summary>Specifies a 270-degree clockwise rotation without flipping.</summary>
        Rotate270FlipNone,

        /// <summary>Specifies no clockwise rotation followed by a horizontal flip.</summary>
        RotateNoneFlipX,

        /// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal flip.</summary>
        Rotate90FlipX,

        /// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal flip.</summary>
        Rotate180FlipX,

        /// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal flip.</summary>
        Rotate270FlipX,

        /// <summary>Specifies no clockwise rotation followed by a vertical flip.</summary>
        RotateNoneFlipY = 6,

        /// <summary>Specifies a 90-degree clockwise rotation followed by a vertical flip.</summary>
        Rotate90FlipY,

        /// <summary>Specifies a 180-degree clockwise rotation followed by a vertical flip.</summary>
        Rotate180FlipY = 4,

        /// <summary>Specifies a 270-degree clockwise rotation followed by a vertical flip.</summary>
        Rotate270FlipY,

        /// <summary>Specifies no clockwise rotation followed by a horizontal and vertical flip.</summary>
        RotateNoneFlipXY = 2,

        /// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
        Rotate90FlipXY,

        /// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
        Rotate180FlipXY = 0,

        /// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
        Rotate270FlipXY
    }
}