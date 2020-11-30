using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class WcdFile
    {
        public WeaponClassDataType[] WeaponDataTable { get; private set; }
        public WcdFile(string fileName)
        {
            WeaponDataTable = LoadWeaponData(fileName);
        }

        private WeaponClassDataType[] LoadWeaponData(string fileName)
        {
            //reads WCD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var weaponDataTable = new WeaponClassDataType[entries];
                for (var i = 0; i < weaponDataTable.Length; i++)
                {
                    var entry = new WeaponClassDataType();
                    entry.Index = reader.ReadInt16();
                    entry.Union1 = reader.ReadUInt16();
                    entry.DamageType = (DamageDataType)reader.ReadInt32();
                    entry.Range = reader.ReadInt16();
                    entry.Flags = reader.ReadUInt16();
                    entry.Name = reader.ReadBytes(18);
                    entry.MinAlt = reader.ReadByte();
                    entry.BulletDispersion = reader.ReadByte();
                    entry.HitChance = reader.ReadBytes((int) MoveType.MOVEMENT_TYPES);
                    entry.FireRate = reader.ReadByte();
                    entry.Rariety = reader.ReadByte();
                    entry.GuidanceFlags = reader.ReadUInt16();
                    entry.Collective = reader.ReadByte();
                    entry.BulletInfo = reader.ReadByte();
                    entry.RackGroup = reader.ReadInt16();
                    entry.Weight = reader.ReadUInt16();
                    entry.DragIndex = reader.ReadInt16();
                    entry.Union2 = reader.ReadUInt16();
                    entry.RadarType = reader.ReadInt16();
                    entry.SimDataIdx = reader.ReadInt16();
                    entry.MaxAlt = reader.ReadSByte();
                    entry.BulletRoundsPerSec = reader.ReadByte();
                    weaponDataTable[i] = entry;
                }
                return weaponDataTable;
            }
    
        }
        private void Save(string fileName)
        {
            //writes WCD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)WeaponDataTable.Length);
                for (var i = 0; i < WeaponDataTable.Length; i++)
                {
                    var entry = WeaponDataTable[i];
                    writer.Write(entry.Index);
                    writer.Write(entry.Union1);
                    writer.Write((int)entry.DamageType);
                    writer.Write(entry.Range);
                    writer.Write(entry.Flags);
                    writer.Write(entry.Name);
                    writer.Write(entry.MinAlt);
                    writer.Write(entry.BulletDispersion);
                    writer.Write(entry.HitChance);
                    writer.Write(entry.FireRate);
                    writer.Write(entry.Rariety);
                    writer.Write(entry.GuidanceFlags);
                    writer.Write(entry.Collective);
                    writer.Write(entry.BulletInfo);
                    writer.Write(entry.RackGroup);
                    writer.Write(entry.Weight); 
                    writer.Write(entry.DragIndex);
                    writer.Write(entry.Union2);
                    writer.Write(entry.RadarType);
                    writer.Write(entry.SimDataIdx);
                    writer.Write(entry.MaxAlt);
                    writer.Write(entry.BulletRoundsPerSec);
                }
            }

        }
    }
}
