using System;
using System.ComponentModel;

namespace Common.Drawing
{
    /// <summary>Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
    /// <filterpriority>1</filterpriority>
    [Serializable]
    public struct Point
    {
        private System.Drawing.Point WrappedPoint;

        private Point(System.Drawing.Point point)
        {
            WrappedPoint = point;
        }

        /// <summary>
        ///     Represents a <see cref="T:Common.Drawing.Point" /> that has <see cref="P:Common.Drawing.Point.X" /> and
        ///     <see cref="P:Common.Drawing.Point.Y" /> values set to zero.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        public static readonly Point Empty = System.Drawing.Point.Empty;

        /// <summary>Gets a value indicating whether this <see cref="T:Common.Drawing.Point" /> is empty.</summary>
        /// <returns>
        ///     true if both <see cref="P:Common.Drawing.Point.X" /> and <see cref="P:Common.Drawing.Point.Y" /> are 0;
        ///     otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty => WrappedPoint.IsEmpty;

        /// <summary>Gets or sets the x-coordinate of this <see cref="T:Common.Drawing.Point" />.</summary>
        /// <returns>The x-coordinate of this <see cref="T:Common.Drawing.Point" />.</returns>
        /// <filterpriority>1</filterpriority>
        public int X
        {
            get => WrappedPoint.X;
            set => WrappedPoint.X = value;
        }

