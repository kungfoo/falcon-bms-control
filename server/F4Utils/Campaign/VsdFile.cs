using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class VsdFile
    {
        public VisualDataType[] VisualDataTable { get; private set; }
        public VsdFile(string fileName)
        {
            VisualDataTable = LoadVisualData(fileName);
        }

        private VisualDataType[] LoadVisualData(string fileName)
        {
            //reads VSD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var visualDataTable = new VisualDataType[entries];
                for (var i = 0; i < visualDataTable.Length; i++)
                {
                    var entry = new VisualDataType();
                    entry.nominalRange = reader.ReadSingle();
                    entry.top = reader.ReadSingle();
                    entry.bottom = reader.ReadSingle();
                    entry.left = reader.ReadSingle();
                    entry.right = reader.ReadSingle();
                    visualDataTable[i] = entry;
                }
                return visualDataTable;
            }

        }
        public void Save(string fileName)
        {
            //writes VSD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)VisualDataTable.Length);
                for (var i = 0; i < VisualDataTable.Length; i++)
                {
                    var entry = VisualDataTable[i];
                    writer.Write(entry.nominalRange);
                    writer.Write(entry.top);
                    writer.Write(entry.bottom);
                    writer.Write(entry.left);
                    writer.Write(entry.right);
                }
            }

        }
    }
}
