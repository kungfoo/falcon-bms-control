using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Common.Drawing.Imaging;

namespace Common.Drawing
{
    /// <summary>
    ///     An abstract base class that provides functionality for the <see cref="T:Common.Drawing.Bitmap" /> and
    ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> descended classes.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [Serializable]
    public class Image : MarshalByRefObject, ISerializable, ICloneable, IDisposable
    {
        /// <summary>
        ///     Provides a callback method for determining when the
        ///     <see
        ///         cref="M:Common.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,Common.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" />
        ///     method should prematurely cancel execution.
        /// </summary>
        /// <returns>
        ///     This method returns true if it decides that the
        ///     <see
        ///         cref="M:Common.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,Common.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" />
        ///     method should prematurely stop execution; otherwise, it returns false.
        /// </returns>
        public delegate bool GetThumbnailImageAbort();

        protected Image(System.Drawing.Image image)
        {
            WrappedImage = image;
        }

        protected Image()
        {
        }

        /// <summary>Gets attribute flags for the pixel data of this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>
        ///     The integer representing a bitwise combination of <see cref="T:Common.Drawing.Imaging.ImageFlags" /> for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Browsable(false)]
        public int Flags => WrappedImage.Flags;

        /// <summary>
        ///     Gets an array of GUIDs that represent the dimensions of frames within this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </summary>
        /// <returns>
        ///     An array of GUIDs that specify the dimensions of frames within this <see cref="T:Common.Drawing.Image" /> from
        ///     most significant to least significant.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Browsable(false)]
        public Guid[] FrameDimensionsList => WrappedImage.FrameDimensionsList;

        /// <summary>Gets the height, in pixels, of this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>The height, in pixels, of this <see cref="T:Common.Drawing.Image" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Browsable(false)]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Height => WrappedImage.Height;

        /// <summary>Gets the horizontal resolution, in pixels per inch, of this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>The horizontal resolution, in pixels per inch, of this <see cref="T:Common.Drawing.Image" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float HorizontalResolution => WrappedImage.HorizontalResolution;

        /// <summary>Gets or sets the color palette used for this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Imaging.ColorPalette" /> that represents the color palette used for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Browsable(false)]
        public ColorPalette Palette
        {
            get => WrappedImage.Palette;
            set => WrappedImage.Palette = value;
        }

        /// <summary>Gets the width and height of this image.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.SizeF" /> structure that represents the width and height of this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public SizeF PhysicalDimension => WrappedImage.PhysicalDimension;

        /// <summary>Gets the pixel format for this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Imaging.PixelFormat" /> that represents the pixel format for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public PixelFormat PixelFormat => (PixelFormat) WrappedImage.PixelFormat;

        /// <summary>Gets IDs of the property items stored in this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>An array of the property IDs, one for each property item stored in this image.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Browsable(false)]
        public int[] PropertyIdList => WrappedImage.PropertyIdList;

        /// <summary>Gets all the property items (pieces of metadata) stored in this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>
        ///     An array of <see cref="T:Common.Drawing.Imaging.PropertyItem" /> objects, one for each property item stored in
        ///     the image.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Browsable(false)]
        public PropertyItem[] PropertyItems => WrappedImage.PropertyItems.Convert<PropertyItem>().ToArray();

        /// <summary>Gets the file format of this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.Imaging.ImageFormat" /> that represents the file format of this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public ImageFormat RawFormat => WrappedImage.RawFormat;

        /// <summary>Gets the width and height, in pixels, of this image.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Size" /> structure that represents the width and height, in pixels, of this
        ///     image.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Size Size => WrappedImage.Size;

        /// <summary>Gets or sets an object that provides additional data about the image.</summary>
        /// <returns>The <see cref="T:System.Object" /> that provides additional data about the image.</returns>
        /// <filterpriority>1</filterpriority>
        [Bindable(true)]
        [DefaultValue(null)]
        [Localizable(false)]
        [TypeConverter(typeof(StringConverter))]
        public object Tag
        {
            get => WrappedImage.Tag;
            set => WrappedImage.Tag = value;
        }

        /// <summary>Gets the vertical resolution, in pixels per inch, of this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>The vertical resolution, in pixels per inch, of this <see cref="T:Common.Drawing.Image" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float VerticalResolution => WrappedImage.VerticalResolution;

