using Common.Drawing.Drawing2D;
using Common.Drawing.Imaging;

namespace Common.Drawing
{
    /// <summary>
    ///     Each property of the <see cref="T:Common.Drawing.TextureBrush" /> class is a
    ///     <see cref="T:Common.Drawing.Brush" /> object that uses an image to fill the interior of a shape. This class cannot
    ///     be inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    public sealed class TextureBrush : Brush
    {
        /// <summary>Initializes a new <see cref="T:Common.Drawing.TextureBrush" /> object that uses the specified image.</summary>
        /// <param name="bitmap">
        ///     The <see cref="T:Common.Drawing.Image" /> object with which this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object fills interiors.
        /// </param>
        public TextureBrush(Image bitmap)
        {
            WrappedTextureBrush = new System.Drawing.TextureBrush(bitmap);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.TextureBrush" /> object that uses the specified image and wrap
        ///     mode.
        /// </summary>
        /// <param name="image">
        ///     The <see cref="T:Common.Drawing.Image" /> object with which this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object fills interiors.
        /// </param>
        /// <param name="wrapMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> enumeration that specifies how this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object is tiled.
        /// </param>
        public TextureBrush(Image image, WrapMode wrapMode)
        {
            WrappedTextureBrush = new System.Drawing.TextureBrush(image, (System.Drawing.Drawing2D.WrapMode) wrapMode);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.TextureBrush" /> object that uses the specified image, wrap
        ///     mode, and bounding rectangle.
        /// </summary>
        /// <param name="image">
        ///     The <see cref="T:Common.Drawing.Image" /> object with which this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object fills interiors.
        /// </param>
        /// <param name="wrapMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> enumeration that specifies how this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object is tiled.
        /// </param>
        /// <param name="dstRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that represents the bounding rectangle for
        ///     this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </param>
        public TextureBrush(Image image, WrapMode wrapMode, RectangleF dstRect)
        {
            WrappedTextureBrush =
                new System.Drawing.TextureBrush(image, (System.Drawing.Drawing2D.WrapMode) wrapMode, dstRect);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.TextureBrush" /> object that uses the specified image, wrap
        ///     mode, and bounding rectangle.
        /// </summary>
        /// <param name="image">
        ///     The <see cref="T:Common.Drawing.Image" /> object with which this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object fills interiors.
        /// </param>
        /// <param name="wrapMode">
        ///     A <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> enumeration that specifies how this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object is tiled.
        /// </param>
        /// <param name="dstRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that represents the bounding rectangle for
        ///     this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </param>
        public TextureBrush(Image image, WrapMode wrapMode, Rectangle dstRect)
        {
            WrappedTextureBrush =
                new System.Drawing.TextureBrush(image, (System.Drawing.Drawing2D.WrapMode) wrapMode, dstRect);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.TextureBrush" /> object that uses the specified image and
        ///     bounding rectangle.
        /// </summary>
        /// <param name="image">
        ///     The <see cref="T:Common.Drawing.Image" /> object with which this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object fills interiors.
        /// </param>
        /// <param name="dstRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that represents the bounding rectangle for
        ///     this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </param>
        public TextureBrush(Image image, RectangleF dstRect)
        {
            WrappedTextureBrush = new System.Drawing.TextureBrush(image, dstRect);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.TextureBrush" /> object that uses the specified image, bounding
        ///     rectangle, and image attributes.
        /// </summary>
        /// <param name="image">
        ///     The <see cref="T:Common.Drawing.Image" /> object with which this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object fills interiors.
        /// </param>
        /// <param name="dstRect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that represents the bounding rectangle for
        ///     this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </param>
        /// <param name="imageAttr">
        ///     An <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> object that contains additional
        ///     information about the image used by this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </param>
        public TextureBrush(Image image, RectangleF dstRect, ImageAttributes imageAttr)
        {
            WrappedTextureBrush = new System.Drawing.TextureBrush(image, dstRect, imageAttr);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.TextureBrush" /> object that uses the specified image and
        ///     bounding rectangle.
        /// </summary>
        /// <param name="image">
        ///     The <see cref="T:Common.Drawing.Image" /> object with which this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object fills interiors.
        /// </param>
        /// <param name="dstRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that represents the bounding rectangle for
        ///     this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </param>
        public TextureBrush(Image image, Rectangle dstRect)
        {
            WrappedTextureBrush = new System.Drawing.TextureBrush(image, dstRect);
        }

        /// <summary>
        ///     Initializes a new <see cref="T:Common.Drawing.TextureBrush" /> object that uses the specified image, bounding
        ///     rectangle, and image attributes.
        /// </summary>
        /// <param name="image">
        ///     The <see cref="T:Common.Drawing.Image" /> object with which this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object fills interiors.
        /// </param>
        /// <param name="dstRect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that represents the bounding rectangle for
        ///     this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </param>
        /// <param name="imageAttr">
        ///     An <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> object that contains additional
        ///     information about the image used by this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </param>
        public TextureBrush(Image image, Rectangle dstRect, ImageAttributes imageAttr)
        {
            WrappedTextureBrush = new System.Drawing.TextureBrush(image, dstRect, imageAttr);
        }

        internal TextureBrush(System.Drawing.TextureBrush textureBrush)
        {
            WrappedTextureBrush = textureBrush;
        }

        /// <summary>
        ///     Gets the <see cref="T:Common.Drawing.Image" /> object associated with this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:Common.Drawing.Image" /> object that represents the image with which this
        ///     <see cref="T:Common.Drawing.TextureBrush" /> object fills shapes.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Image Image => WrappedTextureBrush.Image;

        /// <summary>
        ///     Gets or sets a copy of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object that defines a local
        ///     geometric transformation for the image associated with this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </summary>
        /// <returns>
        ///     A copy of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object that defines a geometric transformation
        ///     that applies only to fills drawn by using this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Matrix Transform
        {
            get => WrappedTextureBrush.Transform;
            set => WrappedTextureBrush.Transform = value;
        }

        /// <summary>
        ///     Gets or sets a <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> enumeration that indicates the wrap mode for
        ///     this <see cref="T:Common.Drawing.TextureBrush" /> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Common.Drawing.Drawing2D.WrapMode" /> enumeration that specifies how fills drawn by using this
        ///     <see cref="T:Common.Drawing.Drawing2D.LinearGradientBrush" /> object are tiled.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public WrapMode WrapMode
        {
            get => (WrapMode) WrappedTextureBrush.WrapMode;
            set => WrappedTextureBrush.WrapMode = (System.Drawing.Drawing2D.WrapMode) value;
        }

        private System.Drawing.TextureBrush WrappedTextureBrush
        {
            get => WrappedBrush as System.Drawing.TextureBrush;
            set => WrappedBrush = value;
        }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.TextureBrush" /> object.</summary>
        /// <returns>
        ///     The <see cref="T:Common.Drawing.TextureBrush" /> object this method creates, cast as an
        ///     <see cref="T:System.Object" /> object.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public override object Clone()
        {
            return new TextureBrush((System.Drawing.TextureBrush) WrappedTextureBrush.Clone());
        }

        /// <summary>
        ///     Multiplies the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object that represents the local geometric
        ///     transformation of this <see cref="T:Common.Drawing.TextureBrush" /> object by the specified
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
            WrappedTextureBrush.MultiplyTransform(matrix);
        }

        /// <summary>
        ///     Multiplies the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> object that represents the local geometric
        ///     transformation of this <see cref="T:Common.Drawing.TextureBrush" /> object by the specified
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
            WrappedTextureBrush.MultiplyTransform(matrix, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>Resets the Transform property of this <see cref="T:Common.Drawing.TextureBrush" /> object to identity.</summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ResetTransform()
        {
            WrappedTextureBrush.ResetTransform();
        }

        /// <summary>
        ///     Rotates the local geometric transformation of this <see cref="T:Common.Drawing.TextureBrush" /> object by the
        ///     specified amount. This method prepends the rotation to the transformation.
        /// </summary>
        /// <param name="angle">The angle of rotation. </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle)
        {
            WrappedTextureBrush.RotateTransform(angle);
        }

        /// <summary>
        ///     Rotates the local geometric transformation of this <see cref="T:Common.Drawing.TextureBrush" /> object by the
        ///     specified amount in the specified order.
        /// </summary>
        /// <param name="angle">The angle of rotation. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether to
        ///     append or prepend the rotation matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle, MatrixOrder order)
        {
            WrappedTextureBrush.RotateTransform(angle, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Scales the local geometric transformation of this <see cref="T:Common.Drawing.TextureBrush" /> object by the
        ///     specified amounts. This method prepends the scaling matrix to the transformation.
        /// </summary>
        /// <param name="sx">The amount by which to scale the transformation in the x direction. </param>
        /// <param name="sy">The amount by which to scale the transformation in the y direction. </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy)
        {
            WrappedTextureBrush.ScaleTransform(sx, sy);
        }

        /// <summary>
        ///     Scales the local geometric transformation of this <see cref="T:Common.Drawing.TextureBrush" /> object by the
        ///     specified amounts in the specified order.
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
            WrappedTextureBrush.ScaleTransform(sx, sy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Translates the local geometric transformation of this <see cref="T:Common.Drawing.TextureBrush" /> object by
        ///     the specified dimensions. This method prepends the translation to the transformation.
        /// </summary>
        /// <param name="dx">The dimension by which to translate the transformation in the x direction. </param>
        /// <param name="dy">The dimension by which to translate the transformation in the y direction. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy)
        {
            WrappedTextureBrush.TranslateTransform(dx, dy);
        }

        /// <summary>
        ///     Translates the local geometric transformation of this <see cref="T:Common.Drawing.TextureBrush" /> object by
        ///     the specified dimensions in the specified order.
        /// </summary>
        /// <param name="dx">The dimension by which to translate the transformation in the x direction. </param>
        /// <param name="dy">The dimension by which to translate the transformation in the y direction. </param>
        /// <param name="order">The order (prepend or append) in which to apply the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            WrappedTextureBrush.TranslateTransform(dx, dy, (System.Drawing.Drawing2D.MatrixOrder) order);
        }


        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.TextureBrush" /> to a
        ///     <see cref="T:Common.Drawing.TextureBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Texture" /> that results from the conversion.</returns>
        /// <param name="textureBrush">The <see cref="T:System.Drawing.TextureBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator TextureBrush(System.Drawing.TextureBrush textureBrush)
        {
            return textureBrush == null ? null : new TextureBrush(textureBrush);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.TextureBrush" /> to a
        ///     <see cref="T:System.Drawing.TextureBrush" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.TextureBrush" /> that results from the conversion.</returns>
        /// <param name="textureBrush">The <see cref="T:Common.Drawing.TextureBrush" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.TextureBrush(TextureBrush textureBrush)
        {
            return textureBrush?.WrappedTextureBrush;
        }
    }
}