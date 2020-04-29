using System;
using System.IO;

namespace F4Utils.Speech
{
    public interface IAudioCodec : IDisposable
    {
        int Encode(byte[] inputBuffer, int inputBufferOffset, int dataLength, byte[] outputBuffer,
                   int outputBufferOffset);

        int Decode(byte[] inputBuffer, int inputBufferOffset, int dataLength, ref byte[] outputBuffer,
                   int outputBufferOffset);

        int Encode(Stream inputStream, int dataLength, Stream outputStream);
        int Decode(Stream inputStream, int dataLength, int uncompressedLength, Stream outputStream);
    }
}