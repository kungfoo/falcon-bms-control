using System;
using System.IO;
using System.Text;

namespace F4Utils.Terrain.Structs
{
    [Serializable]
    public struct TextureBinPathRecord
    {
        public TextureBinPathRecord(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                this.type = reader.ReadInt32();
                this.size = reader.ReadSingle();
                this.x1 = reader.ReadSingle();
                this.y1 = reader.ReadSingle();
                this.x2 = reader.ReadSingle();
                this.y2 = reader.ReadSingle();
            }

        }
        public float size;
        public int type;
        public float x1;
        public float x2;
        public float y1;
        public float y2;
    }
}