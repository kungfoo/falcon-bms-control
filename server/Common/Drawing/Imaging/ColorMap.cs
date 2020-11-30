namespace Common.Drawing.Imaging
{
    /// <summary>
    ///     Defines a map for converting colors. Several methods of the
    ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color-remap table,
    ///     which is an array of <see cref="T:Common.Drawing.Imaging.ColorMap" /> structures. Not inheritable.
    /// </summary>
    public sealed class ColorMap
    {
        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.ColorMap" /> class.</summary>
        public ColorMap()
        {
            WrappedColorMap = new System.Drawing.Imaging.ColorMap();
        }

        private ColorMap(System.Drawing.Imaging.ColorMap colorMap)
        {
            WrappedColorMap = colorMap;
        }

        /// <summary>Gets or sets the new <see cref="T:Common.Drawing.Color" /> structure to which to convert.</summary>
        /// <returns>The new <see cref="T:Common.Drawing.Color" /> structure to which to convert.</returns>
        public Color NewColor
        {
            get => WrappedColorMap.NewColor;
            set => WrappedColorMap.NewColor = value;
        }

        /// <summary>Gets or sets the existing <see cref="T:Common.Drawing.Color" /> structure to be converted.</summary>
        /// <returns>The existing <see cref="T:Common.Drawing.Color" /> structure to be converted.</returns>
        public Color OldColor
        {
            get => WrappedColorMap.OldColor;
            set => WrappedColorMap.OldColor = value;
        }

        private System.Drawing.Imaging.ColorMap WrappedColorMap { get; }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.ColorMap" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.ColorMap" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.ColorMap" /> that results from the conversion.</returns>
        /// <param name="colorMap">The <see cref="T:System.Drawing.Imaging.ColorMap" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator ColorMap(System.Drawing.Imaging.ColorMap colorMap)
        {
            return colorMap == null ? null : new ColorMap(colorMap);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.ColorMap" /> to a
        ///     <see cref="T:System.Drawing.Imaging.ColorMap" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.ColorMap" /> that results from the conversion.</returns>
        /// <param name="colorMap">The <see cref="T:Common.Drawing.Imaging.ColorMap" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.ColorMap(ColorMap colorMap)
        {
            return colorMap?.WrappedColorMap;
        }
    }
}