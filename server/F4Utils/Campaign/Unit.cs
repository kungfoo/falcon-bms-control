using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class Unit : CampaignBase
    {
        #region Public Fields
        public short unitType;
        public uint last_check;
        public int roster;
        public int unit_flags;
        public short dest_x;
        public short dest_y;
        public VU_ID target_id;
        public VU_ID cargo_id;
        public byte moved;
        public byte losses;
        public byte tactic;
        public ushort current_wp;
        public short name_id;
        public short reinforcement;
        public ushort numWaypoints;
        public Waypoint[] waypoints;
        #endregion
        public const int U_FINAL = 0x100000;
        protected Unit()
            : base()
        {
        }
        public Unit(Stream stream, int version)
            : base(stream, version)
        {
            ReadUnit(stream, version);
        }

        public void ReadUnit(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                last_check = reader.ReadUInt32();
                roster = reader.ReadInt32();
                unit_flags = reader.ReadInt32();
                dest_x = reader.ReadInt16();
                dest_y = reader.ReadInt16();
                target_id = new VU_ID();
                target_id.num_ = reader.ReadUInt32();
                target_id.creator_ = reader.ReadUInt32();

                if (version > 1)
                {
                    cargo_id = new VU_ID();
                    cargo_id.num_ = reader.ReadUInt32();
                    cargo_id.creator_ = reader.ReadUInt32();
                }
                else
                {
                    cargo_id = new VU_ID();
                }
                moved = reader.ReadByte();
                losses = reader.ReadByte();
                tactic = reader.ReadByte();

                if (version >= 71)
                {
                    current_wp = reader.ReadUInt16();
                }
                else
                {
                    current_wp = reader.ReadByte();
                }
                name_id = reader.ReadInt16();
                reinforcement = reader.ReadInt16();
                DecodeWaypoints(stream, version);
            }
        }
        public void WriteUnit(Stream stream, int version)
        {
            base.WriteCampaignBase(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(last_check);
                writer.Write(roster);
                writer.Write(unit_flags);
                writer.Write(dest_x);
                writer.Write(dest_y);
                writer.Write(target_id.num_);
                writer.Write(target_id.creator_);

                if (version > 1)
                {
                    writer.Write(cargo_id.num_);
                    writer.Write(cargo_id.creator_);
                }
                writer.Write(moved);
                writer.Write(losses);
                writer.Write(tactic);

                if (version >= 71)
                {
                    writer.Write(current_wp);
                }
                else
                {
                    writer.Write((byte)current_wp);
                }
                writer.Write(name_id);
                writer.Write(reinforcement);
                EncodeWaypoints(stream, version);
            }
        }
        protected void EncodeWaypoints(Stream stream, int version)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                if (version >= 71)
                {
                    writer.Write(numWaypoints);
                }
                else
                {
                    writer.Write((byte)numWaypoints);
                }
                for (int i = 0; i < numWaypoints; i++)
                {
                    waypoints[i].Write(stream, version);
                }
            }
        }

        protected void DecodeWaypoints(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                if (version >= 71)
                {
                    numWaypoints = reader.ReadUInt16();
                }
                else
                {
                    numWaypoints = (ushort)reader.ReadByte();
                }
                if (numWaypoints > 500) return;
                waypoints = new Waypoint[numWaypoints];
                for (int i = 0; i < numWaypoints; i++)
                {
                    waypoints[i] = new Waypoint(stream, version);
                }
            }
        }
        protected bool Final
        {
            get
            {
                return ((unit_flags & U_FINAL) > 0);
            }
        }
    }
}