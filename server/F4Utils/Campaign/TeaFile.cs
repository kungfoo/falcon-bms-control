using System;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class TeaFile
    {
        #region Public Fields
        public short numTeams;
        public Team[] teams;
        public AirTaskingManager[] airTaskingManagers;
        public GroundTaskingManager[] groundTaskingManagers;
        public NavalTaskingManager[] navalTaskingManagers;

        #endregion

        protected int _version = 0;
        protected TeaFile()
            : base()
        {
        }
        public TeaFile(Stream stream, int version)
            : this()
        {
            Read(stream, version);
        }

        protected void Read(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                _version = version;
                numTeams = reader.ReadInt16();

                if (numTeams > 8)
                    numTeams = 8;
                teams = new Team[numTeams];
                airTaskingManagers = new AirTaskingManager[numTeams];
                groundTaskingManagers = new GroundTaskingManager[numTeams];
                navalTaskingManagers = new NavalTaskingManager[numTeams];

                for (int i = 0; i < numTeams; i++)
                {
                    Team thisTeam = new Team(stream, version);
                    teams[i] = thisTeam;
                    airTaskingManagers[i] = new AirTaskingManager(stream, version);
                    groundTaskingManagers[i] = new GroundTaskingManager(stream, version);
                    navalTaskingManagers[i] = new NavalTaskingManager(stream, version);
                }
            }
        }
        public void Write(Stream stream, int version)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(numTeams);
                for (int i = 0; i < numTeams; i++)
                {
                    teams[i].Write(stream, version);
                    airTaskingManagers[i].WriteAirTaskingManager(stream, version);
                    groundTaskingManagers[i].WriteGroundTaskingManager(stream, version);
                    navalTaskingManagers[i].WriteNavalTaskingManager(stream, version);
                }
            }
        }
    }
}