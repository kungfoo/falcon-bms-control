using F4Utils.Campaign.F4Structs;
using System.IO;

namespace F4Utils.Campaign.Save
{
    public class TtFile
    {
        public short AirTactics { get; private set; }
        public short GroundTactics { get; private set; }
        public short NavalTactics { get; private set; }
        public short FirstAirTactic { get; private set; }
        public short FirstGroundTactic { get; private set; }
        public short FirstNavalTactic { get; private set; }
        public short TotalTactics { get; private set; }
        public TacticData[] TacticsTable { get; private set; }

        public TtFile(string fileName)
        {
            LoadTtFile(fileName);
        }

        private void LoadTtFile(string fileName)
        {
            //reads TT file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
                AirTactics = reader.ReadInt16();
                GroundTactics = reader.ReadInt16();
                NavalTactics = reader.ReadInt16();
	            FirstAirTactic = 1;
	            FirstGroundTactic = (short)(1 + AirTactics);
	            FirstNavalTactic = (short)(1 + AirTactics + GroundTactics);
	            TotalTactics =(short)(1 + AirTactics + GroundTactics + NavalTactics);
	            TacticsTable = new TacticData[TotalTactics];
                for (var i = 0; i < TacticsTable.Length; i++)
                {
                    TacticsTable[i] = new TacticData(stream);
                }
            }
        }
        public void Save(string fileName)
        {
            //writes TT file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(AirTactics);
                writer.Write(GroundTactics);
                writer.Write(NavalTactics);
                for (var i = 0; i < TacticsTable.Length; i++)
                {
                    TacticsTable[i].Write(stream);
                }
            }
        }
    }
}
