using System;
using System.ComponentModel;

namespace Common.Drawing
{
    /// <summary>
    ///     Stores an ordered pair of integers, which specify a <see cref="P:Common.Drawing.Size.Height" /> and
    ///     <see cref="P:Common.Drawing.Size.Width" />.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [Serializable]
    public struct Size
    {
        private System.Drawing.Size WrappedSize;

        private Size(System.Drawing.Size size)
        {
            WrappedSize = size;
        }

        /// <summary>
        ///     Gets a <see cref="T:Common.Drawing.Size" /> structure that has a <see cref="P:Common.Drawing.Size.Height" />
        ///     and <see cref="P:Common.Drawing.Size.Width" /> value of 0.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Size" /> that has a <see cref="P:Common.Drawing.Size.Height" /> and
        ///     <see cref="P:Common.Drawing.Size.Width" /> value of 0.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public static readonly Size Empty = System.Drawing.Size.Empty;

        /// <summary>Tests whether this <see cref="T:Common.Drawing.Size" /> structure has width and height of 0.</summary>
        /// <returns>
        ///     This property returns true when this <see cref="T:Common.Drawing.Size" /> structure has both a width and
        ///     height of 0; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty => WrappedSize.IsEmpty;

        /// <summary>Gets or sets the horizontal component of this <see cref="T:Common.Drawing.Size" /> structure.</summary>
        /// <returns>The horizontal component of this <see cref="T:Common.Drawing.Size" /> structure, typically measured in pixels.</returns>
        /// <filterpriority>1</filterpriority>
        public int Width
        {
            get => WrappedSize.Width;
            set => WrappedSize.Width = value;
        }

