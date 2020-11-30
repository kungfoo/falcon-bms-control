using System;
using Common.Drawing.Drawing2D;
using HatchBrush = System.Drawing.Drawing2D.HatchBrush;
using LinearGradientBrush = System.Drawing.Drawing2D.LinearGradientBrush;
using PathGradientBrush = System.Drawing.Drawing2D.PathGradientBrush;

namespace Common.Drawing
{
    /// <summary>Defines an object used to draw lines and curves. This class cannot be inherited.</summary>
    /// <filterpriority>1</filterpriority>
    /// <completionlist cref="T:Common.Drawing.Pens" />
    public sealed class Pen : MarshalByRefObject, ICloneable, IDisposable
    {
        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Pen" /> class with the specified color.</summary>
        /// <param name="color">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that indicates the color of this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </param>
        public Pen(Color color)
        {
            WrappedPen = new System.Drawing.Pen(color);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Pen" /> class with the specified
        ///     <see cref="T:Common.Drawing.Color" /> and <see cref="P:Common.Drawing.Pen.Width" /> properties.
        /// </summary>
        /// <param name="color">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that indicates the color of this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </param>
        /// <param name="width">A value indicating the width of this <see cref="T:Common.Drawing.Pen" />. </param>
        public Pen(Color color, float width)
        {
            WrappedPen = new System.Drawing.Pen(color, width);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Pen" /> class with the specified
        ///     <see cref="T:Common.Drawing.Brush" />.
        /// </summary>
        /// <param name="brush">
        ///     A <see cref="T:Common.Drawing.Brush" /> that determines the fill properties of this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        public Pen(Brush brush)
        {
            WrappedPen = new System.Drawing.Pen(brush);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Pen" /> class with the specified
        ///     <see cref="T:Common.Drawing.Brush" /> and <see cref="P:Common.Drawing.Pen.Width" />.
        /// </summary>
        /// <param name="brush">
        ///     A <see cref="T:Common.Drawing.Brush" /> that determines the characteristics of this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </param>
        /// <param name="width">The width of the new <see cref="T:Common.Drawing.Pen" />. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="brush" /> is null.
        /// </exception>
        public Pen(Brush brush, float width)
        {
            WrappedPen = new System.Drawing.Pen(brush, width);
        }

        private Pen(System.Drawing.Pen pen)
        {
            WrappedPen = pen;
        }

        /// <summary>Gets or sets the alignment for this <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.PenAlignment" /> that represents the alignment for this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        ///     The specified value is not a member of
        ///     <see cref="T:Common.Drawing.Drawing2D.PenAlignment" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.Alignment" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PenAlignment Alignment
        {
            get => (PenAlignment) WrappedPen.Alignment;
            set => WrappedPen.Alignment = (System.Drawing.Drawing2D.PenAlignment) value;
        }

        /// <summary>
        ///     Gets or sets the <see cref="T:Common.Drawing.Brush" /> that determines attributes of this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Brush" /> that determines attributes of this <see cref="T:Common.Drawing.Pen" />
        ///     .
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.Brush" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Brush Brush
        {
            get
            {
                switch (WrappedPen.PenType)
                {
                    case System.Drawing.Drawing2D.PenType.SolidColor:
                        return new SolidBrush(WrappedPen.Brush as System.Drawing.SolidBrush);
                    case System.Drawing.Drawing2D.PenType.HatchFill:
                        return new Drawing2D.HatchBrush(WrappedPen.Brush as HatchBrush);
                    case System.Drawing.Drawing2D.PenType.TextureFill:
                        return new TextureBrush(WrappedPen.Brush as System.Drawing.TextureBrush);
                    case System.Drawing.Drawing2D.PenType.PathGradient:
                        return new Drawing2D.PathGradientBrush(WrappedPen.Brush as PathGradientBrush);
                    case System.Drawing.Drawing2D.PenType.LinearGradient:
                        return new Drawing2D.LinearGradientBrush(WrappedPen.Brush as LinearGradientBrush);
                    default:
                        return null;
                }
            }
            set { WrappedPen.Brush = value; }
        }

        /// <summary>Gets or sets the color of this <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the color of this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.Color" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public Color Color
        {
            get => WrappedPen.Color;
            set => WrappedPen.Color = value;
        }

        /// <summary>
        ///     Gets or sets an array of values that specifies a compound pen. A compound pen draws a compound line made up of
        ///     parallel lines and spaces.
        /// </summary>
        /// <returns>
        ///     An array of real numbers that specifies the compound array. The elements in the array must be in increasing
        ///     order, not less than 0, and not greater than 1.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.CompoundArray" /> property is set on
        ///     an immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the
        ///     <see cref="T:Common.Drawing.Pens" /> class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float[] CompoundArray
        {
            get => WrappedPen.CompoundArray;
            set => WrappedPen.CompoundArray = value;
        }

        /// <summary>Gets or sets a custom cap to use at the end of lines drawn with this <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> that represents the cap used at the end of lines
        ///     drawn with this <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.CustomEndCap" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public CustomLineCap CustomEndCap
        {
            get => WrappedPen.CustomEndCap;
            set => WrappedPen.CustomEndCap = value;
        }

        /// <summary>
        ///     Gets or sets a custom cap to use at the beginning of lines drawn with this <see cref="T:Common.Drawing.Pen" />
        ///     .
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.CustomLineCap" /> that represents the cap used at the beginning of
        ///     lines drawn with this <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.CustomStartCap" /> property is set on
        ///     an immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the
        ///     <see cref="T:Common.Drawing.Pens" /> class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public CustomLineCap CustomStartCap
        {
            get => WrappedPen.CustomStartCap;
            set => WrappedPen.CustomEndCap = value;
        }

        /// <summary>
        ///     Gets or sets the cap style used at the end of the dashes that make up dashed lines drawn with this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="T:Common.Drawing.Drawing2D.DashCap" /> values that represents the cap style used at the
        ///     beginning and end of the dashes that make up dashed lines drawn with this <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        ///     The specified value is not a member of
        ///     <see cref="T:Common.Drawing.Drawing2D.DashCap" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.DashCap" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public DashCap DashCap
        {
            get => (DashCap) WrappedPen.DashCap;
            set => WrappedPen.DashCap = (System.Drawing.Drawing2D.DashCap) value;
        }

        /// <summary>Gets or sets the distance from the start of a line to the beginning of a dash pattern.</summary>
        /// <returns>The distance from the start of a line to the beginning of a dash pattern.</returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.DashOffset" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float DashOffset
        {
            get => WrappedPen.DashOffset;
            set => WrappedPen.DashOffset = value;
        }

        /// <summary>Gets or sets an array of custom dashes and spaces.</summary>
        /// <returns>An array of real numbers that specifies the lengths of alternating dashes and spaces in dashed lines.</returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.DashPattern" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float[] DashPattern
        {
            get => WrappedPen.DashPattern;
            set => WrappedPen.DashPattern = value;
        }

        /// <summary>Gets or sets the style used for dashed lines drawn with this <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.DashStyle" /> that represents the style used for dashed lines drawn
        ///     with this <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.DashStyle" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public DashStyle DashStyle
        {
            get => (DashStyle) WrappedPen.DashStyle;
            set => WrappedPen.DashStyle = (System.Drawing.Drawing2D.DashStyle) value;
        }

        /// <summary>Gets or sets the cap style used at the end of lines drawn with this <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>
        ///     One of the <see cref="T:Common.Drawing.Drawing2D.LineCap" /> values that represents the cap style used at the
        ///     end of lines drawn with this <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        ///     The specified value is not a member of
        ///     <see cref="T:Common.Drawing.Drawing2D.LineCap" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.EndCap" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public LineCap EndCap
        {
            get => (LineCap) WrappedPen.EndCap;
            set => WrappedPen.EndCap = (System.Drawing.Drawing2D.LineCap) value;
        }

        /// <summary>
        ///     Gets or sets the join style for the ends of two consecutive lines drawn with this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.LineJoin" /> that represents the join style for the ends of two
        ///     consecutive lines drawn with this <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.LineJoin" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public LineJoin LineJoin
        {
            get => (LineJoin) WrappedPen.LineJoin;
            set => WrappedPen.LineJoin = (System.Drawing.Drawing2D.LineJoin) value;
        }

        /// <summary>Gets or sets the limit of the thickness of the join on a mitered corner.</summary>
        /// <returns>The limit of the thickness of the join on a mitered corner.</returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.MiterLimit" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float MiterLimit
        {
            get => WrappedPen.MiterLimit;
            set => WrappedPen.MiterLimit = value;
        }

        /// <summary>Gets the style of lines drawn with this <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.PenType" /> enumeration that specifies the style of lines drawn with
        ///     this <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PenType PenType => (PenType) WrappedPen.PenType;

        /// <summary>Gets or sets the cap style used at the beginning of lines drawn with this <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>
        ///     One of the <see cref="T:Common.Drawing.Drawing2D.LineCap" /> values that represents the cap style used at the
        ///     beginning of lines drawn with this <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        ///     The specified value is not a member of
        ///     <see cref="T:Common.Drawing.Drawing2D.LineCap" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.StartCap" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public LineCap StartCap
        {
            get => (LineCap) WrappedPen.StartCap;
            set => WrappedPen.StartCap = (System.Drawing.Drawing2D.LineCap) value;
        }

        /// <summary>Gets or sets a copy of the geometric transformation for this <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>
        ///     A copy of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that represents the geometric transformation
        ///     for this <see cref="T:Common.Drawing.Pen" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.Transform" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Matrix Transform
        {
            get => WrappedPen.Transform;
            set => WrappedPen.Transform = value;
        }

        /// <summary>
        ///     Gets or sets the width of this <see cref="T:Common.Drawing.Pen" />, in units of the
        ///     <see cref="T:Common.Drawing.Graphics" /> object used for drawing.
        /// </summary>
        /// <returns>The width of this <see cref="T:Common.Drawing.Pen" />.</returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="P:Common.Drawing.Pen.Width" /> property is set on an
        ///     immutable <see cref="T:Common.Drawing.Pen" />, such as those returned by the <see cref="T:Common.Drawing.Pens" />
        ///     class.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public float Width
        {
            get => WrappedPen.Width;
            set => WrappedPen.Width = value;
        }

        private System.Drawing.Pen WrappedPen { get; }


        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>An <see cref="T:System.Object" /> that can be cast to a <see cref="T:Common.Drawing.Pen" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public object Clone()
        {
            return new Pen((System.Drawing.Pen) WrappedPen.Clone());
        }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Pen" />.</summary>
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

        ~Pen()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Multiplies the transformation matrix for this <see cref="T:Common.Drawing.Pen" /> by the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <param name="matrix">
        ///     The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object by which to multiply the
        ///     transformation matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void MultiplyTransform(Matrix matrix)
        {
            WrappedPen.MultiplyTransform(matrix);
        }

        /// <summary>
        ///     Multiplies the transformation matrix for this <see cref="T:Common.Drawing.Pen" /> by the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> in the specified order.
        /// </summary>
        /// <param name="matrix">
        ///     The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by which to multiply the transformation
        ///     matrix.
        /// </param>
        /// <param name="order">The order in which to perform the multiplication operation. </param>
        /// <filterpriority>1</filterpriority>
        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            WrappedPen.MultiplyTransform(matrix, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>Resets the geometric transformation matrix for this <see cref="T:Common.Drawing.Pen" /> to identity.</summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ResetTransform()
        {
            WrappedPen.ResetTransform();
        }

        /// <summary>
        ///     Rotates the local geometric transformation by the specified angle. This method prepends the rotation to the
        ///     transformation.
        /// </summary>
        /// <param name="angle">The angle of rotation. </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle)
        {
            WrappedPen.RotateTransform(angle);
        }

        /// <summary>Rotates the local geometric transformation by the specified angle in the specified order.</summary>
        /// <param name="angle">The angle of rotation. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend
        ///     the rotation matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle, MatrixOrder order)
        {
            WrappedPen.RotateTransform(angle, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Scales the local geometric transformation by the specified factors. This method prepends the scaling matrix to
        ///     the transformation.
        /// </summary>
        /// <param name="sx">The factor by which to scale the transformation in the x-axis direction. </param>
        /// <param name="sy">The factor by which to scale the transformation in the y-axis direction. </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy)
        {
            WrappedPen.ScaleTransform(sx, sy);
        }

        /// <summary>Scales the local geometric transformation by the specified factors in the specified order.</summary>
        /// <param name="sx">The factor by which to scale the transformation in the x-axis direction. </param>
        /// <param name="sy">The factor by which to scale the transformation in the y-axis direction. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend
        ///     the scaling matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            WrappedPen.ScaleTransform(sx, sy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Sets the values that determine the style of cap used to end lines drawn by this
        ///     <see cref="T:Common.Drawing.Pen" />.
        /// </summary>
        /// <param name="startCap">
        ///     A <see cref="T:Common.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the
        ///     beginning of lines drawn with this <see cref="T:Common.Drawing.Pen" />.
        /// </param>
        /// <param name="endCap">
        ///     A <see cref="T:Common.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the
        ///     end of lines drawn with this <see cref="T:Common.Drawing.Pen" />.
        /// </param>
        /// <param name="dashCap">
        ///     A <see cref="T:Common.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the
        ///     beginning or end of dashed lines drawn with this <see cref="T:Common.Drawing.Pen" />.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void SetLineCap(LineCap startCap, LineCap endCap, DashCap dashCap)
        {
            WrappedPen.SetLineCap((System.Drawing.Drawing2D.LineCap) startCap,
                (System.Drawing.Drawing2D.LineCap) endCap, (System.Drawing.Drawing2D.DashCap) dashCap);
        }

        /// <summary>
        ///     Translates the local geometric transformation by the specified dimensions. This method prepends the
        ///     translation to the transformation.
        /// </summary>
        /// <param name="dx">The value of the translation in x. </param>
        /// <param name="dy">The value of the translation in y. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy)
        {
            WrappedPen.TranslateTransform(dx, dy);
        }

        /// <summary>Translates the local geometric transformation by the specified dimensions in the specified order.</summary>
        /// <param name="dx">The value of the translation in x. </param>
        /// <param name="dy">The value of the translation in y. </param>
        /// <param name="order">The order (prepend or append) in which to apply the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            WrappedPen.TranslateTransform(dx, dy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedPen.Dispose();
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Pen" /> to a <see cref="T:Common.Drawing.Pen" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Pen" /> that results from the conversion.</returns>
        /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Pen(System.Drawing.Pen pen)
        {
            return pen == null ? null : new Pen(pen);
        }

        /// <summary>Converts the specified <see cref="T:Common.Drawing.Pen" /> to a <see cref="T:System.Drawing.Pen" />.</summary>
        /// <returns>The <see cref="T:System.Drawing.Pen" /> that results from the conversion.</returns>
        /// <param name="pen">The <see cref="T:Common.Drawing.Pen" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Pen(Pen pen)
        {
            return pen?.WrappedPen;
        }
    }
}