        /// <summary>Gets the width, in pixels, of this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>The width, in pixels, of this <see cref="T:Common.Drawing.Image" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Browsable(false)]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Width => WrappedImage.Width;

        protected System.Drawing.Image WrappedImage { get; set; }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Image" /> this method creates, cast as an object.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public virtual object Clone()
        {
            return new Image((System.Drawing.Image) WrappedImage.Clone());
        }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize
        ///     the target object.
        /// </summary>
        /// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">
        ///     The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this
        ///     serialization.
        /// </param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
        {
            ((ISerializable) WrappedImage).GetObjectData(si, context);
        }

        ~Image()
        {
            Dispose(false);
        }


        /// <summary>Creates an <see cref="T:Common.Drawing.Image" /> from the specified file.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Image" /> this method creates.</returns>
        /// <param name="filename">
        ///     A string that contains the name of the file from which to create the
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <exception cref="T:System.OutOfMemoryException">
        ///     The file does not have a valid image format.-or-GDI+ does not support
        ///     the pixel format of the file.
        /// </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="filename" /> is a <see cref="T:System.Uri" />.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Image FromFile(string filename)
        {
            return System.Drawing.Image.FromFile(filename, false);
        }

        /// <summary>
        ///     Creates an <see cref="T:Common.Drawing.Image" /> from the specified file using embedded color management
        ///     information in that file.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Image" /> this method creates.</returns>
        /// <param name="filename">
        ///     A string that contains the name of the file from which to create the
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <param name="useEmbeddedColorManagement">
        ///     Set to true to use color management information embedded in the image file;
        ///     otherwise, false.
        /// </param>
        /// <exception cref="T:System.OutOfMemoryException">
        ///     The file does not have a valid image format.-or-GDI+ does not support
        ///     the pixel format of the file.
        /// </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="filename" /> is a <see cref="T:System.Uri" />.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Image FromFile(string filename, bool useEmbeddedColorManagement)
        {
            return System.Drawing.Image.FromFile(filename, useEmbeddedColorManagement);
        }

        /// <summary>Creates a <see cref="T:Common.Drawing.Bitmap" /> from a handle to a GDI bitmap.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Bitmap" /> this method creates.</returns>
        /// <param name="hBitmap">The GDI bitmap handle from which to create the <see cref="T:Common.Drawing.Bitmap" />. </param>
        /// <filterpriority>1</filterpriority>
        public static Bitmap FromHbitmap(IntPtr hBitmap)
        {
            return System.Drawing.Image.FromHbitmap(hBitmap);
        }

        /// <summary>Creates a <see cref="T:Common.Drawing.Bitmap" /> from a handle to a GDI bitmap and a handle to a GDI palette.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Bitmap" /> this method creates.</returns>
        /// <param name="hBitmap">The GDI bitmap handle from which to create the <see cref="T:Common.Drawing.Bitmap" />. </param>
        /// <param name="hPalette">
        ///     A handle to a GDI palette used to define the bitmap colors if the bitmap specified in the
        ///     <paramref name="hBitmap" /> parameter is not a device-independent bitmap (DIB).
        /// </param>
        /// <filterpriority>1</filterpriority>
        public static Bitmap FromHbitmap(IntPtr hBitmap, IntPtr hPalette)
        {
            return System.Drawing.Image.FromHbitmap(hBitmap, hPalette);
        }

        /// <summary>Creates an <see cref="T:Common.Drawing.Image" /> from the specified data stream.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Image" /> this method creates.</returns>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     The stream does not have a valid image format-or-
        ///     <paramref name="stream" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Image FromStream(Stream stream)
        {
            return System.Drawing.Image.FromStream(stream);
        }

        /// <summary>
        ///     Creates an <see cref="T:Common.Drawing.Image" /> from the specified data stream, optionally using embedded
        ///     color management information in that stream.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Image" /> this method creates.</returns>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <param name="useEmbeddedColorManagement">
        ///     true to use color management information embedded in the data stream;
        ///     otherwise, false.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     The stream does not have a valid image format -or-
        ///     <paramref name="stream" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Image FromStream(Stream stream, bool useEmbeddedColorManagement)
        {
            return System.Drawing.Image.FromStream(stream, useEmbeddedColorManagement);
        }

