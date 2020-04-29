using System;

namespace Common.Drawing.Imaging
{
    /// <summary>Provides properties that get the frame dimensions of an image. Not inheritable.</summary>
    public sealed class FrameDimension
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.FrameDimension" /> class using the
        ///     specified Guid structure.
        /// </summary>
        /// <param name="guid">
        ///     A Guid structure that contains a GUID for this
        ///     <see cref="T:Common.Drawing.Imaging.FrameDimension" /> object.
        /// </param>
        public FrameDimension(Guid guid)
        {
            WrappedFrameDimension = new System.Drawing.Imaging.FrameDimension(guid);
        }

        private FrameDimension(System.Drawing.Imaging.FrameDimension frameDimension)
        {
            WrappedFrameDimension = frameDimension;
        }

        /// <summary>
        ///     Gets a globally unique identifier (GUID) that represents this
        ///     <see cref="T:Common.Drawing.Imaging.FrameDimension" /> object.
        /// </summary>
        /// <returns>
        ///     A Guid structure that contains a GUID that represents this
        ///     <see cref="T:Common.Drawing.Imaging.FrameDimension" /> object.
        /// </returns>
        public Guid Guid => WrappedFrameDimension.Guid;

        /// <summary>Gets the page dimension.</summary>
        /// <returns>The page dimension.</returns>
        public static FrameDimension Page => System.Drawing.Imaging.FrameDimension.Page;

        /// <summary>Gets the resolution dimension.</summary>
        /// <returns>The resolution dimension.</returns>
        public static FrameDimension Resolution => System.Drawing.Imaging.FrameDimension.Resolution;

        /// <summary>Gets the time dimension.</summary>
        /// <returns>The time dimension.</returns>
        public static FrameDimension Time => System.Drawing.Imaging.FrameDimension.Time;

        private System.Drawing.Imaging.FrameDimension WrappedFrameDimension { get; }

        /// <summary>
        ///     Returns a value that indicates whether the specified object is a
        ///     <see cref="T:Common.Drawing.Imaging.FrameDimension" /> equivalent to this
        ///     <see cref="T:Common.Drawing.Imaging.FrameDimension" /> object.
        /// </summary>
        /// <returns>
        ///     Returns true if <paramref name="o" /> is a <see cref="T:Common.Drawing.Imaging.FrameDimension" /> equivalent
        ///     to this <see cref="T:Common.Drawing.Imaging.FrameDimension" /> object; otherwise, false.
        /// </returns>
        /// <param name="o">The object to test. </param>
        public override bool Equals(object o)
        {
            return WrappedFrameDimension.Equals(o);
        }

        /// <summary>Returns a hash code for this <see cref="T:Common.Drawing.Imaging.FrameDimension" /> object.</summary>
        /// <returns>
        ///     Returns an int value that is the hash code of this <see cref="T:Common.Drawing.Imaging.FrameDimension" />
        ///     object.
        /// </returns>
        public override int GetHashCode()
        {
            return WrappedFrameDimension.GetHashCode();
        }

        /// <summary>Converts this <see cref="T:Common.Drawing.Imaging.FrameDimension" /> object to a human-readable string.</summary>
        /// <returns>A string that represents this <see cref="T:Common.Drawing.Imaging.FrameDimension" /> object.</returns>
        public override string ToString()
        {
            return WrappedFrameDimension.ToString();
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.FrameDimension" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.FrameDimension" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.FrameDimension" /> that results from the conversion.</returns>
        /// <param name="frameDimension">The <see cref="T:System.Drawing.Imaging.FrameDimension" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator FrameDimension(System.Drawing.Imaging.FrameDimension frameDimension)
        {
            return frameDimension == null ? null : new FrameDimension(frameDimension);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.FrameDimension" /> to a
        ///     <see cref="T:System.Drawing.Imaging.FrameDimension" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.FrameDimension" /> that results from the conversion.</returns>
        /// <param name="frameDimension">The <see cref="T:Common.Drawing.Imaging.FrameDimension" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.FrameDimension(FrameDimension frameDimension)
        {
            return frameDimension?.WrappedFrameDimension;
        }
    }
}