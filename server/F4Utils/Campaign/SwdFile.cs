using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class SwdFile
    {
        public SimWeaponDataType[] SimWeaponDataTable { get; private set; }
        public SwdFile(string fileName)
        {
            SimWeaponDataTable = LoadSimWeaponData(fileName);
        }

        private SimWeaponDataType[] LoadSimWeaponData(string fileName)
        {
            //reads SWD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var simWeaponDataTable = new SimWeaponDataType[entries];
                for (var i = 0; i < simWeaponDataTable.Length; i++)
                {
                    var entry = new SimWeaponDataType();
                    entry.flags = reader.ReadInt32();
                    entry.cd = reader.ReadSingle();
                    entry.weight = reader.ReadSingle();
                    entry.area = reader.ReadSingle();
                    entry.xEjection = reader.ReadSingle();
                    entry.yEjection = reader.ReadSingle();
                    entry.zEjection = reader.ReadSingle();
                    for (var j = 0; j < entry.mnemonic.Length; j++)
                    {
                        entry.mnemonic[j] = reader.ReadSByte();
                    }
                    entry.weaponClass = reader.ReadInt32();
                    entry.domain = reader.ReadInt32();
                    entry.weaponType = reader.ReadInt32();
                    entry.dataIdx = reader.ReadInt32();
                    simWeaponDataTable[i] = entry;
                }
                return simWeaponDataTable;
            }

        }
        public void Save(string fileName)
        {
            //writes SWD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)SimWeaponDataTable.Length);
                for (var i = 0; i < SimWeaponDataTable.Length; i++)
                {
                    var entry = SimWeaponDataTable[i];
                    
                    writer.Write(entry.flags);
                    writer.Write(entry.cd);
                    writer.Write(entry.weight);
                    writer.Write(entry.area);
                    writer.Write(entry.xEjection);
                    writer.Write(entry.yEjection);
                    writer.Write(entry.zEjection);
                    for (var j = 0; j < entry.mnemonic.Length; j++)
                    {
                        writer.Write(entry.mnemonic[j]);
                    }
                    writer.Write(entry.weaponClass);
                    writer.Write(entry.domain);
                    writer.Write(entry.weaponType);
                    writer.Write(entry.dataIdx);
                }
            }

        }
    }
}
