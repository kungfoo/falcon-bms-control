using System;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [ComVisible(true)]
    [Serializable]
    public enum DirectInputBindingType
    {
        None = 0,
        Button = -2,
        POVDirection = -3
    }
}