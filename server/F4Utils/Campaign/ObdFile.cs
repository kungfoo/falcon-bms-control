using System;
using F4Utils.Campaign.F4Structs;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{

    public class ObdFile
    {
        #region Public Fields

        public ObjectiveDelta[] deltas;

        #endregion

        protected int _version = 0;

        protected ObdFile()
            : base()
        {
        }
        public ObdFile(byte[] compressed, int version)
            : this()
        {
            _version = version;
            short numObjectiveDeltas = 0;
            byte[] expanded = Expand(compressed, out numObjectiveDeltas);
            if (expanded != null) Decode(expanded, version, numObjectiveDeltas);
        }
        protected void Decode(byte[] bytes, int version, short numObjectiveDeltas)
        {
            using (var stream = new MemoryStream(bytes))
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen:true))
            {
                deltas = new ObjectiveDelta[numObjectiveDeltas];

                for (int i = 0; i < numObjectiveDeltas; i++)
                {
                    ObjectiveDelta thisObjectiveDelta = new ObjectiveDelta();

                    VU_ID id = new VU_ID();
                    id.num_ = reader.ReadUInt32();
                    id.creator_ = reader.ReadUInt32();
                    thisObjectiveDelta.id = id;

                    thisObjectiveDelta.last_repair = reader.ReadUInt32();
                    thisObjectiveDelta.owner = reader.ReadByte();
                    thisObjectiveDelta.supply = reader.ReadByte();
                    thisObjectiveDelta.fuel = reader.ReadByte();
                    thisObjectiveDelta.losses = reader.ReadByte();
                    var numFstatus = reader.ReadByte();
                    thisObjectiveDelta.fStatus = new byte[numFstatus];
                    if (version < 64)
                    {
                        thisObjectiveDelta.fStatus[0] = reader.ReadByte();
                    }
                    else
                    {
                        for (int j = 0; j < numFstatus; j++)
                        {
                            thisObjectiveDelta.fStatus[j] = reader.ReadByte();
                        }
                    }
                    deltas[i] = thisObjectiveDelta;
                }
            }
        }
        protected static byte[] Expand(byte[] compressed, out short numObjectiveDeltas)
        {
            using (var stream = new MemoryStream(compressed))
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen:true))
            {
                int cSize = reader.ReadInt32();
                numObjectiveDeltas = reader.ReadInt16();
                int uncompressedSize = reader.ReadInt32();
                if (uncompressedSize == 0) return null;
                var actualCompressed = reader.ReadBytes(compressed.Length - 10);
                byte[] uncompressed = null;
                uncompressed = Lzss.Codec.Decompress(actualCompressed, uncompressedSize);
                return uncompressed;
            }
        }
    }
}