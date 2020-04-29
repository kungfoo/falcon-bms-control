using System;
using System.Linq;
using Common.Drawing.Text;

namespace Common.Drawing
{
    /// <summary>
    ///     Defines a group of type faces having a similar basic design and certain variations in styles. This class
    ///     cannot be inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    public sealed class FontFamily : MarshalByRefObject, IDisposable
    {
        /// <summary>Initializes a new <see cref="T:Common.Drawing.FontFamily" /> with the specified name.</summary>
        /// <param name="name">The name of the new <see cref="T:Common.Drawing.FontFamily" />. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="name" /> is an empty string ("").-or-<paramref name="name" /> specifies a font that is not
        ///     installed on the computer running the application.-or-<paramref name="name" /> specifies a font that is not a
        ///     TrueType font.
        /// </exception>
        public FontFamily(string name)
        {
            WrappedFontFamily = new System.Drawing.FontFamily(name);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.FontFamily" /> in the specified
        ///     <see cref="T:Common.Drawing.Text.FontCollection" /> with the specified name.
        /// </summary>
        /// <param name="name">
        ///     A <see cref="T:System.String" /> that represents the name of the new
        ///     <see cref="T:Common.Drawing.FontFamily" />.
        /// </param>
        /// <param name="fontCollection">
        ///     The <see cref="T:Common.Drawing.Text.FontCollection" /> that contains this
        ///     <see cref="T:Common.Drawing.FontFamily" />.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="name" /> is an empty string ("").-or-<paramref name="name" /> specifies a font that is not
        ///     installed on the computer running the application.-or-<paramref name="name" /> specifies a font that is not a
        ///     TrueType font.
        /// </exception>
        public FontFamily(string name, FontCollection fontCollection)
        {
            WrappedFontFamily = new System.Drawing.FontFamily(name, fontCollection);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.FontFamily" /> from the specified generic font family.</summary>
        /// <param name="genericFamily">
        ///     The <see cref="T:Common.Drawing.Text.GenericFontFamilies" /> from which to create the new
        ///     <see cref="T:Common.Drawing.FontFamily" />.
        /// </param>
        public FontFamily(GenericFontFamilies genericFamily)
        {
            WrappedFontFamily = new System.Drawing.FontFamily((System.Drawing.Text.GenericFontFamilies) genericFamily);
        }

        private FontFamily(System.Drawing.FontFamily fontFamily)
        {
            WrappedFontFamily = fontFamily;
        }

        /// <summary>
        ///     Returns an array that contains all the <see cref="T:Common.Drawing.FontFamily" /> objects associated with the
        ///     current graphics context.
        /// </summary>
        /// <returns>An array of <see cref="T:Common.Drawing.FontFamily" /> objects associated with the current graphics context.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public static FontFamily[] Families => System.Drawing.FontFamily.Families.Convert<FontFamily>().ToArray();

        /// <summary>Gets a generic monospace <see cref="T:Common.Drawing.FontFamily" />.</summary>
        /// <returns>A <see cref="T:Common.Drawing.FontFamily" /> that represents a generic monospace font.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public static FontFamily GenericMonospace => System.Drawing.FontFamily.GenericMonospace;

        /// <summary>Gets a generic sans serif <see cref="T:Common.Drawing.FontFamily" /> object.</summary>
        /// <returns>A <see cref="T:Common.Drawing.FontFamily" /> object that represents a generic sans serif font.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public static FontFamily GenericSansSerif => System.Drawing.FontFamily.GenericSansSerif;

        /// <summary>Gets a generic serif <see cref="T:Common.Drawing.FontFamily" />.</summary>
        /// <returns>A <see cref="T:Common.Drawing.FontFamily" /> that represents a generic serif font.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public static FontFamily GenericSerif => System.Drawing.FontFamily.GenericSerif;

        /// <summary>Gets the name of this <see cref="T:Common.Drawing.FontFamily" />.</summary>
        /// <returns>A <see cref="T:System.String" /> that represents the name of this <see cref="T:Common.Drawing.FontFamily" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public string Name => WrappedFontFamily.Name;

        private System.Drawing.FontFamily WrappedFontFamily { get; }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.FontFamily" />.</summary>
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

        ~FontFamily()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Indicates whether the specified object is a <see cref="T:Common.Drawing.FontFamily" /> and is identical to
        ///     this <see cref="T:Common.Drawing.FontFamily" />.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="obj" /> is a <see cref="T:Common.Drawing.FontFamily" /> and is identical to this
        ///     <see cref="T:Common.Drawing.FontFamily" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to test. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            return WrappedFontFamily.Equals(obj);
        }

        /// <summary>
        ///     Returns the cell ascent, in design units, of the <see cref="T:Common.Drawing.FontFamily" /> of the specified
        ///     style.
        /// </summary>
        /// <returns>
        ///     The cell ascent for this <see cref="T:Common.Drawing.FontFamily" /> that uses the specified
        ///     <see cref="T:Common.Drawing.FontStyle" />.
        /// </returns>
        /// <param name="style">A <see cref="T:Common.Drawing.FontStyle" /> that contains style information for the font. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int GetCellAscent(FontStyle style)
        {
            return WrappedFontFamily.GetCellAscent((System.Drawing.FontStyle) style);
        }

        /// <summary>
        ///     Returns the cell descent, in design units, of the <see cref="T:Common.Drawing.FontFamily" /> of the specified
        ///     style.
        /// </summary>
        /// <returns>
        ///     The cell descent metric for this <see cref="T:Common.Drawing.FontFamily" /> that uses the specified
        ///     <see cref="T:Common.Drawing.FontStyle" />.
        /// </returns>
        /// <param name="style">A <see cref="T:Common.Drawing.FontStyle" /> that contains style information for the font. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int GetCellDescent(FontStyle style)
        {
            return WrappedFontFamily.GetCellDescent((System.Drawing.FontStyle) style);
        }

        /// <summary>Gets the height, in font design units, of the em square for the specified style.</summary>
        /// <returns>The height of the em square.</returns>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> for which to get the em height. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int GetEmHeight(FontStyle style)
        {
            return WrappedFontFamily.GetEmHeight((System.Drawing.FontStyle) style);
        }

        /// <summary>
        ///     Returns an array that contains all the <see cref="T:Common.Drawing.FontFamily" /> objects available for the
        ///     specified graphics context.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Common.Drawing.FontFamily" /> objects available for the specified
        ///     <see cref="T:Common.Drawing.Graphics" /> object.
        /// </returns>
        /// <param name="graphics">
        ///     The <see cref="T:Common.Drawing.Graphics" /> object from which to return
        ///     <see cref="T:Common.Drawing.FontFamily" /> objects.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="graphics " />isnull.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Obsolete("Do not use method GetFamilies, use property Families instead")]
        public static FontFamily[] GetFamilies(Graphics graphics)
        {
            return System.Drawing.FontFamily.GetFamilies(graphics).Convert<FontFamily>().ToArray();
        }

        /// <summary>Gets a hash code for this <see cref="T:Common.Drawing.FontFamily" />.</summary>
        /// <returns>The hash code for this <see cref="T:Common.Drawing.FontFamily" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public override int GetHashCode()
        {
            return WrappedFontFamily.GetHashCode();
        }

        /// <summary>
        ///     Returns the line spacing, in design units, of the <see cref="T:Common.Drawing.FontFamily" /> of the specified
        ///     style. The line spacing is the vertical distance between the base lines of two consecutive lines of text.
        /// </summary>
        /// <returns>The distance between two consecutive lines of text.</returns>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> to apply. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int GetLineSpacing(FontStyle style)
        {
            return WrappedFontFamily.GetLineSpacing((System.Drawing.FontStyle) style);
        }

        /// <summary>Returns the name, in the specified language, of this <see cref="T:Common.Drawing.FontFamily" />.</summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents the name, in the specified language, of this
        ///     <see cref="T:Common.Drawing.FontFamily" />.
        /// </returns>
        /// <param name="language">The language in which the name is returned. </param>
        /// <filterpriority>1</filterpriority>
        public string GetName(int language)
        {
            return WrappedFontFamily.GetName(language);
        }

        /// <summary>Indicates whether the specified <see cref="T:Common.Drawing.FontStyle" /> enumeration is available.</summary>
        /// <returns>true if the specified <see cref="T:Common.Drawing.FontStyle" /> is available; otherwise, false.</returns>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> to test. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsStyleAvailable(FontStyle style)
        {
            return WrappedFontFamily.IsStyleAvailable((System.Drawing.FontStyle) style);
        }

        /// <summary>Converts this <see cref="T:Common.Drawing.FontFamily" /> to a human-readable string representation.</summary>
        /// <returns>The string that represents this <see cref="T:Common.Drawing.FontFamily" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public override string ToString()
        {
            return WrappedFontFamily.ToString();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedFontFamily.Dispose();
            }
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.FontFamily" /> to a
        ///     <see cref="T:Common.Drawing.FontFamily" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.FontFamily" /> that results from the conversion.</returns>
        /// <param name="fontFamily">The <see cref="T:System.Drawing.FontFamily" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator FontFamily(System.Drawing.FontFamily fontFamily)
        {
            return fontFamily == null ? null : new FontFamily(fontFamily);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.FontFamily" /> to a
        ///     <see cref="T:System.Drawing.FontFamily" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.FontFamily" /> that results from the conversion.</returns>
        /// <param name="fontFamily">The <see cref="T:Common.Drawing.FontFamily" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.FontFamily(FontFamily fontFamily)
        {
            return fontFamily?.WrappedFontFamily;
        }
    }
}