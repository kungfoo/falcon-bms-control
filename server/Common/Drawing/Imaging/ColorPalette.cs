using System.Linq;

namespace Common.Drawing.Imaging
{
    /// <summary>Defines an array of colors that make up a color palette. The colors are 32-bit ARGB colors. Not inheritable.</summary>
    public sealed class ColorPalette
    {
        private ColorPalette(System.Drawing.Imaging.ColorPalette colorPalette)
        {
            WrappedColorPalette = colorPalette;
        }

        /// <summary>Gets an array of <see cref="T:Common.Drawing.Color" /> structures.</summary>
        /// <returns>
        ///     The array of <see cref="T:Common.Drawing.Color" /> structure that make up this
        ///     <see cref="T:Common.Drawing.Imaging.ColorPalette" />.
        /// </returns>
        public Color[] Entries => WrappedColorPalette.Entries.Convert<Color>().ToArray();

        /// <summary>Gets a value that specifies how to interpret the color information in the array of colors.</summary>
        /// <returns>
        ///     The following flag values are valid: 0x00000001The color values in the array contain alpha information.
        ///     0x00000002The colors in the array are grayscale values. 0x00000004The colors in the array are halftone values.
        /// </returns>
        public int Flags => WrappedColorPalette.Flags;

        private System.Drawing.Imaging.ColorPalette WrappedColorPalette { get; }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.ColorPalette" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.ColorPalette" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.ColorPalette" /> that results from the conversion.</returns>
        /// <param name="colorPalette">The <see cref="T:System.Drawing.Imaging.ColorPalette" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator ColorPalette(System.Drawing.Imaging.ColorPalette colorPalette)
        {
            return colorPalette == null ? null : new ColorPalette(colorPalette);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.ColorPalette" /> to a
        ///     <see cref="T:System.Drawing.Imaging.ColorPalette" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.ColorPalette" /> that results from the conversion.</returns>
        /// <param name="colorPalette">The <see cref="T:Common.Drawing.Imaging.ColorPalette" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.ColorPalette(ColorPalette colorPalette)
        {
            return colorPalette?.WrappedColorPalette;
        }
    }
}