using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{

    public class Brigade : GroundUnit
    {
        #region Public Fields
        public VU_ID[] element;
        public byte elements;
        #endregion

        protected Brigade()
            : base()
        {
        }
        public Brigade(Stream stream, int version)
            : base(stream, version)
        {
            ReadBrigade(stream, version);

        }

        protected void ReadBrigade(Stream stream, int version)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                elements = reader.ReadByte();
                element = new VU_ID[elements];
                for (int i = 0; i < elements; i++)
                {
                    var thisElement = new VU_ID();
                    thisElement.num_ = reader.ReadUInt32();
                    thisElement.creator_ = reader.ReadUInt32();
                    element[i] = thisElement;
                }
            }
        }
        public void WriteBrigade(Stream stream, int version)
        {
            base.WriteGroundUnit(stream, version);
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(elements);
                for (int i = 0; i < elements; i++)
                {
                    var thisElement = element[i];
                    writer.Write(thisElement.num_);
                    writer.Write(thisElement.creator_);
                }
            }
        }
    }
}