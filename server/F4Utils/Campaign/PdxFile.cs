using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class PdxFile
    {
        public PtDataType[] PtDataTable { get; private set; }
        public PdxFile(string fileName)
        {
            PtDataTable = LoadPtData(fileName);
        }

        private PtDataType[] LoadPtData(string fileName)
        {
            //reads PDX file
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
                    entry.zOffset = reader.ReadSingle();
                    entry.height = reader.ReadSingle();
                    entry.width = reader.ReadSingle();
                    entry.length = reader.ReadSingle();
                    entry.type = reader.ReadByte();
                    entry.flags = reader.ReadByte();
                    entry.rootIdx = reader.ReadByte();
                    entry.branchIdx = reader.ReadByte();
                    ptDataTable[i] = entry;
                }
                return ptDataTable;
            }
        }
        public void Save(string fileName)
        {
            //writes PDX file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)PtDataTable.Length);
                for (var i = 0; i < PtDataTable.Length; i++)
                {
                    var entry = PtDataTable[i];
                    writer.Write(entry.xOffset);
                    writer.Write(entry.yOffset);
                    writer.Write(entry.zOffset);
                    writer.Write(entry.height);
                    writer.Write(entry.width);
                    writer.Write(entry.length);
                    writer.Write(entry.type);
                    writer.Write(entry.flags);
                    writer.Write(entry.rootIdx);
                    writer.Write(entry.branchIdx);
                }
            }
        }
    }
}
