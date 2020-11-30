using System;
using System.Text;
using System.IO;
namespace F4Utils.Campaign
{
    public struct EmbeddedFileInfo
    {
        public string FileName;
        public uint FileOffset;
        public uint FileSizeBytes;
    }

    public class F4CampaignFileBundleReader
    {
        protected EmbeddedFileInfo[] _embeddedFileDirectory;
        protected byte[] _rawBytes;

        public F4CampaignFileBundleReader()
            : base()
        {
        }
        public F4CampaignFileBundleReader(string campaignFileBundleFileName)
            : this()
        {
            Load(campaignFileBundleFileName);
        }
        public void Load(string campaignFileBundleFileName)
        {
            var fi = new FileInfo(campaignFileBundleFileName);
            if (!fi.Exists) throw new FileNotFoundException(campaignFileBundleFileName);
            _rawBytes = new byte[fi.Length];
            using (var fs = new FileStream(campaignFileBundleFileName, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(_rawBytes, 0, (int)fi.Length);
            }
            var directoryStartOffset = BitConverter.ToUInt32(_rawBytes, 0);
            var numEmbeddedFiles = BitConverter.ToUInt32(_rawBytes, (int)directoryStartOffset);
            _embeddedFileDirectory = new EmbeddedFileInfo[numEmbeddedFiles];
            var curLoc = (int)directoryStartOffset + 4;
            for (var i = 0; i < numEmbeddedFiles; i++)
            {
                var thisFileResourceInfo = new EmbeddedFileInfo();
                var thisFileNameLength = (byte)(_rawBytes[curLoc] & 0xFF);
                curLoc++;
                var thisFileName = Encoding.ASCII.GetString(_rawBytes, curLoc, thisFileNameLength);
                thisFileResourceInfo.FileName = thisFileName;
                curLoc += thisFileNameLength;
                thisFileResourceInfo.FileOffset = BitConverter.ToUInt32(_rawBytes, curLoc);
                curLoc += 4;
                thisFileResourceInfo.FileSizeBytes = BitConverter.ToUInt32(_rawBytes, curLoc);
                curLoc += 4;
                _embeddedFileDirectory[i] = thisFileResourceInfo;
            }
        }
        public EmbeddedFileInfo[] GetEmbeddedFileDirectory()
        {
            if (_embeddedFileDirectory == null || _rawBytes == null || _rawBytes.Length == 0) throw new InvalidOperationException("Campaign bundle file not loaded yet.");
            return _embeddedFileDirectory;
        }
        public byte[] GetEmbeddedFileContents(string embeddedFileName)
        {
            if (_embeddedFileDirectory == null || _rawBytes == null || _rawBytes.Length == 0) throw new InvalidOperationException("Campaign bundle file not loaded yet.");
            for (int i = 0; i < _embeddedFileDirectory.Length; i++)
            {
                var thisFile = _embeddedFileDirectory[i];
                if (thisFile.FileName.ToLowerInvariant() == embeddedFileName.ToLowerInvariant())
                {
                    var toReturn = new byte[thisFile.FileSizeBytes];
                    Array.Copy(_rawBytes, thisFile.FileOffset, toReturn, 0, thisFile.FileSizeBytes);
                    return toReturn;
                }
            }
            throw new FileNotFoundException(embeddedFileName);
        }

    }
}