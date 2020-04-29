﻿using System;
using System.Runtime.InteropServices;

namespace Common.Drawing.Imaging
{
    /// <summary>Used to pass a value, or an array of values, to an image encoder. </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class EncoderParameter : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and one unsigned 8-bit integer. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="value">
        ///     An 8-bit unsigned integer that specifies the value stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        public EncoderParameter(Encoder encoder, byte value)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and one 8-bit value. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeUndefined" /> or
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="value">
        ///     A byte that specifies the value stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        /// <param name="undefined">
        ///     If true, the <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property is set
        ///     to <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeUndefined" />; otherwise, the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property is set to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />.
        /// </param>
        public EncoderParameter(Encoder encoder, byte value, bool undefined)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, value, undefined);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and one, 16-bit integer. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeShort" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="value">
        ///     A 16-bit integer that specifies the value stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object. Must be nonnegative.
        /// </param>
        public EncoderParameter(Encoder encoder, short value)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and one 64-bit integer. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeLong" /> (32 bits), and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="value">
        ///     A 64-bit integer that specifies the value stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object. Must be nonnegative. This parameter is converted
        ///     to a 32-bit integer before it is stored in the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        public EncoderParameter(Encoder encoder, long value)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and a pair of 32-bit integers. The pair of
        ///     integers represents a fraction, the first integer being the numerator, and the second integer being the
        ///     denominator. Sets the <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeRational" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="numerator">A 32-bit integer that represents the numerator of a fraction. Must be nonnegative. </param>
        /// <param name="demoninator">A 32-bit integer that represents the denominator of a fraction. Must be nonnegative. </param>
        public EncoderParameter(Encoder encoder, int numerator, int denominator)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, numerator, denominator);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and a pair of 64-bit integers. The pair of
        ///     integers represents a range of integers, the first integer being the smallest number in the range, and the second
        ///     integer being the largest number in the range. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeLongRange" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="rangebegin">
        ///     A 64-bit integer that represents the smallest number in a range of integers. Must be
        ///     nonnegative. This parameter is converted to a 32-bit integer before it is stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        /// <param name="rangeend">
        ///     A 64-bit integer that represents the largest number in a range of integers. Must be nonnegative.
        ///     This parameter is converted to a 32-bit integer before it is stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        public EncoderParameter(Encoder encoder, long rangebegin, long rangeend)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, rangebegin, rangeend);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and four, 32-bit integers. The four integers
        ///     represent a range of fractions. The first two integers represent the smallest fraction in the range, and the
        ///     remaining two integers represent the largest fraction in the range. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeRationalRange" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="numerator1">
        ///     A 32-bit integer that represents the numerator of the smallest fraction in the range. Must be
        ///     nonnegative.
        /// </param>
        /// <param name="demoninator1">
        ///     A 32-bit integer that represents the denominator of the smallest fraction in the range. Must
        ///     be nonnegative.
        /// </param>
        /// <param name="numerator2">
        ///     A 32-bit integer that represents the denominator of the smallest fraction in the range. Must
        ///     be nonnegative.
        /// </param>
        /// <param name="demoninator2">
        ///     A 32-bit integer that represents the numerator of the largest fraction in the range. Must be
        ///     nonnegative.
        /// </param>
        public EncoderParameter(Encoder encoder, int numerator1, int demoninator1, int numerator2, int demoninator2)
        {
            WrappedEncoderParameter =
                new System.Drawing.Imaging.EncoderParameter(encoder, numerator1, demoninator1, numerator2,
                    demoninator2);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and a character string. The string is converted to
        ///     a null-terminated ASCII string before it is stored in the <see cref="T:Common.Drawing.Imaging.EncoderParameter" />
        ///     object. Sets the <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeAscii" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the length of the ASCII string
        ///     including the NULL terminator.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="value">
        ///     A <see cref="T:System.String" /> that specifies the value stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        public EncoderParameter(Encoder encoder, string value)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and an array of unsigned 8-bit integers. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the
        ///     array.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="value">
        ///     An array of 8-bit unsigned integers that specifies the values stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        public EncoderParameter(Encoder encoder, byte[] value)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and an array of bytes. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeUndefined" /> or
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the
        ///     array.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="value">
        ///     An array of bytes that specifies the values stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        /// <param name="undefined">
        ///     If true, the <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property is set
        ///     to <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeUndefined" />; otherwise, the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property is set to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />.
        /// </param>
        public EncoderParameter(Encoder encoder, byte[] value, bool undefined)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, value, undefined);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and an array of 16-bit integers. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeShort" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the
        ///     array.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="value">
        ///     An array of 16-bit integers that specifies the values stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object. The integers in the array must be nonnegative.
        /// </param>
        public EncoderParameter(Encoder encoder, short[] value)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and an array of 64-bit integers. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeLong" /> (32-bit), and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the
        ///     array.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="value">
        ///     An array of 64-bit integers that specifies the values stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object. The integers in the array must be nonnegative. The
        ///     64-bit integers are converted to 32-bit integers before they are stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        public EncoderParameter(Encoder encoder, long[] value)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and two arrays of 32-bit integers. The two arrays
        ///     represent an array of fractions. Sets the <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" />
        ///     property to <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeRational" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the
        ///     <paramref name="numerator" /> array, which must be the same as the number of elements in the
        ///     <paramref name="denominator" /> array.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="numerator">
        ///     An array of 32-bit integers that specifies the numerators of the fractions. The integers in the
        ///     array must be nonnegative.
        /// </param>
        /// <param name="denominator">
        ///     An array of 32-bit integers that specifies the denominators of the fractions. The integers in
        ///     the array must be nonnegative. A denominator of a given index is paired with the numerator of the same index.
        /// </param>
        public EncoderParameter(Encoder encoder, int[] numerator, int[] denominator)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, numerator, denominator);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and two arrays of 64-bit integers. The two arrays
        ///     represent an array integer ranges. Sets the <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" />
        ///     property to <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeLongRange" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the
        ///     <paramref name="rangebegin" /> array, which must be the same as the number of elements in the
        ///     <paramref name="rangeend" /> array.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="rangebegin">
        ///     An array of 64-bit integers that specifies the minimum values for the integer ranges. The
        ///     integers in the array must be nonnegative. The 64-bit integers are converted to 32-bit integers before they are
        ///     stored in the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </param>
        /// <param name="rangeend">
        ///     An array of 64-bit integers that specifies the maximum values for the integer ranges. The
        ///     integers in the array must be nonnegative. The 64-bit integers are converted to 32-bit integers before they are
        ///     stored in the <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> object. A maximum value of a given index is
        ///     paired with the minimum value of the same index.
        /// </param>
        public EncoderParameter(Encoder encoder, long[] rangebegin, long[] rangeend)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, rangebegin, rangeend);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and four arrays of 32-bit integers. The four
        ///     arrays represent an array rational ranges. A rational range is the set of all fractions from a minimum fractional
        ///     value through a maximum fractional value. Sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" /> property to
        ///     <see cref="F:Common.Drawing.Imaging.EncoderParameterValueType.ValueTypeRationalRange" />, and sets the
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the
        ///     <paramref name="numerator1" /> array, which must be the same as the number of elements in the other three arrays.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="numerator1">
        ///     An array of 32-bit integers that specifies the numerators of the minimum values for the
        ///     ranges. The integers in the array must be nonnegative.
        /// </param>
        /// <param name="denominator1">
        ///     An array of 32-bit integers that specifies the denominators of the minimum values for the
        ///     ranges. The integers in the array must be nonnegative.
        /// </param>
        /// <param name="numerator2">
        ///     An array of 32-bit integers that specifies the numerators of the maximum values for the
        ///     ranges. The integers in the array must be nonnegative.
        /// </param>
        /// <param name="denominator2">
        ///     An array of 32-bit integers that specifies the denominators of the maximum values for the
        ///     ranges. The integers in the array must be nonnegative.
        /// </param>
        public EncoderParameter(Encoder encoder, int[] numerator1, int[] denominator1, int[] numerator2,
            int[] denominator2)
        {
            WrappedEncoderParameter =
                new System.Drawing.Imaging.EncoderParameter(encoder, numerator1, denominator1, numerator2,
                    denominator2);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> class with the
        ///     specified <see cref="T:Common.Drawing.Imaging.Encoder" /> object and three integers that specify the number of
        ///     values, the data type of the values, and a pointer to the values stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </summary>
        /// <param name="encoder">
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique
        ///     identifier of the parameter category.
        /// </param>
        /// <param name="NumberOfValues">
        ///     An integer that specifies the number of values stored in the
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object. The
        ///     <see cref="P:Common.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property is set to this value.
        /// </param>
        /// <param name="Type">
        ///     A member of the <see cref="T:Common.Drawing.Imaging.EncoderParameterValueType" /> enumeration that
        ///     specifies the data type of the values stored in the <see cref="T:Common.Drawing.Imaging.EncoderParameter" />
        ///     object. The <see cref="T:System.Type" /> and <see cref="P:Common.Drawing.Imaging.EncoderParameter.ValueType" />
        ///     properties are set to this value.
        /// </param>
        /// <param name="Value">A pointer to an array of values of the type specified by the <paramref name="Type" /> parameter.</param>
        [Obsolete(
            "This constructor has been deprecated. Use EncoderParameter(Encoder encoder, int numberValues, EncoderParameterValueType type, IntPtr value) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        public EncoderParameter(Encoder encoder, int NumberOfValues, int Type, int Value)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, NumberOfValues, Type, Value);
        }

        public EncoderParameter(Encoder encoder, int numberValues, EncoderParameterValueType type, IntPtr value)
        {
            WrappedEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, NumberOfValues,
                (System.Drawing.Imaging.EncoderParameterValueType) type, value);
        }

        private EncoderParameter(System.Drawing.Imaging.EncoderParameter encoderParameter)
        {
            WrappedEncoderParameter = encoderParameter;
        }

        /// <summary>
        ///     Gets or sets the <see cref="T:Common.Drawing.Imaging.Encoder" /> object associated with this
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object. The
        ///     <see cref="T:Common.Drawing.Imaging.Encoder" /> object encapsulates the globally unique identifier (GUID) that
        ///     specifies the category (for example <see cref="F:Common.Drawing.Imaging.Encoder.Quality" />,
        ///     <see cref="F:Common.Drawing.Imaging.Encoder.ColorDepth" />, or
        ///     <see cref="F:Common.Drawing.Imaging.Encoder.Compression" />) of the parameter stored in this
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that encapsulates the GUID that specifies the
        ///     category of the parameter stored in this <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </returns>
        public Encoder Encoder
        {
            get => WrappedEncoderParameter.Encoder;
            set => WrappedEncoderParameter.Encoder = value;
        }

        /// <summary>
        ///     Gets the number of elements in the array of values stored in this
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </summary>
        /// <returns>
        ///     An integer that indicates the number of elements in the array of values stored in this
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </returns>
        public int NumberOfValues => WrappedEncoderParameter.NumberOfValues;

        /// <summary>
        ///     Gets the data type of the values stored in this <see cref="T:Common.Drawing.Imaging.EncoderParameter" />
        ///     object.
        /// </summary>
        /// <returns>
        ///     A member of the <see cref="T:Common.Drawing.Imaging.EncoderParameterValueType" /> enumeration that indicates
        ///     the data type of the values stored in this <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </returns>
        public EncoderParameterValueType Type => (EncoderParameterValueType) WrappedEncoderParameter.Type;

        /// <summary>
        ///     Gets the data type of the values stored in this <see cref="T:Common.Drawing.Imaging.EncoderParameter" />
        ///     object.
        /// </summary>
        /// <returns>
        ///     A member of the <see cref="T:Common.Drawing.Imaging.EncoderParameterValueType" /> enumeration that indicates
        ///     the data type of the values stored in this <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </returns>
        public EncoderParameterValueType ValueType => (EncoderParameterValueType) WrappedEncoderParameter.ValueType;

        private System.Drawing.Imaging.EncoderParameter WrappedEncoderParameter { get; }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.KeepAlive(this);
            GC.SuppressFinalize(this);
        }

        ~EncoderParameter()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                WrappedEncoderParameter.Dispose();
            }
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.EncoderParameter" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameter" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> that results from the conversion.</returns>
        /// <param name="encoderParameter">The <see cref="T:System.Drawing.Imaging.EncoderParameter" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator EncoderParameter(System.Drawing.Imaging.EncoderParameter encoderParameter)
        {
            return encoderParameter == null ? null : new EncoderParameter(encoderParameter);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> to a
        ///     <see cref="T:System.Drawing.Imaging.EncoderParameter" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.EncoderParameter" /> that results from the conversion.</returns>
        /// <param name="encoderParameter">The <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.EncoderParameter(EncoderParameter encoderParameter)
        {
            return encoderParameter?.WrappedEncoderParameter;
        }
    }
}