using System;
using System.Linq;
using Common.Drawing.Text;

namespace Common.Drawing
{
    /// <summary>
    ///     Encapsulates text layout information (such as alignment, orientation and tab stops) display manipulations
    ///     (such as ellipsis insertion and national digit substitution) and OpenType features. This class cannot be inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    public sealed class StringFormat : MarshalByRefObject, ICloneable, IDisposable
    {
        /// <summary>Initializes a new <see cref="T:Common.Drawing.StringFormat" /> object.</summary>
        public StringFormat()
        {
            WrappedStringFormat = new System.Drawing.StringFormat();
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.StringFormat" /> object with the specified
        ///     <see cref="T:Common.Drawing.StringFormatFlags" /> enumeration.
        /// </summary>
        /// <param name="options">
        ///     The <see cref="T:Common.Drawing.StringFormatFlags" /> enumeration for the new
        ///     <see cref="T:Common.Drawing.StringFormat" /> object.
        /// </param>
        public StringFormat(StringFormatFlags options)
        {
            WrappedStringFormat = new System.Drawing.StringFormat((System.Drawing.StringFormatFlags) options);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.StringFormat" /> object with the specified
        ///     <see cref="T:Common.Drawing.StringFormatFlags" /> enumeration and language.
        /// </summary>
        /// <param name="options">
        ///     The <see cref="T:Common.Drawing.StringFormatFlags" /> enumeration for the new
        ///     <see cref="T:Common.Drawing.StringFormat" /> object.
        /// </param>
        /// <param name="language">A value that indicates the language of the text. </param>
        public StringFormat(StringFormatFlags options, int language)
        {
            WrappedStringFormat = new System.Drawing.StringFormat((System.Drawing.StringFormatFlags) options, language);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.StringFormat" /> object from the specified existing
        ///     <see cref="T:Common.Drawing.StringFormat" /> object.
        /// </summary>
        /// <param name="format">
        ///     The <see cref="T:Common.Drawing.StringFormat" /> object from which to initialize the new
        ///     <see cref="T:Common.Drawing.StringFormat" /> object.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="format" /> is null.
        /// </exception>
        public StringFormat(StringFormat format)
        {
            WrappedStringFormat = new System.Drawing.StringFormat(format.WrappedStringFormat);
        }

        private StringFormat(System.Drawing.StringFormat stringFormat)
        {
            WrappedStringFormat = stringFormat;
        }

        /// <summary>Gets or sets horizontal alignment of the string..</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.StringAlignment" /> enumeration that specifies the horizontal  alignment of the
        ///     string.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public StringAlignment Alignment
        {
            get => (StringAlignment) WrappedStringFormat.Alignment;
            set => WrappedStringFormat.Alignment = (System.Drawing.StringAlignment) value;
        }

        /// <summary>Gets the language that is used when local digits are substituted for western digits.</summary>
        /// <returns>
        ///     A National Language Support (NLS) language identifier that identifies the language that will be used when
        ///     local digits are substituted for western digits. You can pass the
        ///     <see cref="P:System.Globalization.CultureInfo.LCID" /> property of a
        ///     <see cref="T:System.Globalization.CultureInfo" /> object as the NLS language identifier. For example, suppose you
        ///     create a <see cref="T:System.Globalization.CultureInfo" /> object by passing the string "ar-EG" to a
        ///     <see cref="T:System.Globalization.CultureInfo" /> constructor. If you pass the
        ///     <see cref="P:System.Globalization.CultureInfo.LCID" /> property of that
        ///     <see cref="T:System.Globalization.CultureInfo" /> object along with.
        ///     <see cref="F:Common.Drawing.StringDigitSubstitute.Traditional" /> to the
        ///     <see cref="M:Common.Drawing.StringFormat.SetDigitSubstitution(System.Int32,Common.Drawing.StringDigitSubstitute)" />
        ///     method, then Arabic-Indic digits will be substituted for western digits at display time.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int DigitSubstitutionLanguage => WrappedStringFormat.DigitSubstitutionLanguage;

        /// <summary>Gets the method to be used for digit substitution.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.StringDigitSubstitute" /> enumeration value that specifies how to substitute
        ///     characters in a string that cannot be displayed because they are not supported by the current font.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public StringDigitSubstitute DigitSubstitutionMethod => (StringDigitSubstitute) WrappedStringFormat
            .DigitSubstitutionMethod;

        /// <summary>
        ///     Gets or sets a <see cref="T:Common.Drawing.StringFormatFlags" /> enumeration that contains formatting
        ///     information.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.StringFormatFlags" /> enumeration that contains formatting information.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public StringFormatFlags FormatFlags
        {
            get => (StringFormatFlags) WrappedStringFormat.FormatFlags;
            set => WrappedStringFormat.FormatFlags = (System.Drawing.StringFormatFlags) value;
        }

        /// <summary>Gets a generic default <see cref="T:Common.Drawing.StringFormat" /> object.</summary>
        /// <returns>The generic default <see cref="T:Common.Drawing.StringFormat" /> object.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public static StringFormat GenericDefault => System.Drawing.StringFormat.GenericDefault;

        /// <summary>Gets a generic typographic <see cref="T:Common.Drawing.StringFormat" /> object.</summary>
        /// <returns>A generic typographic <see cref="T:Common.Drawing.StringFormat" /> object.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public static StringFormat GenericTypographic => System.Drawing.StringFormat.GenericTypographic;

        /// <summary>
        ///     Gets or sets the <see cref="T:Common.Drawing.Text.HotkeyPrefix" /> object for this
        ///     <see cref="T:Common.Drawing.StringFormat" /> object.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.Text.HotkeyPrefix" /> object for this
        ///     <see cref="T:Common.Drawing.StringFormat" /> object, the default is
        ///     <see cref="F:Common.Drawing.Text.HotkeyPrefix.None" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public HotkeyPrefix HotkeyPrefix
        {
            get => (HotkeyPrefix) WrappedStringFormat.HotkeyPrefix;
            set => WrappedStringFormat.HotkeyPrefix = (System.Drawing.Text.HotkeyPrefix) value;
        }

        /// <summary>Gets or sets the vertical alignment of the string.</summary>
        /// <returns>A <see cref="T:Common.Drawing.StringAlignment" /> enumeration that represents the vertical line alignment.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public StringAlignment LineAlignment
        {
            get => (StringAlignment) WrappedStringFormat.LineAlignment;
            set => WrappedStringFormat.LineAlignment = (System.Drawing.StringAlignment) value;
        }

        /// <summary>
        ///     Gets or sets the <see cref="T:Common.Drawing.StringTrimming" /> enumeration for this
        ///     <see cref="T:Common.Drawing.StringFormat" /> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.StringTrimming" /> enumeration that indicates how text drawn with this
        ///     <see cref="T:Common.Drawing.StringFormat" /> object is trimmed when it exceeds the edges of the layout rectangle.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public StringTrimming Trimming
        {
            get => (StringTrimming) WrappedStringFormat.Trimming;
            set => WrappedStringFormat.Trimming = (System.Drawing.StringTrimming) value;
        }

        private System.Drawing.StringFormat WrappedStringFormat { get; }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.StringFormat" /> object.</summary>
        /// <returns>The <see cref="T:Common.Drawing.StringFormat" /> object this method creates.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public object Clone()
        {
            return new StringFormat((System.Drawing.StringFormat) WrappedStringFormat.Clone());
        }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.StringFormat" /> object.</summary>
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

        ~StringFormat()
        {
            Dispose(false);
        }

        /// <summary>Gets the tab stops for this <see cref="T:Common.Drawing.StringFormat" /> object.</summary>
        /// <returns>An array of distances (in number of spaces) between tab stops.</returns>
        /// <param name="firstTabOffset">The number of spaces between the beginning of a text line and the first tab stop. </param>
        /// <filterpriority>1</filterpriority>
        public float[] GetTabStops(out float firstTabOffset)
        {
            return WrappedStringFormat.GetTabStops(out firstTabOffset);
        }

        /// <summary>Specifies the language and method to be used when local digits are substituted for western digits.</summary>
        /// <param name="language">
        ///     A National Language Support (NLS) language identifier that identifies the language that will be
        ///     used when local digits are substituted for western digits. You can pass the
        ///     <see cref="P:System.Globalization.CultureInfo.LCID" /> property of a
        ///     <see cref="T:System.Globalization.CultureInfo" /> object as the NLS language identifier. For example, suppose you
        ///     create a <see cref="T:System.Globalization.CultureInfo" /> object by passing the string "ar-EG" to a
        ///     <see cref="T:System.Globalization.CultureInfo" /> constructor. If you pass the
        ///     <see cref="P:System.Globalization.CultureInfo.LCID" /> property of that
        ///     <see cref="T:System.Globalization.CultureInfo" /> object along with
        ///     <see cref="F:Common.Drawing.StringDigitSubstitute.Traditional" /> to the
        ///     <see cref="M:Common.Drawing.StringFormat.SetDigitSubstitution(System.Int32,Common.Drawing.StringDigitSubstitute)" />
        ///     method, then Arabic-Indic digits will be substituted for western digits at display time.
        /// </param>
        /// <param name="substitute">
        ///     An element of the <see cref="T:Common.Drawing.StringDigitSubstitute" /> enumeration that
        ///     specifies how digits are displayed.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void SetDigitSubstitution(int language, StringDigitSubstitute substitute)
        {
            WrappedStringFormat.SetDigitSubstitution(language, (System.Drawing.StringDigitSubstitute) substitute);
        }

        /// <summary>
        ///     Specifies an array of <see cref="T:Common.Drawing.CharacterRange" /> structures that represent the ranges of
        ///     characters measured by a call to the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.MeasureCharacterRanges(System.String,Common.Drawing.Font,Common.Drawing.RectangleF,Common.Drawing.StringFormat)" />
        ///     method.
        /// </summary>
        /// <param name="ranges">
        ///     An array of <see cref="T:Common.Drawing.CharacterRange" /> structures that specifies the ranges of characters
        ///     measured by a call to the
        ///     <see
        ///         cref="M:Common.Drawing.Graphics.MeasureCharacterRanges(System.String,Common.Drawing.Font,Common.Drawing.RectangleF,Common.Drawing.StringFormat)" />
        ///     method.
        /// </param>
        /// <exception cref="T:System.OverflowException">More than 32 character ranges are set.</exception>
        /// <filterpriority>1</filterpriority>
        public void SetMeasurableCharacterRanges(CharacterRange[] ranges)
        {
            WrappedStringFormat.SetMeasurableCharacterRanges(ranges.Convert<System.Drawing.CharacterRange>().ToArray());
        }

        /// <summary>Sets tab stops for this <see cref="T:Common.Drawing.StringFormat" /> object.</summary>
        /// <param name="firstTabOffset">The number of spaces between the beginning of a line of text and the first tab stop. </param>
        /// <param name="tabStops">
        ///     An array of distances between tab stops in the units specified by the
        ///     <see cref="P:Common.Drawing.Graphics.PageUnit" /> property.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void SetTabStops(float firstTabOffset, float[] tabStops)
        {
            WrappedStringFormat.SetTabStops(firstTabOffset, tabStops);
        }

        /// <summary>Converts this <see cref="T:Common.Drawing.StringFormat" /> object to a human-readable string.</summary>
        /// <returns>A string representation of this <see cref="T:Common.Drawing.StringFormat" /> object.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public override string ToString()
        {
            return WrappedStringFormat.ToString();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedStringFormat.Dispose();
            }
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.StringFormat" /> to a
        ///     <see cref="T:Common.Drawing.StringFormat" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.StringFormat" /> that results from the conversion.</returns>
        /// <param name="stringFormat">The <see cref="T:System.Drawing.StringFormat" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator StringFormat(System.Drawing.StringFormat stringFormat)
        {
            return stringFormat == null ? null : new StringFormat(stringFormat);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.StringFormat" /> to a
        ///     <see cref="T:System.Drawing.StringFormat" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.StringFormat" /> that results from the conversion.</returns>
        /// <param name="stringFormat">The <see cref="T:Common.Drawing.StringFormat" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.StringFormat(StringFormat stringFormat)
        {
            return stringFormat?.WrappedStringFormat;
        }
    }
}