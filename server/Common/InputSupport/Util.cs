namespace Common.InputSupport
{
    public static class Util
    {
        public static PovDirections GetPovDirection(int state)
        {
            float currentDegrees = state / 100;
            if (state == -1) currentDegrees = -1;
            /*  POV directions in degrees
                      0
                337.5  22.5   
               315         45
             292.5           67.5
            270                90
             247.5           112.5
              225          135
                202.5  157.5
                    180
             */
            var direction = PovDirections.None;
            if (currentDegrees > 337.5 && currentDegrees <= 360 || currentDegrees >= 0 && currentDegrees <= 22.5)
            {
                direction = PovDirections.Up;
            }
            else if (currentDegrees > 22.5 && currentDegrees <= 67.5)
            {
                direction = PovDirections.UpRight;
            }
            else if (currentDegrees > 67.5 && currentDegrees <= 112.5)
            {
                direction = PovDirections.Right;
            }
            else if (currentDegrees > 112.5 && currentDegrees <= 157.5)
            {
                direction = PovDirections.DownRight;
            }
            else if (currentDegrees > 157.5 && currentDegrees <= 202.5)
            {
                direction = PovDirections.Down;
            }
            else if (currentDegrees > 202.5 && currentDegrees <= 247.5)
            {
                direction = PovDirections.DownLeft;
            }
            else if (currentDegrees > 247.5 && currentDegrees <= 292.5)
            {
                direction = PovDirections.Left;
            }
            else if (currentDegrees > 292.5 && currentDegrees <= 337.5)
            {
                direction = PovDirections.UpLeft;
            }
            return direction;
        }
    }
}