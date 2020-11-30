using System;
using System.Runtime.InteropServices;

namespace Common.Drawing.Imaging
{
    /// <summary>
    ///     Defines a 5 x 5 matrix that contains the coordinates for the RGBAW space. Several methods of the
    ///     <see cref="T:Common.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color matrix. This
    ///     class cannot be inherited.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class ColorMatrix
    {
        /// <summary>Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.ColorMatrix" /> class.</summary>
        public ColorMatrix()
        {
            WrappedColorMatrix = new System.Drawing.Imaging.ColorMatrix();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.ColorMatrix" /> class using the elements
        ///     in the specified matrix <paramref name="newColorMatrix" />.
        /// </summary>
        /// <param name="newColorMatrix">
        ///     The values of the elements for the new <see cref="T:Common.Drawing.Imaging.ColorMatrix" />
        ///     .
        /// </param>
        [CLSCompliant(false)]
        public ColorMatrix(float[][] newColorMatrix)
        {
            WrappedColorMatrix = new System.Drawing.Imaging.ColorMatrix(newColorMatrix);
        }

        private ColorMatrix(System.Drawing.Imaging.ColorMatrix colorMatrix)
        {
            WrappedColorMatrix = colorMatrix;
        }

        /// <summary>
        ///     Gets or sets the element at the specified row and column in the
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the specified row and column.</returns>
        /// <param name="row">The row of the element.</param>
        /// <param name="column">The column of the element.</param>
        public float this[int row, int column]
        {
            get => WrappedColorMatrix[row, column];
            set => WrappedColorMatrix[row, column] = value;
        }

        /// <summary>
        ///     Gets or sets the element at the 0 (zero) row and 0 column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the 0 row and 0 column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix00
        {
            get => WrappedColorMatrix.Matrix00;
            set => WrappedColorMatrix.Matrix00 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the 0 (zero) row and first column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the 0 row and first column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" /> .</returns>
        public float Matrix01
        {
            get => WrappedColorMatrix.Matrix01;
            set => WrappedColorMatrix.Matrix01 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the 0 (zero) row and second column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the 0 row and second column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix02
        {
            get => WrappedColorMatrix.Matrix02;
            set => WrappedColorMatrix.Matrix02 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the 0 (zero) row and third column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.
        /// </summary>
        /// <returns>The element at the 0 row and third column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix03
        {
            get => WrappedColorMatrix.Matrix03;
            set => WrappedColorMatrix.Matrix03 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the 0 (zero) row and fourth column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the 0 row and fourth column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix04
        {
            get => WrappedColorMatrix.Matrix04;
            set => WrappedColorMatrix.Matrix04 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the first row and 0 (zero) column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the first row and 0 column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix10
        {
            get => WrappedColorMatrix.Matrix10;
            set => WrappedColorMatrix.Matrix10 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the first row and first column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the first row and first column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix11
        {
            get => WrappedColorMatrix.Matrix11;
            set => WrappedColorMatrix.Matrix11 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the first row and second column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the first row and second column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix12
        {
            get => WrappedColorMatrix.Matrix12;
            set => WrappedColorMatrix.Matrix12 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the first row and third column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.
        /// </summary>
        /// <returns>The element at the first row and third column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix13
        {
            get => WrappedColorMatrix.Matrix13;
            set => WrappedColorMatrix.Matrix13 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the first row and fourth column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the first row and fourth column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix14
        {
            get => WrappedColorMatrix.Matrix14;
            set => WrappedColorMatrix.Matrix14 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the second row and 0 (zero) column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the second row and 0 column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix20
        {
            get => WrappedColorMatrix.Matrix20;
            set => WrappedColorMatrix.Matrix20 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the second row and first column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the second row and first column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix21
        {
            get => WrappedColorMatrix.Matrix21;
            set => WrappedColorMatrix.Matrix21 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the second row and second column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the second row and second column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix22
        {
            get => WrappedColorMatrix.Matrix22;
            set => WrappedColorMatrix.Matrix22 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the second row and third column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the second row and third column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix23
        {
            get => WrappedColorMatrix.Matrix23;
            set => WrappedColorMatrix.Matrix23 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the second row and fourth column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the second row and fourth column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix24
        {
            get => WrappedColorMatrix.Matrix24;
            set => WrappedColorMatrix.Matrix24 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the third row and 0 (zero) column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the third row and 0 column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix30
        {
            get => WrappedColorMatrix.Matrix30;
            set => WrappedColorMatrix.Matrix30 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the third row and first column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the third row and first column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix31
        {
            get => WrappedColorMatrix.Matrix31;
            set => WrappedColorMatrix.Matrix31 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the third row and second column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the third row and second column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix32
        {
            get => WrappedColorMatrix.Matrix32;
            set => WrappedColorMatrix.Matrix32 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the third row and third column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.
        /// </summary>
        /// <returns>The element at the third row and third column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix33
        {
            get => WrappedColorMatrix.Matrix33;
            set => WrappedColorMatrix.Matrix33 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the third row and fourth column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the third row and fourth column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix34
        {
            get => WrappedColorMatrix.Matrix34;
            set => WrappedColorMatrix.Matrix34 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the fourth row and 0 (zero) column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the fourth row and 0 column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix40
        {
            get => WrappedColorMatrix.Matrix40;
            set => WrappedColorMatrix.Matrix40 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the fourth row and first column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the fourth row and first column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix41
        {
            get => WrappedColorMatrix.Matrix41;
            set => WrappedColorMatrix.Matrix41 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the fourth row and second column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the fourth row and second column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix42
        {
            get => WrappedColorMatrix.Matrix42;
            set => WrappedColorMatrix.Matrix42 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the fourth row and third column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.
        /// </summary>
        /// <returns>The element at the fourth row and third column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix43
        {
            get => WrappedColorMatrix.Matrix43;
            set => WrappedColorMatrix.Matrix43 = value;
        }

        /// <summary>
        ///     Gets or sets the element at the fourth row and fourth column of this
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The element at the fourth row and fourth column of this <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix44
        {
            get => WrappedColorMatrix.Matrix44;
            set => WrappedColorMatrix.Matrix44 = value;
        }

        private System.Drawing.Imaging.ColorMatrix WrappedColorMatrix { get; }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.ColorMatrix" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.ColorMatrix" /> that results from the conversion.</returns>
        /// <param name="colorMatrix">The <see cref="T:System.Drawing.Imaging.ColorMatrix" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator ColorMatrix(System.Drawing.Imaging.ColorMatrix colorMatrix)
        {
            return colorMatrix == null ? null : new ColorMatrix(colorMatrix);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.ColorMatrix" /> to a
        ///     <see cref="T:System.Drawing.Imaging.ColorMatrix" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.ColorMatrix" /> that results from the conversion.</returns>
        /// <param name="colorMatrix">The <see cref="T:Common.Drawing.Imaging.ColorMatrix" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.ColorMatrix(ColorMatrix colorMatrix)
        {
            return colorMatrix?.WrappedColorMatrix;
        }
    }
}