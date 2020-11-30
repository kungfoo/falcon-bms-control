using System;
using System.Linq;

namespace Common.Drawing.Drawing2D
{
    /// <summary>Encapsulates a 3-by-3 affine matrix that represents a geometric transform. This class cannot be inherited.</summary>
    public sealed class Matrix : MarshalByRefObject, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> class as the identity
        ///     matrix.
        /// </summary>
        public Matrix()
        {
            WrappedMatrix = new System.Drawing.Drawing2D.Matrix();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> class with the specified
        ///     elements.
        /// </summary>
        /// <param name="m11">
        ///     The value in the first row and first column of the new
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </param>
        /// <param name="m12">
        ///     The value in the first row and second column of the new
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </param>
        /// <param name="m21">
        ///     The value in the second row and first column of the new
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </param>
        /// <param name="m22">
        ///     The value in the second row and second column of the new
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </param>
        /// <param name="dx">
        ///     The value in the third row and first column of the new
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </param>
        /// <param name="dy">
        ///     The value in the third row and second column of the new
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </param>
        public Matrix(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            WrappedMatrix = new System.Drawing.Drawing2D.Matrix(m11, m12, m21, m22, dx, dy);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> class to the geometric
        ///     transform defined by the specified rectangle and array of points.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.RectangleF" /> structure that represents the rectangle to be
        ///     transformed.
        /// </param>
        /// <param name="plgpts">
        ///     An array of three <see cref="T:Common.Drawing.PointF" /> structures that represents the points of
        ///     a parallelogram to which the upper-left, upper-right, and lower-left corners of the rectangle is to be transformed.
        ///     The lower-right corner of the parallelogram is implied by the first three corners.
        /// </param>
        public Matrix(RectangleF rect, PointF[] plgpts)
        {
            WrappedMatrix =
                new System.Drawing.Drawing2D.Matrix(rect, plgpts.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Drawing2D.Matrix" /> class to the geometric
        ///     transform defined by the specified rectangle and array of points.
        /// </summary>
        /// <param name="rect">
        ///     A <see cref="T:Common.Drawing.Rectangle" /> structure that represents the rectangle to be
        ///     transformed.
        /// </param>
        /// <param name="plgpts">
        ///     An array of three <see cref="T:Common.Drawing.Point" /> structures that represents the points of a
        ///     parallelogram to which the upper-left, upper-right, and lower-left corners of the rectangle is to be transformed.
        ///     The lower-right corner of the parallelogram is implied by the first three corners.
        /// </param>
        public Matrix(Rectangle rect, Point[] plgpts)
        {
            WrappedMatrix = new System.Drawing.Drawing2D.Matrix(rect, plgpts.Convert<System.Drawing.Point>().ToArray());
        }

        private Matrix(System.Drawing.Drawing2D.Matrix wrappedMatrix)
        {
            WrappedMatrix = wrappedMatrix;
        }

        /// <summary>
        ///     Gets an array of floating-point values that represents the elements of this
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <returns>
        ///     An array of floating-point values that represents the elements of this
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float[] Elements => WrappedMatrix.Elements;

        /// <summary>Gets a value indicating whether this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> is the identity matrix.</summary>
        /// <returns>This property is true if this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> is identity; otherwise, false.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsIdentity => WrappedMatrix.IsIdentity;

        /// <summary>Gets a value indicating whether this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> is invertible.</summary>
        /// <returns>
        ///     This property is true if this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> is invertible; otherwise,
        ///     false.
        /// </returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsInvertible => WrappedMatrix.IsInvertible;

        /// <summary>
        ///     Gets the x translation value (the dx value, or the element in the third row and first column) of this
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <returns>The x translation value of this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float OffsetX => WrappedMatrix.OffsetX;

        /// <summary>
        ///     Gets the y translation value (the dy value, or the element in the third row and second column) of this
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <returns>The y translation value of this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float OffsetY => WrappedMatrix.OffsetY;

        private System.Drawing.Drawing2D.Matrix WrappedMatrix { get; }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.</summary>
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

        ~Matrix()
        {
            Dispose(false);
        }

        /// <summary>Creates an exact copy of this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.</summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that this method creates.</returns>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Matrix Clone()
        {
            return new Matrix(WrappedMatrix.Clone());
        }

        /// <summary>
        ///     Tests whether the specified object is a <see cref="T:Common.Drawing.Drawing2D.Matrix" /> and is identical to
        ///     this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <returns>
        ///     This method returns true if <paramref name="obj" /> is the specified
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> identical to this <see cref="T:Common.Drawing.Drawing2D.Matrix" />
        ///     ; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to test. </param>
        public override bool Equals(object obj)
        {
            return WrappedMatrix.Equals(obj);
        }

        /// <summary>Returns a hash code.</summary>
        /// <returns>The hash code for this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.</returns>
        public override int GetHashCode()
        {
            return WrappedMatrix.GetHashCode();
        }

        /// <summary>Inverts this <see cref="T:Common.Drawing.Drawing2D.Matrix" />, if it is invertible.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Invert()
        {
            WrappedMatrix.Invert();
        }

        /// <summary>
        ///     Multiplies this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by the matrix specified in the
        ///     <paramref name="matrix" /> parameter, by prepending the specified <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <param name="matrix">
        ///     The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by which this
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> is to be multiplied.
        /// </param>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Multiply(Matrix matrix)
        {
            WrappedMatrix.Multiply(matrix.WrappedMatrix);
        }

        /// <summary>
        ///     Multiplies this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by the matrix specified in the
        ///     <paramref name="matrix" /> parameter, and in the order specified in the <paramref name="order" /> parameter.
        /// </summary>
        /// <param name="matrix">
        ///     The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by which this
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> is to be multiplied.
        /// </param>
        /// <param name="order">
        ///     The <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> that represents the order of the
        ///     multiplication.
        /// </param>
        public void Multiply(Matrix matrix, MatrixOrder order)
        {
            WrappedMatrix.Multiply(matrix.WrappedMatrix, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>Resets this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> to have the elements of the identity matrix.</summary>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Reset()
        {
            WrappedMatrix.Reset();
        }

        /// <summary>
        ///     Prepend to this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> a clockwise rotation, around the origin and
        ///     by the specified angle.
        /// </summary>
        /// <param name="angle">The angle of the rotation, in degrees. </param>
        public void Rotate(float angle)
        {
            WrappedMatrix.Rotate(angle);
        }

        /// <summary>
        ///     Applies a clockwise rotation of an amount specified in the <paramref name="angle" /> parameter, around the
        ///     origin (zero x and y coordinates) for this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <param name="angle">The angle (extent) of the rotation, in degrees. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or
        ///     prepend) in which the rotation is applied to this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </param>
        public void Rotate(float angle, MatrixOrder order)
        {
            WrappedMatrix.Rotate(angle, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Applies a clockwise rotation to this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> around the point
        ///     specified in the <paramref name="point" /> parameter, and by prepending the rotation.
        /// </summary>
        /// <param name="angle">The angle (extent) of the rotation, in degrees. </param>
        /// <param name="point">A <see cref="T:Common.Drawing.PointF" /> that represents the center of the rotation. </param>
        public void RotateAt(float angle, PointF point)
        {
            WrappedMatrix.RotateAt(angle, point);
        }

        /// <summary>
        ///     Applies a clockwise rotation about the specified point to this
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> in the specified order.
        /// </summary>
        /// <param name="angle">The angle of the rotation, in degrees. </param>
        /// <param name="point">A <see cref="T:Common.Drawing.PointF" /> that represents the center of the rotation. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or
        ///     prepend) in which the rotation is applied.
        /// </param>
        public void RotateAt(float angle, PointF point, MatrixOrder order)
        {
            WrappedMatrix.RotateAt(angle, point, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Applies the specified scale vector to this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by prepending the
        ///     scale vector.
        /// </summary>
        /// <param name="scaleX">
        ///     The value by which to scale this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> in the x-axis
        ///     direction.
        /// </param>
        /// <param name="scaleY">
        ///     The value by which to scale this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> in the y-axis
        ///     direction.
        /// </param>
        public void Scale(float scaleX, float scaleY)
        {
            WrappedMatrix.Scale(scaleX, scaleY);
        }

        /// <summary>
        ///     Applies the specified scale vector (<paramref name="scaleX" /> and <paramref name="scaleY" />) to this
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> using the specified order.
        /// </summary>
        /// <param name="scaleX">
        ///     The value by which to scale this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> in the x-axis
        ///     direction.
        /// </param>
        /// <param name="scaleY">
        ///     The value by which to scale this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> in the y-axis
        ///     direction.
        /// </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or
        ///     prepend) in which the scale vector is applied to this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </param>
        public void Scale(float scaleX, float scaleY, MatrixOrder order)
        {
            WrappedMatrix.Scale(scaleX, scaleY, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Applies the specified shear vector to this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by prepending the
        ///     shear transformation.
        /// </summary>
        /// <param name="shearX">The horizontal shear factor. </param>
        /// <param name="shearY">The vertical shear factor. </param>
        public void Shear(float shearX, float shearY)
        {
            WrappedMatrix.Shear(shearX, shearY);
        }

        /// <summary>
        ///     Applies the specified shear vector to this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> in the specified
        ///     order.
        /// </summary>
        /// <param name="shearX">The horizontal shear factor. </param>
        /// <param name="shearY">The vertical shear factor. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or
        ///     prepend) in which the shear is applied.
        /// </param>
        public void Shear(float shearX, float shearY, MatrixOrder order)
        {
            WrappedMatrix.Shear(shearX, shearY, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Applies the geometric transform represented by this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> to a
        ///     specified array of points.
        /// </summary>
        /// <param name="pts">
        ///     An array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points to
        ///     transform.
        /// </param>
        public void TransformPoints(PointF[] pts)
        {
            WrappedMatrix.TransformPoints(pts.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Applies the geometric transform represented by this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> to a
        ///     specified array of points.
        /// </summary>
        /// <param name="pts">An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points to transform. </param>
        public void TransformPoints(Point[] pts)
        {
            WrappedMatrix.TransformPoints(pts.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Multiplies each vector in an array by the matrix. The translation elements of this matrix (third row) are
        ///     ignored.
        /// </summary>
        /// <param name="pts">An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points to transform. </param>
        public void TransformVectors(PointF[] pts)
        {
            WrappedMatrix.TransformVectors(pts.Convert<System.Drawing.PointF>().ToArray());
        }

        /// <summary>
        ///     Applies only the scale and rotate components of this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> to the
        ///     specified array of points.
        /// </summary>
        /// <param name="pts">An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points to transform. </param>
        public void TransformVectors(Point[] pts)
        {
            WrappedMatrix.TransformVectors(pts.Convert<System.Drawing.Point>().ToArray());
        }

        /// <summary>
        ///     Applies the specified translation vector (<paramref name="offsetX" /> and <paramref name="offsetY" />) to this
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" /> by prepending the translation vector.
        /// </summary>
        /// <param name="offsetX">The x value by which to translate this <see cref="T:Common.Drawing.Drawing2D.Matrix" />. </param>
        /// <param name="offsetY">The y value by which to translate this <see cref="T:Common.Drawing.Drawing2D.Matrix" />. </param>
        public void Translate(float offsetX, float offsetY)
        {
            WrappedMatrix.Translate(offsetX, offsetY);
        }

        /// <summary>
        ///     Applies the specified translation vector to this <see cref="T:Common.Drawing.Drawing2D.Matrix" /> in the
        ///     specified order.
        /// </summary>
        /// <param name="offsetX">The x value by which to translate this <see cref="T:Common.Drawing.Drawing2D.Matrix" />. </param>
        /// <param name="offsetY">The y value by which to translate this <see cref="T:Common.Drawing.Drawing2D.Matrix" />. </param>
        /// <param name="order">
        ///     A <see cref="T:Common.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or
        ///     prepend) in which the translation is applied to this <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </param>
        public void Translate(float offsetX, float offsetY, MatrixOrder order)
        {
            WrappedMatrix.Translate(offsetX, offsetY, (System.Drawing.Drawing2D.MatrixOrder) order);
        }

        /// <summary>
        ///     Multiplies each vector in an array by the matrix. The translation elements of this matrix (third row) are
        ///     ignored.
        /// </summary>
        /// <param name="pts">An array of <see cref="T:Common.Drawing.Point" /> structures that represents the points to transform.</param>
        public void VectorTransformPoints(Point[] pts)
        {
            WrappedMatrix.VectorTransformPoints(pts.Convert<System.Drawing.Point>().ToArray());
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedMatrix.Dispose();
            }
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> that results from the conversion.</returns>
        /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Matrix(System.Drawing.Drawing2D.Matrix matrix)
        {
            return matrix == null ? null : new Matrix(matrix);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.Matrix" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.Matrix" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that results from the conversion.</returns>
        /// <param name="matrix">The <see cref="T:Common.Drawing.Drawing2D.Matrix" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.Matrix(Matrix matrix)
        {
            return matrix?.WrappedMatrix;
        }
    }
}