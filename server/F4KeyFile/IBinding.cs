using System;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [ComVisible(true)]
    public interface ILineInFile
    {
        int LineNum { get; set; }
    }

    [ComVisible(true)]
    public interface IBinding:ILineInFile
    {
        String Callback { get; set; }
    }
}