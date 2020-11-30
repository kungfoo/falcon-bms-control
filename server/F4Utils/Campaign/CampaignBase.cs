using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class CampaignBase
    {
        #region Public Fields

        public VU_ID id;
        public ushort entityType;
        public short x;
        public short y;
        public float z;
        public uint spotTime;
        public short spotted;
        public short baseFlags;
        public byte owner;
        public short campId;

        #endregion
        protected CampaignBase() { }
        public CampaignBase(Stream stream, int version):this()
        {
            ReadCampaignBase(stream, version);
        }

        protected void ReadCampaignBase(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                id = new VU_ID();
                id.num_ = reader.ReadUInt32();
                id.creator_ = reader.ReadUInt32();

                entityType = reader.ReadUInt16();

                x = reader.ReadInt16();
                y = reader.ReadInt16();

                if (version < 70)
                {
                    z = 0;
                }
                else
                {
                    z = reader.ReadSingle();
                }
                spotTime = reader.ReadUInt32();
                spotted = reader.ReadInt16();
                baseFlags = reader.ReadInt16();
                owner = reader.ReadByte();
                campId = reader.ReadInt16();

            }
        }
        public void WriteCampaignBase(Stream stream, int version)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(id.num_);
                writer.Write(id.creator_);
                writer.Write(entityType);
                writer.Write(x);
                writer.Write(y);

                if (version >= 70)
                {
                    writer.Write(z);
                }
                writer.Write(spotTime);
                writer.Write(spotted);
                writer.Write(baseFlags);
                writer.Write(owner);
                writer.Write(campId);
            }
        }
    }
}