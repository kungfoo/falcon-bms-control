using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class SsdFile
    {
        public SquadronStoresDataType[] SquadronStoresDataTable { get; private set; }
        public SsdFile(string fileName)
        {
            SquadronStoresDataTable = LoadSquadronStoresData(fileName);
        }

        private SquadronStoresDataType[] LoadSquadronStoresData(string fileName)
        {
            //reads SSD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var squadronStoresDataTable = new SquadronStoresDataType[entries];
                for (var i = 0; i < squadronStoresDataTable.Length; i++)
                {
                    var entry = new SquadronStoresDataType();
                    entry.Stores = reader.ReadBytes((int) CampLibConstants.MAXIMUM_WEAPTYPES);
                    entry.infiniteAG = reader.ReadByte();
                    entry.infiniteAA = reader.ReadByte();
                    entry.infiniteGun = reader.ReadByte();
                    squadronStoresDataTable[i] = entry;
                }
                return squadronStoresDataTable;
            }
        }
        public void Save(string fileName)
        {
            //writes SSD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)SquadronStoresDataTable.Length);
                for (var i = 0; i < SquadronStoresDataTable.Length; i++)
                {
                    var entry = SquadronStoresDataTable[i];
                    writer.Write(entry.Stores);
                    writer.Write(entry.infiniteAG);
                    writer.Write(entry.infiniteAA);
                    writer.Write(entry.infiniteGun);
                }
            }
        }
    }
}
