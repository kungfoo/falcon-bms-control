using System;
using System.Text;
using F4Utils.Campaign.F4Structs;
using System.IO;

namespace F4Utils.Campaign
{

    public class CmpFile
    {
        #region Public Fields

        public uint CurrentTime;
        public uint TE_StartTime;
        public uint TE_TimeLimit;
        public int TE_VictoryPoints;
        public int TE_Type;
        public int TE_number_teams;
        public int[] TE_number_aircraft = new int[8];
        public int[] TE_number_f16s = new int[8];
        public int TE_team;
        public int[] TE_team_pts = new int[8];
        public int TE_flags;
        public TeamBasicInfo[] TeamBasicInfo = new TeamBasicInfo[8];
        public uint lastMajorEvent;
        public uint lastResupply;
        public uint lastRepair;
        public uint lastReinforcement;
        public short TimeStamp;
        public short Group;
        public short GroundRatio;
        public short AirRatio;
        public short AirDefenseRatio;
        public short NavalRatio;
        public short Brief;
        public short TheaterSizeX;
        public short TheaterSizeY;
        public byte CurrentDay;
        public byte ActiveTeams;
        public byte DayZero;
        public byte EndgameResult;
        public byte Situation;
        public byte EnemyAirExp;
        public byte EnemyADExp;
        public byte BullseyeName;
        public short BullseyeX;
        public short BullseyeY;
        public string TheaterName;
        public string Scenario;
        public string SaveFile;
        public string UIName;
        public VU_ID PlayerSquadronID;
        public int NumRecentEventEntries;
        public EventNode[] RecentEventEntries;
        public int NumPriorityEventEntries;
        public EventNode[] PriorityEventEntries;
        public short CampMapSize;
        public byte[] CampMap;
        public short LastIndexNum;
        public short NumAvailableSquadrons;
        public SquadInfo[] SquadInfo;
        public byte Tempo;
        public int CreatorIP;
        public int CreationTime;
        public int CreationRand;

        #endregion

        protected int _version = 0;
        protected CmpFile()
            : base()
        {
        }
        public CmpFile(byte[] compressed, int version)
            : this()
        {
            _version = version;
            byte[] expanded = Expand(compressed);
            if (expanded != null) Decode(expanded);
        }
        protected void Decode(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen:true))
            {
                int nullLoc = 0;
                CurrentTime = reader.ReadUInt32();
                if (CurrentTime == 0) CurrentTime = 1;

                if (_version >= 48)
                {
                    TE_StartTime = reader.ReadUInt32();
                    TE_TimeLimit = reader.ReadUInt32();
                    if (_version >= 49)
                    {
                        TE_VictoryPoints = reader.ReadInt32();
                    }
                    else
                    {
                        TE_VictoryPoints = 0;
                    }
                }
                else
                {
                    TE_StartTime = CurrentTime;
                    TE_TimeLimit = CurrentTime + (60 * 60 * 5 * 1000);
                    TE_VictoryPoints = 0;
                }
                if (_version >= 52)
                {
                    TE_Type = reader.ReadInt32();
                    TE_number_teams = reader.ReadInt32();

                    for (int i = 0; i < 8; i++)
                    {
                        TE_number_aircraft[i] = reader.ReadInt32();
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        TE_number_f16s[i] = reader.ReadInt32();
                    }

                    TE_team = reader.ReadInt32();

                    for (int i = 0; i < 8; i++)
                    {
                        TE_team_pts[i] = reader.ReadInt32();
                    }

                    TE_flags = reader.ReadInt32();

                    nullLoc = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        TeamBasicInfo info = new TeamBasicInfo();
                        info.teamFlag = reader.ReadByte();
                        info.teamColor = reader.ReadByte();
                        var teamNameBytes = reader.ReadBytes(20);
                        info.teamName = Encoding.ASCII.GetString(teamNameBytes, 0, 20);
                        nullLoc = info.teamName.IndexOf('\0');
                        if (nullLoc > -1) info.teamName = info.teamName.Substring(0, nullLoc);
                        var teamMottoBytes = reader.ReadBytes(200);
                        info.teamMotto = Encoding.ASCII.GetString(teamMottoBytes, 0, 200);
                        nullLoc = info.teamMotto.IndexOf('\0');
                        if (nullLoc > -1) info.teamMotto = info.teamMotto.Substring(0, nullLoc);
                        this.TeamBasicInfo[i] = info;
                    }
                }
                else
                {
                    TE_Type = 0;
                    TE_number_teams = 0;
                    TE_number_aircraft = new int[8];
                    TE_number_f16s = new int[8];
                    TE_team = 0;
                    TE_team_pts = new int[8];
                    TE_flags = 0;
                }
                if (_version >= 19)
                {
                    lastMajorEvent = reader.ReadUInt32();
                }

