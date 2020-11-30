using System;
using System.ComponentModel;

namespace Common.Drawing
{
    /// <summary>Stores a set of four integers that represent the location and size of a rectangle</summary>
    /// <filterpriority>1</filterpriority>
    [Serializable]
    public struct Rectangle
    {
        private System.Drawing.Rectangle WrappedRectangle;

        private Rectangle(System.Drawing.Rectangle rectangle)
        {
            WrappedRectangle = rectangle;
        }

        /// <summary>Represents a <see cref="T:Common.Drawing.Rectangle" /> structure with its properties left uninitialized.</summary>
        /// <filterpriority>1</filterpriority>
        public static readonly Rectangle Empty = System.Drawing.Rectangle.Empty;

        /// <summary>
        ///     Gets or sets the coordinates of the upper-left corner of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Point" /> that represents the upper-left corner of this
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public Point Location
        {
            get => WrappedRectangle.Location;
            set => WrappedRectangle.Location = value;
        }

        /// <summary>Gets or sets the size of this <see cref="T:Common.Drawing.Rectangle" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Size" /> that represents the width and height of this
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public Size Size
        {
            get => WrappedRectangle.Size;
            set => WrappedRectangle.Size = value;
        }

        /// <summary>
        ///     Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </summary>
        /// <returns>
        ///     The x-coordinate of the upper-left corner of this <see cref="T:Common.Drawing.Rectangle" /> structure. The
        ///     default is 0.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public int X
        {
            get => WrappedRectangle.X;
            set => WrappedRectangle.X = value;
        }

        /// <summary>
        ///     Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </summary>
        /// <returns>
        ///     The y-coordinate of the upper-left corner of this <see cref="T:Common.Drawing.Rectangle" /> structure. The
        ///     default is 0.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public int Y
        {
            get => WrappedRectangle.Y;
            set => WrappedRectangle.Y = value;
        }

        /// <summary>Gets or sets the width of this <see cref="T:Common.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The width of this <see cref="T:Common.Drawing.Rectangle" /> structure. The default is 0.</returns>
        /// <filterpriority>1</filterpriority>
        public int Width
        {
            get => WrappedRectangle.Width;
            set => WrappedRectangle.Width = value;
        }

        /// <summary>Gets or sets the height of this <see cref="T:Common.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The height of this <see cref="T:Common.Drawing.Rectangle" /> structure. The default is 0.</returns>
        /// <filterpriority>1</filterpriority>
        public int Height
        {
            get => WrappedRectangle.Height;
            set => WrappedRectangle.Height = value;
        }

        /// <summary>Gets the x-coordinate of the left edge of this <see cref="T:Common.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The x-coordinate of the left edge of this <see cref="T:Common.Drawing.Rectangle" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Left => WrappedRectangle.Left;

        /// <summary>Gets the y-coordinate of the top edge of this <see cref="T:Common.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The y-coordinate of the top edge of this <see cref="T:Common.Drawing.Rectangle" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Top => WrappedRectangle.Top;

        /// <summary>
        ///     Gets the x-coordinate that is the sum of <see cref="P:Common.Drawing.Rectangle.X" /> and
        ///     <see cref="P:Common.Drawing.Rectangle.Width" /> property values of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </summary>
        /// <returns>
        ///     The x-coordinate that is the sum of <see cref="P:Common.Drawing.Rectangle.X" /> and
        ///     <see cref="P:Common.Drawing.Rectangle.Width" /> of this <see cref="T:Common.Drawing.Rectangle" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Right => WrappedRectangle.Right;

        /// <summary>
        ///     Gets the y-coordinate that is the sum of the <see cref="P:Common.Drawing.Rectangle.Y" /> and
        ///     <see cref="P:Common.Drawing.Rectangle.Height" /> property values of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </summary>
        /// <returns>
        ///     The y-coordinate that is the sum of <see cref="P:Common.Drawing.Rectangle.Y" /> and
        ///     <see cref="P:Common.Drawing.Rectangle.Height" /> of this <see cref="T:Common.Drawing.Rectangle" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Bottom => WrappedRectangle.Bottom;

