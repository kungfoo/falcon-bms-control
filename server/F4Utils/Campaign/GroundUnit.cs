using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class GroundUnit : Unit
    {
        #region Public Fields
        public byte orders;
        public short division;
        public VU_ID aobj;
        #endregion

        protected GroundUnit()
            : base()
        {
        }
        public GroundUnit(Stream stream, int version)
            : base(stream, version)
        {
            ReadGroundUnit(stream, version);

        }

        protected void ReadGroundUnit(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                orders = reader.ReadByte();
                division = reader.ReadInt16();
                aobj = new VU_ID();
                aobj.num_ = reader.ReadUInt32();
                aobj.creator_ = reader.ReadUInt32();
            }
        }
        public void WriteGroundUnit(Stream stream, int version)
        {
            base.WriteUnit(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(orders);
                writer.Write(division);
                writer.Write(aobj.num_);
                writer.Write(aobj.creator_);
            }
        }

    }
}