                lastResupply = reader.ReadUInt32();
                lastRepair = reader.ReadUInt32();
                lastReinforcement = reader.ReadUInt32();
                this.TimeStamp = reader.ReadInt16();

                Group = reader.ReadInt16();
                GroundRatio = reader.ReadInt16();
                AirRatio = reader.ReadInt16();
                AirDefenseRatio = reader.ReadInt16();
                NavalRatio = reader.ReadInt16();
                Brief = reader.ReadInt16();
                TheaterSizeX = reader.ReadInt16();
                TheaterSizeY = reader.ReadInt16();
                CurrentDay = reader.ReadByte();
                ActiveTeams = reader.ReadByte();
                DayZero = reader.ReadByte();
                EndgameResult = reader.ReadByte();
                Situation = reader.ReadByte();
                EnemyAirExp = reader.ReadByte();
                EnemyADExp = reader.ReadByte();
                BullseyeName = reader.ReadByte();
                BullseyeX = reader.ReadInt16();
                BullseyeY = reader.ReadInt16();
                var theaterNameBytes = reader.ReadBytes(40);
                TheaterName = Encoding.ASCII.GetString(theaterNameBytes, 0, 40);
                nullLoc = TheaterName.IndexOf('\0');
                if (nullLoc > -1) TheaterName = TheaterName.Substring(0, nullLoc);

                var scenarioBytes = reader.ReadBytes(40);
                Scenario = Encoding.ASCII.GetString(scenarioBytes, 0, 40);
                nullLoc = Scenario.IndexOf('\0');
                if (nullLoc > -1) Scenario = Scenario.Substring(0, nullLoc);

                var saveFileBytes = reader.ReadBytes(40);
                SaveFile = Encoding.ASCII.GetString(saveFileBytes, 0, 40);
                nullLoc = SaveFile.IndexOf('\0');
                if (nullLoc > -1) SaveFile = SaveFile.Substring(0, nullLoc);

                var uiNameBytes = reader.ReadBytes(40);
                UIName = Encoding.ASCII.GetString(uiNameBytes, 0, 40);
                nullLoc = UIName.IndexOf('\0');
                if (nullLoc > -1) UIName = UIName.Substring(0, nullLoc);

                VU_ID squadronId = new VU_ID();
                squadronId.num_ = reader.ReadUInt32();
                squadronId.creator_ = reader.ReadUInt32();
                PlayerSquadronID = squadronId;

                NumRecentEventEntries = reader.ReadInt16();
                if (NumRecentEventEntries > 0)
                {
                    RecentEventEntries = new EventNode[NumRecentEventEntries];
                    for (int i = 0; i < NumRecentEventEntries; i++)
                    {
                        EventNode thisNode = new EventNode();
                        thisNode.x = reader.ReadInt16();
                        thisNode.y = reader.ReadInt16();
                        thisNode.time = reader.ReadUInt32();
                        thisNode.flags = reader.ReadByte();
                        thisNode.Team = reader.ReadByte();
                        reader.ReadBytes(2); //align on int32 boundary
                        //skip EventText pointer
                        reader.ReadBytes(4);
                        //skip UiEventNode pointer
                        reader.ReadBytes(4);
                        short eventTextSize = reader.ReadInt16();
                        var eventTextBytes = reader.ReadBytes(eventTextSize);
                        string eventText = Encoding.ASCII.GetString(eventTextBytes, 0, eventTextSize);
                        nullLoc = eventText.IndexOf('\0');
                        if (nullLoc > -1) eventText = eventText.Substring(0, nullLoc);
                        thisNode.eventText = eventText;
                        RecentEventEntries[i] = thisNode;
                    }
                }


