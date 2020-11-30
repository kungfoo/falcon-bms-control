using System;
using System.IO;
using System.Text;

namespace F4Utils.Terrain.Structs
{
    [Serializable]
    public struct TextureBinAreaRecord
    {
        public TextureBinAreaRecord(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream, Encoding.Default, leaveOpen:true))
            {
                this.type = reader.ReadInt32();
                this.size = reader.ReadSingle();
                this.x = reader.ReadSingle();
                this.y = reader.ReadSingle();
            }
        }
        public float size;
        public int type;
        public float x;
        public float y;
    }
}