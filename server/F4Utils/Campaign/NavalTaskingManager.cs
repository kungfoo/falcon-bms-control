using System;
using System.IO;
using System.Text;
namespace F4Utils.Campaign
{
    public class NavalTaskingManager : CampaignManager
    {
        #region Public Fields
        public short flags;
        #endregion

        protected NavalTaskingManager()
            : base()
        {
        }
        public NavalTaskingManager(Stream stream, int version)
            : base(stream, version)
        {
            ReadNavalTaskingManager(stream, version);
        }

        protected void ReadNavalTaskingManager(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                flags = reader.ReadInt16();
            }
        }
        public void WriteNavalTaskingManager(Stream stream, int version)
        {
            base.WriteCampaignManager(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(flags);
            }
        }

    }
}