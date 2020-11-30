using System.Runtime.InteropServices;

namespace Common.Drawing.Imaging
{
    /// <summary>Contains attributes of an associated <see cref="T:Common.Drawing.Imaging.Metafile" />. Not inheritable.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MetafileHeader
    {
        private MetafileHeader(System.Drawing.Imaging.MetafileHeader metafileHeader)
        {
            WrappedMetafileHeader = metafileHeader;
        }

        private MetafileHeader()
        {
        }

        /// <summary>
        ///     Gets a <see cref="T:Common.Drawing.Rectangle" /> that bounds the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Rectangle" /> that bounds the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Rectangle Bounds => WrappedMetafileHeader.Bounds;

        /// <summary>
        ///     Gets the horizontal resolution, in dots per inch, of the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     The horizontal resolution, in dots per inch, of the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float DpiX => WrappedMetafileHeader.DpiX;

        /// <summary>
        ///     Gets the vertical resolution, in dots per inch, of the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>The vertical resolution, in dots per inch, of the associated <see cref="T:Common.Drawing.Imaging.Metafile" />.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float DpiY => WrappedMetafileHeader.DpiY;

        /// <summary>Gets the size, in bytes, of the enhanced metafile plus header file.</summary>
        /// <returns>The size, in bytes, of the enhanced metafile plus header file.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int EmfPlusHeaderSize => WrappedMetafileHeader.EmfPlusHeaderSize;

        /// <summary>
        ///     Gets the logical horizontal resolution, in dots per inch, of the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     The logical horizontal resolution, in dots per inch, of the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int LogicalDpiX => WrappedMetafileHeader.LogicalDpiX;

        /// <summary>
        ///     Gets the logical vertical resolution, in dots per inch, of the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     The logical vertical resolution, in dots per inch, of the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int LogicalDpiY => WrappedMetafileHeader.LogicalDpiY;

        /// <summary>Gets the size, in bytes, of the associated <see cref="T:Common.Drawing.Imaging.Metafile" />.</summary>
        /// <returns>The size, in bytes, of the associated <see cref="T:Common.Drawing.Imaging.Metafile" />.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int MetafileSize => WrappedMetafileHeader.MetafileSize;

        /// <summary>Gets the type of the associated <see cref="T:Common.Drawing.Imaging.Metafile" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Imaging.MetafileType" /> enumeration that represents the type of the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public MetafileType Type => (MetafileType) WrappedMetafileHeader.Type;

        /// <summary>Gets the version number of the associated <see cref="T:Common.Drawing.Imaging.Metafile" />.</summary>
        /// <returns>The version number of the associated <see cref="T:Common.Drawing.Imaging.Metafile" />.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int Version => WrappedMetafileHeader.Version;

        /// <summary>
        ///     Gets the Windows metafile (WMF) header file for the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Imaging.MetaHeader" /> that contains the WMF header file for the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public MetaHeader WmfHeader => WrappedMetafileHeader.WmfHeader;

        private System.Drawing.Imaging.MetafileHeader WrappedMetafileHeader { get; }

        /// <summary>
        ///     Returns a value that indicates whether the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is
        ///     device dependent.
        /// </summary>
        /// <returns>true if the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is device dependent; otherwise, false.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsDisplay()
        {
            return WrappedMetafileHeader.IsDisplay();
        }

        /// <summary>
        ///     Returns a value that indicates whether the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in
        ///     the Windows enhanced metafile format.
        /// </summary>
        /// <returns>
        ///     true if the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile
        ///     format; otherwise, false.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsEmf()
        {
            return WrappedMetafileHeader.IsEmf();
        }

        /// <summary>
        ///     Returns a value that indicates whether the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in
        ///     the Windows enhanced metafile format or the Windows enhanced metafile plus format.
        /// </summary>
        /// <returns>
        ///     true if the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile
        ///     format or the Windows enhanced metafile plus format; otherwise, false.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsEmfOrEmfPlus()
        {
            return WrappedMetafileHeader.IsEmfOrEmfPlus();
        }

        /// <summary>
        ///     Returns a value that indicates whether the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in
        ///     the Windows enhanced metafile plus format.
        /// </summary>
        /// <returns>
        ///     true if the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile
        ///     plus format; otherwise, false.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsEmfPlus()
        {
            return WrappedMetafileHeader.IsEmfPlus();
        }

        /// <summary>
        ///     Returns a value that indicates whether the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in
        ///     the Dual enhanced metafile format. This format supports both the enhanced and the enhanced plus format.
        /// </summary>
        /// <returns>
        ///     true if the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in the Dual enhanced metafile
        ///     format; otherwise, false.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsEmfPlusDual()
        {
            return WrappedMetafileHeader.IsEmfPlusDual();
        }

        /// <summary>
        ///     Returns a value that indicates whether the associated <see cref="T:Common.Drawing.Imaging.Metafile" />
        ///     supports only the Windows enhanced metafile plus format.
        /// </summary>
        /// <returns>
        ///     true if the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> supports only the Windows enhanced
        ///     metafile plus format; otherwise, false.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsEmfPlusOnly()
        {
            return WrappedMetafileHeader.IsEmfPlusOnly();
        }

        /// <summary>
        ///     Returns a value that indicates whether the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in
        ///     the Windows metafile format.
        /// </summary>
        /// <returns>
        ///     true if the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in the Windows metafile format;
        ///     otherwise, false.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsWmf()
        {
            return WrappedMetafileHeader.IsWmf();
        }

        /// <summary>
        ///     Returns a value that indicates whether the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in
        ///     the Windows placeable metafile format.
        /// </summary>
        /// <returns>
        ///     true if the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> is in the Windows placeable metafile
        ///     format; otherwise, false.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsWmfPlaceable()
        {
            return WrappedMetafileHeader.IsWmfPlaceable();
        }


        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.MetafileHeader" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.MetafileHeader" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> that results from the conversion.</returns>
        /// <param name="metafileHeader">The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator MetafileHeader(System.Drawing.Imaging.MetafileHeader metafileHeader)
        {
            return metafileHeader == null ? null : new MetafileHeader(metafileHeader);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> to a
        ///     <see cref="T:System.Drawing.Imaging.MetafileHeader" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> that results from the conversion.</returns>
        /// <param name="metafileHeader">The <see cref="T:Common.Drawing.Imaging.MetafileHeader" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.MetafileHeader(MetafileHeader metafileHeader)
        {
            return metafileHeader?.WrappedMetafileHeader;
        }
    }
}