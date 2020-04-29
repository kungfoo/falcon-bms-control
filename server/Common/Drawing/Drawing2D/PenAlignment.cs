namespace Common.Drawing.Drawing2D
{
    /// <summary>
    ///     Specifies the alignment of a <see cref="T:Common.Drawing.Pen" /> object in relation to the theoretical,
    ///     zero-width line.
    /// </summary>
    public enum PenAlignment
    {
        /// <summary>Specifies that the <see cref="T:Common.Drawing.Pen" /> object is centered over the theoretical line.</summary>
        Center,

        /// <summary>Specifies that the <see cref="T:Common.Drawing.Pen" /> is positioned on the inside of the theoretical line.</summary>
        Inset,

        /// <summary>Specifies the <see cref="T:Common.Drawing.Pen" /> is positioned on the outside of the theoretical line.</summary>
        Outset,

        /// <summary>Specifies the <see cref="T:Common.Drawing.Pen" /> is positioned to the left of the theoretical line.</summary>
        Left,

        /// <summary>Specifies the <see cref="T:Common.Drawing.Pen" /> is positioned to the right of the theoretical line.</summary>
        Right
    }
}