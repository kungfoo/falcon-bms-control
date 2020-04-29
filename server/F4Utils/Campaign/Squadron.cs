using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class Squadron : AirUnit
    {
        #region Public Fields
        public int fuel;
        public byte specialty;
        public byte[] stores;
        public Pilot[] pilots;
        public int[] schedule;
        public VU_ID airbase_id;
        public VU_ID hot_spot;
        public VU_ID junk;
        public byte[] rating;
        public short aa_kills;
        public short ag_kills;
        public short as_kills;
        public short an_kills;
        public short missions_flown;
        public short mission_score;
        public byte total_losses;
        public byte pilot_losses;
        public byte squadron_patch;
        #endregion

        protected Squadron()
            : base()
        {
        }
        public Squadron(Stream stream, int version)
            : base(stream, version)
        {
            ReadSquadron(stream, version);
        }

        private void ReadSquadron(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                fuel = reader.ReadInt32();
                specialty = reader.ReadByte();

                if (version < 69)
                {
                    stores = new byte[200];
                    for (int i = 0; i < 200; i++)
                    {
                        stores[i] = reader.ReadByte();
                    }
                }
                else if (version < 72)
                {
                    stores = new byte[220];
                    for (int i = 0; i < 220; i++)
                    {
                        stores[i] = reader.ReadByte();
                    }
                }
                else
                {
                    stores = new byte[600];
                    for (int i = 0; i < 600; i++)
                    {
                        stores[i] = reader.ReadByte();
                    }
                }


                if (version < 47)
                {
                    if (version >= 29)
                    {
                        pilots = new Pilot[48];
                        for (int j = 0; j < pilots.Length; j++)
                        {
                            Pilot thisPilot = new Pilot();
                            thisPilot.pilot_id = reader.ReadInt16();
                            thisPilot.pilot_skill_and_rating = reader.ReadByte();
                            thisPilot.pilot_status = reader.ReadByte();
                            thisPilot.aa_kills = reader.ReadByte();
                            thisPilot.ag_kills = reader.ReadByte();
                            thisPilot.as_kills = reader.ReadByte();
                            thisPilot.an_kills = reader.ReadByte();
                            /*
                            p.missions_flown = reader.ReadInt16();
                             */
                            pilots[j] = thisPilot;
                        }
                    }
                    else
                    {
                        pilots = new Pilot[36];
                        for (int j = 0; j < pilots.Length; j++)
                        {
                            Pilot thisPilot = new Pilot();
                            thisPilot.pilot_id = reader.ReadInt16();
                            thisPilot.pilot_skill_and_rating = reader.ReadByte();
                            thisPilot.pilot_status = reader.ReadByte();
                            thisPilot.aa_kills = reader.ReadByte();
                            thisPilot.ag_kills = reader.ReadByte();
                            thisPilot.as_kills = reader.ReadByte();
                            thisPilot.an_kills = reader.ReadByte();
                            /*
                            p.missions_flown = reader.ReadInt16();
                             */
                            pilots[j] = thisPilot;
                        }
                    }
                }
                else
                {
                    pilots = new Pilot[48];
                    for (int j = 0; j < pilots.Length; j++)
                    {
                        Pilot thisPilot = new Pilot();
                        thisPilot.pilot_id = reader.ReadInt16();
                        thisPilot.pilot_skill_and_rating = reader.ReadByte();
                        thisPilot.pilot_status = reader.ReadByte();
                        thisPilot.aa_kills = reader.ReadByte();
                        thisPilot.ag_kills = reader.ReadByte();
                        thisPilot.as_kills = reader.ReadByte();
                        thisPilot.an_kills = reader.ReadByte();
                        thisPilot.missions_flown = reader.ReadInt16();
                        pilots[j] = thisPilot;
                    }
                }
                schedule = new int[16];
                for (int j = 0; j < schedule.Length; j++)
                {
                    schedule[j] = reader.ReadInt32();
                }
                airbase_id = new VU_ID();
                airbase_id.num_ = reader.ReadUInt32();
                airbase_id.creator_ = reader.ReadUInt32();

                hot_spot = new VU_ID();
                hot_spot.num_ = reader.ReadUInt32();
                hot_spot.creator_ = reader.ReadUInt32();

                if (version >= 6 && version < 16)
                {
                    junk = new VU_ID();
                    junk.num_ = reader.ReadUInt32();
                    junk.creator_ = reader.ReadUInt32();
                }
                rating = new byte[16];
                for (int j = 0; j < rating.Length; j++)
                {
                    rating[j] = reader.ReadByte();
                }
                aa_kills = reader.ReadInt16();
                ag_kills = reader.ReadInt16();
                as_kills = reader.ReadInt16();
                an_kills = reader.ReadInt16();
                missions_flown = reader.ReadInt16();
                mission_score = reader.ReadInt16();
                total_losses = reader.ReadByte();

                if (version >= 9)
                {
                    pilot_losses = reader.ReadByte();
                }
                else
                {
                    pilot_losses = 0;
                }

                if (version >= 45)
                {
                    squadron_patch = reader.ReadByte();
                }
            }
        }
        public void WriteSquadron(Stream stream, int version)
        {
            base.WriteAirUnit(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(fuel);
                writer.Write(specialty);

                if (version < 69)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        writer.Write(stores[i]);
                    }
                }
                else if (version < 72)
                {
                    for (int i = 0; i < 220; i++)
                    {
                        writer.Write(stores[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 600; i++)
                    {
                        writer.Write(stores[i]);
                    }
                }


                for (int j = 0; j < pilots.Length; j++)
                {
                    var thisPilot = pilots[j];
                    writer.Write(thisPilot.pilot_id);
                    writer.Write(thisPilot.pilot_skill_and_rating);
                    writer.Write(thisPilot.pilot_status);
                    writer.Write(thisPilot.aa_kills);
                    writer.Write(thisPilot.ag_kills);
                    writer.Write(thisPilot.as_kills);
                    writer.Write(thisPilot.an_kills);
                    if (version >= 47)
                    {
                        writer.Write(thisPilot.missions_flown);
                    }
                }

                for (int j = 0; j < schedule.Length; j++)
                {
                    writer.Write(schedule[j]);
                }
                writer.Write(airbase_id.num_);
                writer.Write(airbase_id.creator_);

                writer.Write(hot_spot.num_);
                writer.Write(hot_spot.creator_);

                if (version >= 6 && version < 16)
                {
                    writer.Write(junk.num_);
                    writer.Write(junk.creator_);
                }

                for (int j = 0; j < rating.Length; j++)
                {
                    writer.Write(rating[j]);
                }
                writer.Write(aa_kills);
                writer.Write(ag_kills);
                writer.Write(as_kills);
                writer.Write(an_kills);
                writer.Write(missions_flown);
                writer.Write(mission_score);
                writer.Write(total_losses);

                if (version >= 9)
                {
                    writer.Write(pilot_losses);
                }

                if (version >= 45)
                {
                    writer.Write(squadron_patch);
                }
            }
        }
    }
}