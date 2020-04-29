using System;
using System.IO;
using System.Runtime.InteropServices;

namespace F4Utils.Speech
{
    public class SpeexCodec : IAudioCodec
    {
        private const int MAX_FRAME_SIZE = 2000;
        private bool _disposed;

        public SpeexCodec()
        {
            Initialize();
        }

        #region IAudioCodec Members

        public int Decode(byte[] inputBuffer, int inputBufferOffset, int dataLength, ref byte[] outputBuffer,
                          int outputBufferOffset)
        {
            var speexBits = new Speex.SpeexBits();
            var decoderState = IntPtr.Zero;

            var bytesWritten = 0;

            var compressedBits = new byte[200];
            var decompressedBits = new short[MAX_FRAME_SIZE];

            var unmanagedStoragePointer = IntPtr.Zero;
            var compressedBitsGCHandle = new GCHandle();
            var decompressedBitsGCHandle = new GCHandle();
            try
            {
                unmanagedStoragePointer = Marshal.AllocHGlobal(4);
                compressedBitsGCHandle = GCHandle.Alloc(compressedBits, GCHandleType.Pinned);
                decompressedBitsGCHandle = GCHandle.Alloc(decompressedBits, GCHandleType.Pinned);

                try
                {
                    Speex.speex_bits_init(ref speexBits);
                    var mode = Speex.speex_lib_get_mode(Speex.SPEEX_MODEID_NB);
                    decoderState = Speex.speex_decoder_init(mode);
                    Marshal.WriteInt32(unmanagedStoragePointer, 0);
                    Speex.speex_decoder_ctl(decoderState, Speex.SPEEX_SET_ENH, unmanagedStoragePointer);

                    Speex.speex_decoder_ctl(decoderState, Speex.SPEEX_GET_FRAME_SIZE, unmanagedStoragePointer);
                    var frameSize = Marshal.ReadInt32(unmanagedStoragePointer);

                    const int rate = 8000;
                    Marshal.WriteInt32(unmanagedStoragePointer, rate);
                    Speex.speex_decoder_ctl(decoderState, Speex.SPEEX_SET_SAMPLING_RATE, unmanagedStoragePointer);

                    Speex.speex_decoder_ctl(decoderState, Speex.SPEEX_GET_LOOKAHEAD, unmanagedStoragePointer);
                    var lookahead = Marshal.ReadInt32(unmanagedStoragePointer);

                    using (var inStream = new MemoryStream(inputBuffer, inputBufferOffset, dataLength))
                    using (
                        var outstream = new MemoryStream(outputBuffer, outputBufferOffset,
                                                         outputBuffer.Length - outputBufferOffset))
                    {
                        while (true)
                        {
                            var numBytesThisFrame = inStream.ReadByte();
                            if (numBytesThisFrame < 1)
                                break;
                            var bytesRead = inStream.Read(compressedBits, 0, numBytesThisFrame);
                            if (bytesRead < numBytesThisFrame)
                                throw new IOException("Unexpected end of stream encountered.");
                            Speex.speex_bits_read_from(ref speexBits, compressedBits, numBytesThisFrame);

                            var returnVal = Speex.speex_decode_int(decoderState, ref speexBits, decompressedBits);
                            var bitsRemaining = Speex.speex_bits_remaining(ref speexBits);
                            if (returnVal == -1)
                            {
                                break;
                            }
                            if (returnVal == -2)
                            {
                                throw new IOException("Decoding error: corrupted stream?\n");
                            }
                            else if (bitsRemaining < 0)
                            {
                                throw new IOException("Decoding overflow: corrupted stream?\n");
                            }

                            for (var i = 0; i < frameSize; i++)
                            {
                                if (outstream.Position > outstream.Length - 2) break;
                                outstream.Write(BitConverter.GetBytes(decompressedBits[i]), 0, 2);
                                bytesWritten += 2;
                            }
                        }
                        outstream.Flush();
                        outstream.Close();
                        inStream.Close();
                    }
                }
                finally
                {
                    Speex.speex_decoder_destroy(decoderState);
                    Speex.speex_bits_destroy(ref speexBits);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(unmanagedStoragePointer);
                compressedBitsGCHandle.Free();
                decompressedBitsGCHandle.Free();
                //modeGCHandle.Free();
            }
            return bytesWritten;
        }

        public int Encode(byte[] inputBuffer, int inputBufferOffset, int dataLength, byte[] outputBuffer,
                          int outputBufferOffset)
        {
            return Encode(inputBuffer, inputBufferOffset, dataLength, outputBuffer, outputBufferOffset, 8);
        }

        public int Encode(Stream inputStream, int dataLength, Stream outputStream)
        {
            return Encode(inputStream, dataLength, outputStream, 8);
        }

        public int Decode(Stream inputStream, int dataLength, int uncompressedLength, Stream outputStream)
        {
            var inputBytes = new byte[dataLength];
            var bytesRead = inputStream.Read(inputBytes, 0, dataLength);
            if (bytesRead < dataLength) throw new IOException("Unexpected end of stream encountered.");
            var outputBytes = new byte[uncompressedLength + 200];
            var decodedLength = Decode(inputBytes, 0, dataLength, ref outputBytes, 0);
            outputStream.Write(outputBytes, 0, decodedLength);
            if (decodedLength < uncompressedLength)
            {
                for (var i = 0; i < uncompressedLength - decodedLength; i++)
                {
                    outputStream.WriteByte(0); //fill rest of stream with NULLs
                }
            }
            outputStream.Flush();
            return decodedLength;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private static void Initialize()
        {
        }

        ~SpeexCodec()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //dispose of managed resources here
                }
                //dispose of unmanaged resources here
                _disposed = true;
            }
        }

