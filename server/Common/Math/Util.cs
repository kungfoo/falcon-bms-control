namespace Common.Math
{
    public class Util
    {
        public static float AngleDelta(float ang1, float ang2)
        {
            ang1 = ang1 % 360;
            ang2 = ang2 % 360;
            if (ang1 == ang2)
            {
                return 0.0f; //No angle to compute
            }
            var fDif = ang2 - ang1; //There is an angle to compute
            if (fDif >= 180.0f)
            {
                fDif = fDif - 180.0f; //correct the half
                fDif = 180.0f - fDif; //invert the half
                fDif = 0 - fDif; //reverse direction
                return fDif;
            }
            if (fDif <= -180.0f)
            {
                fDif = fDif + 180.0f; //correct the half
                fDif = 180.0f + fDif;
                return fDif;
            }
            return fDif;
        }
    }
}