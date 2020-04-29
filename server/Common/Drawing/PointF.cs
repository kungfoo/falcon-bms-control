using System;
using System.ComponentModel;

namespace Common.Drawing
{
    /// <summary>
    ///     Represents an ordered pair of floating-point x- and y-coordinates that defines a point in a two-dimensional
    ///     plane.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [Serializable]
    public struct PointF
    {
        private System.Drawing.PointF WrappedPointF;

        private PointF(System.Drawing.PointF pointF)
        {
            WrappedPointF = pointF;
        }

        /// <summary>
        ///     Represents a new instance of the <see cref="T:Common.Drawing.PointF" /> class with member data left
        ///     uninitialized.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        public static readonly PointF Empty = System.Drawing.PointF.Empty;

        /// <summary>Gets a value indicating whether this <see cref="T:Common.Drawing.PointF" /> is empty.</summary>
        /// <returns>
        ///     true if both <see cref="P:Common.Drawing.PointF.X" /> and <see cref="P:Common.Drawing.PointF.Y" /> are 0;
        ///     otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty => WrappedPointF.IsEmpty;

        /// <summary>Gets or sets the x-coordinate of this <see cref="T:Common.Drawing.PointF" />.</summary>
        /// <returns>The x-coordinate of this <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <filterpriority>1</filterpriority>
        public float X
        {
            get => WrappedPointF.X;
            set => WrappedPointF.X = value;
        }

