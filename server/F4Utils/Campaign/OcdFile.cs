
using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class OcdFile
    {
        public ObjClassDataType[] ObjDataTable { get; private set; }
        public OcdFile(string fileName)
        {
            ObjDataTable = LoadObjectiveData(fileName);
        }

        private ObjClassDataType[] LoadObjectiveData(string fileName)
        {
            //reads OCD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var objDataTable = new ObjClassDataType[entries];
                for (var i = 0; i < objDataTable.Length; i++)
                {
                    var entry = new ObjClassDataType();
                    entry.Index = reader.ReadInt16();
                    entry.Name = reader.ReadBytes(20);
                    entry.DataRate = reader.ReadInt16();
                    entry.DeagDistance = reader.ReadInt16();
                    entry.PtDataIndex = reader.ReadInt16();
                    entry.Detection = reader.ReadBytes((int) MoveType.MOVEMENT_TYPES);
                    entry.DamageMod = reader.ReadBytes((int)DamageDataType.OtherDam + 1);
                    reader.ReadByte(); //padding
                    entry.IconIndex = reader.ReadInt16();
                    entry.Features = reader.ReadByte();
                    entry.RadarFeature = reader.ReadByte();
                    entry.FirstFeature = reader.ReadInt16();
                    objDataTable[i] = entry;
                }
                return objDataTable;
            }
        }
        private void Save(string fileName)
        {
            //writes OCD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)ObjDataTable.Length);
                for (var i = 0; i < ObjDataTable.Length; i++)
                {
                    var entry = ObjDataTable[i];
                    writer.Write(entry.Index);
                    writer.Write(entry.Name);
                    writer.Write(entry.DataRate);
                    writer.Write(entry.DeagDistance);
                    writer.Write(entry.PtDataIndex);
                    writer.Write(entry.Detection);
                    writer.Write(entry.DamageMod);
                    writer.Write((byte)0x00); //padding
                    writer.Write(entry.IconIndex);
                    writer.Write(entry.Features);
                    writer.Write(entry.RadarFeature);
                    writer.Write(entry.FirstFeature);
                }
            }
        }
    }
}
