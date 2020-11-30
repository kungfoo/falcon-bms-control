using System;

namespace Common.InputSupport
{
    /// <summary>
    ///     Enumeration of the various types of possible axes that can be defined on a device
    /// </summary>
    [Serializable]
    public enum AxisType
    {
        Unknown = 0,
        X = 1,
        Y = 2,
        Z = 3,
        XR = 4,
        YR = 5,
        ZR = 6,
        Slider = 7,
        Pov = 8
    }
}