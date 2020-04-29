using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Common.Drawing
{
    /// <summary>
    ///     Represents a Windows icon, which is a small bitmap image that is used to represent an object. Icons can be
    ///     thought of as transparent bitmaps, although their size is determined by the system.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [Serializable]
    public sealed class Icon : MarshalByRefObject, ISerializable, ICloneable, IDisposable
    {
        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Icon" /> class from the specified file name.</summary>
        /// <param name="fileName">The file to load the <see cref="T:Common.Drawing.Icon" /> from. </param>
        public Icon(string fileName)
        {
            WrappedIcon = new System.Drawing.Icon(fileName);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Icon" /> class of the specified size from the
        ///     specified file.
        /// </summary>
        /// <param name="fileName">The name and path to the file that contains the icon data.</param>
        /// <param name="size">The desired size of the icon.</param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="string" /> is null or does not contain image data.</exception>
        public Icon(string fileName, Size size)
        {
            WrappedIcon = new System.Drawing.Icon(fileName, size);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Icon" /> class with the specified width and
        ///     height from the specified file.
        /// </summary>
        /// <param name="fileName">The name and path to the file that contains the <see cref="T:Common.Drawing.Icon" /> data.</param>
        /// <param name="width">The desired width of the <see cref="T:Common.Drawing.Icon" />.</param>
        /// <param name="height">The desired height of the <see cref="T:Common.Drawing.Icon" />.</param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="string" /> is null or does not contain image data.</exception>
        public Icon(string fileName, int width, int height)
        {
            WrappedIcon = new System.Drawing.Icon(fileName, width, height);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Icon" /> class and attempts to find a version of
        ///     the icon that matches the requested size.
        /// </summary>
        /// <param name="original">The <see cref="T:Common.Drawing.Icon" /> from which to load the newly sized icon. </param>
        /// <param name="size">
        ///     A <see cref="T:Common.Drawing.Size" /> structure that specifies the height and width of the new
        ///     <see cref="T:Common.Drawing.Icon" />.
        /// </param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="original" /> parameter is null.</exception>
        public Icon(Icon original, Size size)
        {
            WrappedIcon = new System.Drawing.Icon(original, size);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Icon" /> class and attempts to find a version of
        ///     the icon that matches the requested size.
        /// </summary>
        /// <param name="original">The icon to load the different size from. </param>
        /// <param name="width">The width of the new icon. </param>
        /// <param name="height">The height of the new icon. </param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="original" /> parameter is null.</exception>
        public Icon(Icon original, int width, int height)
        {
            WrappedIcon = new System.Drawing.Icon(original, width, height);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Icon" /> class from a resource in the specified
        ///     assembly.
        /// </summary>
        /// <param name="type">A <see cref="T:System.Type" /> that specifies the assembly in which to look for the resource. </param>
        /// <param name="resource">The resource name to load. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     An icon specified by <paramref name="resource" /> cannot be found in the
        ///     assembly that contains the specified <paramref name="type" />.
        /// </exception>
        public Icon(Type type, string resource)
        {
            WrappedIcon = new System.Drawing.Icon(type, resource);
        }

        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Icon" /> class from the specified data stream.</summary>
        /// <param name="stream">The data stream from which to load the <see cref="T:Common.Drawing.Icon" />. </param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is null.</exception>
        public Icon(Stream stream)
        {
            WrappedIcon = new System.Drawing.Icon(stream);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Icon" /> class of the specified size from the
        ///     specified stream.
        /// </summary>
        /// <param name="stream">The stream that contains the icon data.</param>
        /// <param name="size">The desired size of the icon.</param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> is null or does not contain image data.</exception>
        public Icon(Stream stream, Size size)
        {
            WrappedIcon = new System.Drawing.Icon(stream, size);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Icon" /> class from the specified data stream
        ///     and with the specified width and height.
        /// </summary>
        /// <param name="stream">The data stream from which to load the icon. </param>
        /// <param name="width">The width, in pixels, of the icon. </param>
        /// <param name="height">The height, in pixels, of the icon. </param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is null.</exception>
        public Icon(Stream stream, int width, int height)
        {
            WrappedIcon = new System.Drawing.Icon(stream, width, height);
        }

        private Icon(System.Drawing.Icon icon)
        {
            WrappedIcon = icon;
        }

        private Icon()
        {
        }

        /// <summary>
        ///     Gets the Windows handle for this <see cref="T:Common.Drawing.Icon" />. This is not a copy of the handle; do
        ///     not free it.
        /// </summary>
        /// <returns>The Windows handle for the icon.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public IntPtr Handle => WrappedIcon.Handle;

        /// <summary>Gets the height of this <see cref="T:Common.Drawing.Icon" />.</summary>
        /// <returns>The height of this <see cref="T:Common.Drawing.Icon" />.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Height => WrappedIcon.Height;

        /// <summary>Gets the size of this <see cref="T:Common.Drawing.Icon" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Size" /> structure that specifies the width and height of this
        ///     <see cref="T:Common.Drawing.Icon" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public Size Size => WrappedIcon.Size;

        /// <summary>Gets the width of this <see cref="T:Common.Drawing.Icon" />.</summary>
        /// <returns>The width of this <see cref="T:Common.Drawing.Icon" />.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Width => WrappedIcon.Width;

        private System.Drawing.Icon WrappedIcon { get; }

        /// <summary>Clones the <see cref="T:Common.Drawing.Icon" />, creating a duplicate image.</summary>
        /// <returns>An object that can be cast to an <see cref="T:Common.Drawing.Icon" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public object Clone()
        {
            return new Icon((System.Drawing.Icon) WrappedIcon.Clone());
        }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Icon" />.</summary>
        /// <filterpriority>1</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is required to
        ///     serialize the target object.
        /// </summary>
        /// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">
        ///     The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this
        ///     serialization.
        /// </param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
        {
            (WrappedIcon as ISerializable).GetObjectData(si, context);
        }

        ~Icon()
        {
            Dispose(false);
        }

        /// <summary>Returns an icon representation of an image that is contained in the specified file.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Icon" /> representation of the image that is contained in the specified file.</returns>
        /// <param name="filePath">The path to the file that contains an image.</param>
        /// <exception cref="T:System.ArgumentException">
        ///     The <paramref name="filePath" /> does not indicate a valid file.-or-The
        ///     <paramref name="filePath" /> indicates a Universal Naming Convention (UNC) path.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Icon ExtractAssociatedIcon(string filePath)
        {
            return System.Drawing.Icon.ExtractAssociatedIcon(filePath);
        }

        /// <summary>Creates a GDI+ <see cref="T:Common.Drawing.Icon" /> from the specified Windows handle to an icon (HICON).</summary>
        /// <returns>The <see cref="T:Common.Drawing.Icon" /> this method creates.</returns>
        /// <param name="handle">A Windows handle to an icon. </param>
        /// <filterpriority>1</filterpriority>
        public static Icon FromHandle(IntPtr handle)
        {
            return System.Drawing.Icon.FromHandle(handle);
        }

        /// <summary>Saves this <see cref="T:Common.Drawing.Icon" /> to the specified output <see cref="T:System.IO.Stream" />.</summary>
        /// <param name="outputStream">The <see cref="T:System.IO.Stream" /> to save to. </param>
        /// <filterpriority>1</filterpriority>
        public void Save(Stream outputStream)
        {
            WrappedIcon.Save(outputStream);
        }

        /// <summary>Converts this <see cref="T:Common.Drawing.Icon" /> to a GDI+ <see cref="T:Common.Drawing.Bitmap" />.</summary>
        /// <returns>A <see cref="T:Common.Drawing.Bitmap" /> that represents the converted <see cref="T:Common.Drawing.Icon" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public Bitmap ToBitmap()
        {
            return WrappedIcon.ToBitmap();
        }

        /// <summary>Gets a human-readable string that describes the <see cref="T:Common.Drawing.Icon" />.</summary>
        /// <returns>A string that describes the <see cref="T:Common.Drawing.Icon" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public override string ToString()
        {
            return WrappedIcon.ToString();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedIcon.Dispose();
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Icon" /> to a <see cref="T:Common.Drawing.Icon" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Icon" /> that results from the conversion.</returns>
        /// <param name="icon">The <see cref="T:System.Drawing.Icon" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Icon(System.Drawing.Icon icon)
        {
            return icon == null ? null : new Icon(icon);
        }

        /// <summary>Converts the specified <see cref="T:Common.Drawing.Icon" /> to a <see cref="T:System.Drawing.Icon" />.</summary>
        /// <returns>The <see cref="T:System.Drawing.Icon" /> that results from the conversion.</returns>
        /// <param name="icon">The <see cref="T:Common.Drawing.Icon" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Icon(Icon icon)
        {
            return icon?.WrappedIcon;
        }
    }
}