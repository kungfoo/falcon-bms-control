namespace Common.Imaging.Internal
{
    // Standard PCX header
    public struct PCXHead
    {
        public short BPL;
        public byte BitPerPixel;
        public byte[] ClrMap; //[16*3];
        public byte Encoding;
        public byte[] Filler; //[58];
        public short HRes;
        public byte ID;
        public byte NumPlanes;
        public short Pal_t;
        public byte Reserved1;
        public short VRes;
        public byte Version;
        public short X1;
        public short X2;
        public short Y1;
        public short Y2;
    }
}