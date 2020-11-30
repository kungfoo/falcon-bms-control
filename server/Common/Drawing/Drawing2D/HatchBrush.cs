﻿namespace Common.Drawing.Drawing2D
{
    /// <summary>
    ///     Defines a rectangular brush with a hatch style, a foreground color, and a background color. This class cannot
    ///     be inherited.
    /// </summary>
    public sealed class HatchBrush : Brush
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> class with the
        ///     specified <see cref="T:Common.Drawing.Drawing2D.HatchStyle" /> enumeration and foreground color.
        /// </summary>
        /// <param name="hatchstyle">
        ///     One of the <see cref="T:Common.Drawing.Drawing2D.HatchStyle" /> values that represents the
        ///     pattern drawn by this <see cref="T:Common.Drawing.Drawing2D.HatchBrush" />.
        /// </param>
        /// <param name="foreColor">
        ///     The <see cref="T:Common.Drawing.Color" /> structure that represents the color of lines drawn by
        ///     this <see cref="T:Common.Drawing.Drawing2D.HatchBrush" />.
        /// </param>
        public HatchBrush(HatchStyle hatchstyle, Color foreColor)
        {
            WrappedHatchBrush =
                new System.Drawing.Drawing2D.HatchBrush((System.Drawing.Drawing2D.HatchStyle) hatchstyle, foreColor);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> class with the
        ///     specified <see cref="T:Common.Drawing.Drawing2D.HatchStyle" /> enumeration, foreground color, and background color.
        /// </summary>
        /// <param name="hatchstyle">
        ///     One of the <see cref="T:Common.Drawing.Drawing2D.HatchStyle" /> values that represents the
        ///     pattern drawn by this <see cref="T:Common.Drawing.Drawing2D.HatchBrush" />.
        /// </param>
        /// <param name="foreColor">
        ///     The <see cref="T:Common.Drawing.Color" /> structure that represents the color of lines drawn by
        ///     this <see cref="T:Common.Drawing.Drawing2D.HatchBrush" />.
        /// </param>
        /// <param name="backColor">
        ///     The <see cref="T:Common.Drawing.Color" /> structure that represents the color of spaces between
        ///     the lines drawn by this <see cref="T:Common.Drawing.Drawing2D.HatchBrush" />.
        /// </param>
        public HatchBrush(HatchStyle hatchstyle, Color foreColor, Color backColor)
        {
            WrappedHatchBrush =
                new System.Drawing.Drawing2D.HatchBrush((System.Drawing.Drawing2D.HatchStyle) hatchstyle, foreColor,
                    backColor);
        }

        internal HatchBrush(System.Drawing.Drawing2D.HatchBrush hatchBrush)
        {
            WrappedHatchBrush = hatchBrush;
        }

        /// <summary>
        ///     Gets the color of spaces between the hatch lines drawn by this
        ///     <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the background color for this
        ///     <see cref="T:Common.Drawing.Drawing2D.HatchBrush" />.
        /// </returns>
        public Color BackgroundColor => WrappedHatchBrush.BackgroundColor;

        /// <summary>Gets the color of hatch lines drawn by this <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> object.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the foreground color for this
        ///     <see cref="T:Common.Drawing.Drawing2D.HatchBrush" />.
        /// </returns>
        public Color ForegroundColor => WrappedHatchBrush.ForegroundColor;

        /// <summary>Gets the hatch style of this <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> object.</summary>
        /// <returns>
        ///     One of the <see cref="T:Common.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern of this
        ///     <see cref="T:Common.Drawing.Drawing2D.HatchBrush" />.
        /// </returns>
        public HatchStyle HatchStyle => (HatchStyle) WrappedHatchBrush.HatchStyle;

        private System.Drawing.Drawing2D.HatchBrush WrappedHatchBrush
        {
            get => WrappedBrush as System.Drawing.Drawing2D.HatchBrush;
            set => WrappedBrush = value;
        }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> object.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> this method creates, cast as an object.</returns>
        public override object Clone()
        {
            return (HatchBrush) (System.Drawing.Drawing2D.HatchBrush) WrappedHatchBrush.Clone();
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.HatchBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> that results from the conversion.</returns>
        /// <param name="hatchBrush">The <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator HatchBrush(System.Drawing.Drawing2D.HatchBrush hatchBrush)
        {
            return hatchBrush == null ? null : new HatchBrush(hatchBrush);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> that results from the conversion.</returns>
        /// <param name="hatchBrush">The <see cref="T:Common.Drawing.Drawing2D.HatchBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.HatchBrush(HatchBrush hatchBrush)
        {
            return hatchBrush?.WrappedHatchBrush;
        }
    }
}