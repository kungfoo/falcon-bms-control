using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class IcdFile
    {
        public IRSTDataType[] IRSTDataTable { get; private set; }
        public IcdFile(string fileName)
        {
            IRSTDataTable = LoadIRSTData(fileName);
        }

        private IRSTDataType[] LoadIRSTData(string fileName)
        {
            //reads ICD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var irstDataTable = new IRSTDataType[entries];
                for (var i = 0; i < irstDataTable.Length; i++)
                {
                    var entry = new IRSTDataType();
                    entry.NominalRange = reader.ReadSingle();
                    entry.FOVHalfAngle = reader.ReadSingle();
                    entry.GimbalLimitHalfAngle = reader.ReadSingle();
                    entry.GroundFactor = reader.ReadSingle();
                    entry.FlareChance = reader.ReadSingle();
                    irstDataTable[i] = entry;
                }
                return irstDataTable;
            }

        }
        public void Save(string fileName)
        {
            //writes ICD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(IRSTDataTable.Length);
                for (var i = 0; i < IRSTDataTable.Length; i++)
                {
                    var entry = IRSTDataTable[i];
                    writer.Write(entry.NominalRange);
                    writer.Write(entry.FOVHalfAngle);
                    writer.Write(entry.GimbalLimitHalfAngle);
                    writer.Write( entry.GroundFactor);
                    writer.Write(entry.FlareChance);
                }
            }

        }
    }
}
