using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class Battalion : GroundUnit
    {
        #region Public Fields
        public uint last_move;
        public uint last_combat;
        public VU_ID parent_id;
        public VU_ID last_obj;
        public short lfx;
        public short lfy;
        public short rfx;
        public short rfy;
        public byte supply;
        public byte fatigue;
        public byte morale;
        public byte heading;
        public byte final_heading;
        public byte dummy;
        public byte position;
        #endregion

        protected Battalion()
            : base()
        {
        }
        public Battalion(Stream stream, int version)
            : base(stream, version)
        {
            ReadBattalion(stream, version);
        }

        protected void ReadBattalion(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                last_move = reader.ReadUInt32();
                last_combat = reader.ReadUInt32();

                parent_id = new VU_ID();
                parent_id.num_ = reader.ReadUInt32();
                parent_id.creator_ = reader.ReadUInt32();

                last_obj = new VU_ID();
                last_obj.num_ = reader.ReadUInt32();
                last_obj.creator_ = reader.ReadUInt32();

                supply = reader.ReadByte();
                fatigue = reader.ReadByte();
                morale = reader.ReadByte();
                heading = reader.ReadByte();
                final_heading = reader.ReadByte();

                if (version < 15)
                {
                    dummy = reader.ReadByte();
                }
                position = reader.ReadByte();

            }
        }
        public void WriteBattalion(Stream stream, int version)
        {
            base.WriteGroundUnit(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(last_move);
                writer.Write(last_combat);

                writer.Write(parent_id.num_);
                writer.Write(parent_id.creator_);

                writer.Write(last_obj.num_);
                writer.Write(last_obj.creator_);

                writer.Write(supply);
                writer.Write(fatigue);
                writer.Write(morale);
                writer.Write(heading);
                writer.Write(final_heading);

                if (version < 15)
                {
                    writer.Write((byte)0x00);
                }
                writer.Write(position);

            }
        }
    }
}