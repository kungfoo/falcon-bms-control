using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class RwdFile
    {
        public RwrDataType[] RWRDataTable { get; private set; }
        public RwdFile(string fileName)
        {
            RWRDataTable = LoadRWRData(fileName);
        }

        private RwrDataType[] LoadRWRData(string fileName)
        {
            //reads RWD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var rwrDataTable = new RwrDataType[entries];
                for (var i = 0; i < rwrDataTable.Length; i++)
                {
                    var entry = new RwrDataType();
                    entry.nominalRange = reader.ReadSingle();
                    entry.top = reader.ReadSingle();
                    entry.bottom = reader.ReadSingle();
                    entry.left = reader.ReadSingle();
                    entry.right = reader.ReadSingle();
                    entry.flag = reader.ReadInt16();
                    rwrDataTable[i] = entry;
                }
                return rwrDataTable;
            }
        }
        public void Save(string fileName)
        {
            //writes RWD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)RWRDataTable.Length);
                for (var i = 0; i < RWRDataTable.Length; i++)
                {
                    var entry = RWRDataTable[i];
                    writer.Write(entry.nominalRange);
                    writer.Write(entry.top);
                    writer.Write(entry.bottom);
                    writer.Write(entry.left);
                    writer.Write(entry.right);
                    writer.Write(entry.flag);
                }
            }
        }
    }
}
