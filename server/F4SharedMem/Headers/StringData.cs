using System;
using System.Collections.Generic;
using System.Text;
namespace F4SharedMem.Headers
{
    [Serializable]
    public class StringData
    {
        // changelog:
        // 1: initial BMS 4.34 version
        // 2: added 3dbuttons.dat/3dckpit.dat file paths
        // 3: added NavPoints

        public const uint STRINGDATA_AREA_SIZE_MAX = 1024 * 1024;

        public uint VersionNum;  // Version of the StringData shared memory area - only indicates changes to the StringIdentifier enum
        public uint NoOfStrings;       // How many strings do we have in the area?
        public uint dataSize;          // the overall size of the StringData/FalconSharedMemoryAreaString shared memory area
        public IEnumerable<StringStruct> data = new List<StringStruct>();

        public static StringData GetStringData(byte[] data)
        {
            if (data == null) return null;
            int offset = 0;
            var toReturn = new StringData();
            toReturn.VersionNum = BitConverter.ToUInt32(data, offset);
            offset += sizeof(uint);
            toReturn.NoOfStrings = BitConverter.ToUInt32(data, offset);
            offset += sizeof(uint);
            toReturn.dataSize = BitConverter.ToUInt32(data, offset);
            offset += sizeof(uint);
            for (var i = 0; i < toReturn.NoOfStrings; i++)
            {
                if (offset >= data.Length - sizeof(uint)) break;
                var sStruct = new StringStruct();
                sStruct.strId = BitConverter.ToUInt32(data, offset);
                offset += sizeof(uint);
                if (offset >= data.Length - sizeof(uint)) break;
                sStruct.strLength = BitConverter.ToUInt32(data, offset);
                offset += sizeof(uint);
                sStruct.strData = new byte[sStruct.strLength];
                Array.Copy(data, offset, sStruct.strData, 0, Math.Min(sStruct.strLength, data.Length-offset));
                offset += (int)sStruct.strLength +1;
                (toReturn.data as IList<StringStruct>).Add(sStruct);
            }
            return toReturn;
        }
    }
    [Serializable]
    public struct StringStruct
    {
        public uint strId;
        public uint strLength;     // The length of the string in "strData", stored *without* termination!
        public byte[] strData;
        public string value { get { return strData !=null && strData.Length >0 ? Encoding.Default.GetString(strData, 0, (int)strLength) : string.Empty; } } 
        public override string ToString()
        {
            var identifier = Enum.GetName(typeof(StringIdentifier), strId);
            if (string.IsNullOrWhiteSpace(identifier))
            {
                identifier = strId.ToString();
            }
            return $"{identifier}={value}";
        }
    }
}

