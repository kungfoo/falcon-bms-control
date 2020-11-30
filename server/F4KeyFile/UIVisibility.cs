using System;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [ComVisible(true)]
    [Serializable]
    public enum UIVisibility
    {
        Locked=-0,
        VisibleWithChangesAllowed = 1,
        Headline = -1,
        Hidden = -2
    }
}