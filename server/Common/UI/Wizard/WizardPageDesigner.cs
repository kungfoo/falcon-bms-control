using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace Common.UI.Wizard
{
    /// <summary>
    ///     Summary description for WizardPageDesigner.
    /// </summary>
    public class WizardPageDesigner : ParentControlDesigner
    {
        public override DesignerVerbCollection Verbs
        {
            get
            {
                var verbs = new DesignerVerbCollection();
                verbs.Add(new DesignerVerb("Remove Page", handleRemovePage));

                return verbs;
            }
        }

        private void handleRemovePage(object sender, EventArgs e)
        {
            var page = Control as WizardPage;

            var h = (IDesignerHost) GetService(typeof(IDesignerHost));
            var c = (IComponentChangeService) GetService(typeof(IComponentChangeService));

            var dt = h.CreateTransaction("Remove Page");

            if (page.Parent is Wizard)
            {
                var wiz = page.Parent as Wizard;

                c.OnComponentChanging(wiz, null);
                //Drop from wizard
                wiz.Pages.Remove(page);
                wiz.Controls.Remove(page);
                c.OnComponentChanged(wiz, null, null, null);
                h.DestroyComponent(page);
            }
            else
            {
                c.OnComponentChanging(page, null);
                //Mark for destruction
                page.Dispose();
                c.OnComponentChanged(page, null, null, null);
            }
            dt.Commit();
        }
    }
}