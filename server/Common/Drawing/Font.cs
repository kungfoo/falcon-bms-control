using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Common.Drawing
{
    /// <summary>
    ///     Defines a particular format for text, including font face, size, and style attributes. This class cannot be
    ///     inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [Serializable]
    public sealed class Font : MarshalByRefObject, ICloneable, ISerializable, IDisposable
    {
        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.Font" /> that uses the specified existing
        ///     <see cref="T:Common.Drawing.Font" /> and <see cref="T:Common.Drawing.FontStyle" /> enumeration.
        /// </summary>
        /// <param name="prototype">
        ///     The existing <see cref="T:Common.Drawing.Font" /> from which to create the new
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </param>
        /// <param name="newStyle">
        ///     The <see cref="T:Common.Drawing.FontStyle" /> to apply to the new
        ///     <see cref="T:Common.Drawing.Font" />. Multiple values of the <see cref="T:Common.Drawing.FontStyle" /> enumeration
        ///     can be combined with the OR operator.
        /// </param>
        public Font(Font prototype, FontStyle newStyle)
        {
            WrappedFont = new System.Drawing.Font(prototype, (System.Drawing.FontStyle) newStyle);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size, style, and unit.</summary>
        /// <param name="family">The <see cref="T:Common.Drawing.FontFamily" /> of the new <see cref="T:Common.Drawing.Font" />. </param>
        /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> of the new font. </param>
        /// <param name="unit">The <see cref="T:Common.Drawing.GraphicsUnit" /> of the new font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="family" /> is null.
        /// </exception>
        public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit)
        {
            WrappedFont = new System.Drawing.Font(family, emSize, (System.Drawing.FontStyle) style,
                (System.Drawing.GraphicsUnit) unit);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
        /// <param name="family">The <see cref="T:Common.Drawing.FontFamily" /> of the new <see cref="T:Common.Drawing.Font" />. </param>
        /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> of the new font. </param>
        /// <param name="unit">The <see cref="T:Common.Drawing.GraphicsUnit" /> of the new font. </param>
        /// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for the new font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="family" /> is null.
        /// </exception>
        public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            WrappedFont = new System.Drawing.Font(family, emSize, (System.Drawing.FontStyle) style,
                (System.Drawing.GraphicsUnit) unit, gdiCharSet);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
        /// <param name="family">The <see cref="T:Common.Drawing.FontFamily" /> of the new <see cref="T:Common.Drawing.Font" />. </param>
        /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> of the new font. </param>
        /// <param name="unit">The <see cref="T:Common.Drawing.GraphicsUnit" /> of the new font. </param>
        /// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font. </param>
        /// <param name="gdiVerticalFont">A Boolean value indicating whether the new font is derived from a GDI vertical font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="family" /> is null
        /// </exception>
        public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet,
            bool gdiVerticalFont)
        {
            WrappedFont = new System.Drawing.Font(family, emSize, (System.Drawing.FontStyle) style,
                (System.Drawing.GraphicsUnit) unit, gdiCharSet, gdiVerticalFont);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
        /// <param name="familyName">
        ///     A string representation of the <see cref="T:Common.Drawing.FontFamily" /> for the new
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </param>
        /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> of the new font. </param>
        /// <param name="unit">The <see cref="T:Common.Drawing.GraphicsUnit" /> of the new font. </param>
        /// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            WrappedFont = new System.Drawing.Font(familyName, emSize, (System.Drawing.FontStyle) style,
                (System.Drawing.GraphicsUnit) unit, gdiCharSet);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.Font" /> using the specified size, style, unit, and character
        ///     set.
        /// </summary>
        /// <param name="familyName">
        ///     A string representation of the <see cref="T:Common.Drawing.FontFamily" /> for the new
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </param>
        /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> of the new font. </param>
        /// <param name="unit">The <see cref="T:Common.Drawing.GraphicsUnit" /> of the new font. </param>
        /// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font. </param>
        /// <param name="gdiVerticalFont">
        ///     A Boolean value indicating whether the new <see cref="T:Common.Drawing.Font" /> is
        ///     derived from a GDI vertical font.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet,
            bool gdiVerticalFont)
        {
            WrappedFont = new System.Drawing.Font(familyName, emSize, (System.Drawing.FontStyle) style,
                (System.Drawing.GraphicsUnit) unit, gdiCharSet, gdiVerticalFont);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size and style. </summary>
        /// <param name="family">The <see cref="T:Common.Drawing.FontFamily" /> of the new <see cref="T:Common.Drawing.Font" />. </param>
        /// <param name="emSize">The em-size, in points, of the new font. </param>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> of the new font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="family" /> is null.
        /// </exception>
        public Font(FontFamily family, float emSize, FontStyle style)
        {
            WrappedFont = new System.Drawing.Font(family, emSize, (System.Drawing.FontStyle) style);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size and unit. Sets the style to
        ///     <see cref="F:Common.Drawing.FontStyle.Regular" />.
        /// </summary>
        /// <param name="family">The <see cref="T:Common.Drawing.FontFamily" /> of the new <see cref="T:Common.Drawing.Font" />. </param>
        /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
        /// <param name="unit">The <see cref="T:Common.Drawing.GraphicsUnit" /> of the new font. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="family" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        public Font(FontFamily family, float emSize, GraphicsUnit unit)
        {
            WrappedFont = new System.Drawing.Font(family, emSize, (System.Drawing.GraphicsUnit) unit);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size. </summary>
        /// <param name="family">The <see cref="T:Common.Drawing.FontFamily" /> of the new <see cref="T:Common.Drawing.Font" />. </param>
        /// <param name="emSize">The em-size, in points, of the new font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        public Font(FontFamily family, float emSize)
        {
            WrappedFont = new System.Drawing.Font(family, emSize);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size, style, and unit.</summary>
        /// <param name="familyName">
        ///     A string representation of the <see cref="T:Common.Drawing.FontFamily" /> for the new
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </param>
        /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> of the new font. </param>
        /// <param name="unit">The <see cref="T:Common.Drawing.GraphicsUnit" /> of the new font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number.
        /// </exception>
        public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit)
        {
            WrappedFont = new System.Drawing.Font(familyName, emSize, (System.Drawing.FontStyle) style,
                (System.Drawing.GraphicsUnit) unit);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size and style. </summary>
        /// <param name="familyName">
        ///     A string representation of the <see cref="T:Common.Drawing.FontFamily" /> for the new
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </param>
        /// <param name="emSize">The em-size, in points, of the new font. </param>
        /// <param name="style">The <see cref="T:Common.Drawing.FontStyle" /> of the new font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        public Font(string familyName, float emSize, FontStyle style)
        {
            WrappedFont = new System.Drawing.Font(familyName, emSize, (System.Drawing.FontStyle) style);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size and unit. The style is set to
        ///     <see cref="F:Common.Drawing.FontStyle.Regular" />.
        /// </summary>
        /// <param name="familyName">
        ///     A string representation of the <see cref="T:Common.Drawing.FontFamily" /> for the new
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </param>
        /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
        /// <param name="unit">The <see cref="T:Common.Drawing.GraphicsUnit" /> of the new font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.
        /// </exception>
        public Font(string familyName, float emSize, GraphicsUnit unit)
        {
            WrappedFont = new System.Drawing.Font(familyName, emSize, (System.Drawing.GraphicsUnit) unit);
        }

        /// <summary>Initializes a new <see cref="T:Common.Drawing.Font" /> using a specified size. </summary>
        /// <param name="familyName">
        ///     A string representation of the <see cref="T:Common.Drawing.FontFamily" /> for the new
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </param>
        /// <param name="emSize">The em-size, in points, of the new font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number.
        /// </exception>
        public Font(string familyName, float emSize)
        {
            WrappedFont = new System.Drawing.Font(familyName, emSize);
        }

        private Font(System.Drawing.Font font)
        {
            WrappedFont = font;
        }

        /// <summary>Gets a value that indicates whether this <see cref="T:Common.Drawing.Font" /> is bold.</summary>
        /// <returns>true if this <see cref="T:Common.Drawing.Font" /> is bold; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Bold => WrappedFont.Bold;

        /// <summary>Gets the <see cref="T:Common.Drawing.FontFamily" /> associated with this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.FontFamily" /> associated with this <see cref="T:Common.Drawing.Font" />.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public FontFamily FontFamily => WrappedFont.FontFamily;

        /// <summary>Gets a byte value that specifies the GDI character set that this <see cref="T:Common.Drawing.Font" /> uses.</summary>
        /// <returns>
        ///     A byte value that specifies the GDI character set that this <see cref="T:Common.Drawing.Font" /> uses. The
        ///     default is 1.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public byte GdiCharSet => WrappedFont.GdiCharSet;

        /// <summary>
        ///     Gets a Boolean value that indicates whether this <see cref="T:Common.Drawing.Font" /> is derived from a GDI
        ///     vertical font.
        /// </summary>
        /// <returns>true if this <see cref="T:Common.Drawing.Font" /> is derived from a GDI vertical font; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool GdiVerticalFont => WrappedFont.GdiVerticalFont;

        /// <summary>Gets the line spacing of this font.</summary>
        /// <returns>The line spacing, in pixels, of this font. </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Browsable(false)]
        public int Height => WrappedFont.Height;

        /// <summary>Gets a value indicating whether the font is a member of <see cref="T:Common.Drawing.SystemFonts" />. </summary>
        /// <returns>
        ///     true if the font is a member of <see cref="T:Common.Drawing.SystemFonts" />; otherwise, false. The default is
        ///     false.
        /// </returns>
        [Browsable(false)]
        public bool IsSystemFont => WrappedFont.IsSystemFont;

        /// <summary>Gets a value that indicates whether this font has the italic style applied.</summary>
        /// <returns>true to indicate this font has the italic style applied; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Italic => WrappedFont.Italic;

        /// <summary>Gets the face name of this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>A string representation of the face name of this <see cref="T:Common.Drawing.Font" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name => WrappedFont.Name;

        /// <summary>Gets the name of the font originally specified.</summary>
        /// <returns>The string representing the name of the font originally specified.</returns>
        [Browsable(false)]
        public string OriginalFontName => WrappedFont.OriginalFontName;

        /// <summary>
        ///     Gets the em-size of this <see cref="T:Common.Drawing.Font" /> measured in the units specified by the
        ///     <see cref="P:Common.Drawing.Font.Unit" /> property.
        /// </summary>
        /// <returns>The em-size of this <see cref="T:Common.Drawing.Font" />.</returns>
        /// <filterpriority>1</filterpriority>
        public float Size => WrappedFont.Size;

        /// <summary>Gets the em-size, in points, of this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>The em-size, in points, of this <see cref="T:Common.Drawing.Font" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        [Browsable(false)]
        public float SizeInPoints => WrappedFont.SizeInPoints;

        /// <summary>
        ///     Gets a value that indicates whether this <see cref="T:Common.Drawing.Font" /> specifies a horizontal line
        ///     through the font.
        /// </summary>
        /// <returns>true if this <see cref="T:Common.Drawing.Font" /> has a horizontal line through it; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Strikeout => WrappedFont.Strikeout;

        /// <summary>Gets style information for this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.FontStyle" /> enumeration that contains style information for this
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public FontStyle Style => (FontStyle) WrappedFont.Style;

        /// <summary>
        ///     Gets the name of the system font if the <see cref="P:Common.Drawing.Font.IsSystemFont" /> property returns
        ///     true.
        /// </summary>
        /// <returns>
        ///     The name of the system font, if <see cref="P:Common.Drawing.Font.IsSystemFont" /> returns true; otherwise, an
        ///     empty string ("").
        /// </returns>
        [Browsable(false)]
        public string SystemFontName => WrappedFont.SystemFontName;

        /// <summary>Gets a value that indicates whether this <see cref="T:Common.Drawing.Font" /> is underlined.</summary>
        /// <returns>true if this <see cref="T:Common.Drawing.Font" /> is underlined; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Underline => WrappedFont.Underline;

        /// <summary>Gets the unit of measure for this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.GraphicsUnit" /> that represents the unit of measure for this
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public GraphicsUnit Unit => (GraphicsUnit) WrappedFont.Unit;

        private System.Drawing.Font WrappedFont { get; }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Font" /> this method creates, cast as an <see cref="T:System.Object" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public object Clone()
        {
            return new Font((System.Drawing.Font) WrappedFont.Clone());
        }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) WrappedFont).GetObjectData(info, context);
        }

        ~Font()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Indicates whether the specified object is a <see cref="T:Common.Drawing.Font" /> and has the same
        ///     <see cref="P:Common.Drawing.Font.FontFamily" />, <see cref="P:Common.Drawing.Font.GdiVerticalFont" />,
        ///     <see cref="P:Common.Drawing.Font.GdiCharSet" />, <see cref="P:Common.Drawing.Font.Style" />,
        ///     <see cref="P:Common.Drawing.Font.Size" />, and <see cref="P:Common.Drawing.Font.Unit" /> property values as this
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </summary>
        /// <returns>
        ///     true if the <paramref name="obj" /> parameter is a <see cref="T:Common.Drawing.Font" /> and has the same
        ///     <see cref="P:Common.Drawing.Font.FontFamily" />, <see cref="P:Common.Drawing.Font.GdiVerticalFont" />,
        ///     <see cref="P:Common.Drawing.Font.GdiCharSet" />, <see cref="P:Common.Drawing.Font.Style" />,
        ///     <see cref="P:Common.Drawing.Font.Size" />, and <see cref="P:Common.Drawing.Font.Unit" /> property values as this
        ///     <see cref="T:Common.Drawing.Font" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to test. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            return WrappedFont.Equals(obj);
        }

        /// <summary>Creates a <see cref="T:Common.Drawing.Font" /> from the specified Windows handle to a device context.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Font" /> this method creates.</returns>
        /// <param name="hdc">A handle to a device context. </param>
        /// <exception cref="T:System.ArgumentException">The font for the specified device context is not a TrueType font.</exception>
        /// <filterpriority>1</filterpriority>
        public static Font FromHdc(IntPtr hdc)
        {
            return System.Drawing.Font.FromHdc(hdc);
        }

        /// <summary>Creates a <see cref="T:Common.Drawing.Font" /> from the specified Windows handle.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Font" /> this method creates.</returns>
        /// <param name="hfont">A Windows handle to a GDI font. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="hfont" /> points to an object that is not a TrueType font.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public static Font FromHfont(IntPtr hfont)
        {
            return System.Drawing.Font.FromHfont(hfont);
        }

        /// <summary>Creates a <see cref="T:Common.Drawing.Font" /> from the specified GDI logical font (LOGFONT) structure.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Font" /> that this method creates.</returns>
        /// <param name="lf">
        ///     An <see cref="T:System.Object" /> that represents the GDI LOGFONT structure from which to create the
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public static Font FromLogFont(object lf)
        {
            return System.Drawing.Font.FromLogFont(lf);
        }

        /// <summary>Creates a <see cref="T:Common.Drawing.Font" /> from the specified GDI logical font (LOGFONT) structure.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Font" /> that this method creates.</returns>
        /// <param name="lf">
        ///     An <see cref="T:System.Object" /> that represents the GDI LOGFONT structure from which to create the
        ///     <see cref="T:Common.Drawing.Font" />.
        /// </param>
        /// <param name="hdc">
        ///     A handle to a device context that contains additional information about the <paramref name="lf" />
        ///     structure.
        /// </param>
        /// <exception cref="T:System.ArgumentException">The font is not a TrueType font.</exception>
        /// <filterpriority>1</filterpriority>
        public static Font FromLogFont(object lf, IntPtr hdc)
        {
            return System.Drawing.Font.FromLogFont(lf, hdc);
        }

        /// <summary>Gets the hash code for this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>The hash code for this <see cref="T:Common.Drawing.Font" />.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return WrappedFont.GetHashCode();
        }

        /// <summary>
        ///     Returns the line spacing, in the current unit of a specified <see cref="T:Common.Drawing.Graphics" />, of this
        ///     font.
        /// </summary>
        /// <returns>The line spacing, in pixels, of this font.</returns>
        /// <param name="graphics">
        ///     A <see cref="T:Common.Drawing.Graphics" /> that holds the vertical resolution, in dots per inch,
        ///     of the display device as well as settings for page unit and page scale.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="graphics" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float GetHeight(Graphics graphics)
        {
            return WrappedFont.GetHeight(graphics);
        }

        /// <summary>Returns the line spacing, in pixels, of this font. </summary>
        /// <returns>The line spacing, in pixels, of this font.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float GetHeight()
        {
            return WrappedFont.GetHeight();
        }

        /// <summary>
        ///     Returns the height, in pixels, of this <see cref="T:Common.Drawing.Font" /> when drawn to a device with the
        ///     specified vertical resolution.
        /// </summary>
        /// <returns>The height, in pixels, of this <see cref="T:Common.Drawing.Font" />.</returns>
        /// <param name="dpi">The vertical resolution, in dots per inch, used to calculate the height of the font. </param>
        /// <filterpriority>1</filterpriority>
        public float GetHeight(float dpi)
        {
            return WrappedFont.GetHeight(dpi);
        }

        /// <summary>Returns a handle to this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>A Windows handle to this <see cref="T:Common.Drawing.Font" />.</returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">The operation was unsuccessful.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public IntPtr ToHfont()
        {
            return WrappedFont.ToHfont();
        }

        /// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <param name="logFont">An <see cref="T:System.Object" /> that represents the LOGFONT structure that this method creates. </param>
        /// <filterpriority>1</filterpriority>
        public void ToLogFont(object logFont)
        {
            WrappedFont.ToLogFont(logFont);
        }

        /// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <param name="logFont">An <see cref="T:System.Object" /> that represents the LOGFONT structure that this method creates. </param>
        /// <param name="graphics">
        ///     A <see cref="T:Common.Drawing.Graphics" /> that provides additional information for the LOGFONT
        ///     structure.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="graphics" /> is null.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public void ToLogFont(object logFont, Graphics graphics)
        {
            WrappedFont.ToLogFont(logFont, graphics);
        }

        /// <summary>Returns a human-readable string representation of this <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>A string that represents this <see cref="T:Common.Drawing.Font" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public override string ToString()
        {
            return WrappedFont.ToString();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedFont.Dispose();
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Font" /> to a <see cref="T:Common.Drawing.Font" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Font" /> that results from the conversion.</returns>
        /// <param name="font">The <see cref="T:System.Drawing.Font" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Font(System.Drawing.Font font)
        {
            return font == null ? null : new Font(font);
        }

        /// <summary>Converts the specified <see cref="T:Common.Drawing.Font" /> to a <see cref="T:System.Drawing.Font" />.</summary>
        /// <returns>The <see cref="T:System.Drawing.Font" /> that results from the conversion.</returns>
        /// <param name="font">The <see cref="T:Common.Drawing.Font" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Font(Font font)
        {
            return font?.WrappedFont;
        }
    }
}