        /// <summary>Gets or sets the y-coordinate of this <see cref="T:Common.Drawing.PointF" />.</summary>
        /// <returns>The y-coordinate of this <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <filterpriority>1</filterpriority>
        public float Y
        {
            get => WrappedPointF.Y;
            set => WrappedPointF.Y = value;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.PointF" /> class with the specified coordinates.</summary>
        /// <param name="x">The horizontal position of the point. </param>
        /// <param name="y">The vertical position of the point. </param>
        public PointF(float x, float y)
        {
            WrappedPointF = new System.Drawing.PointF(x, y);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.PointF" /> structure to a
        ///     <see cref="T:System.Drawing.PointF" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.PointF" /> that results from the conversion.</returns>
        /// <param name="p">The <see cref="T:Common.Drawing.PointF" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.PointF(PointF p)
        {
            return p.WrappedPointF;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.PointF" /> structure to a
        ///     <see cref="T:Common.Drawing.PointF" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.PointF" /> that results from the conversion.</returns>
        /// <param name="p">The <see cref="T:System.Drawing.PointF" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator PointF(System.Drawing.PointF p)
        {
            return new PointF(p);
        }

        /// <summary>Translates a <see cref="T:Common.Drawing.PointF" /> by a given <see cref="T:Common.Drawing.Size" />.</summary>
        /// <returns>Returns the translated <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.PointF" /> to translate. </param>
        /// <param name="sz">
        ///     A <see cref="T:Common.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of
        ///     <paramref name="pt" />.
        /// </param>
        /// <filterpriority>3</filterpriority>
        public static PointF operator +(PointF pt, Size sz)
        {
            return pt.WrappedPointF + sz;
        }

        /// <summary>
        ///     Translates a <see cref="T:Common.Drawing.PointF" /> by the negative of a given
        ///     <see cref="T:Common.Drawing.Size" />.
        /// </summary>
        /// <returns>The translated <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">
        ///     The <see cref="T:Common.Drawing.Size" /> that specifies the numbers to subtract from the coordinates
        ///     of <paramref name="pt" />.
        /// </param>
        /// <filterpriority>3</filterpriority>
        public static PointF operator -(PointF pt, Size sz)
        {
            return pt.WrappedPointF - sz;
        }

        /// <summary>Translates the <see cref="T:Common.Drawing.PointF" /> by the specified <see cref="T:Common.Drawing.SizeF" />.</summary>
        /// <returns>The translated <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">
        ///     The <see cref="T:Common.Drawing.SizeF" /> that specifies the numbers to add to the x- and
        ///     y-coordinates of the <see cref="T:Common.Drawing.PointF" />.
        /// </param>
        public static PointF operator +(PointF pt, SizeF sz)
        {
            return pt.WrappedPointF + sz;
        }

        /// <summary>
        ///     Translates a <see cref="T:Common.Drawing.PointF" /> by the negative of a specified
        ///     <see cref="T:Common.Drawing.SizeF" />.
        /// </summary>
        /// <returns>The translated <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">
        ///     The <see cref="T:Common.Drawing.SizeF" /> that specifies the numbers to subtract from the coordinates
        ///     of <paramref name="pt" />.
        /// </param>
        public static PointF operator -(PointF pt, SizeF sz)
        {
            return pt.WrappedPointF - sz;
        }

        /// <summary>
        ///     Compares two <see cref="T:Common.Drawing.PointF" /> structures. The result specifies whether the values of the
        ///     <see cref="P:Common.Drawing.PointF.X" /> and <see cref="P:Common.Drawing.PointF.Y" /> properties of the two
        ///     <see cref="T:Common.Drawing.PointF" /> structures are equal.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="P:Common.Drawing.PointF.X" /> and <see cref="P:Common.Drawing.PointF.Y" /> values of
        ///     the left and right <see cref="T:Common.Drawing.PointF" /> structures are equal; otherwise, false.
        /// </returns>
        /// <param name="left">A <see cref="T:Common.Drawing.PointF" /> to compare. </param>
        /// <param name="right">A <see cref="T:Common.Drawing.PointF" /> to compare. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(PointF left, PointF right)
        {
            return left.WrappedPointF == right.WrappedPointF;
        }

        /// <summary>Determines whether the coordinates of the specified points are not equal.</summary>
        /// <returns>
        ///     true to indicate the <see cref="P:Common.Drawing.PointF.X" /> and <see cref="P:Common.Drawing.PointF.Y" />
        ///     values of <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.
        /// </returns>
        /// <param name="left">A <see cref="T:Common.Drawing.PointF" /> to compare.</param>
        /// <param name="right">A <see cref="T:Common.Drawing.PointF" /> to compare.</param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(PointF left, PointF right)
        {
            return left.WrappedPointF != right.WrappedPointF;
        }

        /// <summary>
        ///     Translates a given <see cref="T:Common.Drawing.PointF" /> by the specified
        ///     <see cref="T:Common.Drawing.Size" />.
        /// </summary>
        /// <returns>The translated <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">
        ///     The <see cref="T:Common.Drawing.Size" /> that specifies the numbers to add to the coordinates of
        ///     <paramref name="pt" />.
        /// </param>
        public static PointF Add(PointF pt, Size sz)
        {
            return System.Drawing.PointF.Add(pt.WrappedPointF, sz);
        }

        /// <summary>Translates a <see cref="T:Common.Drawing.PointF" /> by the negative of a specified size.</summary>
        /// <returns>The translated <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">
        ///     The <see cref="T:Common.Drawing.Size" /> that specifies the numbers to subtract from the coordinates
        ///     of <paramref name="pt" />.
        /// </param>
        public static PointF Subtract(PointF pt, Size sz)
        {
            return System.Drawing.PointF.Subtract(pt.WrappedPointF, sz);
        }

        /// <summary>
        ///     Translates a given <see cref="T:Common.Drawing.PointF" /> by a specified <see cref="T:Common.Drawing.SizeF" />
        ///     .
        /// </summary>
        /// <returns>The translated <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">
        ///     The <see cref="T:Common.Drawing.SizeF" /> that specifies the numbers to add to the coordinates of
        ///     <paramref name="pt" />.
        /// </param>
        public static PointF Add(PointF pt, SizeF sz)
        {
            return System.Drawing.PointF.Add(pt.WrappedPointF, sz);
        }

        /// <summary>Translates a <see cref="T:Common.Drawing.PointF" /> by the negative of a specified size.</summary>
        /// <returns>The translated <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">
        ///     The <see cref="T:Common.Drawing.SizeF" /> that specifies the numbers to subtract from the coordinates
        ///     of <paramref name="pt" />.
        /// </param>
        public static PointF Subtract(PointF pt, SizeF sz)
        {
            return System.Drawing.PointF.Subtract(pt.WrappedPointF, sz);
        }

        /// <summary>
        ///     Specifies whether this <see cref="T:Common.Drawing.PointF" /> contains the same coordinates as the specified
        ///     <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if <paramref name="obj" /> is a <see cref="T:Common.Drawing.PointF" /> and has the
        ///     same coordinates as this <see cref="T:Common.Drawing.Point" />.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            return WrappedPointF.Equals(obj);
        }

        /// <summary>Returns a hash code for this <see cref="T:Common.Drawing.PointF" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:Common.Drawing.PointF" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return WrappedPointF.GetHashCode();
        }

        /// <summary>Converts this <see cref="T:Common.Drawing.PointF" /> to a human readable string.</summary>
        /// <returns>A string that represents this <see cref="T:Common.Drawing.PointF" />.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return WrappedPointF.ToString();
        }
    }
}