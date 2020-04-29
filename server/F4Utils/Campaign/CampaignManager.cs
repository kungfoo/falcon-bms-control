using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class CampaignManager
    {
        #region Public Fields
        public VU_ID id;
        public VU_ID ownerId;
        public ushort entityType;
        public short managerFlags;
        public byte owner;
        #endregion

        protected CampaignManager() { }
        public CampaignManager(Stream stream, int version)
        {
            ReadCampaignManager(stream, version);
        }

        public void ReadCampaignManager(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                id = new VU_ID();
                id.num_ = reader.ReadUInt32();
                id.creator_ = reader.ReadUInt32();
                entityType = reader.ReadUInt16();
                managerFlags = reader.ReadInt16();
                owner = reader.ReadByte();
            }
        }
        public void WriteCampaignManager(Stream stream, int version)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(id.num_);
                writer.Write(id.creator_);
                writer.Write(entityType);
                writer.Write(managerFlags);
                writer.Write(owner);
            }
        }
    }
}