        /// <summary>
        ///     Creates an <see cref="T:Common.Drawing.Image" /> from the specified data stream, optionally using embedded
        ///     color management information and validating the image data.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Image" /> this method creates.</returns>
        /// <param name="stream">
        ///     A <see cref="T:System.IO.Stream" /> that contains the data for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <param name="useEmbeddedColorManagement">
        ///     true to use color management information embedded in the data stream;
        ///     otherwise, false.
        /// </param>
        /// <param name="validateImageData">true to validate the image data; otherwise, false.</param>
        /// <exception cref="T:System.ArgumentException">The stream does not have a valid image format.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Image FromStream(Stream stream, bool useEmbeddedColorManagement, bool validateImageData)
        {
            return System.Drawing.Image.FromStream(stream, useEmbeddedColorManagement, validateImageData);
        }

        /// <summary>Gets the bounds of the image in the specified unit.</summary>
        /// <returns>The <see cref="T:Common.Drawing.RectangleF" /> that represents the bounds of the image, in the specified unit.</returns>
        /// <param name="pageUnit">
        ///     One of the <see cref="T:Common.Drawing.GraphicsUnit" /> values indicating the unit of measure
        ///     for the bounding rectangle.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public RectangleF GetBounds(ref GraphicsUnit pageUnit)
        {
            var pageUnitInternal = (System.Drawing.GraphicsUnit) pageUnit;
            var bounds = WrappedImage.GetBounds(ref pageUnitInternal);
            pageUnit = (GraphicsUnit) pageUnitInternal;
            return bounds;
        }

        /// <summary>Returns information about the parameters supported by the specified image encoder.</summary>
        /// <returns>
        ///     An <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> that contains an array of
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> objects. Each
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> contains information about one of the parameters supported
        ///     by the specified image encoder.
        /// </returns>
        /// <param name="encoder">A GUID that specifies the image encoder. </param>
        /// <filterpriority>1</filterpriority>
        public EncoderParameters GetEncoderParameterList(Guid encoder)
        {
            return WrappedImage.GetEncoderParameterList(encoder);
        }

        /// <summary>Returns the number of frames of the specified dimension.</summary>
        /// <returns>The number of frames in the specified dimension.</returns>
        /// <param name="dimension">
        ///     A <see cref="T:Common.Drawing.Imaging.FrameDimension" /> that specifies the identity of the
        ///     dimension type.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int GetFrameCount(FrameDimension dimension)
        {
            return WrappedImage.GetFrameCount(dimension);
        }

        /// <summary>Returns the color depth, in number of bits per pixel, of the specified pixel format.</summary>
        /// <returns>The color depth of the specified pixel format.</returns>
        /// <param name="pixfmt">
        ///     The <see cref="T:Common.Drawing.Imaging.PixelFormat" /> member that specifies the format for which
        ///     to find the size.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public static int GetPixelFormatSize(PixelFormat pixfmt)
        {
            return System.Drawing.Image.GetPixelFormatSize((System.Drawing.Imaging.PixelFormat) pixfmt);
        }

        /// <summary>Gets the specified property item from this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.PropertyItem" /> this method gets.</returns>
        /// <param name="propid">The ID of the property item to get. </param>
        /// <exception cref="T:System.ArgumentException">The image format of this image does not support property items.</exception>
        /// <filterpriority>1</filterpriority>
        public PropertyItem GetPropertyItem(int propid)
        {
            return WrappedImage.GetPropertyItem(propid);
        }

        /// <summary>Returns a thumbnail for this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>An <see cref="T:Common.Drawing.Image" /> that represents the thumbnail.</returns>
        /// <param name="thumbWidth">The width, in pixels, of the requested thumbnail image. </param>
        /// <param name="thumbHeight">The height, in pixels, of the requested thumbnail image. </param>
        /// <param name="callback">
        ///     A <see cref="T:Common.Drawing.Image.GetThumbnailImageAbort" /> delegate. Note   You must create
        ///     a delegate and pass a reference to the delegate as the <paramref name="callback" /> parameter, but the delegate is
        ///     not used.
        /// </param>
        /// <param name="callbackData">Must be <see cref="F:System.IntPtr.Zero" />. </param>
        /// <filterpriority>1</filterpriority>
        public Image GetThumbnailImage(int thumbWidth, int thumbHeight, GetThumbnailImageAbort callback,
            IntPtr callbackData)
        {
            return WrappedImage.GetThumbnailImage(thumbWidth, thumbHeight, () => { return callback(); }, callbackData);
        }

