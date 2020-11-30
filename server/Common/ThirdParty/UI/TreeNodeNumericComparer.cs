#region Using statements
using System;
using System.Collections; // required for TreeNodeNumericComparer : IComparer only
using System.Windows.Forms;
using Common.Strings;
#endregion

namespace Common.UI
{
    public sealed class TreeNodeNumericComparer : IComparer
    {
        #region Constructors
        public TreeNodeNumericComparer()
        { }
        #endregion
        #region Public methods
        public int Compare(object x, object y)
        {
            if ((x is TreeNode) && (y is TreeNode))
            {
                return StringLogicalComparer.Compare(((TreeNode)x).Text, ((TreeNode)y).Text);
            }
            return -1;
        }
        #endregion
    }//EOC
}