using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Common.UI.Wizard
{
    /// <summary>
    ///     A wizard is the control added to a form to provide a step by step functionality.
    ///     It contains <see cref="WizardPage" />s in the <see cref="Pages" /> collection, which
    ///     are containers for other controls. Only one wizard page is shown at a time in the client
    ///     are of the wizard.
    /// </summary>
    [Designer(typeof(WizardDesigner))]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Wizard))]
    public class Wizard : UserControl
    {
        protected internal Button btnBack;
        private Button btnCancel;
        protected internal Button btnNext;
        private Panel pnlButtonBright3d;
        private Panel pnlButtonDark3d;
        protected internal Panel pnlButtons;

        /// <summary>
        ///     Wizard control with designer support
        /// </summary>
        public Wizard()
        {
            //Empty collection of Pages
            Pages = new PageCollection(this);

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        /// <summary>
        ///     Gets/Sets the enabled state of the back button.
        /// </summary>
        [Category("Wizard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool BackEnabled
        {
            get => btnBack.Enabled;
            set => btnBack.Enabled = value;
        }

        /// <summary>
        ///     Gets/Sets the enabled state of the cancel button.
        /// </summary>
        [Category("Wizard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CancelEnabled
        {
            get => btnCancel.Enabled;
            set => btnCancel.Enabled = value;
        }

        /// <summary>
        ///     Gets/Sets the enabled state of the Next button.
        /// </summary>
        [Category("Wizard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NextEnabled
        {
            get => btnNext.Enabled;
            set => btnNext.Enabled = value;
        }

        /// <summary>
        ///     Alternative way of getting/Setiing  the current page by using wizardPage objects
        /// </summary>
        public WizardPage Page { get; private set; }

        /// <summary>
        ///     Returns the collection of Pages in the wizard
        /// </summary>
        [Category("Wizard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PageCollection Pages { get; }

        /// <summary>
        ///     Gets/Sets the activePage in the wizard
        /// </summary>
        [Category("Wizard")]
        internal int PageIndex
        {
            get => Pages.IndexOf(Page);
            set
            {
                //Do I have any pages?
                if (Pages.Count == 0)
                {
                    //No then show nothing
                    ActivatePage(-1);
                    return;
                }
                // Validate the page asked for
                if (value < -1 || value >= Pages.Count)
                {
                    throw new ArgumentOutOfRangeException("PageIndex",
                        value,
                        "The page index must be between 0 and " +
                        Convert.ToString(Pages.Count - 1)
                    );
                }
                //Select the new page
                ActivatePage(value);
            }
        }


        /// <summary>
        ///     Called when the cancel button is pressed, before the form is closed. Set e.Cancel to true if
        ///     you do not wish the cancel to close the wizard.
        /// </summary>
        public event CancelEventHandler CloseFromCancel;

        /// <summary>
        ///     Closes the current page after a <see cref="WizardPage.CloseFromBack" />, then moves to
        ///     the previous page and calls <see cref="WizardPage.ShowFromBack" />
        /// </summary>
        public void Back()
        {
            Debug.Assert(PageIndex < Pages.Count, "Page Index was beyond Maximum pages");
            //Can I press back
            Debug.Assert(PageIndex > 0 && PageIndex < Pages.Count, "Attempted to go back to a page that doesn't exist");
            //Tell the application that I closed a page
            var newPage = Page.OnCloseFromBack(this);

            ActivatePage(newPage);
            //Tell the application I have shown a page
            Page.OnShowFromBack(this);
        }

        /// <summary>
        ///     Moves to the page given and calls <see cref="WizardPage.ShowFromBack" />
        /// </summary>
        /// <remarks>
        ///     Does NOT call <see cref="WizardPage.CloseFromBack" /> on the current page
        /// </remarks>
        /// <param name="page"></param>
        public void BackTo(WizardPage page)
        {
            //Since we have a page to go to, then there is no need to validate most of it
            ActivatePage(page);
            //Tell the application, I have just shown a page
            Page.OnShowFromNext(this);
        }


        /// <summary>
        ///     Closes the current page after a <see cref="WizardPage.CloseFromNext" />, then moves to
        ///     the Next page and calls <see cref="WizardPage.ShowFromNext" />
        /// </summary>
        public void Next()
        {
            Debug.Assert(PageIndex >= 0, "Page Index was below 0");
            //Tell the Application I just closed a Page
            var newPage = Page.OnCloseFromNext(this);

            //Did I just press Finish instead of Next
            if (PageIndex < Pages.Count - 1
                && (Page.IsFinishPage == false || DesignMode))
            {
                //No still going
                ActivatePage(newPage);
                //Tell the application, I have just shown a page
                Page.OnShowFromNext(this);
            }
            else
            {
                Debug.Assert(PageIndex < Pages.Count, "Error I've just gone past the finish",
                    "btnNext_Click tried to go to page " + Convert.ToString(PageIndex + 1)
                    + ", but I only have " + Convert.ToString(Pages.Count));
                //yep Finish was pressed
                if (DesignMode == false)
                {
                    ParentForm.Close();
                }
            }
        }

        /// <summary>
        ///     Moves to the page given and calls <see cref="WizardPage.ShowFromNext" />
        /// </summary>
        /// <remarks>
        ///     Does NOT call <see cref="WizardPage.CloseFromNext" /> on the current page
        /// </remarks>
        /// <param name="page"></param>
        public void NextTo(WizardPage page)
        {
            //Since we have a page to go to, then there is no need to validate most of it
            ActivatePage(page);
            //Tell the application, I have just shown a page
            Page.OnShowFromNext(this);
        }


        protected internal void ActivatePage(int index)
        {
            //If the new page is invalid
            if (index < 0 || index >= Pages.Count)
            {
                btnNext.Enabled = false;
                btnBack.Enabled = false;

                return;
            }


            //Change to the new Page
            var tWizPage = Pages[index];

            //Really activate the page
            ActivatePage(tWizPage);
        }

        protected internal void ActivatePage(WizardPage page)
        {
            //Deactivate the current
            if (Page != null)
            {
                Page.Visible = false;
            }


            //Activate the new page
            Page = page;

            if (Page != null)
            {
                //Ensure that this panel displays inside the wizard
                Page.Parent = this;
                if (Contains(Page) == false)
                {
                    Container.Add(Page);
                }
                //Make it fill the space
                Page.Dock = DockStyle.Fill;
                Page.Visible = true;
                Page.BringToFront();
                Page.FocusFirstTabIndex();
            }

            //What should the back button say
            if (PageIndex > 0)
            {
                btnBack.Enabled = true;
            }
            else
            {
                btnBack.Enabled = false;
            }

            //What should the Next button say
            if (Pages.IndexOf(Page) < Pages.Count - 1
                && Page.IsFinishPage == false)
            {
                btnNext.Text = "&Next >";
                btnNext.Enabled = true;
                //Don't close the wizard :)
                btnNext.DialogResult = DialogResult.None;
            }
            else
            {
                btnNext.Text = "Fi&nish";
                //Dont allow a finish in designer
                if (DesignMode
                    && Pages.IndexOf(Page) == Pages.Count - 1)
                {
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    //If Not in design mode then allow a close
                    btnNext.DialogResult = DialogResult.OK;
                }
            }

            //Cause a refresh
            if (Page != null)
            {
                Page.Invalidate();
            }
            else
            {
                Invalidate();
            }
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Back();
        }

        private void btnBack_MouseDown(object sender, MouseEventArgs e)
        {
            if (DesignMode)
            {
                Back();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var arg = new CancelEventArgs();

            //Throw the event out to subscribers
            if (CloseFromCancel != null)
            {
                CloseFromCancel(this, arg);
            }
            //If nobody told me to cancel
            if (arg.Cancel == false)
            {
                //Then we close the form
                FindForm().Close();
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void btnNext_MouseDown(object sender, MouseEventArgs e)
        {
            if (DesignMode)
            {
                Next();
            }
        }

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlButtons = new Panel();
            btnCancel = new Button();
            btnNext = new Button();
            btnBack = new Button();
            pnlButtonBright3d = new Panel();
            pnlButtonDark3d = new Panel();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnNext);
            pnlButtons.Controls.Add(btnBack);
            pnlButtons.Controls.Add(pnlButtonBright3d);
            pnlButtons.Controls.Add(pnlButtonDark3d);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 224);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(444, 48);
            pnlButtons.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.FlatStyle = FlatStyle.System;
            btnCancel.Location = new Point(356, 12);
            btnCancel.Name = "btnCancel";
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNext.FlatStyle = FlatStyle.System;
            btnNext.Location = new Point(272, 12);
            btnNext.Name = "btnNext";
            btnNext.TabIndex = 4;
            btnNext.Text = "&Next >";
            btnNext.Click += btnNext_Click;
            btnNext.MouseDown += btnNext_MouseDown;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBack.FlatStyle = FlatStyle.System;
            btnBack.Location = new Point(196, 12);
            btnBack.Name = "btnBack";
            btnBack.TabIndex = 3;
            btnBack.Text = "< &Back";
            btnBack.Click += btnBack_Click;
            btnBack.MouseDown += btnBack_MouseDown;
            // 
            // pnlButtonBright3d
            // 
            pnlButtonBright3d.BackColor = SystemColors.ControlLightLight;
            pnlButtonBright3d.Dock = DockStyle.Top;
            pnlButtonBright3d.Location = new Point(0, 1);
            pnlButtonBright3d.Name = "pnlButtonBright3d";
            pnlButtonBright3d.Size = new Size(444, 1);
            pnlButtonBright3d.TabIndex = 1;
            // 
            // pnlButtonDark3d
            // 
            pnlButtonDark3d.BackColor = SystemColors.ControlDark;
            pnlButtonDark3d.Dock = DockStyle.Top;
            pnlButtonDark3d.Location = new Point(0, 0);
            pnlButtonDark3d.Name = "pnlButtonDark3d";
            pnlButtonDark3d.Size = new Size(444, 1);
            pnlButtonDark3d.TabIndex = 2;
            // 
            // Wizard
            // 
            Controls.Add(pnlButtons);
            Font = new Font("Tahoma", 8.25F, FontStyle.Regular,
                GraphicsUnit.Point, 0);
            Name = "Wizard";
            Size = new Size(444, 272);
            Load += Wizard_Load;
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void Wizard_Load(object sender, EventArgs e)
        {
            //Attempt to activate a page
            ActivatePage(0);

            //Can I add my self as default cancel button
            var form = FindForm();
            if (form != null && DesignMode == false)
            {
                form.CancelButton = btnCancel;
            }
        }

#if DEBUG

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
            {
                const string noPagesText = "No wizard pages inside the wizard.";


                var textSize = e.Graphics.MeasureString(noPagesText, Font);
                var layout = new RectangleF((Width - textSize.Width) / 2,
                    (pnlButtons.Top - textSize.Height) / 2,
                    textSize.Width, textSize.Height);

                var dashPen = (Pen) SystemPens.GrayText.Clone();
                dashPen.DashStyle = DashStyle.Dash;

                e.Graphics.DrawRectangle(dashPen,
                    Left + 8, Top + 8,
                    Width - 17, pnlButtons.Top - 17);

                e.Graphics.DrawString(noPagesText, Font, new SolidBrush(SystemColors.GrayText), layout);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (DesignMode)
            {
                Invalidate();
            }
        }

#endif
    }
}