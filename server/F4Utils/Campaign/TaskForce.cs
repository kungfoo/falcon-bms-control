using System;
using System.IO;
using System.Text;
namespace F4Utils.Campaign
{
    public class TaskForce : Unit
    {
        #region Public Fields
        public byte orders;
        public byte supply;
        #endregion

        protected TaskForce()
            : base()
        {
        }
        public TaskForce(Stream stream, int version)
            : base(stream, version)
        {
            ReadTaskForce(stream, version);
        }

        protected void ReadTaskForce(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                orders = reader.ReadByte();
                supply = reader.ReadByte();
            }
        }
        public void WriteTaskForce(Stream stream, int version)
        {
            base.WriteUnit(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(orders);
                writer.Write(supply);
            }

        }
    }
}