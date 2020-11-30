using Common.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace F4Utils.Speech
{
    public enum CodecType
    {
        Unknown = 0,
        LH,
        SPX
    }

    public class TlkFile
    {
        private const int TLK_HEADER_SIZE = 12;
        private static readonly byte[] TLK_HEADER_FIELD1 = new byte[] {0x68, 0x0a, 0x00, 0x00};
        private static readonly byte[] TLK_HEADER_FIELD2 = new byte[] {0xb4, 0x19, 0x00, 0x00};
        private static readonly byte[] TLK_HEADER_FIELD3 = new byte[] {0x00, 0x00, 0x00, 0x00};
        [ThreadStatic] private static readonly byte[] _compressedAudioBuffer = new byte[8*1024*1024];
        [ThreadStatic] private static readonly byte[] _uncompressedAudioBuffer = new byte[16*1024*1024];

        public TlkFileDirectory Directory;
        private string _tlkFilePath;

        private TlkFile()
        {
        }

        private TlkFile(string path)
        {
            var fi = new FileInfo(path);
            if (!fi.Exists) throw new FileNotFoundException(path);
            _tlkFilePath = path;
        }

        public TlkFileRecord[] Records
        {
            get { return Directory.records; }
            set { Directory.records = value; }
        }

        public static TlkFile Load(string tlkFilePath)
        {
            var tlkFile = new TlkFile(tlkFilePath);
            using (var fs = new FileStream(tlkFilePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                fs.Seek(0, SeekOrigin.Begin);
                tlkFile.Directory = new TlkFileDirectory
                                        {
                                            field1 = br.ReadUInt32(),
                                            field2 = br.ReadUInt32(),
                                            field3 = br.ReadUInt32()
                                        };

                var firstFileDescriptorOffset = br.ReadUInt32();
                var numRecords = (firstFileDescriptorOffset - TLK_HEADER_SIZE)/4;
                fs.Seek(-4, SeekOrigin.Current);
                tlkFile.Directory.records = new TlkFileRecord[numRecords];
                for (var i = 0; i < numRecords; i++)
                {
                    var thisRecord = new TlkFileRecord {tlkId = (uint) i, offset = br.ReadUInt32()};
                    var curPos = fs.Position;
                    fs.Seek(thisRecord.offset, SeekOrigin.Begin);
                    thisRecord.uncompressedDataLength = br.ReadUInt32();
                    thisRecord.compressedDataLength = br.ReadUInt32();
                    thisRecord.compressedDataOffset = thisRecord.offset + 8;
                    fs.Seek(curPos, SeekOrigin.Begin);
                    tlkFile.Directory.records[i] = thisRecord;
                }
            }
            return tlkFile;
        }

        private void GetCompressedAudioDataFromRecord(TlkFileRecord record, byte[] outputBuffer, int outputBufferOffset)
        {
            using (var fs = new FileStream(_tlkFilePath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    fs.Seek(record.compressedDataOffset, SeekOrigin.Begin);
                    fs.Read(outputBuffer, outputBufferOffset, (int) record.compressedDataLength);
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        private static int DecompressAudioData(CodecType codecType, byte[] compressedDataBuffer,
                                               int compressedBufferOffset, int compressedDataLength, byte[] outputBuffer,
                                               int outputBufferOffset)
        {
            var codec = GetCodec(codecType);

            using (codec)
            {
                return codec.Decode(compressedDataBuffer, compressedBufferOffset, compressedDataLength, ref outputBuffer,
                                    outputBufferOffset);
            }
        }

        public static int DecompressAudioDataFromStream(CodecType codecType, Stream compressedDataStream,
                                                        int compressedDataLength, int uncompressedLength,
                                                        Stream uncompressedDataStream)
        {
            var codec = GetCodec(codecType);

            using (codec)
            {
                return codec.Decode(compressedDataStream, compressedDataLength, uncompressedLength,
                                    uncompressedDataStream);
            }
        }

        public static int CompressAudioToStream(CodecType codecType, Stream uncompressedDataStream,
                                                int uncompressedDataLength, Stream compressedDataStream)
        {
            var codec = GetCodec(codecType);

            using (codec)
            {
                return codec.Encode(uncompressedDataStream, uncompressedDataLength, compressedDataStream);
            }
        }

        public CodecType DetectTlkFileCodecType()
        {
            if (Records != null && Records.Length > 0)
            {
                var firstRecord = Records[0];
                var isLh = false;
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        DecompressRecordAndWriteToStream(CodecType.LH, firstRecord, ms);
                    }
                    isLh = true;
                }
                catch (Exception)
                {
                }
                if (isLh) return CodecType.LH;

                var isSpx = false;
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        DecompressRecordAndWriteToStream(CodecType.SPX, firstRecord, ms);
                    }
                    isSpx = true;
                }
                catch (Exception)
                {
                }
                if (isSpx) return CodecType.SPX;
            }
            return CodecType.Unknown;
        }

        private static IAudioCodec GetCodec(CodecType codecType)
        {
            switch (codecType)
            {
                case CodecType.LH:
                    return new LHCodec();
                case CodecType.SPX:
                    return new SpeexCodec();
                default:
                    return null;
            }
        }

        public void WriteRecordToFile(TlkFileRecord record, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                try
                {
                    WriteRecordToStream(record, stream);
                }
                finally
                {
                    stream.Close();
                }
            }
        }

        public void WriteRecordToStream(TlkFileRecord record, Stream stream)
        {
            Array.Clear(_compressedAudioBuffer, 0, _compressedAudioBuffer.Length);
            GetCompressedAudioDataFromRecord(record, _compressedAudioBuffer, 0);
            stream.Write(BitConverter.GetBytes(record.uncompressedDataLength), 0, 4);
            stream.Write(BitConverter.GetBytes(record.compressedDataLength), 0, 4);
            stream.Write(_compressedAudioBuffer, 0, (int) record.compressedDataLength);
            stream.Flush();
        }

        public void DecompressRecordAndWriteToFile(CodecType codecType, TlkFileRecord record, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                try
                {
                    DecompressRecordAndWriteToStream(codecType, record, stream);
                }
                finally
                {
                    stream.Close();
                }
            }
        }

        public void DecompressRecordAndWriteToStream(CodecType codecType, TlkFileRecord record, Stream stream)
        {
            Array.Clear(_compressedAudioBuffer, 0, _compressedAudioBuffer.Length);
            Array.Clear(_uncompressedAudioBuffer, 0, _uncompressedAudioBuffer.Length);
            GetCompressedAudioDataFromRecord(record, _compressedAudioBuffer, 0);
            DecompressAudioDataAsWAVToStream(codecType, _compressedAudioBuffer, 0, (int) record.compressedDataLength,
                                             stream);
        }

        private static void DecompressAudioDataAsWAVToStream(CodecType codecType, byte[] compressedAudioBuffer,
                                                             int compressedAudioBufferOffset, int compressedDataLength,
                                                             Stream stream)
        {
            var uncompressedSize = DecompressAudioData(codecType, compressedAudioBuffer, compressedAudioBufferOffset,
                                                       compressedDataLength, _uncompressedAudioBuffer, 0);
            WritePCMBitsAsWAVToStream(_uncompressedAudioBuffer, 0, uncompressedSize, stream);
        }


        public static void WritePCMBitsAsWAVToStream(byte[] pcmBuffer, int pcmBufferOffset, int pcmLength, Stream stream)
        {
            stream.Write(Encoding.ASCII.GetBytes("RIFF"), 0, 4);
            stream.Write(BitConverter.GetBytes(pcmLength + 36), 0, 4);
            stream.Write(Encoding.ASCII.GetBytes("WAVE"), 0, 4);
            stream.Write(Encoding.ASCII.GetBytes("fmt "), 0, 4);
            stream.Write(BitConverter.GetBytes((UInt32) 16), 0, 4);
            var format = new WAVEFORMATEX();
            Util.InitWaveFormatEXData(ref format);
            stream.Write(BitConverter.GetBytes(format.wFormatTag), 0, 2);
            stream.Write(BitConverter.GetBytes(format.nChannels), 0, 2);
            stream.Write(BitConverter.GetBytes(format.nSamplesPerSec), 0, 4);
            stream.Write(BitConverter.GetBytes(format.nAvgBytesPerSec), 0, 4);
            stream.Write(BitConverter.GetBytes(format.nBlockAlign), 0, 2);
            stream.Write(BitConverter.GetBytes(format.wBitsPerSample), 0, 2);
            stream.Write(Encoding.ASCII.GetBytes("data"), 0, 4);
            stream.Write(BitConverter.GetBytes(pcmLength), 0, 4);
            stream.Write(pcmBuffer, pcmBufferOffset, pcmLength);
            stream.Flush();
        }

        private static CodecType GetCodecTypeFromFileExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileName.Trim())) return CodecType.Unknown;
            var extension = new FileInfo(fileName).Extension;
            if (string.IsNullOrEmpty(extension)) return CodecType.Unknown;
            switch (extension.ToUpper())
            {
                case ".SPX":
                    return CodecType.SPX;
                case ".LH":
                    return CodecType.LH;
            }
            return CodecType.Unknown;
        }

        public static void BuildFromCompressedAudioFiles(string[] compressedAudioFileNames, string fileNameToCreate,
                                                         TextWriter outputWriter)
        {
            if (compressedAudioFileNames == null) throw new ArgumentNullException("compressedAudioFileNames");
            if (compressedAudioFileNames.Length == 0) throw new ArgumentException("compressedAudioFileNames");
            using (var fs = new FileStream(fileNameToCreate, FileMode.Create, FileAccess.ReadWrite))
            {
                WriteDefaultTlkFileHeaderToStream(fs);

                //leave room for directory section
                foreach (var t in compressedAudioFileNames)
                {
                    fs.Write(BitConverter.GetBytes(0), 0, 4);
                }

                //write out compressed data to data section
                var index = 0;
                foreach (var file in compressedAudioFileNames)
                {
                    if (outputWriter != null)
                    {
                        outputWriter.Write("Adding " + file + "...");
                    }
                    //and write this file to the data section
                    var recordStartPosition = fs.Position;
                    uint uncompressedSize;
                    uint compresssedSize;
                    LoadCompressedFileAndWriteToStream(fs, file, out uncompressedSize, out compresssedSize);
                    var recordEndPosition = fs.Position;

                    //now write the directory entry for this file
                    fs.Seek(((index*4) + TLK_HEADER_SIZE), SeekOrigin.Begin);
                    fs.Write(BitConverter.GetBytes(recordStartPosition), 0, 4);

                    //and reposition the file stream pointer to the data section location where the next data entry should begin
                    fs.Seek(recordEndPosition, SeekOrigin.Begin);
                    index++;
                    if (outputWriter != null)
                    {
                        outputWriter.WriteLine("Completed.");
                    }
                }
            }
        }

        private static void LoadCompressedFileAndWriteToStream(Stream stream, string file, out uint uncompressedSize,
                                                               out uint compressedSize)
        {
            var rawBuffer = File.ReadAllBytes(file); //read in compressed audio file
            if (rawBuffer == null || rawBuffer.Length < 8)
            {
                throw new IOException(string.Format("Corrupt input file:{0}", file));
            }
            uncompressedSize = BitConverter.ToUInt32(rawBuffer, 0);
            compressedSize = BitConverter.ToUInt32(rawBuffer, 4);
            stream.Write(BitConverter.GetBytes(uncompressedSize), 0, 4);
            stream.Write(BitConverter.GetBytes(compressedSize), 0, 4);
            if (rawBuffer.Length < compressedSize + 8)
            {
                throw new IOException(string.Format("Corrupt input file:{0}", file));
            }
            stream.Write(rawBuffer, 8, (int) compressedSize);
        }

        private static void WriteDefaultTlkFileHeaderToStream(Stream stream)
        {
            //write file header magic
            stream.Write(TLK_HEADER_FIELD1, 0, TLK_HEADER_FIELD1.Length);
            stream.Write(TLK_HEADER_FIELD2, 0, TLK_HEADER_FIELD2.Length);
            stream.Write(TLK_HEADER_FIELD3, 0, TLK_HEADER_FIELD3.Length);
        }

        private void UpdateRecords(IEnumerable<TlkFileRecord> updatedRecords)
        {
            PopulateCompressedDataFieldInAllRecords();
            var recordList = new List<TlkFileRecord>(Records);
            foreach (var updatedRecord in updatedRecords)
            {
                var recordIndex = GetRecordIndex(recordList, updatedRecord.tlkId);
                if (recordIndex >= 0)
                {
                    recordList[recordIndex] = updatedRecord;
                }
                else
                {
                    recordList.Add(updatedRecord);
                }
            }
            Records = recordList.ToArray();
            FixupOffsets();
        }

        private static int GetRecordIndex(List<TlkFileRecord> records, uint tlkId)
        {
            for (var i = 0; i < records.Count; i++)
            {
                var thisRecord = records[i];
                if (thisRecord.tlkId == tlkId) return i;
            }
            return -1;
        }

        private void FixupOffsets()
        {
            var curOffset = (uint) (TLK_HEADER_SIZE + (Directory.records.Length*4));
            for (var i = 0; i < Directory.records.Length; i++)
            {
                var thisRecord = Directory.records[i];
                thisRecord.offset = curOffset;
                thisRecord.compressedDataOffset = thisRecord.offset + 8;
                curOffset += thisRecord.compressedDataLength;
            }
        }

        private void PopulateCompressedDataFieldInAllRecords()
        {
            for (var i = 0; i < Directory.records.Length; i++)
            {
                var record = Directory.records[i];
                if (record.compressedData == null || record.compressedData.Length == 0)
                {
                    var outputBuffer = new byte[record.compressedDataLength];
                    GetCompressedAudioDataFromRecord(record, outputBuffer, 0);
                    record.compressedData = outputBuffer;
                    Directory.records[i] = record;
                }
            }
        }

        public static void ImportWAVFiles(CodecType codecType, Dictionary<uint, string> files, string tlkFileName,
                                          TextWriter outputWriter)
        {
            var tlkFile = Load(tlkFileName);
            using (var codec = GetCodec(codecType))
            {
                var recordsToInsert = new List<TlkFileRecord>();
                foreach (var dictionaryEntry in files)
                {
                    var wavFileName = dictionaryEntry.Value;
                    var tlkFileId = dictionaryEntry.Key;
                    if (outputWriter != null)
                    {
                        outputWriter.Write("Importing " + wavFileName + "...");
                    }
                    using (var ms = new MemoryStream())
                    {
                        CompressWAVFileAndWriteToStream(wavFileName, ms, codec);
                        ms.Flush();
                        ms.Seek(0, SeekOrigin.Begin);
                        var thisRecordToInsert = new TlkFileRecord
                                                     {
                                                         tlkId = tlkFileId,
                                                         uncompressedDataLength =
                                                             (uint) new FileInfo(wavFileName).Length,
                                                         compressedData = ms.ToArray()
                                                     };
                        thisRecordToInsert.compressedDataLength = (uint) thisRecordToInsert.compressedData.Length;
                        recordsToInsert.Add(thisRecordToInsert);
                    }
                    if (outputWriter != null)
                    {
                        outputWriter.WriteLine("Completed.");
                    }
                }
                if (outputWriter != null)
                {
                    outputWriter.Write("Reindexing TLK file...");
                }
                tlkFile.UpdateRecords(recordsToInsert);
                if (outputWriter != null)
                {
                    outputWriter.WriteLine("Completed.");
                }
            }
            tlkFile.Save();
        }

        public static void ImportCompressedFiles(Dictionary<uint, string> files, string tlkFileName, CodecType codecType,
                                                 TextWriter outputWriter)
        {
            var tlkFile = Load(tlkFileName);
            var tlkFileCodec = tlkFile.DetectTlkFileCodecType();
            if (tlkFileCodec != CodecType.Unknown && tlkFileCodec != codecType)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Invalid <codec> argument: {0} was built using files that were encoded using the {1} codec, not the {2} codec.",
                        tlkFileName, tlkFileCodec, codecType));
            }
            var recordsToInsert = new List<TlkFileRecord>();
            foreach (var dictionaryEntry in files)
            {
                var compressedFileName = dictionaryEntry.Value;
                var tlkFileId = dictionaryEntry.Key;
                if (outputWriter != null)
                {
                    outputWriter.Write("Importing " + compressedFileName + "...");
                }
                using (var ms = new MemoryStream())
                {
                    uint compressedSize;
                    uint uncompressedSize;
                    LoadCompressedFileAndWriteToStream(ms, compressedFileName, out uncompressedSize, out compressedSize);
                    ms.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    var thisRecordToInsert = new TlkFileRecord
                                                 {
                                                     tlkId = tlkFileId,
                                                     uncompressedDataLength = uncompressedSize
                                                 };
                    var compressedFileBytes = ms.ToArray();
                    thisRecordToInsert.compressedData = new byte[compressedFileBytes.Length - 8];
                    Array.Copy(compressedFileBytes, 8, thisRecordToInsert.compressedData, 0,
                               compressedFileBytes.Length - 8);
                    thisRecordToInsert.compressedDataLength = compressedSize;
                    recordsToInsert.Add(thisRecordToInsert);
                }
                if (outputWriter != null)
                {
                    outputWriter.WriteLine("Completed.");
                }
            }
            if (outputWriter != null)
            {
                outputWriter.Write("Reindexing TLK file...");
            }
            tlkFile.UpdateRecords(recordsToInsert);
            if (outputWriter != null)
            {
                outputWriter.WriteLine("Completed.");
            }

            tlkFile.Save();
        }

        public void Save()
        {
            Save(_tlkFilePath);
        }

        public void Save(string fileName)
        {
            if (_tlkFilePath != null) if (_tlkFilePath != fileName) _tlkFilePath = fileName;
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                WriteDefaultTlkFileHeaderToStream(fs);

                //leave room for directory section
                foreach (var t in Directory.records)
                {
                    fs.Write(BitConverter.GetBytes(0), 0, 4);
                }
                fs.Flush();
                //write out compressed data to data section
                var index = 0;
                foreach (var record in Directory.records)
                {
                    //compress and write this file to the data section
                    var recordStartPosition = fs.Position;
                    fs.Write(BitConverter.GetBytes(record.uncompressedDataLength), 0, 4);
                    fs.Write(BitConverter.GetBytes(record.compressedDataLength), 0, 4);
                    fs.Write(record.compressedData, 0, (int) record.compressedDataLength);
                    fs.Flush();
                    var recordEndPosition = fs.Position;
                    //now write the directory entry for this file
                    fs.Seek(((index*4) + TLK_HEADER_SIZE), SeekOrigin.Begin);
                    fs.Write(BitConverter.GetBytes(recordStartPosition), 0, 4);
                    fs.Flush();
                    //and reposition the file stream pointer to the data section location where the next data entry should begin
                    fs.Seek(recordEndPosition, SeekOrigin.Begin);
                    index++;
                }
                fs.Flush();
                fs.Close();
            }
        }

        public static void BuildFromWAVFiles(CodecType codecType, string[] wavFileNames, string fileNameToCreate,
                                             TextWriter outputWriter)
        {
            using (var fs = new FileStream(fileNameToCreate, FileMode.Create, FileAccess.ReadWrite))
            {
                WriteDefaultTlkFileHeaderToStream(fs);
                fs.Flush();
                //leave room for directory section
                foreach (var t in wavFileNames)
                {
                    fs.Write(BitConverter.GetBytes(0), 0, 4);
                }
                fs.Flush();
                //compress each file and write out compressed data to data section
                Array.Clear(_compressedAudioBuffer, 0, _compressedAudioBuffer.Length);
                using (var codec = GetCodec(codecType))
                {
                    var index = 0;
                    foreach (var file in wavFileNames)
                    {
                        if (outputWriter != null)
                        {
                            outputWriter.Write("Adding " + file + "...");
                        }
                        //compress and write this file to the data section
                        var recordStartPosition = fs.Position;
                        CompressWAVFileAndWriteToStream(file, fs, codec);
                        fs.Flush();
                        var recordEndPosition = fs.Position;

                        //now write the directory entry for this file
                        fs.Seek(((index*4) + TLK_HEADER_SIZE), SeekOrigin.Begin);
                        fs.Write(BitConverter.GetBytes(recordStartPosition), 0, 4);
                        fs.Flush();

                        //and reposition the file stream pointer to the data section location where the next data entry should begin
                        fs.Seek(recordEndPosition, SeekOrigin.Begin);
                        index++;
                        if (outputWriter != null)
                        {
                            outputWriter.WriteLine("Completed.");
                        }
                        fs.Flush();
                    }
                }
                fs.Flush();
                fs.Close();
            }
        }

        public static void CompressWAVFileAndWriteToStream(string wavFileName, Stream stream, IAudioCodec codec)
        {
            int compressedSize;
            int pcmSize;
            compressedSize = CompressWAVFile(wavFileName, codec, _compressedAudioBuffer, 0, out pcmSize);
            stream.Write(BitConverter.GetBytes(pcmSize), 0, 4);
            stream.Write(BitConverter.GetBytes(compressedSize), 0, 4);
            stream.Write(_compressedAudioBuffer, 0, compressedSize);
        }

        private static int CompressWAVFile(string wavFileName, IAudioCodec codec, byte[] compressedBuffer,
                                           int compressedBufferOffset, out int pcmSize)
        {
            var wavFileBuffer = File.ReadAllBytes(wavFileName); //read in WAV file
            return CompressWAVBits(wavFileBuffer, 0, wavFileBuffer.Length, compressedBuffer, 0, codec, out pcmSize);
        }

        public static int CompressWAVBits(byte[] wavFileBuffer, int wavFileBufferOffset, int wavFileBufferLength,
                                          byte[] compressedBuffer, int compressedBufferOffset, IAudioCodec codec,
                                          out int pcmSize)
        {
            if (compressedBuffer.Length - compressedBufferOffset < wavFileBuffer.Length)
                Array.Resize(ref compressedBuffer, wavFileBuffer.Length + compressedBufferOffset); //allocate buffers
            int compressedSize;
            int dataChunkSizeFieldPosition;
            using (var stream = new MemoryStream(wavFileBuffer, wavFileBufferOffset, wavFileBufferLength))
            using (var reader = new BinaryReader(stream))
            {
                dataChunkSizeFieldPosition = stream.ScanUntilFound(Encoding.ASCII.GetBytes("data"));
                pcmSize = reader.ReadInt32();
            }
            
            compressedSize = codec.Encode(wavFileBuffer, dataChunkSizeFieldPosition, pcmSize,
                                          compressedBuffer, compressedBufferOffset); //compress PCM data
            return compressedSize;
        }
    }
}