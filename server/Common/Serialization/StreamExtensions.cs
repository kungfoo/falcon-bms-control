using System;
using System.IO;

namespace Common.Serialization
{
    public static class StreamExtensions
    {
        public static int ScanUntilFound(this Stream stream, byte[] searchBytes)
        {
            byte[] streamBuffer = new byte[searchBytes.Length];
            int nextRead = searchBytes.Length;
            int totalScannedBytes = 0;

            while (true)
            {
                FillBuffer(stream, streamBuffer, nextRead);
                totalScannedBytes += nextRead; 
                if (ArraysMatch(searchBytes, streamBuffer, 0))
                    return totalScannedBytes; 

                nextRead = FindPartialMatch(searchBytes, streamBuffer);
            }
        }

        static int FindPartialMatch(byte[] searchBytes, byte[] streamBuffer)
        {
            for (int i = 1; i < searchBytes.Length; i++)
            {
                if (ArraysMatch(searchBytes, streamBuffer, i))
                {
                    Array.Copy(streamBuffer, i, streamBuffer, 0, searchBytes.Length - i);
                    return i;
                }
            }

            return 4;
        }

        static void FillBuffer(Stream stream, byte[] streamBuffer, int bytesNeeded)
        {
            var bytesAlreadyRead = streamBuffer.Length - bytesNeeded; 
            while (bytesAlreadyRead < streamBuffer.Length)
            {
                bytesAlreadyRead += stream.Read(streamBuffer, bytesAlreadyRead, streamBuffer.Length - bytesAlreadyRead);
            }
        }

        static bool ArraysMatch(byte[] searchBytes, byte[] streamBuffer, int startAt)
        {
            for (int i = 0; i < searchBytes.Length - startAt; i++)
            {
                if (searchBytes[i] != streamBuffer[i + startAt])
                    return false;
            }
            return true;
        }
    }
}
