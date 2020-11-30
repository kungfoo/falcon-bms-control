using System.Collections;
using System.Windows.Forms.Design;

//If you are misssing ParentControlDesigner, then don't forget that you need a reference in
//this project to System.Design

namespace Common.UI.Wizard
{
    /// <summary>
    /// </summary>
    public class InfoPageDesigner : ParentControlDesigner
    {
        /// <summary>
        ///     Prevents the grid from being drawn on the Wizard
        /// </summary>
        protected override bool DrawGrid => false;

        /// <summary>
        ///     Drops the BackgroundImage property
        /// </summary>
        /// <param name="properties">properties to remove BackGroundImage from</param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);
            if (properties.Contains("BackgroundImage"))
            {
                properties.Remove("BackgroundImage");
            }
        }
    }
}