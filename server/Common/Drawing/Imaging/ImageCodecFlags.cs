using System;

namespace Common.Drawing.Imaging
{
    /// <summary>Provides attributes of an image encoder/decoder (codec).</summary>
    [Flags]
    public enum ImageCodecFlags
    {
        /// <summary>The codec supports encoding (saving).</summary>
        Encoder = 1,

        /// <summary>The codec supports decoding (reading).</summary>
        Decoder = 2,

        /// <summary>The codec supports raster images (bitmaps).</summary>
        SupportBitmap = 4,

        /// <summary>The codec supports vector images (metafiles).</summary>
        SupportVector = 8,

        /// <summary>The encoder requires a seekable output stream.</summary>
        SeekableEncode = 16,

        /// <summary>The decoder has blocking behavior during the decoding process.</summary>
        BlockingDecode = 32,

        /// <summary>The codec is built into GDI+.</summary>
        Builtin = 65536,

        /// <summary>Not used.</summary>
        System = 131072,

        /// <summary>Not used.</summary>
        User = 262144
    }
}