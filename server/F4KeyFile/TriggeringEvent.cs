using System;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [Serializable]
    [ComVisible(true)]
    public enum TriggeringEvent
    {
        OnPress=0,
        OnRelease=0x42
    }
}
