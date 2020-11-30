using System;

namespace Common.Drawing.Imaging
{
    /// <summary>
    ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object encapsulates a globally unique identifier (GUID)
    ///     that identifies the category of an image encoder parameter.
    /// </summary>
    public sealed class Encoder
    {
        /// <summary>
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique
        ///     identifier for the compression parameter category.
        /// </summary>
        public static readonly Encoder Compression = System.Drawing.Imaging.Encoder.Compression;

        /// <summary>
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique
        ///     identifier for the color depth parameter category.
        /// </summary>
        public static readonly Encoder ColorDepth = System.Drawing.Imaging.Encoder.ColorDepth;

        /// <summary>
        ///     Represents an <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally
        ///     unique identifier for the scan method parameter category.
        /// </summary>
        public static readonly Encoder ScanMethod = System.Drawing.Imaging.Encoder.ScanMethod;

        /// <summary>
        ///     Represents an <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally
        ///     unique identifier for the version parameter category.
        /// </summary>
        public static readonly Encoder Version = System.Drawing.Imaging.Encoder.Version;

        /// <summary>
        ///     Represents an <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally
        ///     unique identifier for the render method parameter category.
        /// </summary>
        public static readonly Encoder RenderMethod = System.Drawing.Imaging.Encoder.RenderMethod;

        /// <summary>
        ///     Gets an <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique
        ///     identifier for the quality parameter category.
        /// </summary>
        public static readonly Encoder Quality = System.Drawing.Imaging.Encoder.Quality;

        /// <summary>
        ///     Represents an <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally
        ///     unique identifier for the transformation parameter category.
        /// </summary>
        public static readonly Encoder Transformation = System.Drawing.Imaging.Encoder.Transformation;

        /// <summary>
        ///     Represents an <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally
        ///     unique identifier for the luminance table parameter category.
        /// </summary>
        public static readonly Encoder LuminanceTable = System.Drawing.Imaging.Encoder.LuminanceTable;

        /// <summary>
        ///     An <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique
        ///     identifier for the chrominance table parameter category.
        /// </summary>
        public static readonly Encoder ChrominanceTable = System.Drawing.Imaging.Encoder.ChrominanceTable;

        /// <summary>
        ///     Represents an <see cref="T:Common.Drawing.Imaging.Encoder" /> object that is initialized with the globally
        ///     unique identifier for the save flag parameter category.
        /// </summary>
        public static readonly Encoder SaveFlag = System.Drawing.Imaging.Encoder.SaveFlag;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Common.Drawing.Imaging.Encoder" /> class from the specified
        ///     globally unique identifier (GUID). The GUID specifies an image encoder parameter category.
        /// </summary>
        /// <param name="guid">A globally unique identifier that identifies an image encoder parameter category. </param>
        public Encoder(Guid guid)
        {
            WrappedEncoder = new System.Drawing.Imaging.Encoder(guid);
        }

        private Encoder(System.Drawing.Imaging.Encoder encoder)
        {
            WrappedEncoder = encoder;
        }

        /// <summary>Gets a globally unique identifier (GUID) that identifies an image encoder parameter category.</summary>
        /// <returns>The GUID that identifies an image encoder parameter category.</returns>
        public Guid Guid => WrappedEncoder.Guid;

        private System.Drawing.Imaging.Encoder WrappedEncoder { get; }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Imaging.Encoder" /> to a
        ///     <see cref="T:Common.Drawing.Imaging.Encoder" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Imaging.Encoder" /> that results from the conversion.</returns>
        /// <param name="encoder">The <see cref="T:System.Drawing.Imaging.Encoder" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator Encoder(System.Drawing.Imaging.Encoder encoder)
        {
            return encoder == null ? null : new Encoder(encoder);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Imaging.Encoder" /> to a
        ///     <see cref="T:System.Drawing.Imaging.Encoder" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Imaging.Encoder" /> that results from the conversion.</returns>
        /// <param name="encoder">The <see cref="T:Common.Drawing.Imaging.Encoder" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Imaging.Encoder(Encoder encoder)
        {
            return encoder?.WrappedEncoder;
        }
    }
}