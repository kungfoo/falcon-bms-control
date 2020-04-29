using System;
using System.Runtime.InteropServices;

namespace F4Utils.Speech
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TlkFileDirectory
    {
        public uint field1;
        public uint field2;
        public uint field3;
        public TlkFileRecord[] records;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TlkFileRecord
    {
        public uint tlkId;
        public uint offset;
        public uint uncompressedDataLength;
        public uint compressedDataLength;
        public uint compressedDataOffset;
        public byte[] compressedData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FragFileDataRecord
    {
        public ushort speaker;
        public ushort fileNbr;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FragFileHeaderRecord
    {
        public ushort fragHdrNbr;
        public ushort totalSpeakers;
        public uint fragOffset;
        public FragFileDataRecord[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EvalFileHeaderRecord
    {
        public ushort evalHdrNbr;
        public ushort numEvals;
        public uint evalOffset;
        public EvalFileDataRecord[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EvalFileDataRecord
    {
        public short evalElem;
        public ushort fragNbr;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CommFileHeaderRecord
    {
        public ushort commHdrNbr;
        public ushort warp;
        public byte priority;
        public sbyte positionElement;
        public short bullseye;
        public byte totalElements;
        public byte totalEvals;
        public uint commOffset;
        public CommFileDataRecord[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CommFileDataRecord
    {
        public short fragIdOrEvalId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WAVEFORMATEX
    {
        public UInt16 wFormatTag;
        public UInt16 nChannels;
        public UInt32 nSamplesPerSec;
        public UInt32 nAvgBytesPerSec;
        public UInt16 nBlockAlign;
        public UInt16 wBitsPerSample;
        public UInt16 cbSize;
    }
}