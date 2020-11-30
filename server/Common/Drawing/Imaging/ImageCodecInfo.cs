using System;
using System.Linq;

namespace Common.Drawing.Imaging
{
    /// <summary>
    ///     The <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> class provides the necessary storage members and
    ///     methods to retrieve all pertinent information about the installed image encoders and decoders (called codecs). Not
    ///     inheritable.
    /// </summary>
    public sealed class ImageCodecInfo
    {
        private ImageCodecInfo(System.Drawing.Imaging.ImageCodecInfo imageCodecInfo)
        {
            WrappedImageCodecInfo = imageCodecInfo;
        }

        private ImageCodecInfo()
        {
        }

        /// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</summary>
        /// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</returns>
        public Guid Clsid
        {
            get => WrappedImageCodecInfo.Clsid;
            set => WrappedImageCodecInfo.Clsid = value;
        }

        /// <summary>Gets or sets a string that contains the name of the codec.</summary>
        /// <returns>A string that contains the name of the codec.</returns>
        public string CodecName
        {
            get => WrappedImageCodecInfo.CodecName;
            set => WrappedImageCodecInfo.CodecName = value;
        }

        /// <summary>
        ///     Gets or sets string that contains the path name of the DLL that holds the codec. If the codec is not in a DLL,
        ///     this pointer is null.
        /// </summary>
        /// <returns>A string that contains the path name of the DLL that holds the codec.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public string DllName
        {
            get => WrappedImageCodecInfo.DllName;
            set => WrappedImageCodecInfo.DllName = value;
        }

        /// <summary>
        ///     Gets or sets string that contains the file name extension(s) used in the codec. The extensions are separated
        ///     by semicolons.
        /// </summary>
        /// <returns>A string that contains the file name extension(s) used in the codec.</returns>
        public string FilenameExtension
        {
            get => WrappedImageCodecInfo.FilenameExtension;
            set => WrappedImageCodecInfo.FilenameExtension = value;
        }

        /// <summary>
        ///     Gets or sets 32-bit value used to store additional information about the codec. This property returns a
        ///     combination of flags from the <see cref="T:Common.Drawing.Imaging.ImageCodecFlags" /> enumeration.
        /// </summary>
        /// <returns>A 32-bit value used to store additional information about the codec.</returns>
        public ImageCodecFlags Flags
        {
            get => (ImageCodecFlags) WrappedImageCodecInfo.Flags;
            set => WrappedImageCodecInfo.Flags = (System.Drawing.Imaging.ImageCodecFlags) value;
        }

        /// <summary>Gets or sets a string that describes the codec's file format.</summary>
        /// <returns>A string that describes the codec's file format.</returns>
        public string FormatDescription
        {
            get => WrappedImageCodecInfo.FormatDescription;
            set => WrappedImageCodecInfo.FormatDescription = value;
        }

        /// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</summary>
        /// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</returns>
        public Guid FormatID
        {
            get => WrappedImageCodecInfo.FormatID;
            set => WrappedImageCodecInfo.FormatID = value;
        }

        /// <summary>Gets or sets a string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</summary>
        /// <returns>A string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</returns>
        public string MimeType
        {
            get => WrappedImageCodecInfo.MimeType;
            set => WrappedImageCodecInfo.MimeType = value;
        }

        /// <summary>Gets or sets a two dimensional array of bytes that can be used as a filter.</summary>
        /// <returns>A two dimensional array of bytes that can be used as a filter.</returns>
        [CLSCompliant(false)]
        public byte[][] SignatureMasks
        {
            get => WrappedImageCodecInfo.SignatureMasks;
            set => WrappedImageCodecInfo.SignatureMasks = value;
        }

        /// <summary>Gets or sets a two dimensional array of bytes that represents the signature of the codec.</summary>
        /// <returns>A two dimensional array of bytes that represents the signature of the codec.</returns>
        [CLSCompliant(false)]
        public byte[][] SignaturePatterns
        {
            get => WrappedImageCodecInfo.SignaturePatterns;
            set => WrappedImageCodecInfo.SignaturePatterns = value;
        }

        /// <summary>Gets or sets the version number of the codec.</summary>
        /// <returns>The version number of the codec.</returns>
        public int Version
        {
            get => WrappedImageCodecInfo.Version;
            set => WrappedImageCodecInfo.Version = value;
        }

        private System.Drawing.Imaging.ImageCodecInfo WrappedImageCodecInfo { get; }

        /// <summary>
        ///     Returns an array of <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> objects that contain information
        ///     about the image decoders built into GDI+.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> objects. Each
        ///     <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> object in the array contains information about one of the
        ///     built-in image decoders.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public static ImageCodecInfo[] GetImageDecoders()
        {
            return System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders().Convert<ImageCodecInfo>().ToArray();
        }

        /// <summary>
        ///     Returns an array of <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> objects that contain information
        ///     about the image encoders built into GDI+.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> objects. Each
        ///     <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> object in the array contains information about one of the
        ///     built-in image encoders.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public static ImageCodecInfo[] GetImageEncoders()
        {
            return System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders().Convert<ImageCodecInfo>().ToArray();
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> that results from the conversion.</returns>
        /// <param name="imageCodecInfo">The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator ImageCodecInfo(System.Drawing.Imaging.ImageCodecInfo imageCodecInfo)
        {
            return imageCodecInfo == null ? null : new ImageCodecInfo(imageCodecInfo);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> to a
        ///     <see cref="T:System.Drawing.Imaging.ImageCodecInfo" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> that results from the conversion.</returns>
        /// <param name="imageCodecInfo">The <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.ImageCodecInfo(ImageCodecInfo imageCodecInfo)
        {
            return imageCodecInfo?.WrappedImageCodecInfo;
        }
    }
}