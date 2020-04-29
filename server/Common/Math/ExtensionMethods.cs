using System;

namespace Common.Math
{
    public static class ExtensionMethods
    {
        public static string FormatDecimal(this float toConvert, int decimalPlaces)
        {
            return FormatDecimal((double) toConvert, decimalPlaces);
        }

        public static string FormatDecimal(this double toConvert, int decimalPlaces)
        {
            var rounded = System.Math.Round(toConvert, decimalPlaces, MidpointRounding.AwayFromZero);
            var formatString = "{0:0." + new string('0', decimalPlaces) + "}";
            return string.Format(formatString, rounded);
        }
    }
}