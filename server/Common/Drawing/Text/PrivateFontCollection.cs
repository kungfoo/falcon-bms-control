using System;

namespace Common.Drawing.Text
{
    /// <summary>Provides a collection of font families built from font files that are provided by the client application.</summary>
    public sealed class PrivateFontCollection : FontCollection
    {
        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Text.PrivateFontCollection" /> class. </summary>
        public PrivateFontCollection()
        {
            WrappedPrivateFontCollection = new System.Drawing.Text.PrivateFontCollection();
        }

        private PrivateFontCollection(System.Drawing.Text.PrivateFontCollection privateFontCollection)
        {
            WrappedPrivateFontCollection = privateFontCollection;
        }

        private System.Drawing.Text.PrivateFontCollection WrappedPrivateFontCollection
        {
            get => WrappedFontCollection as System.Drawing.Text.PrivateFontCollection;
            set => WrappedFontCollection = value;
        }

        /// <summary>Adds a font from the specified file to this <see cref="T:Common.Drawing.Text.PrivateFontCollection" />. </summary>
        /// <param name="filename">A <see cref="T:System.String" /> that contains the file name of the font to add. </param>
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The specified font is not supported or the font file cannot be
        ///     found.
        /// </exception>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void AddFontFile(string filename)
        {
            WrappedPrivateFontCollection.AddFontFile(filename);
        }

        /// <summary>Adds a font contained in system memory to this <see cref="T:Common.Drawing.Text.PrivateFontCollection" />.</summary>
        /// <param name="memory">The memory address of the font to add. </param>
        /// <param name="length">The memory length of the font to add. </param>
        public void AddMemoryFont(IntPtr memory, int length)
        {
            WrappedPrivateFontCollection.AddMemoryFont(memory, length);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedPrivateFontCollection.Dispose();
            }
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Text.PrivateFontCollection" /> to a
        ///     <see cref="T:Common.Drawing.Text.PrivateFontCollection" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Text.PrivateFontCollection" /> that results from the conversion.</returns>
        /// <param name="privateFontCollection">The <see cref="T:System.Drawing.Text.PrivateFontCollection" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator PrivateFontCollection(
            System.Drawing.Text.PrivateFontCollection privateFontCollection)
        {
            return privateFontCollection == null ? null : new PrivateFontCollection(privateFontCollection);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Text.PrivateFontCollection" /> to a
        ///     <see cref="T:System.Drawing.Text.PrivateFontCollection" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Text.PrivateFontCollection" /> that results from the conversion.</returns>
        /// <param name="privateFontCollection">The <see cref="T:Common.Drawing.Text.PrivateFontCollection" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Text.PrivateFontCollection(
            PrivateFontCollection privateFontCollection)
        {
            return privateFontCollection?.WrappedPrivateFontCollection;
        }
    }
}