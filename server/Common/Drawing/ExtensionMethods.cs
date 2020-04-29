using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Drawing
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Convert<T>(this IEnumerable source) => 
            from dynamic current in source select (T)(current);
    }
}