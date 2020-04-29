using System;

namespace Common.InputSupport
{
    /// <summary>
    ///     Enumeration of the various types of supported controls that can appear on a device
    /// </summary>
    [Serializable]
    public enum ControlType
    {
        Unknown = 0,
        Axis = 1,
        Button = 2,
        Pov = 3,
        Key = 4
    }
}