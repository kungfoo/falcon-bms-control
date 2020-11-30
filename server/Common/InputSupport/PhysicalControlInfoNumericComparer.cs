using System.Collections;
using Common.Strings;

// required for PhysicalControlInfoNumericComparer : IComparer only

namespace Common.InputSupport
{
    /// <summary>
    ///     Compares two PhysicalControlInfo objects by their Aliases (useful in editors)
    /// </summary>
    internal sealed class PhysicalControlInfoNumericComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var info = x as PhysicalControlInfo;
            if (info != null && y is PhysicalControlInfo)
            {
                return StringLogicalComparer.Compare(info.Alias, ((PhysicalControlInfo) y).Alias);
            }
            return -1;
        }
    }

//EOC
}