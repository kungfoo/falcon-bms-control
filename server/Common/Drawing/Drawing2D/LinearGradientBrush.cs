using System.Linq;

namespace Common.Drawing.Drawing2D
{
    /// <summary>Encapsulates a <see cref="T:Common.Drawing.Brush" /> with a linear gradient. This class cannot be inherited.</summary>
    public sealed class LinearGradientBrush : Brush
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> class with the
        ///     specified points and colors.
        /// </summary>
        /// <param name="point1">
        ///     A <see cref="T:Common.Drawing.PointF" /> structure that represents the starting point of the
        ///     linear gradient.
        /// </param>
        /// <param name="point2">
        ///     A <see cref="T:Common.Drawing.PointF" /> structure that represents the endpoint of the linear
        ///     gradient.
        /// </param>
        /// <param name="color1">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the starting color of the linear
        ///     gradient.
        /// </param>
        /// <param name="color2">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the ending color of the linear
        ///     gradient.
        /// </param>
        public LinearGradientBrush(PointF point1, PointF point2, Color color1, Color color2)
        {
            WrappedLinearGradientBrush =
                new System.Drawing.Drawing2D.LinearGradientBrush(point1, point2, color1, color2);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> class with the
        ///     specified points and colors.
        /// </summary>
        /// <param name="point1">
        ///     A <see cref="T:Common.Drawing.Point" /> structure that represents the starting point of the linear
        ///     gradient.
        /// </param>
        /// <param name="point2">
        ///     A <see cref="T:Common.Drawing.Point" /> structure that represents the endpoint of the linear
        ///     gradient.
        /// </param>
        /// <param name="color1">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the starting color of the linear
        ///     gradient.
        /// </param>
        /// <param name="color2">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the ending color of the linear
        ///     gradient.
        /// </param>
        public LinearGradientBrush(Point point1, Point point2, Color color1, Color color2)
        {
            WrappedLinearGradientBrush =
                new System.Drawing.Drawing2D.LinearGradientBrush(point1, point2, color1, color2);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> based on a
        ///     rectangle, starting and ending colors, and an orientation mode.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the bounds of the linear
        ///     gradient.
        /// </param>
        /// <param name="color1">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the starting color for the
        ///     gradient.
        /// </param>
        /// <param name="color2">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the ending color for the
        ///     gradient.
        /// </param>
        /// <param name="linearGradientMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.LinearGradientMode" /> enumeration element
        ///     that specifies the orientation of the gradient. The orientation determines the starting and ending points of the
        ///     gradient. For example, LinearGradientMode.ForwardDiagonal specifies that the starting point is the upper-left
        ///     corner of the rectangle and the ending point is the lower-right corner of the rectangle.
        /// </param>
        public LinearGradientBrush(RectangleF rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
        {
            WrappedLinearGradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, color1, color2,
                (System.Drawing.Drawing2D.LinearGradientMode) linearGradientMode);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> class based on a
        ///     rectangle, starting and ending colors, and orientation.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the bounds of the linear
        ///     gradient.
        /// </param>
        /// <param name="color1">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the starting color for the
        ///     gradient.
        /// </param>
        /// <param name="color2">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the ending color for the
        ///     gradient.
        /// </param>
        /// <param name="linearGradientMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.LinearGradientMode" /> enumeration element
        ///     that specifies the orientation of the gradient. The orientation determines the starting and ending points of the
        ///     gradient. For example, LinearGradientMode.ForwardDiagonal specifies that the starting point is the upper-left
        ///     corner of the rectangle and the ending point is the lower-right corner of the rectangle.
        /// </param>
        public LinearGradientBrush(Rectangle rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
        {
            WrappedLinearGradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, color1, color2,
                (System.Drawing.Drawing2D.LinearGradientMode) linearGradientMode);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> class based on a
        ///     rectangle, starting and ending colors, and an orientation angle.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the bounds of the linear
        ///     gradient.
        /// </param>
        /// <param name="color1">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the starting color for the
        ///     gradient.
        /// </param>
        /// <param name="color2">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the ending color for the
        ///     gradient.
        /// </param>
        /// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. </param>
        public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle)
        {
            WrappedLinearGradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, color1, color2, angle);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> class based on a
        ///     rectangle, starting and ending colors, and an orientation angle.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the bounds of the linear
        ///     gradient.
        /// </param>
        /// <param name="color1">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the starting color for the
        ///     gradient.
        /// </param>
        /// <param name="color2">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the ending color for the
        ///     gradient.
        /// </param>
        /// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. </param>
        /// <param name="isAngleScaleable">
        ///     Set to true to specify that the angle is affected by the transform associated with this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, false.
        /// </param>
        public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle, bool isAngleScaleable)
        {
            WrappedLinearGradientBrush =
                new System.Drawing.Drawing2D.LinearGradientBrush(rect, color1, color2, angle, isAngleScaleable);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> class based on a
        ///     rectangle, starting and ending colors, and an orientation angle.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the bounds of the linear
        ///     gradient.
        /// </param>
        /// <param name="color1">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the starting color for the
        ///     gradient.
        /// </param>
        /// <param name="color2">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the ending color for the
        ///     gradient.
        /// </param>
        /// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. </param>
        public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle)
        {
            WrappedLinearGradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, color1, color2, angle);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> class based on a
        ///     rectangle, starting and ending colors, and an orientation angle.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that specifies the bounds of the linear
        ///     gradient.
        /// </param>
        /// <param name="color1">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the starting color for the
        ///     gradient.
        /// </param>
        /// <param name="color2">
        ///     A <see cref="T:Common.Drawing.Color" /> structure that represents the ending color for the
        ///     gradient.
        /// </param>
        /// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. </param>
        /// <param name="isAngleScaleable">
        ///     Set to true to specify that the angle is affected by the transform associated with this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, false.
        /// </param>
        public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle, bool isAngleScaleable)
        {
            WrappedLinearGradientBrush =
                new System.Drawing.Drawing2D.LinearGradientBrush(rect, color1, color2, angle, isAngleScaleable);
        }

        internal LinearGradientBrush(System.Drawing.Drawing2D.LinearGradientBrush linearGradientBrush)
        {
            WrappedLinearGradientBrush = linearGradientBrush;
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
            get => WrappedLinearGradientBrush.Blend;
            set => WrappedLinearGradientBrush.Blend = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether gamma correction is enabled for this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" />.
        /// </summary>
        /// <returns>
        ///     The value is true if gamma correction is enabled for this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, false.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool GammaCorrection
        {
            get => WrappedLinearGradientBrush.GammaCorrection;
            set => WrappedLinearGradientBrush.GammaCorrection = value;
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
            get => WrappedLinearGradientBrush.InterpolationColors;
            set => WrappedLinearGradientBrush.InterpolationColors = value;
        }

        /// <summary>Gets or sets the starting and ending colors of the gradient.</summary>
        /// <returns>
        ///     An array of two <see cref="T:Common.Drawing.Color" /> structures that represents the starting and ending
        ///     colors of the gradient.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Color[] LinearColors
        {
            get => WrappedLinearGradientBrush.LinearColors.Convert<Color>().ToArray();
            set => WrappedLinearGradientBrush.LinearColors = value.Convert<System.Drawing.Color>().ToArray();
        }

        /// <summary>Gets a rectangular region that defines the starting and ending points of the gradient.</summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that specifies the starting and ending points of the
        ///     gradient.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public RectangleF Rectangle => WrappedLinearGradientBrush.Rectangle;

        /// <summary>
        ///     Gets or sets a copy <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that defines a local geometric transform
        ///     for this <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" />.
        /// </summary>
        /// <returns>
        ///     A copy of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that defines a geometric transform that applies
        ///     only to fills drawn with this <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Matrix Transform
        {
            get => WrappedLinearGradientBrush.Transform;
            set => WrappedLinearGradientBrush.Transform = value;
        }

        /// <summary>
        ///     Gets or sets a <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> enumeration that indicates the wrap mode for
        ///     this <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> are tiled.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public WrapMode WrapMode
        {
            get => (WrapMode) WrappedLinearGradientBrush.WrapMode;
            set => WrappedLinearGradientBrush.WrapMode = (System.Drawing.Drawing2D.WrapMode) value;
        }

        private System.Drawing.Drawing2D.LinearGradientBrush WrappedLinearGradientBrush
        {
            get => WrappedBrush as System.Drawing.Drawing2D.LinearGradientBrush;
            set => WrappedBrush = value;
        }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> this method creates, cast as an object.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public override object Clone()
        {
            return new LinearGradientBrush(
                (System.Drawing.Drawing2D.LinearGradientBrush) WrappedLinearGradientBrush.Clone());
        }

        /// <summary>
        ///     Multiplies the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object that represents the local geometric
        ///     transformation of this <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object by the specified
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
            WrappedLinearGradientBrush.MultiplyTransform(matrix);
        }

        /// <summary>
        ///     Multiplies the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object that represents the local geometric
        ///     transformation of this <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object by the specified
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
            WrappedLinearGradientBrush.MultiplyTransform(matrix, (System.Drawing.Drawing2D.MatrixOrder) order);
        }


