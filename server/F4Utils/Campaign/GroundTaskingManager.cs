using System;
using System.IO;
using System.Text;
namespace F4Utils.Campaign
{
    public class GroundTaskingManager : CampaignManager
    {
        #region Public Fields
        public short flags;
        #endregion

        protected GroundTaskingManager()
            : base()
        {
        }
        public GroundTaskingManager(Stream stream, int version)
            : base(stream, version)
        {
            ReadGroundTaskingManager(stream, version);
        }

        protected void ReadGroundTaskingManager(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                flags = reader.ReadInt16();
            }
        }
        public void WriteGroundTaskingManager(Stream stream, int version)
        {
            base.WriteCampaignManager(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(flags);
            }
        }

    }
}