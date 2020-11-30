namespace Common.Drawing
{
    /// <summary>
    ///     Defines a brush of a single color. Brushes are used to fill graphics shapes, such as rectangles, ellipses,
    ///     pies, polygons, and paths. This class cannot be inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    public sealed class SolidBrush : Brush
    {
        /// <summary>Initializes a new <see cref="T:Common.Drawing.SolidBrush" /> object of the specified color.</summary>
        /// <param name="color">A <see cref="T:Common.Drawing.Color" /> structure that represents the color of this brush. </param>
        public SolidBrush(Color color)
        {
            WrappedSolidBrush = new System.Drawing.SolidBrush(color);
        }

        internal SolidBrush(System.Drawing.SolidBrush solidBrush)
        {
            WrappedSolidBrush = solidBrush;
        }

        /// <summary>Gets or sets the color of this <see cref="T:Common.Drawing.SolidBrush" /> object.</summary>
        /// <returns>A <see cref="T:Common.Drawing.Color" /> structure that represents the color of this brush.</returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.SolidBrush.Color" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.SolidBrush" />.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public Color Color
        {
            get => WrappedSolidBrush.Color;
            set => WrappedSolidBrush.Color = value;
        }

        private System.Drawing.SolidBrush WrappedSolidBrush
        {
            get => WrappedBrush as System.Drawing.SolidBrush;
            set => WrappedBrush = value;
        }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.SolidBrush" /> object.</summary>
        /// <returns>The <see cref="T:Common.Drawing.SolidBrush" /> object that this method creates.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public override object Clone()
        {
            return new SolidBrush((System.Drawing.SolidBrush) WrappedSolidBrush.Clone());
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.SolidBrush" /> to a
        ///     <see cref="T:Common.Drawing.SolidBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.SolidBrush" /> that results from the conversion.</returns>
        /// <param name="solidBrush">The <see cref="T:System.Drawing.SolidBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator SolidBrush(System.Drawing.SolidBrush solidBrush)
        {
            return solidBrush == null ? null : new SolidBrush(solidBrush);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.SolidBrush" /> to a
        ///     <see cref="T:System.Drawing.SolidBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.SolidBrush" /> that results from the conversion.</returns>
        /// <param name="solidBrush">The <see cref="T:Common.Drawing.SolidBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.SolidBrush(SolidBrush solidBrush)
        {
            return solidBrush?.WrappedSolidBrush;
        }
    }
}