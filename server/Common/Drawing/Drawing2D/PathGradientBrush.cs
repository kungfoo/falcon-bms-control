using System.Linq;

namespace Common.Drawing.Drawing2D
{
    /// <summary>
    ///     Encapsulates a <see cref="T:Common.Drawing.Brush" /> object that fills the interior of a
    ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> object with a gradient. This class cannot be inherited.
    /// </summary>
    public sealed class PathGradientBrush : Brush
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> class with the
        ///     specified points.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points that make
        ///     up the vertices of the path.
        /// </param>
        public PathGradientBrush(PointF[] points)
        {
            WrappedPathGradientBrush =
                new System.Drawing.Drawing2D.PathGradientBrush(points.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> class with the
        ///     specified points and wrap mode.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points that make
        ///     up the vertices of the path.
        /// </param>
        /// <param name="wrapMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> are tiled.
        /// </param>
        public PathGradientBrush(PointF[] points, WrapMode wrapMode)
        {
            WrappedPathGradientBrush = new System.Drawing.Drawing2D.PathGradientBrush(
                points.Convert<System.Drawing.PointF>().ToArray(), (System.Drawing.Drawing2D.WrapMode) wrapMode);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> class with the
        ///     specified points.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points that make
        ///     up the vertices of the path.
        /// </param>
        public PathGradientBrush(Point[] points)
        {
            WrappedPathGradientBrush =
                new System.Drawing.Drawing2D.PathGradientBrush(points.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> class with the
        ///     specified points and wrap mode.
        /// </summary>
        /// <param name="points">
        ///     An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points that make
        ///     up the vertices of the path.
        /// </param>
        /// <param name="wrapMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> are tiled.
        /// </param>
        public PathGradientBrush(Point[] points, WrapMode wrapMode)
        {
            WrappedPathGradientBrush = new System.Drawing.Drawing2D.PathGradientBrush(
                points.Convert<System.Drawing.Point>().ToArray(), (System.Drawing.Drawing2D.WrapMode) wrapMode);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> class with the
        ///     specified path.
        /// </summary>
        /// <param name="path">
        ///     The <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> that defines the area filled by this
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />.
        /// </param>
        public PathGradientBrush(GraphicsPath path)
        {
            WrappedPathGradientBrush = new System.Drawing.Drawing2D.PathGradientBrush(path);
        }

        internal PathGradientBrush(System.Drawing.Drawing2D.PathGradientBrush pathGradientBrush)
        {
            WrappedPathGradientBrush = pathGradientBrush;
        }

        /// <summary>
        ///     Gets or sets a <see cref="T:Common.Drawing.Drawing2D.Blend" /> that specifies positions and factors that
        ///     define a custom falloff for the gradient.
        /// </summary>
        /// <returns>A <see cref="T:Common.Drawing.Drawing2D.Blend" /> that represents a custom falloff for the gradient.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Blend Blend
        {
            get => WrappedPathGradientBrush.Blend;
            set => WrappedPathGradientBrush.Blend = value;
        }

        /// <summary>Gets or sets the color at the center of the path gradient.</summary>
        /// <returns>A <see cref="T:Common.Drawing.Color" /> that represents the color at the center of the path gradient.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Color CenterColor
        {
            get => WrappedPathGradientBrush.CenterColor;
            set => WrappedPathGradientBrush.CenterColor = value;
        }

        /// <summary>Gets or sets the center point of the path gradient.</summary>
        /// <returns>A <see cref="T:Common.Drawing.PointF" /> that represents the center point of the path gradient.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PointF CenterPoint
        {
            get => WrappedPathGradientBrush.CenterPoint;
            set => WrappedPathGradientBrush.CenterPoint = value;
        }

        /// <summary>Gets or sets the focus point for the gradient falloff.</summary>
        /// <returns>A <see cref="T:Common.Drawing.PointF" /> that represents the focus point for the gradient falloff.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PointF FocusScales
        {
            get => WrappedPathGradientBrush.FocusScales;
            set => WrappedPathGradientBrush.FocusScales = value;
        }

        /// <summary>Gets or sets a <see cref="T:Common.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</summary>
        /// <returns>A <see cref="T:Common.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public ColorBlend InterpolationColors
        {
            get => WrappedPathGradientBrush.InterpolationColors;
            set => WrappedPathGradientBrush.InterpolationColors = value;
        }

        /// <summary>Gets a bounding rectangle for this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.RectangleF" /> that represents a rectangular region that bounds the path this
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> fills.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public RectangleF Rectangle => WrappedPathGradientBrush.Rectangle;

        /// <summary>
        ///     Gets or sets an array of colors that correspond to the points in the path this
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> fills.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Common.Drawing.Color" /> structures that represents the colors associated with each
        ///     point in the path this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> fills.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Color[] SurroundColors
        {
            get => WrappedPathGradientBrush.SurroundColors.Convert<Color>().ToArray();
            set => WrappedPathGradientBrush.SurroundColors = value.Convert<System.Drawing.Color>().ToArray();
        }

        /// <summary>
        ///     Gets or sets a copy of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that defines a local geometric
        ///     transform for this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />.
        /// </summary>
        /// <returns>
        ///     A copy of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that defines a geometric transform that applies
        ///     only to fills drawn with this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Matrix Transform
        {
            get => WrappedPathGradientBrush.Transform;
            set => WrappedPathGradientBrush.Transform = value;
        }

        /// <summary>
        ///     Gets or sets a <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> that indicates the wrap mode for this
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> are tiled.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public WrapMode WrapMode
        {
            get => (WrapMode) WrappedPathGradientBrush.WrapMode;
            set => WrappedPathGradientBrush.WrapMode = (System.Drawing.Drawing2D.WrapMode) value;
        }

        private System.Drawing.Drawing2D.PathGradientBrush WrappedPathGradientBrush
        {
            get => WrappedBrush as System.Drawing.Drawing2D.PathGradientBrush;
            set => WrappedBrush = value;
        }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> this method creates, cast as an object.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public override object Clone()
        {
            return (PathGradientBrush) (System.Drawing.Drawing2D.PathGradientBrush) WrappedPathGradientBrush.Clone();
        }

        /// <summary>
        ///     Multiplies the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object that represents the local geometric
        ///     transformation of this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> object by the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object by prepending the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object.
        /// </summary>
        /// <param name="matrix">
        ///     The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object by which to multiply the geometric
        ///     transformation.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void MultiplyTransform(Matrix matrix)
        {
            WrappedPathGradientBrush.MultiplyTransform(matrix);
        }

        /// <summary>
        ///     Multiplies the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object that represents the local geometric
        ///     transformation of this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> object by the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object in the specified order.
        /// </summary>
        /// <param name="matrix">
        ///     The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object by which to multiply the geometric
        ///     transformation.
        /// </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies the order in
        ///     which to multiply the two matrices.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            WrappedPathGradientBrush.MultiplyTransform(matrix, (System.Drawing.Drawing2D.MatrixOrder) order);
        }


        /// <summary>
        ///     Resets the Transform property of this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> object to
        ///     identity.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ResetTransform()
        {
            WrappedPathGradientBrush.ResetTransform();
        }

        /// <summary>
        ///     Rotates the local geometric transformation of this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />
        ///     object by the specified amount. This method prepends the rotation to the transformation.
        /// </summary>
        /// <param name="angle">The angle of rotation. </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle)
        {
            WrappedPathGradientBrush.RotateTransform(angle);
        }

        /// <summary>
        ///     Rotates the local geometric transformation of this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />
        ///     object by the specified amount in the specified order.
        /// </summary>
        /// <param name="angle">The angle of rotation. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether to
        ///     append or prepend the rotation matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle, MatrixOrder order)
        {
            WrappedPathGradientBrush.RotateTransform(angle, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Scales the local geometric transformation of this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />
        ///     object by the specified amounts. This method prepends the scaling matrix to the transformation.
        /// </summary>
        /// <param name="sx">The amount by which to scale the transformation in the x direction. </param>
        /// <param name="sy">The amount by which to scale the transformation in the y direction. </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy)
        {
            WrappedPathGradientBrush.ScaleTransform(sx, sy);
        }

        /// <summary>
        ///     Scales the local geometric transformation of this <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />
        ///     object by the specified amounts in the specified order.
        /// </summary>
        /// <param name="sx">The amount by which to scale the transformation in the x direction. </param>
        /// <param name="sy">The amount by which to scale the transformation in the y direction. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether to
        ///     append or prepend the scaling matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            WrappedPathGradientBrush.ScaleTransform(sx, sy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>Creates a gradient with a center color and a linear falloff to one surrounding color.</summary>
        /// <param name="focus">
        ///     A value from 0 through 1 that specifies where, along any radial from the center of the path to the
        ///     path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest
        ///     intensity at the center of the path.
        /// </param>
        public void SetBlendTriangularShape(float focus)
        {
            WrappedPathGradientBrush.SetBlendTriangularShape(focus);
        }

        /// <summary>Creates a gradient with a center color and a linear falloff to each surrounding color.</summary>
        /// <param name="focus">
        ///     A value from 0 through 1 that specifies where, along any radial from the center of the path to the
        ///     path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest
        ///     intensity at the center of the path.
        /// </param>
        /// <param name="scale">
        ///     A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended
        ///     with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the
        ///     default value.
        /// </param>
        public void SetBlendTriangularShape(float focus, float scale)
        {
            WrappedPathGradientBrush.SetBlendTriangularShape(focus, scale);
        }

        /// <summary>
        ///     Creates a gradient brush that changes color starting from the center of the path outward to the path's
        ///     boundary. The transition from one color to another is based on a bell-shaped curve.
        /// </summary>
        /// <param name="focus">
        ///     A value from 0 through 1 that specifies where, along any radial from the center of the path to the
        ///     path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest
        ///     intensity at the center of the path.
        /// </param>
        public void SetSigmaBellShape(float focus)
        {
            WrappedPathGradientBrush.SetSigmaBellShape(focus);
        }

        /// <summary>
        ///     Creates a gradient brush that changes color starting from the center of the path outward to the path's
        ///     boundary. The transition from one color to another is based on a bell-shaped curve.
        /// </summary>
        /// <param name="focus">
        ///     A value from 0 through 1 that specifies where, along any radial from the center of the path to the
        ///     path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest
        ///     intensity at the center of the path.
        /// </param>
        /// <param name="scale">
        ///     A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended
        ///     with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the
        ///     default value.
        /// </param>
        public void SetSigmaBellShape(float focus, float scale)
        {
            WrappedPathGradientBrush.SetSigmaBellShape(focus, scale);
        }

        /// <summary>
        ///     Translates the local geometric transformation of this
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> object by the specified dimensions. This method
        ///     prepends the translation to the transformation.
        /// </summary>
        /// <param name="dx">The dimension by which to translate the transformation in the x direction. </param>
        /// <param name="dy">The dimension by which to translate the transformation in the y direction. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy)
        {
            WrappedPathGradientBrush.TranslateTransform(dx, dy);
        }

        /// <summary>
        ///     Translates the local geometric transformation of this
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> object by the specified dimensions in the specified
        ///     order.
        /// </summary>
        /// <param name="dx">The dimension by which to translate the transformation in the x direction. </param>
        /// <param name="dy">The dimension by which to translate the transformation in the y direction. </param>
        /// <param name="order">The order (prepend or append) in which to apply the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            WrappedPathGradientBrush.TranslateTransform(dx, dy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }


        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Texture" /> that results from the conversion.</returns>
        /// <param name="pathGradientBrush">The <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator PathGradientBrush(System.Drawing.Drawing2D.PathGradientBrush pathGradientBrush)
        {
            return pathGradientBrush == null ? null : new PathGradientBrush(pathGradientBrush);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> that results from the conversion.</returns>
        /// <param name="pathGradientBrush">The <see cref="T:Common.Drawing.Drawing2D.PathGradientBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.PathGradientBrush(PathGradientBrush pathGradientBrush)
        {
            return pathGradientBrush?.WrappedPathGradientBrush;
        }
    }
}