        /// <summary>
        ///     Returns a value that indicates whether the pixel format for this <see cref="T:Common.Drawing.Image" />
        ///     contains alpha information.
        /// </summary>
        /// <returns>true if <paramref name="pixfmt" /> contains alpha information; otherwise, false.</returns>
        /// <param name="pixfmt">The <see cref="T:Common.Drawing.Imaging.PixelFormat" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsAlphaPixelFormat(PixelFormat pixfmt)
        {
            return System.Drawing.Image.IsAlphaPixelFormat((System.Drawing.Imaging.PixelFormat) pixfmt);
        }

        /// <summary>Returns a value that indicates whether the pixel format is 32 bits per pixel.</summary>
        /// <returns>true if <paramref name="pixfmt" /> is canonical; otherwise, false.</returns>
        /// <param name="pixfmt">The <see cref="T:Common.Drawing.Imaging.PixelFormat" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsCanonicalPixelFormat(PixelFormat pixfmt)
        {
            return System.Drawing.Image.IsCanonicalPixelFormat((System.Drawing.Imaging.PixelFormat) pixfmt);
        }

        /// <summary>Returns a value that indicates whether the pixel format is 64 bits per pixel.</summary>
        /// <returns>true if <paramref name="pixfmt" /> is extended; otherwise, false.</returns>
        /// <param name="pixfmt">The <see cref="T:Common.Drawing.Imaging.PixelFormat" /> enumeration to test. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsExtendedPixelFormat(PixelFormat pixfmt)
        {
            return System.Drawing.Image.IsExtendedPixelFormat((System.Drawing.Imaging.PixelFormat) pixfmt);
        }

        /// <summary>Removes the specified property item from this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <param name="propid">The ID of the property item to remove. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     The image does not contain the requested property item.-or-The image
        ///     format for this image does not support property items.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void RemovePropertyItem(int propid)
        {
            WrappedImage.RemovePropertyItem(propid);
        }

        /// <summary>Rotates, flips, or rotates and flips the <see cref="T:Common.Drawing.Image" />.</summary>
        /// <param name="rotateFlipType">
        ///     A <see cref="T:Common.Drawing.RotateFlipType" /> member that specifies the type of
        ///     rotation and flip to apply to the image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void RotateFlip(RotateFlipType rotateFlipType)
        {
            WrappedImage.RotateFlip((System.Drawing.RotateFlipType) rotateFlipType);
        }

        /// <summary>Saves this <see cref="T:Common.Drawing.Image" /> to the specified file or stream.</summary>
        /// <param name="filename">
        ///     A string that contains the name of the file to which to save this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="filename" /> is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.InteropServices.ExternalException">
        ///     The image was saved with the wrong image
        ///     format.-or- The image was saved to the same file it was created from.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void Save(string filename)
        {
            WrappedImage.Save(filename);
        }

        /// <summary>Saves this <see cref="T:Common.Drawing.Image" /> to the specified file in the specified format.</summary>
        /// <param name="filename">
        ///     A string that contains the name of the file to which to save this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <param name="format">
        ///     The <see cref="T:Common.Drawing.Imaging.ImageFormat" /> for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="filename" /> or <paramref name="format" /> is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.InteropServices.ExternalException">
        ///     The image was saved with the wrong image
        ///     format.-or- The image was saved to the same file it was created from.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void Save(string filename, ImageFormat format)
        {
            WrappedImage.Save(filename, format);
        }

        /// <summary>
        ///     Saves this <see cref="T:Common.Drawing.Image" /> to the specified file, with the specified encoder and
        ///     image-encoder parameters.
        /// </summary>
        /// <param name="filename">
        ///     A string that contains the name of the file to which to save this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <param name="encoder">
        ///     The <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <param name="encoderParams">
        ///     An <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> to use for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="filename" /> or <paramref name="encoder" /> is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.InteropServices.ExternalException">
        ///     The image was saved with the wrong image
        ///     format.-or- The image was saved to the same file it was created from.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void Save(string filename, ImageCodecInfo encoder, EncoderParameters encoderParams)
        {
            WrappedImage.Save(filename, encoder, encoderParams);
        }

