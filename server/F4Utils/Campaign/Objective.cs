using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class Objective : CampaignBase
    {
        #region Public Fields
        public short objectiveType;
        public uint lastRepair;
        public uint obj_flags;
        public byte supply;
        public byte fuel;
        public byte losses;
        public byte[] fstatus;
        public byte priority;
        public short nameId;
        public VU_ID parent;
        public byte first_owner;
        public byte links;
        public CampObjectiveLinkDataType[] link_data;
        public float[] detect_ratio; //radar_data, init size=8
        #endregion

        protected Objective()
            : base()
        {
        }
        public Objective(Stream stream, int version)
            : base(stream, version)
        {
            ReadObjective(stream, version);
        }

        protected void ReadObjective(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                lastRepair = reader.ReadUInt32();

                if (version > 1)
                {
                    obj_flags = reader.ReadUInt32();
                }
                else
                {
                    obj_flags = reader.ReadUInt16();
                }

                supply = reader.ReadByte();
                fuel = reader.ReadByte();
                losses = reader.ReadByte();
                byte numStatuses = reader.ReadByte();

                fstatus = new byte[numStatuses];
                for (int i = 0; i < numStatuses; i++)
                {
                    fstatus[i] = reader.ReadByte();
                }
                priority = reader.ReadByte();
                nameId = reader.ReadInt16();

                parent = new VU_ID();
                parent.num_ = reader.ReadUInt32();
                parent.creator_ = reader.ReadUInt32();

                first_owner = reader.ReadByte();
                links = reader.ReadByte();

                if (links > 0)
                {
                    link_data = new CampObjectiveLinkDataType[links];
                }
                else
                {
                    link_data = null;
                }
                for (int i = 0; i < links; i++)
                {
                    CampObjectiveLinkDataType thisLink = new CampObjectiveLinkDataType();
                    thisLink.costs = new byte[(int)MoveType.MOVEMENT_TYPES];
                    for (int j = 0; j < (int)MoveType.MOVEMENT_TYPES; j++)
                    {
                        thisLink.costs[j] = reader.ReadByte();
                    }
                    VU_ID newId = new VU_ID();
                    newId.num_ = reader.ReadUInt32();
                    newId.creator_ = reader.ReadUInt32();
                    thisLink.id = newId;
                    link_data[i] = thisLink;
                }

                if (version >= 20)
                {
                    byte hasRadarData = reader.ReadByte();

                    if (hasRadarData > 0)
                    {
                        detect_ratio = new float[8];
                        for (int i = 0; i < 8; i++)
                        {
                            detect_ratio[i] = reader.ReadSingle();
                        }
                    }
                    else
                    {
                        detect_ratio = null;
                    }
                }
                else
                {
                    detect_ratio = null;
                }
            }
        }
        public void WriteObjective(Stream stream, int version)
        {
            base.WriteCampaignBase(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(lastRepair);

                if (version > 1)
                {
                    writer.Write(obj_flags);
                }
                else
                {
                    writer.Write((ushort)obj_flags);
                }

                writer.Write(supply);
                writer.Write(fuel);
                writer.Write(losses);
                writer.Write((byte)fstatus.Length); 

                for (int i = 0; i < fstatus.Length; i++)
                {
                    writer.Write(fstatus[i]);
                }
                writer.Write(priority);
                writer.Write(nameId);

                writer.Write(parent.num_);
                writer.Write(parent.creator_);

                writer.Write(first_owner);
                writer.Write(links);

                for (int i = 0; i < links; i++)
                {
                    var thisLink = link_data[i];
                    for (int j = 0; j < (int)MoveType.MOVEMENT_TYPES; j++)
                    {
                        writer.Write(thisLink.costs[j]);
                    }
                    writer.Write(thisLink.id.num_);
                    writer.Write(thisLink.id.creator_);
                }

                if (version >= 20)
                {
                    var hasRadarData = (byte)(detect_ratio != null ? detect_ratio.Length : 0);
                    writer.Write(hasRadarData);

                    if (hasRadarData > 0)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            writer.Write(detect_ratio[i]);
                        }
                    }
                }
            }
        }
    }
}