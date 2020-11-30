using System;
using System.ComponentModel;

namespace Common.Drawing
{
    /// <summary>Stores an ordered pair of floating-point numbers, typically the width and height of a rectangle.</summary>
    /// <filterpriority>1</filterpriority>
    [Serializable]
    public struct SizeF
    {
        private System.Drawing.SizeF WrappedSizeF;

        private SizeF(System.Drawing.SizeF sizeF)
        {
            WrappedSizeF = sizeF;
        }

        /// <summary>
        ///     Gets a <see cref="T:Common.Drawing.SizeF" /> structure that has a <see cref="P:Common.Drawing.SizeF.Height" />
        ///     and <see cref="P:Common.Drawing.SizeF.Width" /> value of 0.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.SizeF" /> structure that has a <see cref="P:Common.Drawing.SizeF.Height" /> and
        ///     <see cref="P:Common.Drawing.SizeF.Width" /> value of 0.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public static readonly SizeF Empty = System.Drawing.SizeF.Empty;

        /// <summary>
        ///     Gets a value that indicates whether this <see cref="T:Common.Drawing.SizeF" /> structure has zero width and
        ///     height.
        /// </summary>
        /// <returns>
        ///     This property returns true when this <see cref="T:Common.Drawing.SizeF" /> structure has both a width and
        ///     height of zero; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty => WrappedSizeF.IsEmpty;

        /// <summary>Gets or sets the horizontal component of this <see cref="T:Common.Drawing.SizeF" /> structure.</summary>
        /// <returns>
        ///     The horizontal component of this <see cref="T:Common.Drawing.SizeF" /> structure, typically measured in
        ///     pixels.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public float Width
        {
            get => WrappedSizeF.Width;
            set => WrappedSizeF.Width = value;
        }