        /// <summary>Tests whether all numeric properties of this <see cref="T:Common.Drawing.Rectangle" /> have values of zero.</summary>
        /// <returns>
        ///     This property returns true if the <see cref="P:Common.Drawing.Rectangle.Width" />,
        ///     <see cref="P:Common.Drawing.Rectangle.Height" />, <see cref="P:Common.Drawing.Rectangle.X" />, and
        ///     <see cref="P:Common.Drawing.Rectangle.Y" /> properties of this <see cref="T:Common.Drawing.Rectangle" /> all have
        ///     values of zero; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty => WrappedRectangle.IsEmpty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Rectangle" /> class with the specified location
        ///     and size.
        /// </summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle. </param>
        /// <param name="width">The width of the rectangle. </param>
        /// <param name="height">The height of the rectangle. </param>
        public Rectangle(int x, int y, int width, int height)
        {
            WrappedRectangle = new System.Drawing.Rectangle(x, y, width, height);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Rectangle" /> class with the specified location
        ///     and size.
        /// </summary>
        /// <param name="location">
        ///     A <see cref="T:Common.Drawing.Point" /> that represents the upper-left corner of the rectangular
        ///     region.
        /// </param>
        /// <param name="size">
        ///     A <see cref="T:Common.Drawing.Size" /> that represents the width and height of the rectangular
        ///     region.
        /// </param>
        public Rectangle(Point location, Size size)
        {
            WrappedRectangle = new System.Drawing.Rectangle(location, size);
        }

        /// <summary>Creates a <see cref="T:Common.Drawing.Rectangle" /> structure with the specified edge locations.</summary>
        /// <returns>The new <see cref="T:Common.Drawing.Rectangle" /> that this method creates.</returns>
        /// <param name="left">
        ///     The x-coordinate of the upper-left corner of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </param>
        /// <param name="top">
        ///     The y-coordinate of the upper-left corner of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </param>
        /// <param name="right">
        ///     The x-coordinate of the lower-right corner of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </param>
        /// <param name="bottom">
        ///     The y-coordinate of the lower-right corner of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public static Rectangle FromLTRB(int left, int top, int right, int bottom)
        {
            return System.Drawing.Rectangle.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        ///     Tests whether <paramref name="obj" /> is a <see cref="T:Common.Drawing.Rectangle" /> structure with the same
        ///     location and size of this <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <returns>
        ///     This method returns true if <paramref name="obj" /> is a <see cref="T:Common.Drawing.Rectangle" /> structure
        ///     and its <see cref="P:Common.Drawing.Rectangle.X" />, <see cref="P:Common.Drawing.Rectangle.Y" />,
        ///     <see cref="P:Common.Drawing.Rectangle.Width" />, and <see cref="P:Common.Drawing.Rectangle.Height" /> properties
        ///     are equal to the corresponding properties of this <see cref="T:Common.Drawing.Rectangle" /> structure; otherwise,
        ///     false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            return WrappedRectangle.Equals(obj);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Rectangle" /> structure to a
        ///     <see cref="T:System.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Rectangle" /> that results from the conversion.</returns>
        /// <param name="r">The <see cref="T:Common.Drawing.Rectangle" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Rectangle(Rectangle r)
        {
            return r.WrappedRectangle;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Rectangle" /> structure to a
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Rectangle" /> that results from the conversion.</returns>
        /// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Rectangle(System.Drawing.Rectangle r)
        {
            return new Rectangle(r);
        }


        /// <summary>Tests whether two <see cref="T:Common.Drawing.Rectangle" /> structures have equal location and size.</summary>
        /// <returns>
        ///     This operator returns true if the two <see cref="T:Common.Drawing.Rectangle" /> structures have equal
        ///     <see cref="P:Common.Drawing.Rectangle.X" />, <see cref="P:Common.Drawing.Rectangle.Y" />,
        ///     <see cref="P:Common.Drawing.Rectangle.Width" />, and <see cref="P:Common.Drawing.Rectangle.Height" /> properties.
        /// </returns>
        /// <param name="left">
        ///     The <see cref="T:Common.Drawing.Rectangle" /> structure that is to the left of the equality
        ///     operator.
        /// </param>
        /// <param name="right">
        ///     The <see cref="T:Common.Drawing.Rectangle" /> structure that is to the right of the equality
        ///     operator.
        /// </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return left.WrappedRectangle == right.WrappedRectangle;
        }

        /// <summary>Tests whether two <see cref="T:Common.Drawing.Rectangle" /> structures differ in location or size.</summary>
        /// <returns>
        ///     This operator returns true if any of the <see cref="P:Common.Drawing.Rectangle.X" />,
        ///     <see cref="P:Common.Drawing.Rectangle.Y" />, <see cref="P:Common.Drawing.Rectangle.Width" /> or
        ///     <see cref="P:Common.Drawing.Rectangle.Height" /> properties of the two <see cref="T:Common.Drawing.Rectangle" />
        ///     structures are unequal; otherwise false.
        /// </returns>
        /// <param name="left">
        ///     The <see cref="T:Common.Drawing.Rectangle" /> structure that is to the left of the inequality
        ///     operator.
        /// </param>
        /// <param name="right">
        ///     The <see cref="T:Common.Drawing.Rectangle" /> structure that is to the right of the inequality
        ///     operator.
        /// </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return left.WrappedRectangle != right.WrappedRectangle;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.RectangleF" /> structure to a
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure by rounding the <see cref="T:Common.Drawing.RectangleF" />
        ///     values to the next higher integer values.
        /// </summary>
        /// <returns>Returns a <see cref="T:Common.Drawing.Rectangle" />.</returns>
        /// <param name="value">The <see cref="T:Common.Drawing.RectangleF" /> structure to be converted. </param>
        /// <filterpriority>1</filterpriority>
        public static Rectangle Ceiling(RectangleF value)
        {
            return System.Drawing.Rectangle.Ceiling(value);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.RectangleF" /> to a
        ///     <see cref="T:Common.Drawing.Rectangle" /> by truncating the <see cref="T:Common.Drawing.RectangleF" /> values.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.Rectangle" />.</returns>
        /// <param name="value">The <see cref="T:Common.Drawing.RectangleF" /> to be converted. </param>
        /// <filterpriority>1</filterpriority>
        public static Rectangle Truncate(RectangleF value)
        {
            return System.Drawing.Rectangle.Truncate(value);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.RectangleF" /> to a
        ///     <see cref="T:Common.Drawing.Rectangle" /> by rounding the <see cref="T:Common.Drawing.RectangleF" /> values to the
        ///     nearest integer values.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.Rectangle" />.</returns>
        /// <param name="value">The <see cref="T:Common.Drawing.RectangleF" /> to be converted. </param>
        /// <filterpriority>1</filterpriority>
        public static Rectangle Round(RectangleF value)
        {
            return System.Drawing.Rectangle.Round(value);
        }

        /// <summary>
        ///     Determines if the specified point is contained within this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </summary>
        /// <returns>
        ///     This method returns true if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained
        ///     within this <see cref="T:Common.Drawing.Rectangle" /> structure; otherwise false.
        /// </returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool Contains(int x, int y)
        {
            return WrappedRectangle.Contains(x, y);
        }

        /// <summary>
        ///     Determines if the specified point is contained within this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure.
        /// </summary>
        /// <returns>
        ///     This method returns true if the point represented by <paramref name="pt" /> is contained within this
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure; otherwise false.
        /// </returns>
        /// <param name="pt">The <see cref="T:Common.Drawing.Point" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool Contains(Point pt)
        {
            return WrappedRectangle.Contains(pt);
        }

        /// <summary>
        ///     Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this
        ///     <see cref="T:Common.Drawing.Rectangle" /> structure.
        /// </summary>
        /// <returns>
        ///     This method returns true if the rectangular region represented by <paramref name="rect" /> is entirely
        ///     contained within this <see cref="T:Common.Drawing.Rectangle" /> structure; otherwise false.
        /// </returns>
        /// <param name="rect">The <see cref="T:Common.Drawing.Rectangle" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool Contains(Rectangle rect)
        {
            return WrappedRectangle.Contains(rect);
        }

        /// <summary>
        ///     Returns the hash code for this <see cref="T:Common.Drawing.Rectangle" /> structure. For information about the
        ///     use of hash codes, see <see cref="M:System.Object.GetHashCode" /> .
        /// </summary>
        /// <returns>An integer that represents the hash code for this rectangle.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return WrappedRectangle.GetHashCode();
        }

        /// <summary>Enlarges this <see cref="T:Common.Drawing.Rectangle" /> by the specified amount.</summary>
        /// <param name="width">The amount to inflate this <see cref="T:Common.Drawing.Rectangle" /> horizontally. </param>
        /// <param name="height">The amount to inflate this <see cref="T:Common.Drawing.Rectangle" /> vertically. </param>
        /// <filterpriority>1</filterpriority>
        public void Inflate(int width, int height)
        {
            WrappedRectangle.Inflate(width, height);
        }

        /// <summary>Enlarges this <see cref="T:Common.Drawing.Rectangle" /> by the specified amount.</summary>
        /// <param name="size">The amount to inflate this rectangle. </param>
        /// <filterpriority>1</filterpriority>
        public void Inflate(Size size)
        {
            WrappedRectangle.Inflate(size);
        }

        /// <summary>
        ///     Creates and returns an enlarged copy of the specified <see cref="T:Common.Drawing.Rectangle" /> structure. The
        ///     copy is enlarged by the specified amount. The original <see cref="T:Common.Drawing.Rectangle" /> structure remains
        ///     unmodified.
        /// </summary>
        /// <returns>The enlarged <see cref="T:Common.Drawing.Rectangle" />.</returns>
        /// <param name="rect">The <see cref="T:Common.Drawing.Rectangle" /> with which to start. This rectangle is not modified. </param>
        /// <param name="x">The amount to inflate this <see cref="T:Common.Drawing.Rectangle" /> horizontally. </param>
        /// <param name="y">The amount to inflate this <see cref="T:Common.Drawing.Rectangle" /> vertically. </param>
        /// <filterpriority>1</filterpriority>
        public static Rectangle Inflate(Rectangle rect, int x, int y)
        {
            return System.Drawing.Rectangle.Inflate(rect.WrappedRectangle, x, y);
        }

        /// <summary>
        ///     Replaces this <see cref="T:Common.Drawing.Rectangle" /> with the intersection of itself and the specified
        ///     <see cref="T:Common.Drawing.Rectangle" />.
        /// </summary>
        /// <param name="rect">The <see cref="T:Common.Drawing.Rectangle" /> with which to intersect. </param>
        /// <filterpriority>1</filterpriority>
        public void Intersect(Rectangle rect)
        {
            WrappedRectangle.Intersect(rect);
        }

        /// <summary>
        ///     Returns a third <see cref="T:Common.Drawing.Rectangle" /> structure that represents the intersection of two
        ///     other <see cref="T:Common.Drawing.Rectangle" /> structures. If there is no intersection, an empty
        ///     <see cref="T:Common.Drawing.Rectangle" /> is returned.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that represents the intersection of <paramref name="a" /> and
        ///     <paramref name="b" />.
        /// </returns>
        /// <param name="a">A rectangle to intersect. </param>
        /// <param name="b">A rectangle to intersect. </param>
        /// <filterpriority>1</filterpriority>
        public static Rectangle Intersect(Rectangle a, Rectangle b)
        {
            return System.Drawing.Rectangle.Intersect(a.WrappedRectangle, b.WrappedRectangle);
        }

        /// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
        /// <returns>This method returns true if there is any intersection, otherwise false.</returns>
        /// <param name="rect">The rectangle to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool IntersectsWith(Rectangle rect)
        {
            return WrappedRectangle.IntersectsWith(rect.WrappedRectangle);
        }

        /// <summary>
        ///     Gets a <see cref="T:Common.Drawing.Rectangle" /> structure that contains the union of two
        ///     <see cref="T:Common.Drawing.Rectangle" /> structures.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that bounds the union of the two
        ///     <see cref="T:Common.Drawing.Rectangle" /> structures.
        /// </returns>
        /// <param name="a">A rectangle to union. </param>
        /// <param name="b">A rectangle to union. </param>
        /// <filterpriority>1</filterpriority>
        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            return System.Drawing.Rectangle.Union(a.WrappedRectangle, b.WrappedRectangle);
        }

        /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
        /// <param name="pos">Amount to offset the location. </param>
        /// <filterpriority>1</filterpriority>
        public void Offset(Point pos)
        {
            WrappedRectangle.Offset(pos);
        }

        /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
        /// <param name="x">The horizontal offset. </param>
        /// <param name="y">The vertical offset. </param>
        /// <filterpriority>1</filterpriority>
        public void Offset(int x, int y)
        {
            WrappedRectangle.Offset(x, y);
        }

        /// <summary>Converts the attributes of this <see cref="T:Common.Drawing.Rectangle" /> to a human-readable string.</summary>
        /// <returns>
        ///     A string that contains the position, width, and height of this <see cref="T:Common.Drawing.Rectangle" />
        ///     structure ¾ for example, {X=20, Y=20, Width=100, Height=50}
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        public override string ToString()
        {
            return WrappedRectangle.ToString();
        }
    }
}