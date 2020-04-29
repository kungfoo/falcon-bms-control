#region Using statements

using System.Collections;
using System.Collections.Generic;

// required for NumericComparer : IComparer only

#endregion

namespace Common.Strings
{
    /// <summary>
    ///     Provides a Comparer that can properly sort numeric-containing strings.
    ///     The default sorting is "1,110, 112, 2, 20,...";
    ///     this comparer provides for number-line sorting
    ///     (1,2, 10, 20, 110, 120, ...);
    /// </summary>
    public sealed class NumericComparer : IComparer, IComparer<string>
    {
        public int Compare(object x, object y)
        {
            if (x is string && y is string)
            {
                return StringLogicalComparer.Compare((string) x, (string) y);
            }
            return -1;
        }

        public int Compare(string x, string y)
        {
            return Compare(x, (object) y);
        }
    }

//EOC
}