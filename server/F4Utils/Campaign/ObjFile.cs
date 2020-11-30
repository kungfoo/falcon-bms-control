using System;
using System.IO;
using System.Text;

namespace F4Utils.Campaign
{
    public class ObjFile
    {
        #region Public Fields

        public Objective[] objectives;

        #endregion

        protected int _version = 0;
        protected ObjFile()
            : base()
        {
        }
        public ObjFile(byte[] compressed, int version)
            : this()
        {
            _version = version;
            short numObjectives = 0;
            byte[] expanded = Expand(compressed, out numObjectives);
            if (expanded != null) Decode(expanded, version, numObjectives);
        }
        protected byte[] Encode(int version)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                for (int i = 0; i < objectives.Length; i++)
                {
                    writer.Write(objectives[i].objectiveType);
                    objectives[i].WriteObjective(stream, version);
                }
                writer.Flush();
                stream.Flush();
                return stream.ToArray();
            }
            
        }
        protected void Decode(byte[] bytes, int version, short numObjectives)
        {
            this.objectives = new Objective[numObjectives];

            using (var stream = new MemoryStream(bytes))
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen:true))
            {
                for (int i = 0; i < numObjectives; i++)
                {
                    short thisObjectiveType = reader.ReadInt16();
                    objectives[i] = new Objective(stream, version) { objectiveType = thisObjectiveType }; 
                }
            }
        }
        protected void Write(Stream stream, int version)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write((short)objectives.Length);
                var uncompressedBytes = Encode(version);
                writer.Write(uncompressedBytes.Length);
                var compressedData = Lzss.Codec.Compress(uncompressedBytes);
                writer.Write(compressedData.Length);
                writer.Write(compressedData);
            }
        }
        
        protected static byte[] Expand(byte[] compressed, out short numObjectives)
        {
            using (var stream = new MemoryStream(compressed))
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen:true))
            {
                numObjectives = reader.ReadInt16();
                var uncompressedSize = reader.ReadInt32();
                var newSize = reader.ReadInt32();
                if (uncompressedSize == 0) return null;

                var actualCompressed = reader.ReadBytes(compressed.Length - 10);
                byte[] uncompressed = null;
                uncompressed = Lzss.Codec.Decompress(actualCompressed, uncompressedSize);
                return uncompressed;
            }
        }
    }
}