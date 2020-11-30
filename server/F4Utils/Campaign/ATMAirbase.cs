using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class ATMAirbase
    {
        #region Public Fields
        public VU_ID id;
        public byte[] schedule;
        #endregion
        protected ATMAirbase(){}
        public ATMAirbase(Stream stream, int version)
            : this()
        {
            Read(stream, version);
        }

        protected void Read(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                id = new VU_ID();
                id.num_ = reader.ReadUInt32();
                id.creator_ = reader.ReadUInt32();

                schedule = new byte[32];
                for (int j = 0; j < schedule.Length; j++)
                {
                    schedule[j] = reader.ReadByte();
                }
            }
        }
        public void Write(Stream stream, int version)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(id.num_);
                writer.Write(id.creator_);

                for (int j = 0; j < schedule.Length; j++)
                {
                    writer.Write(schedule[j]);
                }
            }
        }
    }
}