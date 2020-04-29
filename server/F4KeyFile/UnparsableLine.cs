using System;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Serializable]
    public sealed class UnparsableLine : ILineInFile
    {
        public UnparsableLine() { }
        public UnparsableLine(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
        public int LineNum { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
