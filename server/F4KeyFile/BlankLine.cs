using System;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Serializable]
    public sealed class BlankLine : ILineInFile
    {
        public BlankLine() { }

        public int LineNum { get; set; }
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