        /// <summary>Saves this image to the specified stream in the specified format.</summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved. </param>
        /// <param name="format">
        ///     An <see cref="T:Common.Drawing.Imaging.ImageFormat" /> that specifies the format of the saved
        ///     image.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="stream" /> or <paramref name="format" /> is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void Save(Stream stream, ImageFormat format)
        {
            WrappedImage.Save(stream, format);
        }

        /// <summary>Saves this image to the specified stream, with the specified encoder and image encoder parameters.</summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved. </param>
        /// <param name="encoder">
        ///     The <see cref="T:Common.Drawing.Imaging.ImageCodecInfo" /> for this
        ///     <see cref="T:Common.Drawing.Image" />.
        /// </param>
        /// <param name="encoderParams">
        ///     An <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> that specifies parameters used
        ///     by the image encoder.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="stream" /> is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void Save(Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams)
        {
            WrappedImage.Save(stream, encoder, encoderParams);
        }

        /// <summary>
        ///     Adds a frame to the file or stream specified in a previous call to the
        ///     <see cref="Overload:Common.Drawing.Image.Save" /> method. Use this method to save selected frames from a
        ///     multiple-frame image to another multiple-frame image.
        /// </summary>
        /// <param name="encoderParams">
        ///     An <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> that holds parameters required
        ///     by the image encoder that is used by the save-add operation.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SaveAdd(EncoderParameters encoderParams)
        {
            WrappedImage.SaveAdd(encoderParams);
        }

        /// <summary>
        ///     Adds a frame to the file or stream specified in a previous call to the
        ///     <see cref="Overload:Common.Drawing.Image.Save" /> method.
        /// </summary>
        /// <param name="image">An <see cref="T:Common.Drawing.Image" /> that contains the frame to add. </param>
        /// <param name="encoderParams">
        ///     An <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> that holds parameters required
        ///     by the image encoder that is used by the save-add operation.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="image" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SaveAdd(Image image, EncoderParameters encoderParams)
        {
            WrappedImage.SaveAdd(image, encoderParams);
        }

        /// <summary>Selects the frame specified by the dimension and index.</summary>
        /// <returns>Always returns 0.</returns>
        /// <param name="dimension">
        ///     A <see cref="T:Common.Drawing.Imaging.FrameDimension" /> that specifies the identity of the
        ///     dimension type.
        /// </param>
        /// <param name="frameIndex">The index of the active frame. </param>
        /// <filterpriority>1</filterpriority>
        public int SelectActiveFrame(FrameDimension dimension, int frameIndex)
        {
            return WrappedImage.SelectActiveFrame(dimension, frameIndex);
        }

        /// <summary>Stores a property item (piece of metadata) in this <see cref="T:Common.Drawing.Image" />.</summary>
        /// <param name="propitem">The <see cref="T:Common.Drawing.Imaging.PropertyItem" /> to be stored. </param>
        /// <exception cref="T:System.ArgumentException">The image format of this image does not support property items.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetPropertyItem(PropertyItem propitem)
        {
            WrappedImage.SetPropertyItem(propitem);
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the <see cref="T:Common.Drawing.Image" /> and optionally releases the
        ///     managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedImage.Dispose();
            }
        }


        /// <summary>Converts the specified <see cref="T:System.Drawing.Image" /> to a <see cref="T:Common.Drawing.Image" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Image" /> that results from the conversion.</returns>
        /// <param name="image">The <see cref="T:System.Drawing.Image" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Image(System.Drawing.Image image)
        {
            return image == null ? null : new Image(image);
        }

        /// <summary>Converts the specified <see cref="T:Common.Drawing.Image" /> to a <see cref="T:System.Drawing.Image" />.</summary>
        /// <returns>The <see cref="T:System.Drawing.Image" /> that results from the conversion.</returns>
        /// <param name="image">The <see cref="T:Common.Drawing.Image" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Image(Image image)
        {
            return image?.WrappedImage;
        }
    }
}