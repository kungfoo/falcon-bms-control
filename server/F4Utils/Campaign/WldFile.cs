using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class WldFile
    {
        public WeaponListDataType[] WeaponListDataTable { get; private set; }
        public WldFile(string fileName)
        {
            WeaponListDataTable = LoadWeaponListData(fileName);
        }

        private WeaponListDataType[] LoadWeaponListData(string fileName)
        {
            //reads WLD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var weaponListDataTable = new WeaponListDataType[entries];
                for (var i = 0; i < weaponListDataTable.Length; i++)
                {
                    var entry = new WeaponListDataType();
                    for (var j = 0; j < 16; j++)
                    {
                        entry.Name[j] = reader.ReadSByte();
                    }
                    for (var j = 0; j < WeaponsConstants.MAX_WEAPONS_IN_LIST; j++)
                    {
                        entry.WeaponID[j] = reader.ReadInt16();
                    }
                    entry.Quantity = reader.ReadBytes(WeaponsConstants.MAX_WEAPONS_IN_LIST);

                    weaponListDataTable[i] = entry;
                }
                return weaponListDataTable;
            }

        }
        public void Save(string fileName)
        {
            //writes WLD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)WeaponListDataTable.Length);
                for (var i = 0; i < WeaponListDataTable.Length; i++)
                {
                    var entry = WeaponListDataTable[i];
                    for (var j = 0; j < 16; j++)
                    {
                        writer.Write(entry.Name[j]);
                    }
                    for (var j = 0; j < WeaponsConstants.MAX_WEAPONS_IN_LIST; j++)
                    {
                        writer.Write(entry.WeaponID[j]);
                    }
                    writer.Write(entry.Quantity);

                }
            }
        }
    }
}