                NumPriorityEventEntries = reader.ReadInt16();
                if (NumPriorityEventEntries > 0)
                {
                    PriorityEventEntries = new EventNode[NumPriorityEventEntries];
                    for (int i = 0; i < NumPriorityEventEntries; i++)
                    {
                        EventNode thisNode = new EventNode();
                        thisNode.x = reader.ReadInt16();
                        thisNode.y = reader.ReadInt16();
                        thisNode.time = reader.ReadUInt32();
                        thisNode.flags = reader.ReadByte();
                        thisNode.Team = reader.ReadByte();

                        reader.ReadBytes(2); //align on int32 boundary
                        //skip EventText pointer
                        reader.ReadBytes(4);
                        //skip UiEventNode pointer
                        reader.ReadBytes(4);

                        short eventTextSize = reader.ReadInt16();
                        var eventTextBytes = reader.ReadBytes(eventTextSize);
                        string eventText = Encoding.ASCII.GetString(eventTextBytes, 0, eventTextSize);
                        nullLoc = eventText.IndexOf('\0');
                        if (nullLoc > -1) eventText = eventText.Substring(0, nullLoc);
                        thisNode.eventText = eventText;
                        PriorityEventEntries[i] = thisNode;
                    }
                }
                CampMapSize = reader.ReadInt16();
                if (CampMapSize > 0)
                {
                    CampMap = reader.ReadBytes(CampMapSize);
                }

