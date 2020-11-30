namespace Common.Drawing.Drawing2D
{
    /// <summary>
    ///     Defines a blend pattern for a <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object. This class
    ///     cannot be inherited.
    /// </summary>
    public sealed class Blend
    {
        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.Blend" /> class.</summary>
        public Blend()
        {
            WrappedBlend = new System.Drawing.Drawing2D.Blend();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.Blend" /> class with the specified
        ///     number of factors and positions.
        /// </summary>
        /// <param name="count">
        ///     The number of elements in the <see cref="P:Common.Drawing.Drawing2D.Blend.Factors" /> and
        ///     <see cref="P:Common.Drawing.Drawing2D.Blend.Positions" /> arrays.
        /// </param>
        public Blend(int count)
        {
            WrappedBlend = new System.Drawing.Drawing2D.Blend(count);
        }

        private Blend(System.Drawing.Drawing2D.Blend blend)
        {
            WrappedBlend = blend;
        }

        /// <summary>Gets or sets an array of blend factors for the gradient.</summary>
        /// <returns>
        ///     An array of blend factors that specify the percentages of the starting color and the ending color to be used
        ///     at the corresponding position.
        /// </returns>
        public float[] Factors
        {
            get => WrappedBlend.Factors;
            set => WrappedBlend.Factors = value;
        }

        /// <summary>Gets or sets an array of blend positions for the gradient.</summary>
        /// <returns>An array of blend positions that specify the percentages of distance along the gradient line.</returns>
        public float[] Positions
        {
            get => WrappedBlend.Positions;
            set => WrappedBlend.Positions = value;
        }

        private System.Drawing.Drawing2D.Blend WrappedBlend { get; }


        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.Blend" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.Blend" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Texture" /> that results from the conversion.</returns>
        /// <param name="blend">The <see cref="T:System.Drawing.Drawing2D.Blend" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Blend(System.Drawing.Drawing2D.Blend blend)
        {
            return blend == null ? null : new Blend(blend);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.Blend" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.Blend" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.Blend" /> that results from the conversion.</returns>
        /// <param name="blend">The <see cref="T:Common.Drawing.Drawing2D.Blend" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.Blend(Blend blend)
        {
            return blend?.WrappedBlend;
        }
    }
}