        /// <summary>Gets or sets the y-coordinate of this <see cref="T:Common.Drawing.Point" />.</summary>
        /// <returns>The y-coordinate of this <see cref="T:Common.Drawing.Point" />.</returns>
        /// <filterpriority>1</filterpriority>
        public int Y
        {
            get => WrappedPoint.Y;
            set => WrappedPoint.Y = value;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Point" /> class with the specified coordinates.</summary>
        /// <param name="x">The horizontal position of the point. </param>
        /// <param name="y">The vertical position of the point. </param>
        public Point(int x, int y)
        {
            WrappedPoint = new System.Drawing.Point(x, y);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Point" /> class from a
        ///     <see cref="T:Common.Drawing.Size" />.
        /// </summary>
        /// <param name="sz">
        ///     A <see cref="T:Common.Drawing.Size" /> that specifies the coordinates for the new
        ///     <see cref="T:Common.Drawing.Point" />.
        /// </param>
        public Point(Size sz)
        {
            WrappedPoint = new System.Drawing.Point(sz);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Point" /> class using coordinates specified by
        ///     an integer value.
        /// </summary>
        /// <param name="dw">A 32-bit integer that specifies the coordinates for the new <see cref="T:Common.Drawing.Point" />. </param>
        public Point(int dw)
        {
            WrappedPoint = new System.Drawing.Point(dw);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Point" /> structure to a
        ///     <see cref="T:Common.Drawing.PointF" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.PointF" /> that results from the conversion.</returns>
        /// <param name="p">The <see cref="T:Common.Drawing.Point" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator PointF(Point p)
        {
            return (System.Drawing.PointF) p.WrappedPoint;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Point" /> structure to a
        ///     <see cref="T:System.Drawing.Point" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Point" /> that results from the conversion.</returns>
        /// <param name="p">The <see cref="T:Common.Drawing.Point" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Point(Point p)
        {
            return p.WrappedPoint;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Point" /> structure to a
        ///     <see cref="T:Common.Drawing.Point" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Point" /> that results from the conversion.</returns>
        /// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Point(System.Drawing.Point p)
        {
            return new Point(p);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Point" /> structure to a
        ///     <see cref="T:Common.Drawing.Size" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Size" /> that results from the conversion.</returns>
        /// <param name="p">The <see cref="T:Common.Drawing.Point" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static explicit operator Size(Point p)
        {
            return (System.Drawing.Size) p.WrappedPoint;
        }

        /// <summary>Translates a <see cref="T:Common.Drawing.Point" /> by a given <see cref="T:Common.Drawing.Size" />.</summary>
        /// <returns>The translated <see cref="T:Common.Drawing.Point" />.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.Point" /> to translate. </param>
        /// <param name="sz">
        ///     A <see cref="T:Common.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of
        ///     <paramref name="pt" />.
        /// </param>
        /// <filterpriority>3</filterpriority>
        public static Point operator +(Point pt, Size sz)
        {
            return System.Drawing.Point.Add(pt.WrappedPoint, sz);
        }

        /// <summary>
        ///     Translates a <see cref="T:Common.Drawing.Point" /> by the negative of a given
        ///     <see cref="T:Common.Drawing.Size" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Point" /> structure that is translated by the negative of a given
        ///     <see cref="T:Common.Drawing.Size" /> structure.
        /// </returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.Point" /> to translate. </param>
        /// <param name="sz">
        ///     A <see cref="T:Common.Drawing.Size" /> that specifies the pair of numbers to subtract from the
        ///     coordinates of <paramref name="pt" />.
        /// </param>
        /// <filterpriority>3</filterpriority>
        public static Point operator -(Point pt, Size sz)
        {
            return System.Drawing.Point.Subtract(pt.WrappedPoint, sz);
        }


        /// <summary>
        ///     Compares two <see cref="T:Common.Drawing.Point" /> objects. The result specifies whether the values of the
        ///     <see cref="P:Common.Drawing.Point.X" /> and <see cref="P:Common.Drawing.Point.Y" /> properties of the two
        ///     <see cref="T:Common.Drawing.Point" /> objects are equal.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="P:Common.Drawing.Point.X" /> and <see cref="P:Common.Drawing.Point.Y" /> values of
        ///     <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.
        /// </returns>
        /// <param name="left">A <see cref="T:Common.Drawing.Point" /> to compare. </param>
        /// <param name="right">A <see cref="T:Common.Drawing.Point" /> to compare. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(Point left, Point right)
        {
            return Equals(left.WrappedPoint, right.WrappedPoint);
        }

        /// <summary>
        ///     Compares two <see cref="T:Common.Drawing.Point" /> objects. The result specifies whether the values of the
        ///     <see cref="P:Common.Drawing.Point.X" /> or <see cref="P:Common.Drawing.Point.Y" /> properties of the two
        ///     <see cref="T:Common.Drawing.Point" /> objects are unequal.
        /// </summary>
        /// <returns>
        ///     true if the values of either the <see cref="P:Common.Drawing.Point.X" /> properties or the
        ///     <see cref="P:Common.Drawing.Point.Y" /> properties of <paramref name="left" /> and <paramref name="right" />
        ///     differ; otherwise, false.
        /// </returns>
        /// <param name="left">A <see cref="T:Common.Drawing.Point" /> to compare. </param>
        /// <param name="right">A <see cref="T:Common.Drawing.Point" /> to compare. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(Point left, Point right)
        {
            return !Equals(left.WrappedPoint, right.WrappedPoint);
        }

        /// <summary>
        ///     Adds the specified <see cref="T:Common.Drawing.Size" /> to the specified <see cref="T:Common.Drawing.Point" />
        ///     .
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Point" /> that is the result of the addition operation.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.Point" /> to add.</param>
        /// <param name="sz">The <see cref="T:Common.Drawing.Size" /> to add</param>
        public static Point Add(Point pt, Size sz)
        {
            return System.Drawing.Point.Add(pt, sz);
        }

        /// <summary>
        ///     Returns the result of subtracting specified <see cref="T:Common.Drawing.Size" /> from the specified
        ///     <see cref="T:Common.Drawing.Point" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Point" /> that is the result of the subtraction operation.</returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.Point" /> to be subtracted from. </param>
        /// <param name="sz">The <see cref="T:Common.Drawing.Size" /> to subtract from the <see cref="T:Common.Drawing.Point" />.</param>
        public static Point Subtract(Point pt, Size sz)
        {
            return System.Drawing.Point.Subtract(pt, sz);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.PointF" /> to a <see cref="T:Common.Drawing.Point" /> by
        ///     rounding the values of the <see cref="T:Common.Drawing.PointF" /> to the next higher integer values.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Point" /> this method converts to.</returns>
        /// <param name="value">The <see cref="T:Common.Drawing.PointF" /> to convert. </param>
        /// <filterpriority>1</filterpriority>
        public static Point Ceiling(PointF value)
        {
            return System.Drawing.Point.Ceiling(value);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.PointF" /> to a <see cref="T:Common.Drawing.Point" /> by
        ///     truncating the values of the <see cref="T:Common.Drawing.Point" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Point" /> this method converts to.</returns>
        /// <param name="value">The <see cref="T:Common.Drawing.PointF" /> to convert. </param>
        /// <filterpriority>1</filterpriority>
        public static Point Truncate(PointF value)
        {
            return System.Drawing.Point.Truncate(value);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.PointF" /> to a <see cref="T:Common.Drawing.Point" />
        ///     object by rounding the <see cref="T:Common.Drawing.Point" /> values to the nearest integer.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Point" /> this method converts to.</returns>
        /// <param name="value">The <see cref="T:Common.Drawing.PointF" /> to convert. </param>
        /// <filterpriority>1</filterpriority>
        public static Point Round(PointF value)
        {
            return System.Drawing.Point.Round(value);
        }

        /// <summary>
        ///     Specifies whether this <see cref="T:Common.Drawing.Point" /> contains the same coordinates as the specified
        ///     <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="obj" /> is a <see cref="T:Common.Drawing.Point" /> and has the same coordinates as
        ///     this <see cref="T:Common.Drawing.Point" />.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            return WrappedPoint.Equals(obj);
        }

        /// <summary>Returns a hash code for this <see cref="T:Common.Drawing.Point" />.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:Common.Drawing.Point" />.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return WrappedPoint.GetHashCode();
        }

        /// <summary>Translates this <see cref="T:Common.Drawing.Point" /> by the specified amount.</summary>
        /// <param name="dx">The amount to offset the x-coordinate. </param>
        /// <param name="dy">The amount to offset the y-coordinate. </param>
        /// <filterpriority>1</filterpriority>
        public void Offset(int dx, int dy)
        {
            WrappedPoint.Offset(dx, dy);
        }

        /// <summary>Translates this <see cref="T:Common.Drawing.Point" /> by the specified <see cref="T:Common.Drawing.Point" />.</summary>
        /// <param name="p">The <see cref="T:Common.Drawing.Point" /> used offset this <see cref="T:Common.Drawing.Point" />.</param>
        public void Offset(Point p)
        {
            WrappedPoint.Offset(p);
        }

        /// <summary>Converts this <see cref="T:Common.Drawing.Point" /> to a human-readable string.</summary>
        /// <returns>A string that represents this <see cref="T:Common.Drawing.Point" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        public override string ToString()
        {
            return WrappedPoint.ToString();
        }
    }
}