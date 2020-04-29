using System.Runtime.InteropServices;

namespace Common.Drawing.Imaging
{
    /// <summary>Contains information about a windows-format (WMF) metafile.</summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public sealed class MetaHeader
    {
        private MetaHeader(System.Drawing.Imaging.MetaHeader metaHeader)
        {
            WrappedMetaHeader = metaHeader;
        }

        /// <summary>Gets or sets the size, in bytes, of the header file.</summary>
        /// <returns>The size, in bytes, of the header file.</returns>
        public short HeaderSize
        {
            get => WrappedMetaHeader.HeaderSize;
            set => WrappedMetaHeader.HeaderSize = value;
        }

        /// <summary>
        ///     Gets or sets the size, in bytes, of the largest record in the associated
        ///     <see cref="T:Common.Drawing.Imaging.Metafile" /> object.
        /// </summary>
        /// <returns>
        ///     The size, in bytes, of the largest record in the associated <see cref="T:Common.Drawing.Imaging.Metafile" />
        ///     object.
        /// </returns>
        public int MaxRecord
        {
            get => WrappedMetaHeader.MaxRecord;
            set => WrappedMetaHeader.MaxRecord = value;
        }

        /// <summary>
        ///     Gets or sets the maximum number of objects that exist in the <see cref="T:Common.Drawing.Imaging.Metafile" />
        ///     object at the same time.
        /// </summary>
        /// <returns>
        ///     The maximum number of objects that exist in the <see cref="T:Common.Drawing.Imaging.Metafile" /> object at the
        ///     same time.
        /// </returns>
        public short NoObjects
        {
            get => WrappedMetaHeader.NoObjects;
            set => WrappedMetaHeader.NoObjects = value;
        }

        /// <summary>Not used. Always returns 0.</summary>
        /// <returns>Always 0.</returns>
        public short NoParameters
        {
            get => WrappedMetaHeader.NoParameters;
            set => WrappedMetaHeader.NoParameters = value;
        }

        /// <summary>Gets or sets the size, in bytes, of the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> object.</summary>
        /// <returns>The size, in bytes, of the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> object.</returns>
        public int Size
        {
            get => WrappedMetaHeader.Size;
            set => WrappedMetaHeader.Size = value;
        }

        /// <summary>Gets or sets the type of the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> object.</summary>
        /// <returns>The type of the associated <see cref="T:Common.Drawing.Imaging.Metafile" /> object.</returns>
        public short Type
        {
            get => WrappedMetaHeader.Type;
            set => WrappedMetaHeader.Type = value;
        }

        /// <summary>Gets or sets the version number of the header format.</summary>
        /// <returns>The version number of the header format.</returns>
        public short Version
        {
            get => WrappedMetaHeader.Version;
            set => WrappedMetaHeader.Version = value;
        }

        private System.Drawing.Imaging.MetaHeader WrappedMetaHeader { get; }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.MetaHeader" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.MetaHeader" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.MetaHeader" /> that results from the conversion.</returns>
        /// <param name="metaHeader">The <see cref="T:System.Drawing.Imaging.MetaHeader" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator MetaHeader(System.Drawing.Imaging.MetaHeader metaHeader)
        {
            return metaHeader == null ? null : new MetaHeader(metaHeader);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.MetaHeader" /> to a
        ///     <see cref="T:System.Drawing.Imaging.MetaHeader" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.MetaHeader" /> that results from the conversion.</returns>
        /// <param name="metaHeader">The <see cref="T:Common.Drawing.Imaging.MetaHeader" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.MetaHeader(MetaHeader metaHeader)
        {
            return metaHeader?.WrappedMetaHeader;
        }
    }
}