        public int Encode(byte[] inputBuffer, int inputBufferOffset, int dataLength, byte[] outputBuffer,
                          int outputBufferOffset, int quality)
        {
            var compressedSize = 0;
            var speexBits = new Speex.SpeexBits();
            var compressedBits = new byte[MAX_FRAME_SIZE];
            var unmanagedStorage = IntPtr.Zero;
            try
            {
                var encoderState = IntPtr.Zero;
                unmanagedStorage = Marshal.AllocHGlobal(4);
                try
                {
                    var mode = Speex.speex_lib_get_mode(Speex.SPEEX_MODEID_NB);

                    var header = new Speex.SpeexHeader();
                    const int rate = 8000;
                    Speex.speex_init_header(ref header, rate, 1, mode);
                    header.frames_per_packet = 1;
                    header.vbr = 0;
                    header.nb_channels = 1;

                    encoderState = Speex.speex_encoder_init(mode);

                    Speex.speex_encoder_ctl(encoderState, Speex.SPEEX_GET_FRAME_SIZE, unmanagedStorage);
                    var frameSize = Marshal.ReadInt32(unmanagedStorage);

                    const int complexity = 10;
                    Marshal.WriteInt32(unmanagedStorage, complexity);
                    Speex.speex_encoder_ctl(encoderState, Speex.SPEEX_SET_COMPLEXITY, unmanagedStorage);

                    Marshal.WriteInt32(unmanagedStorage, rate);
                    Speex.speex_encoder_ctl(encoderState, Speex.SPEEX_SET_SAMPLING_RATE, unmanagedStorage);

                    Marshal.WriteInt32(unmanagedStorage, quality);
                    Speex.speex_encoder_ctl(encoderState, Speex.SPEEX_SET_QUALITY, unmanagedStorage);

                    const int lookahead = 0;
                    Marshal.WriteInt32(unmanagedStorage, lookahead);
                    Speex.speex_encoder_ctl(encoderState, Speex.SPEEX_GET_LOOKAHEAD, unmanagedStorage);

                    Speex.speex_bits_init(ref speexBits);

                    var input = new byte[MAX_FRAME_SIZE];
                    using (var inStream = new MemoryStream(inputBuffer, inputBufferOffset, dataLength))
                    using (
                        var outStream = new MemoryStream(outputBuffer, outputBufferOffset,
                                                         outputBuffer.Length - outputBufferOffset))
                    {
                        while (true)
                        {
                            var pcmBytesToRead = 16/8*header.nb_channels*frameSize;
                            var numBytesRead = inStream.Read(input, 0, pcmBytesToRead);
                            if (numBytesRead == 0) break;
                            var numSamplesToBlank = (pcmBytesToRead - numBytesRead)/(16/8*header.nb_channels);
                            var numSamplesRead = numBytesRead/(16/8*header.nb_channels);

                            for (var i = numSamplesRead; i < numSamplesRead + numSamplesToBlank; i++)
                            {
                                input[(i*2)] = 0;
                                input[(i*2) + 1] = 0;
                            }
                            var thisFrameBytesAsShort = new short[input.Length/2];
                            for (var i = 0; i < thisFrameBytesAsShort.Length; i++)
                            {
                                thisFrameBytesAsShort[i] = BitConverter.ToInt16(input, (i*2));
                            }
                            Speex.speex_encode_int(encoderState, thisFrameBytesAsShort, ref speexBits);
                            Speex.speex_bits_insert_terminator(ref speexBits);

                            var numBytesEncodedThisFrame =
                                (byte) Speex.speex_bits_write(ref speexBits, compressedBits, MAX_FRAME_SIZE);
                            Speex.speex_bits_reset(ref speexBits);

                            outStream.WriteByte(numBytesEncodedThisFrame);
                            compressedSize += 1;

                            outStream.Write(compressedBits, 0, numBytesEncodedThisFrame);
                            compressedSize += numBytesEncodedThisFrame;
                        }
                    }
                }
                finally
                {
                    Speex.speex_encoder_destroy(encoderState);
                    Speex.speex_bits_destroy(ref speexBits);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(unmanagedStorage);
            }
            return compressedSize;
        }

        public int Encode(Stream inputStream, int dataLength, Stream outputStream, int quality)
        {
            var inputBytes = new byte[dataLength];
            var bytesRead = inputStream.Read(inputBytes, 0, dataLength);
            if (bytesRead < dataLength)
            {
                throw new IOException("Unexpected end of stream encountered.");
            }

            var outputBytes = new byte[dataLength*2];
            var encodedLength = Encode(inputBytes, 0, dataLength, outputBytes, 0, quality);
            outputStream.Write(outputBytes, 0, encodedLength);
            outputStream.Flush();
            return encodedLength;
        }
    }
}