        /// <summary>Gets or sets the vertical component of this <see cref="T:Common.Drawing.Size" /> structure.</summary>
        /// <returns>The vertical component of this <see cref="T:Common.Drawing.Size" /> structure, typically measured in pixels.</returns>
        /// <filterpriority>1</filterpriority>
        public int Height
        {
            get => WrappedSize.Height;
            set => WrappedSize.Height = value;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Size" /> structure from the specified
        ///     <see cref="T:Common.Drawing.Point" /> structure.
        /// </summary>
        /// <param name="pt">
        ///     The <see cref="T:Common.Drawing.Point" /> structure from which to initialize this
        ///     <see cref="T:Common.Drawing.Size" /> structure.
        /// </param>
        public Size(Point pt)
        {
            WrappedSize = new System.Drawing.Size(pt);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Size" /> structure from the specified
        ///     dimensions.
        /// </summary>
        /// <param name="width">The width component of the new <see cref="T:Common.Drawing.Size" />. </param>
        /// <param name="height">The height component of the new <see cref="T:Common.Drawing.Size" />. </param>
        public Size(int width, int height)
        {
            WrappedSize = new System.Drawing.Size(width, height);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Size" /> structure to a
        ///     <see cref="T:System.Drawing.Size" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Size" /> that results from the conversion.</returns>
        /// <param name="s">The <see cref="T:Common.Drawing.Size" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Size(Size s)
        {
            return s.WrappedSize;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Size" /> structure to a
        ///     <see cref="T:Common.Drawing.Size" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Size" /> that results from the conversion.</returns>
        /// <param name="s">The <see cref="T:System.Drawing.Size" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Size(System.Drawing.Size s)
        {
            return new Size(s);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Size" /> structure to a
        ///     <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.SizeF" /> structure to which this operator converts.</returns>
        /// <param name="p">The <see cref="T:Common.Drawing.Size" /> structure to convert. </param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator SizeF(Size p)
        {
            return (System.Drawing.SizeF) p.WrappedSize;
        }

        /// <summary>
        ///     Adds the width and height of one <see cref="T:Common.Drawing.Size" /> structure to the width and height of
        ///     another <see cref="T:Common.Drawing.Size" /> structure.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.Size" /> structure that is the result of the addition operation.</returns>
        /// <param name="sz1">The first <see cref="T:Common.Drawing.Size" /> to add. </param>
        /// <param name="sz2">The second <see cref="T:Common.Drawing.Size" /> to add. </param>
        /// <filterpriority>3</filterpriority>
        public static Size operator +(Size sz1, Size sz2)
        {
            return sz1.WrappedSize + sz2.WrappedSize;
        }

        /// <summary>
        ///     Subtracts the width and height of one <see cref="T:Common.Drawing.Size" /> structure from the width and height
        ///     of another <see cref="T:Common.Drawing.Size" /> structure.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.Size" /> structure that is the result of the subtraction operation.</returns>
        /// <param name="sz1">The <see cref="T:Common.Drawing.Size" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:Common.Drawing.Size" /> structure on the right side of the subtraction operator. </param>
        /// <filterpriority>3</filterpriority>
        public static Size operator -(Size sz1, Size sz2)
        {
            return sz1.WrappedSize - sz2.WrappedSize;
        }

        /// <summary>Tests whether two <see cref="T:Common.Drawing.Size" /> structures are equal.</summary>
        /// <returns>true if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, false.</returns>
        /// <param name="sz1">The <see cref="T:Common.Drawing.Size" /> structure on the left side of the equality operator. </param>
        /// <param name="sz2">The <see cref="T:Common.Drawing.Size" /> structure on the right of the equality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(Size sz1, Size sz2)
        {
            return sz1.WrappedSize == sz2.WrappedSize;
        }

        /// <summary>Tests whether two <see cref="T:Common.Drawing.Size" /> structures are different.</summary>
        /// <returns>
        ///     true if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; false if
        ///     <paramref name="sz1" /> and <paramref name="sz2" /> are equal.
        /// </returns>
        /// <param name="sz1">The <see cref="T:Common.Drawing.Size" /> structure on the left of the inequality operator. </param>
        /// <param name="sz2">The <see cref="T:Common.Drawing.Size" /> structure on the right of the inequality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(Size sz1, Size sz2)
        {
            return sz1.WrappedSize != sz2.WrappedSize;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Size" /> structure to a
        ///     <see cref="T:Common.Drawing.Point" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Point" /> structure to which this operator converts.</returns>
        /// <param name="size">The <see cref="T:Common.Drawing.Size" /> structure to convert. </param>
        /// <filterpriority>3</filterpriority>
        public static explicit operator Point(Size size)
        {
            return (System.Drawing.Point) size.WrappedSize;
        }

        /// <summary>
        ///     Adds the width and height of one <see cref="T:Common.Drawing.Size" /> structure to the width and height of
        ///     another <see cref="T:Common.Drawing.Size" /> structure.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.Size" /> structure that is the result of the addition operation.</returns>
        /// <param name="sz1">The first <see cref="T:Common.Drawing.Size" /> structure to add.</param>
        /// <param name="sz2">The second <see cref="T:Common.Drawing.Size" /> structure to add.</param>
        public static Size Add(Size sz1, Size sz2)
        {
            return System.Drawing.Size.Add(sz1.WrappedSize, sz2.WrappedSize);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.SizeF" /> structure to a
        ///     <see cref="T:Common.Drawing.Size" /> structure by rounding the values of the <see cref="T:Common.Drawing.Size" />
        ///     structure to the next higher integer values.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Size" /> structure this method converts to.</returns>
        /// <param name="value">The <see cref="T:Common.Drawing.SizeF" /> structure to convert. </param>
        /// <filterpriority>1</filterpriority>
        public static Size Ceiling(SizeF value)
        {
            return System.Drawing.Size.Ceiling(value);
        }

        /// <summary>
        ///     Subtracts the width and height of one <see cref="T:Common.Drawing.Size" /> structure from the width and height
        ///     of another <see cref="T:Common.Drawing.Size" /> structure.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.Size" /> structure that is a result of the subtraction operation.</returns>
        /// <param name="sz1">The <see cref="T:Common.Drawing.Size" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:Common.Drawing.Size" /> structure on the right side of the subtraction operator. </param>
        public static Size Subtract(Size sz1, Size sz2)
        {
            return System.Drawing.Size.Subtract(sz1.WrappedSize, sz2.WrappedSize);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.SizeF" /> structure to a
        ///     <see cref="T:Common.Drawing.Size" /> structure by truncating the values of the
        ///     <see cref="T:Common.Drawing.SizeF" /> structure to the next lower integer values.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Size" /> structure this method converts to.</returns>
        /// <param name="value">The <see cref="T:Common.Drawing.SizeF" /> structure to convert. </param>
        /// <filterpriority>1</filterpriority>
        public static Size Truncate(SizeF value)
        {
            return System.Drawing.Size.Truncate(value);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.SizeF" /> structure to a
        ///     <see cref="T:Common.Drawing.Size" /> structure by rounding the values of the <see cref="T:Common.Drawing.SizeF" />
        ///     structure to the nearest integer values.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Size" /> structure this method converts to.</returns>
        /// <param name="value">The <see cref="T:Common.Drawing.SizeF" /> structure to convert. </param>
        /// <filterpriority>1</filterpriority>
        public static Size Round(SizeF value)
        {
            return System.Drawing.Size.Round(value);
        }

        /// <summary>
        ///     Tests to see whether the specified object is a <see cref="T:Common.Drawing.Size" /> structure with the same
        ///     dimensions as this <see cref="T:Common.Drawing.Size" /> structure.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="obj" /> is a <see cref="T:Common.Drawing.Size" /> and has the same width and height as
        ///     this <see cref="T:Common.Drawing.Size" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            return WrappedSize.Equals(obj);
        }

        /// <summary>Returns a hash code for this <see cref="T:Common.Drawing.Size" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:Common.Drawing.Size" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return WrappedSize.GetHashCode();
        }

        /// <summary>Creates a human-readable string that represents this <see cref="T:Common.Drawing.Size" /> structure.</summary>
        /// <returns>A string that represents this <see cref="T:Common.Drawing.Size" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        public override string ToString()
        {
            return WrappedSize.ToString();
        }
    }
}