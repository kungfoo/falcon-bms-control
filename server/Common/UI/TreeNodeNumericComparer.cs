#region Using statements

using System.Collections;
using System.Windows.Forms;
using Common.Strings;

// required for TreeNodeNumericComparer : IComparer only

#endregion

namespace Common.UI
{
    public sealed class TreeNodeNumericComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var node = x as TreeNode;
            if (node != null && y is TreeNode)
            {
                return StringLogicalComparer.Compare(node.Text, ((TreeNode) y).Text);
            }
            return -1;
        }
    }

//EOC
}