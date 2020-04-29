using System.IO;
using F4Utils.Campaign.F4Structs;

namespace F4Utils.Campaign
{
    public class RcdFile
    {
        public RadarDataType[] RadarDataTable { get; private set; }
        public RcdFile(string fileName)
        {
            RadarDataTable = LoadRadarData(fileName);
        }

        private RadarDataType[] LoadRadarData(string fileName)
        {
            //reads RCD file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                var entries = reader.ReadInt16();
                var radarDataTable = new RadarDataType[entries];
                for (var i = 0; i < radarDataTable.Length; i++)
                {
                    var entry = new RadarDataType();
                    entry.RWRsound = reader.ReadInt32();
                    entry.RWRsymbol = reader.ReadInt16();
                    entry.RDRDataInd = reader.ReadInt16();
                    for (var j = 0; j < (int)AltLethality.NUM_ALT_LETHALITY; j++)
                    {
                        entry.Lethality[j] = reader.ReadSingle();
                    }
                    entry.NominalRange = reader.ReadSingle();
                    entry.BeamHalfAngle = reader.ReadSingle();
                    entry.ScanHalfAngle = reader.ReadSingle();
                    entry.SweepRate = reader.ReadSingle();
                    entry.CoastTime = reader.ReadUInt32();
                    entry.LookDownPenalty = reader.ReadSingle();
                    entry.JammingPenalty = reader.ReadSingle();
                    entry.NotchPenalty = reader.ReadSingle();
                    entry.NotchSpeed = reader.ReadSingle();
                    entry.ChaffChance = reader.ReadSingle();
                    entry.flag = reader.ReadInt16();
                    radarDataTable[i] = entry;
                }

                for (var k = 0; k < radarDataTable.Length; k++)
                {
                    if (radarDataTable[k].RDRDataInd == 0)
                    {
                        radarDataTable[k].RDRDataInd = (short)k;
                    }
                }
                return radarDataTable;
            }

        }
        public void Save(string fileName)
        {
            //writes RCD file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write((short)RadarDataTable.Length);
                for (var i = 0; i < RadarDataTable.Length; i++)
                {
                    var entry = RadarDataTable[i];
                    writer.Write(entry.RWRsound);
                    writer.Write(entry.RWRsymbol);
                    writer.Write(entry.RDRDataInd);
                    for (var j = 0; j < (int)AltLethality.NUM_ALT_LETHALITY; j++)
                    {
                        writer.Write(entry.Lethality[j]);
                    }
                    writer.Write(entry.NominalRange);
                    writer.Write(entry.BeamHalfAngle);
                    writer.Write(entry.ScanHalfAngle);
                    writer.Write(entry.SweepRate);
                    writer.Write(entry.CoastTime);
                    writer.Write(entry.LookDownPenalty);
                    writer.Write(entry.JammingPenalty);
                    writer.Write(entry.NotchPenalty);
                    writer.Write(entry.NotchSpeed);
                    writer.Write(entry.ChaffChance);
                    writer.Write(entry.flag);
                }
            }

        }
    }
}
