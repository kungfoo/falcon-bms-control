using F4Utils.Campaign.F4Structs;
using System.IO;

namespace F4Utils.Campaign.Save
{
    public class FrcFile
    {
        public uint Timestamp { get; private set; }
        public TeamStatus[] TeamStats { get; private set; }

        public FrcFile(string fileName)
        {
            LoadFrcFile(fileName);
        }

        private void LoadFrcFile(string fileName)
        {
            //reads FRC file
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream))
            {
               Timestamp = reader.ReadUInt32();
               var numTeams = reader.ReadInt16();
                TeamStats = new TeamStatus[numTeams];
                for (var i=0;i<numTeams;i++) 
                {
                    var thisTeamStats = new TeamStatus();
                    thisTeamStats.airDefenseVehs = reader.ReadUInt16();
                    thisTeamStats.aircraft = reader.ReadUInt16();
                    thisTeamStats.groundVehs = reader.ReadUInt16();
                    thisTeamStats.ships =reader.ReadUInt16();
                    thisTeamStats.supply = reader.ReadUInt16();
                    thisTeamStats.fuel =reader.ReadUInt16();
                    thisTeamStats.airbases = reader.ReadUInt16();
                    thisTeamStats.supplyLevel = reader.ReadByte();
                    thisTeamStats.fuelLevel = reader.ReadByte();
                    TeamStats[i] = thisTeamStats;
                }
            }
        }
        public void Save(string fileName)
        {
            //writes FRC file
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(Timestamp);
                writer.Write((short)TeamStats.Length);
                for (var i = 0; i < TeamStats.Length; i++)
                {
                    var thisTeamStats = TeamStats[i];
                    writer.Write(thisTeamStats.airDefenseVehs);
                    writer.Write(thisTeamStats.aircraft);
                    writer.Write(thisTeamStats.groundVehs);
                    writer.Write(thisTeamStats.ships);
                    writer.Write(thisTeamStats.supply);
                    writer.Write(thisTeamStats.fuel);
                    writer.Write(thisTeamStats.airbases);
                    writer.Write(thisTeamStats.supplyLevel);
                    writer.Write(thisTeamStats.fuelLevel);
                }
            }
        }
    }
}
