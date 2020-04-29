using System.Linq;

namespace Common.Drawing.Drawing2D
{
    /// <summary>
    ///     Defines arrays of colors and positions used for interpolating color blending in a multicolor gradient. This
    ///     class cannot be inherited.
    /// </summary>
    public sealed class ColorBlend
    {
        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.ColorBlend" /> class.</summary>
        public ColorBlend()
        {
            WrappedColorBlend = new System.Drawing.Drawing2D.ColorBlend();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.ColorBlend" /> class with the
        ///     specified number of colors and positions.
        /// </summary>
        /// <param name="count">The number of colors and positions in this <see cref="T:Common.Drawing.Drawing2D.ColorBlend" />. </param>
        public ColorBlend(int count)
        {
            WrappedColorBlend = new System.Drawing.Drawing2D.ColorBlend(count);
        }

        private ColorBlend(System.Drawing.Drawing2D.ColorBlend colorBlend)
        {
            WrappedColorBlend = colorBlend;
        }

        /// <summary>Gets or sets an array of colors that represents the colors to use at corresponding positions along a gradient.</summary>
        /// <returns>
        ///     An array of <see cref="T:Common.Drawing.Color" /> structures that represents the colors to use at
        ///     corresponding positions along a gradient.
        /// </returns>
        public Color[] Colors
        {
            get => WrappedColorBlend.Colors.Convert<Color>().ToArray();
            set => WrappedColorBlend.Colors = value.Convert<System.Drawing.Color>().ToArray();
        }

        /// <summary>Gets or sets the positions along a gradient line.</summary>
        /// <returns>An array of values that specify percentages of distance along the gradient line.</returns>
        public float[] Positions
        {
            get => WrappedColorBlend.Positions;
            set => WrappedColorBlend.Positions = value;
        }

        private System.Drawing.Drawing2D.ColorBlend WrappedColorBlend { get; }


        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.ColorBlend" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Texture" /> that results from the conversion.</returns>
        /// <param name="colorBlend">The <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator ColorBlend(System.Drawing.Drawing2D.ColorBlend colorBlend)
        {
            return colorBlend == null ? null : new ColorBlend(colorBlend);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.ColorBlend" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.ColorBlend" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that results from the conversion.</returns>
        /// <param name="colorBlend">The <see cref="T:Common.Drawing.Drawing2D.ColorBlend" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.ColorBlend(ColorBlend colorBlend)
        {
            return colorBlend?.WrappedColorBlend;
        }
    }
}