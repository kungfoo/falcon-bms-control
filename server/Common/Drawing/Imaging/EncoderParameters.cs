using System;
using System.Linq;

namespace Common.Drawing.Imaging
{
    /// <summary>Encapsulates an array of <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> objects.</summary>
    public sealed class EncoderParameters : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> class that can
        ///     contain the specified number of <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> objects.
        /// </summary>
        /// <param name="count">
        ///     An integer that specifies the number of <see cref="T:Common.Drawing.Imaging.EncoderParameter" />
        ///     objects that the <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> object can contain.
        /// </param>
        public EncoderParameters(int count)
        {
            WrappedEncoderParameters = new System.Drawing.Imaging.EncoderParameters(count);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> class that can
        ///     contain one <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> object.
        /// </summary>
        public EncoderParameters()
        {
            WrappedEncoderParameters = new System.Drawing.Imaging.EncoderParameters();
        }

        private EncoderParameters(System.Drawing.Imaging.EncoderParameters encoderParameters)
        {
            WrappedEncoderParameters = encoderParameters;
        }

        /// <summary>Gets or sets an array of <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> objects.</summary>
        /// <returns>The array of <see cref="T:Common.Drawing.Imaging.EncoderParameter" /> objects.</returns>
        public EncoderParameter[] Param
        {
            get => WrappedEncoderParameters.Param.Convert<EncoderParameter>().ToArray();
            set => WrappedEncoderParameters.Param = value.Convert<System.Drawing.Imaging.EncoderParameter>().ToArray();
        }

        private System.Drawing.Imaging.EncoderParameters WrappedEncoderParameters { get; }

        /// <summary>Releases all resources used by this <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> object.</summary>
        public void Dispose()
        {
            WrappedEncoderParameters.Dispose();
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.EncoderParameters" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.EncoderParameters" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> that results from the conversion.</returns>
        /// <param name="encoderParameters">The <see cref="T:System.Drawing.Imaging.EncoderParameters" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator EncoderParameters(System.Drawing.Imaging.EncoderParameters encoderParameters)
        {
            return encoderParameters == null ? null : new EncoderParameters(encoderParameters);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> to a
        ///     <see cref="T:System.Drawing.Imaging.EncoderParameters" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that results from the conversion.</returns>
        /// <param name="encoderParameters">The <see cref="T:Common.Drawing.Imaging.EncoderParameters" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.EncoderParameters(EncoderParameters encoderParameters)
        {
            return encoderParameters?.WrappedEncoderParameters;
        }
    }
}