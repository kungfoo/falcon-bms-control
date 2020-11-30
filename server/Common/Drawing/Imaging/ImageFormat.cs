using System;

namespace Common.Drawing.Imaging
{
    /// <summary>Specifies the file format of the image. Not inheritable.</summary>
    public sealed class ImageFormat
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.ImageFormat" /> class by using the
        ///     specified <see cref="T:System.Guid" /> structure.
        /// </summary>
        /// <param name="guid">The <see cref="T:System.Guid" /> structure that specifies a particular image format. </param>
        public ImageFormat(Guid guid)
        {
            WrappedImageFormat = new System.Drawing.Imaging.ImageFormat(guid);
        }

        private ImageFormat(System.Drawing.Imaging.ImageFormat imageFormat)
        {
            WrappedImageFormat = imageFormat;
        }

        /// <summary>Gets the bitmap (BMP) image format.</summary>
        /// <returns>An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the bitmap image format.</returns>
        public static ImageFormat Bmp => System.Drawing.Imaging.ImageFormat.Bmp;

        /// <summary>Gets the enhanced metafile (EMF) image format.</summary>
        /// <returns>
        ///     An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the enhanced metafile image
        ///     format.
        /// </returns>
        public static ImageFormat Emf => System.Drawing.Imaging.ImageFormat.Emf;

        /// <summary>Gets the Exchangeable Image File (Exif) format.</summary>
        /// <returns>An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the Exif format.</returns>
        public static ImageFormat Exif => System.Drawing.Imaging.ImageFormat.Exif;

        /// <summary>Gets the Graphics Interchange Format (GIF) image format.</summary>
        /// <returns>An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the GIF image format.</returns>
        public static ImageFormat Gif => System.Drawing.Imaging.ImageFormat.Gif;

        /// <summary>
        ///     Gets a <see cref="T:System.Guid" /> structure that represents this
        ///     <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Guid" /> structure that represents this
        ///     <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object.
        /// </returns>
        public Guid Guid => WrappedImageFormat.Guid;

        /// <summary>Gets the Windows icon image format.</summary>
        /// <returns>An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the Windows icon image format.</returns>
        public static ImageFormat Icon => System.Drawing.Imaging.ImageFormat.Icon;

        /// <summary>Gets the Joint Photographic Experts Group (JPEG) image format.</summary>
        /// <returns>An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the JPEG image format.</returns>
        public static ImageFormat Jpeg => System.Drawing.Imaging.ImageFormat.Jpeg;

        /// <summary>Gets the format of a bitmap in memory.</summary>
        /// <returns>An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the format of a bitmap in memory.</returns>
        public static ImageFormat MemoryBmp => System.Drawing.Imaging.ImageFormat.MemoryBmp;

        /// <summary>Gets the W3C Portable Network Graphics (PNG) image format.</summary>
        /// <returns>An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the PNG image format.</returns>
        public static ImageFormat Png => System.Drawing.Imaging.ImageFormat.Png;

        /// <summary>Gets the Tagged Image File Format (TIFF) image format.</summary>
        /// <returns>An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the TIFF image format.</returns>
        public static ImageFormat Tiff => System.Drawing.Imaging.ImageFormat.Tiff;

        /// <summary>Gets the Windows metafile (WMF) image format.</summary>
        /// <returns>
        ///     An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that indicates the Windows metafile image
        ///     format.
        /// </returns>
        public static ImageFormat Wmf => System.Drawing.Imaging.ImageFormat.Wmf;

        private System.Drawing.Imaging.ImageFormat WrappedImageFormat { get; }

        /// <summary>
        ///     Returns a value that indicates whether the specified object is an
        ///     <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that is equivalent to this
        ///     <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="o" /> is an <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object that is
        ///     equivalent to this <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object; otherwise, false.
        /// </returns>
        /// <param name="o">The object to test. </param>
        public override bool Equals(object o)
        {
            return WrappedImageFormat.Equals(o);
        }

        /// <summary>Returns a hash code value that represents this object.</summary>
        /// <returns>A hash code that represents this object.</returns>
        public override int GetHashCode()
        {
            return WrappedImageFormat.GetHashCode();
        }


        /// <summary>Converts this <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object to a human-readable string.</summary>
        /// <returns>A string that represents this <see cref="T:Common.Drawing.Imaging.ImageFormat" /> object.</returns>
        public override string ToString()
        {
            return WrappedImageFormat.ToString();
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.ImageFormat" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.ImageFormat" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.ImageFormat" /> that results from the conversion.</returns>
        /// <param name="imageFormat">The <see cref="T:System.Drawing.Imaging.ImageFormat" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator ImageFormat(System.Drawing.Imaging.ImageFormat imageFormat)
        {
            return imageFormat == null ? null : new ImageFormat(imageFormat);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.ImageFormat" /> to a
        ///     <see cref="T:System.Drawing.Imaging.ImageFormat" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.ImageFormat" /> that results from the conversion.</returns>
        /// <param name="imageFormat">The <see cref="T:Common.Drawing.Imaging.ImageFormat" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.ImageFormat(ImageFormat imageFormat)
        {
            return imageFormat?.WrappedImageFormat;
        }
    }
}