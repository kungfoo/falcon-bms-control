using System;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [ComVisible(true)]
    [Serializable]
    public enum PovDirections
    {
        None = 0,
        Up = 0,
        UpRight = 1,
        Right = 2,
        DownRight = 3,
        Down = 4,
        DownLeft = 5,
        Left = 6,
        UpLeft = 7
    }
}