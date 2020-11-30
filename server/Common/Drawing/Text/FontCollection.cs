using System;
using System.Linq;

namespace Common.Drawing.Text
{
    /// <summary>Provides a base class for installed and private font collections. </summary>
    public class FontCollection : IDisposable
    {
        protected FontCollection(System.Drawing.Text.FontCollection fontCollection)
        {
            WrappedFontCollection = fontCollection;
        }

        internal FontCollection()
        {
        }

        /// <summary>
        ///     Gets the array of <see cref="T:Common.Drawing.FontFamily" /> objects associated with this
        ///     <see cref="T:Common.Drawing.Text.FontCollection" />.
        /// </summary>
        /// <returns>An array of <see cref="T:Common.Drawing.FontFamily" /> objects.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public FontFamily[] Families => WrappedFontCollection.Families.Convert<FontFamily>().ToArray();

        protected System.Drawing.Text.FontCollection WrappedFontCollection { get; set; }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Text.FontCollection" />.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FontCollection()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the <see cref="T:Common.Drawing.Text.FontCollection" /> and
        ///     optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedFontCollection.Dispose();
            }
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Text.FontCollection" /> to a
        ///     <see cref="T:Common.Drawing.Text.FontCollection" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Text.FontCollection" /> that results from the conversion.</returns>
        /// <param name="fontCollection">The <see cref="T:System.Drawing.Text.FontCollection" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator FontCollection(System.Drawing.Text.FontCollection fontCollection)
        {
            return fontCollection == null ? null : new FontCollection(fontCollection);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Text.FontCollection" /> to a
        ///     <see cref="T:System.Drawing.Text.FontCollection" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Text.FontCollection" /> that results from the conversion.</returns>
        /// <param name="fontCollection">The <see cref="T:Common.Drawing.Text.FontCollection" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Text.FontCollection(FontCollection fontCollection)
        {
            return fontCollection?.WrappedFontCollection;
        }
    }
}