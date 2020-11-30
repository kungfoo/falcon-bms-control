using System;

namespace Common.Drawing.Drawing2D
{
    /// <summary>Encapsulates a custom user-defined line cap.</summary>
    public class CustomLineCap : MarshalByRefObject, ICloneable, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> class with the
        ///     specified outline and fill.
        /// </summary>
        /// <param name="fillPath">
        ///     A <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the
        ///     custom cap.
        /// </param>
        /// <param name="strokePath">
        ///     A <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of
        ///     the custom cap.
        /// </param>
        public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath)
        {
            WrappedCustomLineCap = new System.Drawing.Drawing2D.CustomLineCap(fillPath, strokePath);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> class from the
        ///     specified existing <see cref="T:Common.Drawing.Drawing2D.LineCap" /> enumeration with the specified outline and
        ///     fill.
        /// </summary>
        /// <param name="fillPath">
        ///     A <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the
        ///     custom cap.
        /// </param>
        /// <param name="strokePath">
        ///     A <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of
        ///     the custom cap.
        /// </param>
        /// <param name="baseCap">The line cap from which to create the custom cap. </param>
        public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath, LineCap baseCap)
        {
            WrappedCustomLineCap =
                new System.Drawing.Drawing2D.CustomLineCap(fillPath, strokePath,
                    (System.Drawing.Drawing2D.LineCap) baseCap);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> class from the
        ///     specified existing <see cref="T:Common.Drawing.Drawing2D.LineCap" /> enumeration with the specified outline, fill,
        ///     and inset.
        /// </summary>
        /// <param name="fillPath">
        ///     A <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the
        ///     custom cap.
        /// </param>
        /// <param name="strokePath">
        ///     A <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of
        ///     the custom cap.
        /// </param>
        /// <param name="baseCap">The line cap from which to create the custom cap. </param>
        /// <param name="baseInset">The distance between the cap and the line. </param>
        public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath, LineCap baseCap, float baseInset)
        {
            WrappedCustomLineCap = new System.Drawing.Drawing2D.CustomLineCap(fillPath, strokePath,
                (System.Drawing.Drawing2D.LineCap) baseCap, baseInset);
        }

        private CustomLineCap(System.Drawing.Drawing2D.CustomLineCap customLineCap)
        {
            WrappedCustomLineCap = customLineCap;
        }

        private CustomLineCap()
        {
        }

        /// <summary>
        ///     Gets or sets the <see cref="T:Common.Drawing.Drawing2D.LineCap" /> enumeration on which this
        ///     <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> is based.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.Drawing2D.LineCap" /> enumeration on which this
        ///     <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> is based.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public LineCap BaseCap
        {
            get => (LineCap) WrappedCustomLineCap.BaseCap;
            set => WrappedCustomLineCap.BaseCap = (System.Drawing.Drawing2D.LineCap) value;
        }

        /// <summary>Gets or sets the distance between the cap and the line.</summary>
        /// <returns>The distance between the beginning of the cap and the end of the line.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float BaseInset
        {
            get => WrappedCustomLineCap.BaseInset;
            set => WrappedCustomLineCap.BaseInset = value;
        }

        /// <summary>
        ///     Gets or sets the <see cref="T:Common.Drawing.Drawing2D.LineJoin" /> enumeration that determines how lines that
        ///     compose this <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> object are joined.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.Drawing2D.LineJoin" /> enumeration this
        ///     <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> object uses to join lines.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public LineJoin StrokeJoin
        {
            get => (LineJoin) WrappedCustomLineCap.StrokeJoin;
            set => WrappedCustomLineCap.StrokeJoin = (System.Drawing.Drawing2D.LineJoin) value;
        }

        /// <summary>
        ///     Gets or sets the amount by which to scale this <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> Class
        ///     object with respect to the width of the <see cref="T:Common.Drawing.Pen" /> object.
        /// </summary>
        /// <returns>The amount by which to scale the cap.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float WidthScale
        {
            get => WrappedCustomLineCap.WidthScale;
            set => WrappedCustomLineCap.WidthScale = value;
        }

        private System.Drawing.Drawing2D.CustomLineCap WrappedCustomLineCap { get; }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> this method creates, cast as an object.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public object Clone()
        {
            return new CustomLineCap((System.Drawing.Drawing2D.CustomLineCap) WrappedCustomLineCap.Clone());
        }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> object.</summary>
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

        ~CustomLineCap()
        {
            Dispose(false);
        }

        /// <summary>Gets the caps used to start and end lines that make up this custom cap.</summary>
        /// <param name="startCap">
        ///     The <see cref="T:Common.Drawing.Drawing2D.LineCap" /> enumeration used at the beginning of a
        ///     line within this cap.
        /// </param>
        /// <param name="endCap">
        ///     The <see cref="T:Common.Drawing.Drawing2D.LineCap" /> enumeration used at the end of a line within
        ///     this cap.
        /// </param>
        public void GetStrokeCaps(out LineCap startCap, out LineCap endCap)
        {
            System.Drawing.Drawing2D.LineCap startCapInternal;
            System.Drawing.Drawing2D.LineCap endCapInternal;
            WrappedCustomLineCap.GetStrokeCaps(out startCapInternal, out endCapInternal);
            startCap = (LineCap) startCapInternal;
            endCap = (LineCap) endCapInternal;
        }

        /// <summary>Sets the caps used to start and end lines that make up this custom cap.</summary>
        /// <param name="startCap">
        ///     The <see cref="T:Common.Drawing.Drawing2D.LineCap" /> enumeration used at the beginning of a
        ///     line within this cap.
        /// </param>
        /// <param name="endCap">
        ///     The <see cref="T:Common.Drawing.Drawing2D.LineCap" /> enumeration used at the end of a line within
        ///     this cap.
        /// </param>
        public void SetStrokeCaps(LineCap startCap, LineCap endCap)
        {
            WrappedCustomLineCap.SetStrokeCaps((System.Drawing.Drawing2D.LineCap) startCap,
                (System.Drawing.Drawing2D.LineCap) endCap);
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> and
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
                WrappedCustomLineCap.Dispose();
            }
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> that results from the conversion.</returns>
        /// <param name="customLineCap">The <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator CustomLineCap(System.Drawing.Drawing2D.CustomLineCap customLineCap)
        {
            return customLineCap == null ? null : new CustomLineCap(customLineCap);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.CustomLineCap" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> that results from the conversion.</returns>
        /// <param name="customLineCap">The <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.CustomLineCap(CustomLineCap customLineCap)
        {
            return customLineCap?.WrappedCustomLineCap;
        }
    }
}