        /// <summary>
        ///     Resets the Transform property of this <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object to
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
            WrappedLinearGradientBrush.ResetTransform();
        }

        /// <summary>
        ///     Rotates the local geometric transformation of this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object by the specified amount. This method prepends
        ///     the rotation to the transformation.
        /// </summary>
        /// <param name="angle">The angle of rotation. </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle)
        {
            WrappedLinearGradientBrush.RotateTransform(angle);
        }

        /// <summary>
        ///     Rotates the local geometric transformation of this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object by the specified amount in the specified
        ///     order.
        /// </summary>
        /// <param name="angle">The angle of rotation. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether to
        ///     append or prepend the rotation matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle, MatrixOrder order)
        {
            WrappedLinearGradientBrush.RotateTransform(angle, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Scales the local geometric transformation of this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object by the specified amounts. This method prepends
        ///     the scaling matrix to the transformation.
        /// </summary>
        /// <param name="sx">The amount by which to scale the transformation in the x direction. </param>
        /// <param name="sy">The amount by which to scale the transformation in the y direction. </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy)
        {
            WrappedLinearGradientBrush.ScaleTransform(sx, sy);
        }

        /// <summary>
        ///     Scales the local geometric transformation of this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object by the specified amounts in the specified
        ///     order.
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
            WrappedLinearGradientBrush.ScaleTransform(sx, sy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>Creates a linear gradient with a center color and a linear falloff to a single color on both ends.</summary>
        /// <param name="focus">
        ///     A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is
        ///     composed of only the ending color).
        /// </param>
        public void SetBlendTriangularShape(float focus)
        {
            WrappedLinearGradientBrush.SetBlendTriangularShape(focus);
        }

        /// <summary>Creates a linear gradient with a center color and a linear falloff to a single color on both ends.</summary>
        /// <param name="focus">
        ///     A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is
        ///     composed of only the ending color).
        /// </param>
        /// <param name="scale">
        ///     A value from 0 through1 that specifies how fast the colors falloff from the starting color to
        ///     <paramref name="focus" /> (ending color)
        /// </param>
        public void SetBlendTriangularShape(float focus, float scale)
        {
            WrappedLinearGradientBrush.SetBlendTriangularShape(focus, scale);
        }

        /// <summary>Creates a gradient falloff based on a bell-shaped curve.</summary>
        /// <param name="focus">
        ///     A value from 0 through 1 that specifies the center of the gradient (the point where the starting
        ///     color and ending color are blended equally).
        /// </param>
        public void SetSigmaBellShape(float focus)
        {
            WrappedLinearGradientBrush.SetSigmaBellShape(focus);
        }

        /// <summary>Creates a gradient falloff based on a bell-shaped curve.</summary>
        /// <param name="focus">
        ///     A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is
        ///     composed of only the ending color).
        /// </param>
        /// <param name="scale">
        ///     A value from 0 through 1 that specifies how fast the colors falloff from the
        ///     <paramref name="focus" />.
        /// </param>
        public void SetSigmaBellShape(float focus, float scale)
        {
            WrappedLinearGradientBrush.SetSigmaBellShape(focus, scale);
        }

        /// <summary>
        ///     Translates the local geometric transformation of this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object by the specified dimensions. This method
        ///     prepends the translation to the transformation.
        /// </summary>
        /// <param name="dx">The dimension by which to translate the transformation in the x direction. </param>
        /// <param name="dy">The dimension by which to translate the transformation in the y direction. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy)
        {
            WrappedLinearGradientBrush.TranslateTransform(dx, dy);
        }

        /// <summary>
        ///     Translates the local geometric transformation of this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object by the specified dimensions in the specified
        ///     order.
        /// </summary>
        /// <param name="dx">The dimension by which to translate the transformation in the x direction. </param>
        /// <param name="dy">The dimension by which to translate the transformation in the y direction. </param>
        /// <param name="order">The order (prepend or append) in which to apply the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            WrappedLinearGradientBrush.TranslateTransform(dx, dy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }


        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Texture" /> that results from the conversion.</returns>
        /// <param name="linearGradientBrush">The <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator LinearGradientBrush(
            System.Drawing.Drawing2D.LinearGradientBrush linearGradientBrush)
        {
            return linearGradientBrush == null ? null : new LinearGradientBrush(linearGradientBrush);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> that results from the conversion.</returns>
        /// <param name="linearGradientBrush">The <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.LinearGradientBrush(
            LinearGradientBrush linearGradientBrush)
        {
            return linearGradientBrush?.WrappedLinearGradientBrush;
        }
    }
}