using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [Flags]
    [Serializable]
    [ComVisible(true)]
    public enum KeyModifiers
    {
        [Description("")]
        None = 0,
        [Description("SHIFT")]
        Shift = 1,
        [Description("CTRL")]
        Ctrl = 2,
        [Description("SHIFT CTRL")]
        ShiftControl = 3,
        [Description("ALT")]
        Alt = 4,
        [Description("SHIFT ALT")]
        ShiftAlt = 5,
        [Description("CTRL ALT")]
        CtrlAlt = 6,
        [Description("SHIFT CTRL ALT")]
        ShiftCtrlAlt = 7
    }
}