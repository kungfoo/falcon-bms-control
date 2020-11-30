using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class FcdFile
    {
        public FeatureClassDataType[] FeatureDataTable { get; private set; }
        public FcdFile(string fileName)
        {
            FeatureDataTable = LoadFeatureData(fileName);
        }

        private FeatureClassDataType[] LoadFeatureData(string fileName)
        {
            //reads FCD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var featureDataTable = new FeatureClassDataType[entries];
                for (var i = 0; i < featureDataTable.Length; i++)
                {
                    var entry = new FeatureClassDataType();
                    entry.Index = reader.ReadInt16();
                    entry.RepairTime = reader.ReadInt16();
                    entry.Priority = reader.ReadByte();
                    reader.ReadBytes(1);//padding
                    entry.Flags = reader.ReadUInt16();
                    entry.Name = reader.ReadBytes(20);
                    entry.HitPoints = reader.ReadInt16();
                    entry.Height = reader.ReadInt16();
                    entry.Angle = reader.ReadSingle();
                    entry.RadarType = reader.ReadInt16();
                    entry.Detection = reader.ReadBytes((int) MoveType.MOVEMENT_TYPES);
                    entry.DamageMod = reader.ReadBytes((int) DamageDataType.OtherDam + 1);
                    reader.ReadBytes(3);//padding
                    featureDataTable[i] = entry;
                }
                return featureDataTable;
            }

        }
        private void Save(string fileName)
        {
            //writes FCD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)FeatureDataTable.Length);
                for (var i = 0; i < FeatureDataTable.Length; i++)
                {
                    var entry = FeatureDataTable[i];
                    writer.Write(entry.Index);
                    writer.Write(entry.RepairTime);
                    writer.Write(entry.Priority);
                    writer.Write((byte)0x00);//padding
                    writer.Write(entry.Flags);
                    writer.Write(entry.Name);
                    writer.Write(entry.HitPoints);
                    writer.Write(entry.Height);
                    writer.Write(entry.Angle);
                    writer.Write(entry.RadarType);
                    writer.Write(entry.Detection);
                    writer.Write(entry.DamageMod);
                    writer.Write(new byte[3]);//padding
                }
            }

        }
    }
}