                LastIndexNum = reader.ReadInt16();
                NumAvailableSquadrons = reader.ReadInt16();
                if (NumAvailableSquadrons > 0)
                {
                    if (_version < 42)
                    {
                        SquadInfo = new SquadInfo[NumAvailableSquadrons];
                        for (int i = 0; i < NumAvailableSquadrons; i++)
                        {
                            SquadInfo thisSquadInfo = new SquadInfo();
                            thisSquadInfo.x = reader.ReadSingle();
                            thisSquadInfo.y = reader.ReadSingle();

                            VU_ID thisSquadId = new VU_ID();
                            thisSquadId.num_ = reader.ReadUInt32();
                            thisSquadId.creator_ = reader.ReadUInt32();
                            thisSquadInfo.id = thisSquadId;

                            thisSquadInfo.descriptionIndex = reader.ReadInt16();
                            thisSquadInfo.nameId = reader.ReadInt16();
                            thisSquadInfo.specialty = reader.ReadByte();
                            thisSquadInfo.currentStrength = reader.ReadByte();
                            thisSquadInfo.country = reader.ReadByte();

                            var airbaseNameBytes = reader.ReadBytes(80);
                            thisSquadInfo.airbaseName = Encoding.ASCII.GetString(airbaseNameBytes, 0, 80);
                            nullLoc = thisSquadInfo.airbaseName.IndexOf('\0');
                            if (nullLoc > -1) thisSquadInfo.airbaseName = thisSquadInfo.airbaseName.Substring(0, nullLoc);

                            reader.ReadByte(); //align on int32 boundary
                            SquadInfo[i] = thisSquadInfo;
                        }
                    }
                    else
                    {
                        SquadInfo = new SquadInfo[NumAvailableSquadrons];
                        for (int i = 0; i < NumAvailableSquadrons; i++)
                        {
                            SquadInfo thisSquadInfo = new SquadInfo();
                            thisSquadInfo.x = reader.ReadSingle();
                            thisSquadInfo.y = reader.ReadSingle();

                            VU_ID thisSquadId = new VU_ID();
                            thisSquadId.num_ = reader.ReadUInt32();
                            thisSquadId.creator_ = reader.ReadUInt32();
                            thisSquadInfo.id = thisSquadId;

                            thisSquadInfo.descriptionIndex = reader.ReadInt16();
                            thisSquadInfo.nameId = reader.ReadInt16();
                            thisSquadInfo.airbaseIcon = reader.ReadInt16();
                            thisSquadInfo.squadronPath = reader.ReadInt16();
                            thisSquadInfo.specialty = reader.ReadByte();
                            thisSquadInfo.currentStrength = reader.ReadByte();
                            thisSquadInfo.country = reader.ReadByte();
                            var airbaseNameBytes = reader.ReadBytes(40);
                            thisSquadInfo.airbaseName = Encoding.ASCII.GetString(airbaseNameBytes, 0, 40);
                            nullLoc = thisSquadInfo.airbaseName.IndexOf('\0');
                            if (nullLoc > -1) thisSquadInfo.airbaseName = thisSquadInfo.airbaseName.Substring(0, nullLoc);

                            reader.ReadByte(); //align on int32 boundary
                            SquadInfo[i] = thisSquadInfo;
                        }
                    }
                }
                if (_version >= 31)
                {
                    Tempo = reader.ReadByte();
                }
                if (_version >= 43)
                {
                    CreatorIP = reader.ReadInt32();
                    CreationTime = reader.ReadInt32();
                    CreationRand = reader.ReadInt32();
                }
            }
        }
        protected byte[] Encode()
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(CurrentTime);
                if (_version >= 48)
                {
                    writer.Write(TE_StartTime);
                    writer.Write(TE_TimeLimit);
                    if (_version >= 49)
                    {
                        writer.Write(TE_VictoryPoints);
                    }
                }
                if (_version >= 52)
                {
                    writer.Write(TE_Type);
                    writer.Write(TE_number_teams);

                    for (int i = 0; i < 8; i++)
                    {
                        writer.Write(TE_number_aircraft[i]);
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        writer.Write(TE_number_f16s[i]);
                    }

                    writer.Write(TE_team);

                    for (int i = 0; i < 8; i++)
                    {
                        writer.Write(TE_team_pts[i]);
                    }

                    writer.Write(TE_flags);

                    for (int i = 0; i < 8; i++)
                    {
                        var info = this.TeamBasicInfo[i];
                        writer.Write(info.teamFlag);
                        writer.Write(info.teamColor);
                        writer.Write(Encoding.ASCII.GetBytes(info.teamName.PadRight(20, '\0')));
                        writer.Write(Encoding.ASCII.GetBytes(info.teamMotto.PadRight(200, '\0')));
                    }
                }
                if (_version >= 19)
                {
                    writer.Write(lastMajorEvent);
                }

                writer.Write(lastResupply);
                writer.Write(lastRepair);
                writer.Write(lastReinforcement);
                writer.Write(TimeStamp);

                writer.Write(Group);
                writer.Write(GroundRatio);
                writer.Write(AirRatio);
                writer.Write(AirDefenseRatio);
                writer.Write(NavalRatio);
                writer.Write(Brief);
                writer.Write(TheaterSizeX);
                writer.Write(TheaterSizeY);
                writer.Write(CurrentDay);
                writer.Write(ActiveTeams);
                writer.Write(DayZero);
                writer.Write(EndgameResult);
                writer.Write(Situation);
                writer.Write(EnemyAirExp);
                writer.Write(EnemyADExp);
                writer.Write(BullseyeName);
                writer.Write(BullseyeX);
                writer.Write(BullseyeY);

                writer.Write(Encoding.ASCII.GetBytes(TheaterName.PadRight(40, '\0')));
                writer.Write(Encoding.ASCII.GetBytes(Scenario.PadRight(40, '\0')));
                writer.Write(Encoding.ASCII.GetBytes(SaveFile.PadRight(40, '\0')));
                writer.Write(Encoding.ASCII.GetBytes(UIName.PadRight(40, '\0')));

                writer.Write(PlayerSquadronID.num_);
                writer.Write(PlayerSquadronID.creator_);

                writer.Write((short)NumRecentEventEntries);
                if (NumRecentEventEntries > 0)
                {
                    for (int i = 0; i < NumRecentEventEntries; i++)
                    {
                        var thisNode = RecentEventEntries[i];
                        writer.Write(thisNode.x);
                        writer.Write(thisNode.y);
                        writer.Write(thisNode.time);
                        writer.Write(thisNode.flags);
                        writer.Write(thisNode.Team);
                        writer.Write(new byte[2]); //align on int32 boundary
                        //skip EventText pointer
                        writer.Write(new byte[4]);
                        //skip UiEventNode pointer
                        writer.Write(new byte[4]);

                        writer.Write((short)thisNode.eventText.Length + 1);
                        writer.Write(Encoding.ASCII.GetBytes(thisNode.eventText + '\0'));
                    }
                }

                writer.Write((short)NumPriorityEventEntries);
                for (int i = 0; i < NumPriorityEventEntries; i++)
                {
                    var thisNode = PriorityEventEntries[i];
                    writer.Write(thisNode.x);
                    writer.Write(thisNode.y);
                    writer.Write(thisNode.time);
                    writer.Write(thisNode.flags);
                    writer.Write(thisNode.Team);
                    writer.Write(new byte[2]);//align on int32 boundary
                    //skip EventText pointer
                    writer.Write(new byte[4]);
                    //skip UiEventNode pointer
                    writer.Write(new byte[4]);
                    writer.Write((short)thisNode.eventText.Length + 1);
                    writer.Write(Encoding.ASCII.GetBytes(thisNode.eventText + '\0'));
                }
                writer.Write(CampMapSize);
                if (CampMapSize > 0)
                {
                    writer.Write(CampMap);
                }

                writer.Write(LastIndexNum);
                writer.Write(NumAvailableSquadrons);
                if (NumAvailableSquadrons > 0)
                {
                    if (_version < 42)
                    {
                        for (int i = 0; i < NumAvailableSquadrons; i++)
                        {
                            var thisSquadInfo = SquadInfo[i];
                            writer.Write(thisSquadInfo.x);
                            writer.Write(thisSquadInfo.y);

                            writer.Write(thisSquadInfo.id.num_);
                            writer.Write(thisSquadInfo.id.creator_);

                            writer.Write(thisSquadInfo.descriptionIndex);
                            writer.Write(thisSquadInfo.nameId);
                            writer.Write(thisSquadInfo.specialty);
                            writer.Write(thisSquadInfo.currentStrength);
                            writer.Write(thisSquadInfo.country);

                            writer.Write(Encoding.ASCII.GetBytes(thisSquadInfo.airbaseName.PadRight(80,'\0')));
                            writer.Write((byte)0x00);//align on int32 boundary
                        }
                    }
                    else
                    {
                        for (int i = 0; i < NumAvailableSquadrons; i++)
                        {
                            var thisSquadInfo = SquadInfo[i];
                            writer.Write(thisSquadInfo.x);
                            writer.Write(thisSquadInfo.y);

                            writer.Write(thisSquadInfo.id.num_);
                            writer.Write(thisSquadInfo.id.creator_);

                            writer.Write(thisSquadInfo.descriptionIndex);
                            writer.Write(thisSquadInfo.nameId);
                            writer.Write(thisSquadInfo.airbaseIcon);
                            writer.Write(thisSquadInfo.squadronPath);

                            writer.Write(thisSquadInfo.specialty);
                            writer.Write(thisSquadInfo.currentStrength);
                            writer.Write(thisSquadInfo.country);

                            writer.Write(Encoding.ASCII.GetBytes(thisSquadInfo.airbaseName.PadRight(80, '\0')));
                            writer.Write((byte)0x00);//align on int32 boundary
                        }
                    }
                }
                if (_version >= 31)
                {
                    writer.Write(Tempo);
                }
                if (_version >= 43)
                {
                    writer.Write(CreatorIP);
                    writer.Write(CreationTime);
                    writer.Write(CreationRand);
                }
                writer.Flush();
                stream.Flush();
                return stream.ToArray();
            }
        }
        public void Write(Stream stream, int version)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                var uncompressedData = Encode();
                var compressedData = Lzss.Codec.Compress(uncompressedData);
                writer.Write(compressedData.Length);
                writer.Write(uncompressedData.Length);
                writer.Write(compressedData);
            }
        }
        protected static byte[] Expand(byte[] compressed)
        {
            int compressedSize = BitConverter.ToInt32(compressed, 0);
            int uncompressedSize = BitConverter.ToInt32(compressed, 4);
            if (uncompressedSize == 0) return null;
            byte[] actualCompressed = new byte[compressed.Length - 8];
            Array.Copy(compressed, 8, actualCompressed, 0, actualCompressed.Length);
            byte[] uncompressed = null;
            uncompressed = Lzss.Codec.Decompress(actualCompressed, uncompressedSize);
            return uncompressed;
        }


    }
}