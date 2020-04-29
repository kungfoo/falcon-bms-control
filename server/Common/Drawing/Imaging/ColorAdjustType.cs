namespace Common.Drawing.Imaging
{
    /// <summary>Specifies which GDI+ objects use color adjustment information.</summary>
    public enum ColorAdjustType
    {
        /// <summary>
        ///     Color adjustment information that is used by all GDI+ objects that do not have their own color adjustment
        ///     information.
        /// </summary>
        Default,

        /// <summary>Color adjustment information for <see cref="T:Common.Drawing.Bitmap" /> objects.</summary>
        Bitmap,

        /// <summary>Color adjustment information for <see cref="T:Common.Drawing.Brush" /> objects.</summary>
        Brush,

        /// <summary>Color adjustment information for <see cref="T:Common.Drawing.Pen" /> objects.</summary>
        Pen,

        /// <summary>Color adjustment information for text.</summary>
        Text,

        /// <summary>The number of types specified.</summary>
        Count,

        /// <summary>The number of types specified.</summary>
        Any
    }
}