        /// <summary>Gets or sets the vertical component of this <see cref="T:Common.Drawing.SizeF" /> structure.</summary>
        /// <returns>The vertical component of this <see cref="T:Common.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
        /// <filterpriority>1</filterpriority>
        public float Height
        {
            get => WrappedSizeF.Height;
            set => WrappedSizeF.Height = value;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.SizeF" /> structure from the specified existing
        ///     <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </summary>
        /// <param name="size">
        ///     The <see cref="T:Common.Drawing.SizeF" /> structure from which to create the new
        ///     <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </param>
        public SizeF(SizeF size)
        {
            WrappedSizeF = new System.Drawing.SizeF(size);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.SizeF" /> structure from the specified
        ///     <see cref="T:Common.Drawing.PointF" /> structure.
        /// </summary>
        /// <param name="pt">
        ///     The <see cref="T:Common.Drawing.PointF" /> structure from which to initialize this
        ///     <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </param>
        public SizeF(PointF pt)
        {
            WrappedSizeF = new System.Drawing.SizeF(pt);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.SizeF" /> structure from the specified
        ///     dimensions.
        /// </summary>
        /// <param name="width">The width component of the new <see cref="T:Common.Drawing.SizeF" /> structure. </param>
        /// <param name="height">The height component of the new <see cref="T:Common.Drawing.SizeF" /> structure. </param>
        public SizeF(float width, float height)
        {
            WrappedSizeF = new System.Drawing.SizeF(width, height);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.SizeF" /> structure to a
        ///     <see cref="T:System.Drawing.SizeF" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.SizeF" /> that results from the conversion.</returns>
        /// <param name="s">The <see cref="T:Common.Drawing.SizeF" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.SizeF(SizeF s)
        {
            return s.WrappedSizeF;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a
        ///     <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.SizeF" /> that results from the conversion.</returns>
        /// <param name="s">The <see cref="T:System.Drawing.SizeF" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator SizeF(System.Drawing.SizeF s)
        {
            return new SizeF(s);
        }

        /// <summary>
        ///     Adds the width and height of one <see cref="T:Common.Drawing.SizeF" /> structure to the width and height of
        ///     another <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.Size" /> structure that is the result of the addition operation.</returns>
        /// <param name="sz1">The first <see cref="T:Common.Drawing.SizeF" /> structure to add. </param>
        /// <param name="sz2">The second <see cref="T:Common.Drawing.SizeF" /> structure to add. </param>
        /// <filterpriority>3</filterpriority>
        public static SizeF operator +(SizeF sz1, SizeF sz2)
        {
            return sz1.WrappedSizeF + sz2.WrappedSizeF;
        }

        /// <summary>
        ///     Subtracts the width and height of one <see cref="T:Common.Drawing.SizeF" /> structure from the width and
        ///     height of another <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.SizeF" /> that is the result of the subtraction operation.</returns>
        /// <param name="sz1">The <see cref="T:Common.Drawing.SizeF" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:Common.Drawing.SizeF" /> structure on the right side of the subtraction operator. </param>
        /// <filterpriority>3</filterpriority>
        public static SizeF operator -(SizeF sz1, SizeF sz2)
        {
            return sz1.WrappedSizeF - sz2.WrappedSizeF;
        }

        /// <summary>Tests whether two <see cref="T:Common.Drawing.SizeF" /> structures are equal.</summary>
        /// <returns>
        ///     This operator returns true if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height;
        ///     otherwise, false.
        /// </returns>
        /// <param name="sz1">The <see cref="T:Common.Drawing.SizeF" /> structure on the left side of the equality operator. </param>
        /// <param name="sz2">The <see cref="T:Common.Drawing.SizeF" /> structure on the right of the equality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(SizeF sz1, SizeF sz2)
        {
            return sz1.WrappedSizeF == sz2.WrappedSizeF;
        }

        /// <summary>Tests whether two <see cref="T:Common.Drawing.SizeF" /> structures are different.</summary>
        /// <returns>
        ///     This operator returns true if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or
        ///     height; false if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.
        /// </returns>
        /// <param name="sz1">The <see cref="T:Common.Drawing.SizeF" /> structure on the left of the inequality operator. </param>
        /// <param name="sz2">The <see cref="T:Common.Drawing.SizeF" /> structure on the right of the inequality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(SizeF sz1, SizeF sz2)
        {
            return sz1.WrappedSizeF != sz2.WrappedSizeF;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.SizeF" /> structure to a
        ///     <see cref="T:Common.Drawing.PointF" /> structure.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.PointF" /> structure to which this operator converts.</returns>
        /// <param name="size">The <see cref="T:Common.Drawing.SizeF" /> structure to be converted</param>
        /// <filterpriority>3</filterpriority>
        public static explicit operator PointF(SizeF size)
        {
            return (System.Drawing.PointF) size.WrappedSizeF;
        }

        /// <summary>
        ///     Adds the width and height of one <see cref="T:Common.Drawing.SizeF" /> structure to the width and height of
        ///     another <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.SizeF" /> structure that is the result of the addition operation.</returns>
        /// <param name="sz1">The first <see cref="T:Common.Drawing.SizeF" /> structure to add.</param>
        /// <param name="sz2">The second <see cref="T:Common.Drawing.SizeF" /> structure to add.</param>
        public static SizeF Add(SizeF sz1, SizeF sz2)
        {
            return System.Drawing.SizeF.Add(sz1.WrappedSizeF, sz2.WrappedSizeF);
        }

        /// <summary>
        ///     Subtracts the width and height of one <see cref="T:Common.Drawing.SizeF" /> structure from the width and
        ///     height of another <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.SizeF" /> structure that is a result of the subtraction operation.</returns>
        /// <param name="sz1">The <see cref="T:Common.Drawing.SizeF" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:Common.Drawing.SizeF" /> structure on the right side of the subtraction operator. </param>
        public static SizeF Subtract(SizeF sz1, SizeF sz2)
        {
            return System.Drawing.SizeF.Subtract(sz1.WrappedSizeF, sz2.WrappedSizeF);
        }

        /// <summary>
        ///     Tests to see whether the specified object is a <see cref="T:Common.Drawing.SizeF" /> structure with the same
        ///     dimensions as this <see cref="T:Common.Drawing.SizeF" /> structure.
        /// </summary>
        /// <returns>
        ///     This method returns true if <paramref name="obj" /> is a <see cref="T:Common.Drawing.SizeF" /> and has the
        ///     same width and height as this <see cref="T:Common.Drawing.SizeF" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            return WrappedSizeF.Equals(obj);
        }

        /// <summary>Returns a hash code for this <see cref="T:Common.Drawing.Size" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:Common.Drawing.Size" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return WrappedSizeF.GetHashCode();
        }

        /// <summary>
        ///     Converts a <see cref="T:Common.Drawing.SizeF" /> structure to a <see cref="T:Common.Drawing.PointF" />
        ///     structure.
        /// </summary>
        /// <returns>Returns a <see cref="T:Common.Drawing.PointF" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public PointF ToPointF()
        {
            return WrappedSizeF.ToPointF();
        }

        /// <summary>
        ///     Converts a <see cref="T:Common.Drawing.SizeF" /> structure to a <see cref="T:Common.Drawing.Size" />
        ///     structure.
        /// </summary>
        /// <returns>Returns a <see cref="T:Common.Drawing.Size" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public Size ToSize()
        {
            return WrappedSizeF.ToSize();
        }

        /// <summary>Creates a human-readable string that represents this <see cref="T:Common.Drawing.SizeF" /> structure.</summary>
        /// <returns>A string that represents this <see cref="T:Common.Drawing.SizeF" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        public override string ToString()
        {
            return WrappedSizeF.ToString();
        }
    }
}