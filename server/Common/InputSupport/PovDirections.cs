using System;

namespace Common.InputSupport
{
    [Serializable]
    public enum PovDirections
    {
        None = 0,
        Up = 1,
        UpRight = 2,
        Right = 3,
        DownRight = 4,
        Down = 5,
        DownLeft = 6,
        Left = 7,
        UpLeft = 8
    }
}