using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class FedFile
    {
        public FeatureEntry[] FeatureEntryDataTable { get; set; }
        public FedFile(string fileName)
        {
            FeatureEntryDataTable = LoadFeatureEntryData(fileName);
        }

        private FeatureEntry[] LoadFeatureEntryData(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var featureEntryDataTable = new FeatureEntry[entries];
                for (var i = 0; i < featureEntryDataTable.Length; i++)
                {
                    var entry = new FeatureEntry();
                    entry.Index = reader.ReadInt16();
                    entry.Flags = reader.ReadUInt16();
                    entry.eClass = reader.ReadBytes(8);
                    entry.Value = reader.ReadByte();
                    reader.ReadBytes(3); //padding
                    entry.Offset = new vector
                    {
                        x = reader.ReadSingle(),
                        y = reader.ReadSingle(),
                        z = reader.ReadSingle()
                    };

                    entry.Facing = reader.ReadInt16();
                    reader.ReadBytes(2); //padding
                    featureEntryDataTable[i] = entry;
                }
                return featureEntryDataTable;
            }
        }
        public void Save(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)FeatureEntryDataTable.Length);
                for (var i = 0; i < FeatureEntryDataTable.Length; i++)
                {
                    var entry = FeatureEntryDataTable[i];
                    
                    writer.Write(entry.Index);
                    writer.Write(entry.Flags);
                    writer.Write(entry.eClass);
                    writer.Write(entry.Value);
                    writer.Write(new byte[3]); //padding
                    writer.Write(entry.Offset.x);
                    writer.Write(entry.Offset.y);
                    writer.Write(entry.Offset.z);
                    writer.Write(entry.Facing);
                    writer.Write(new byte[2]);//padding
                    
                }
            }
        }
    }
}
