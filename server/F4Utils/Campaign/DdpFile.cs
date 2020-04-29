using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class DdpFile
    {
        public DirtyDataClassType[] DirtyDataTable { get; private set; }
        public DdpFile(string fileName)
        {
            DirtyDataTable = LoadDirtyData(fileName);
        }

        private DirtyDataClassType[] LoadDirtyData(string fileName)
        {
            //reads DDP file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var dirtyDataTable = new DirtyDataClassType[entries];
                for (var i = 0; i < dirtyDataTable.Length; i++)
                {
                    var entry = new DirtyDataClassType();
                    entry.priority = (Dirtyness) reader.ReadInt32();
                    dirtyDataTable[i] = entry;
                }
                return dirtyDataTable;
            }
        }
        public void Save(string fileName)
        {
            //writes DDP file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)DirtyDataTable.Length);
                for (var i = 0; i < DirtyDataTable.Length; i++)
                {
                    var entry = DirtyDataTable[i];
                    writer.Write((int)entry.priority);
                }
            }
        }
    }
}
