using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Common.UI.Wizard
{
    /// <summary>
    ///     An inherited <see cref="InfoContainer" /> that contains a <see cref="Label" />
    ///     with the description of the page.
    /// </summary>
    public class InfoPage : InfoContainer
    {
        private Label lblDescription;

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public InfoPage()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
        }

        /// <summary>
        ///     Gets/Sets the text on the info page
        /// </summary>
        [Category("Appearance")]
        public string PageText
        {
            get => lblDescription.Text;
            set => lblDescription.Text = value;
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblDescription = new Label();
            SuspendLayout();
            // 
            // lblDescription
            // 
            lblDescription.Anchor =
                AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            lblDescription.FlatStyle = FlatStyle.System;
            lblDescription.Font = new Font("Tahoma", 8.25F, FontStyle.Regular,
                GraphicsUnit.Point, 0);
            lblDescription.Location = new Point(172, 56);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(304, 328);
            lblDescription.TabIndex = 8;
            lblDescription.Text = "This wizard enables you to...";
            // 
            // InfoPage
            // 
            Controls.Add(lblDescription);
            Name = "InfoPage";
            Controls.SetChildIndex(lblDescription, 0);
            ResumeLayout(false);
        }
    }
}