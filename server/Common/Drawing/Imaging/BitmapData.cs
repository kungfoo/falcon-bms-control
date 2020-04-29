using System;
using System.Runtime.InteropServices;

namespace Common.Drawing.Imaging
{
    /// <summary>
    ///     Specifies the attributes of a bitmap image. The <see cref="T:Common.Drawing.Imaging.BitmapData" /> class is
    ///     used by the <see cref="Overload:Common.Drawing.Bitmap.LockBits" /> and
    ///     <see cref="M:Common.Drawing.Bitmap.UnlockBits(Common.Drawing.Imaging.BitmapData)" /> methods of the
    ///     <see cref="T:Common.Drawing.Bitmap" /> class. Not inheritable.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class BitmapData
    {
        private readonly System.Drawing.Imaging.BitmapData WrappedBitmapData;

        private BitmapData(System.Drawing.Imaging.BitmapData bitmapData)
        {
            WrappedBitmapData = bitmapData;
        }

        /// <summary>
        ///     Gets or sets the pixel height of the <see cref="T:Common.Drawing.Bitmap" /> object. Also sometimes referred to
        ///     as the number of scan lines.
        /// </summary>
        /// <returns>The pixel height of the <see cref="T:Common.Drawing.Bitmap" /> object.</returns>
        public int Height
        {
            get => WrappedBitmapData.Height;
            set => WrappedBitmapData.Height = value;
        }

        /// <summary>
        ///     Gets or sets the format of the pixel information in the <see cref="T:Common.Drawing.Bitmap" /> object that
        ///     returned this <see cref="T:Common.Drawing.Imaging.BitmapData" /> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Imaging.PixelFormat" /> that specifies the format of the pixel information in
        ///     the associated <see cref="T:Common.Drawing.Bitmap" /> object.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PixelFormat PixelFormat
        {
            get => (PixelFormat) WrappedBitmapData.PixelFormat;
            set => WrappedBitmapData.PixelFormat = (System.Drawing.Imaging.PixelFormat) value;
        }

        /// <summary>Reserved. Do not use.</summary>
        /// <returns>Reserved. Do not use.</returns>
        public int Reserved
        {
            get => WrappedBitmapData.Reserved;
            set => WrappedBitmapData.Reserved = value;
        }

        /// <summary>
        ///     Gets or sets the address of the first pixel data in the bitmap. This can also be thought of as the first scan
        ///     line in the bitmap.
        /// </summary>
        /// <returns>The address of the first pixel data in the bitmap.</returns>
        public IntPtr Scan0
        {
            get => WrappedBitmapData.Scan0;
            set => WrappedBitmapData.Scan0 = value;
        }

        /// <summary>Gets or sets the stride width (also called scan width) of the <see cref="T:Common.Drawing.Bitmap" /> object.</summary>
        /// <returns>The stride width, in bytes, of the <see cref="T:Common.Drawing.Bitmap" /> object.</returns>
        public int Stride
        {
            get => WrappedBitmapData.Stride;
            set => WrappedBitmapData.Stride = value;
        }

        /// <summary>
        ///     Gets or sets the pixel width of the <see cref="T:Common.Drawing.Bitmap" /> object. This can also be thought of
        ///     as the number of pixels in one scan line.
        /// </summary>
        /// <returns>The pixel width of the <see cref="T:Common.Drawing.Bitmap" /> object.</returns>
        public int Width
        {
            get => WrappedBitmapData.Width;
            set => WrappedBitmapData.Width = value;
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.BitmapData" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.BitmapData" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.BitmapData" /> that results from the conversion.</returns>
        /// <param name="bitmapData">The <see cref="T:System.Drawing.Imaging.BitmapData" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator BitmapData(System.Drawing.Imaging.BitmapData bitmapData)
        {
            return bitmapData == null ? null : new BitmapData(bitmapData);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.BitmapData" /> to a
        ///     <see cref="T:System.Drawing.Imaging.BitmapData" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.BitmapData" /> that results from the conversion.</returns>
        /// <param name="bitmapData">The <see cref="T:Common.Drawing.Imaging.BitmapData" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.BitmapData(BitmapData bitmapData)
        {
            return bitmapData?.WrappedBitmapData;
        }
    }
}