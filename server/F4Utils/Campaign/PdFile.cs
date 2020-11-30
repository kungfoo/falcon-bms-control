using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class PdFile
    {
        public PtDataType[] PtDataTable { get; private set; }
        public PdFile(string fileName)
        {
            PtDataTable = LoadPtData(fileName);
        }

        private PtDataType[] LoadPtData(string fileName)
        {
            //reads PD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var ptDataTable = new PtDataType[entries];
                for (var i = 0; i < ptDataTable.Length; i++)
                {
                    var entry = new PtDataType();
                    entry.xOffset = reader.ReadSingle();
                    entry.yOffset = reader.ReadSingle();
                    entry.zOffset = 0.0f;
                    entry.height = PtConstants.PT_DEFAULT_SIZE;
                    entry.width = PtConstants.PT_DEFAULT_SIZE;
                    entry.length = PtConstants.PT_DEFAULT_SIZE;
                    entry.type = reader.ReadByte();
                    entry.flags = reader.ReadByte();
                    reader.ReadBytes(2);//padding
                    entry.rootIdx = 0;
                    entry.branchIdx = 0;
                    ptDataTable[i] = entry;
                }
                return ptDataTable;
            }
        }
        public void Save(string fileName)
        {
            //writes PD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)PtDataTable.Length);
                for (var i = 0; i < PtDataTable.Length; i++)
                {
                    var entry = PtDataTable[i];
                    writer.Write(entry.xOffset);
                    writer.Write(entry.yOffset);
                    writer.Write(entry.type);
                    writer.Write(entry.flags);
                    writer.Write(new byte[2]);//padding
                }
            }
        }
    }
}
