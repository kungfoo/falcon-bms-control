namespace Common.Math
{
    public static class Constants
    {
        public const float FEET_PER_NM = 6076.11549f;
        public const float INCHES_PER_NAUTICAL_MILE = 72913.3858f;
        public const float NAUTICAL_MILES_PER_INCH = 1.0f / INCHES_PER_NAUTICAL_MILE;
        public const float PI = (float)3.1415926535897932384626433832795;
        public const float TWO_PI = (float)6.283185307179586476925286766559;
        public const float PI_OVER_180 = (PI / 180.0f);

        public const float RADIANS_PER_DEGREE = 0.0174532925f;
        public const float FPS_PER_KNOT = 1.68780986f;
        public const float FPS_TO_MPS = 0.3048f;
        public const float INCHES_MERCURY_TO_HECTOPASCALS = 33.86f;
    }
}