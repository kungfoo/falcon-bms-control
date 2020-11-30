using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Common.UI.Wizard
{
    /// <summary>
    ///     Summary description for WizardHeader.
    /// </summary>
    [Designer(typeof(HeaderDesigner))]
    public class Header : UserControl
    {
        private Label lblDescription;
        private Label lblTitle;
        private PictureBox picIcon;
        private Panel pnl3dBright;
        private Panel pnl3dDark;
        private Panel pnlDockPadding;

        /// <summary>
        ///     Constructor for Header
        /// </summary>
        public Header()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        /// <summary>
        ///     Gets/Sets the
        /// </summary>
        [Category("Appearance")]
        public string Description
        {
            get => lblDescription.Text;
            set => lblDescription.Text = value;
        }

        /// <summary>
        ///     Gets/Sets the Icon
        /// </summary>
        [Category("Appearance")]
        public Image Image
        {
            get => picIcon.Image;
            set
            {
                picIcon.Image = value;
                ResizeImageAndText();
            }
        }

        /// <summary>
        ///     Get/Set the title for the wizard page
        /// </summary>
        [Category("Appearance")]
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void Header_SizeChanged(object sender, EventArgs e)
        {
            ResizeImageAndText();
        }

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new ResourceManager(typeof(Header));
            pnlDockPadding = new Panel();
            lblDescription = new Label();
            lblTitle = new Label();
            picIcon = new PictureBox();
            pnl3dDark = new Panel();
            pnl3dBright = new Panel();
            pnlDockPadding.SuspendLayout();
            SuspendLayout();
            // 
            // pnlDockPadding
            // 
            pnlDockPadding.BackColor = SystemColors.Window;
            pnlDockPadding.Controls.Add(lblDescription);
            pnlDockPadding.Controls.Add(lblTitle);
            pnlDockPadding.Controls.Add(picIcon);
            pnlDockPadding.Dock = DockStyle.Fill;
            pnlDockPadding.DockPadding.Bottom = 4;
            pnlDockPadding.DockPadding.Left = 8;
            pnlDockPadding.DockPadding.Right = 4;
            pnlDockPadding.DockPadding.Top = 6;
            pnlDockPadding.Font = new Font("Tahoma", 8.25F, FontStyle.Regular,
                GraphicsUnit.Point, 0);
            pnlDockPadding.Location = new Point(0, 0);
            pnlDockPadding.Name = "pnlDockPadding";
            pnlDockPadding.Size = new Size(324, 64);
            pnlDockPadding.TabIndex = 6;
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.FlatStyle = FlatStyle.System;
            lblDescription.Location = new Point(8, 22);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(260, 38);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Description";
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.FlatStyle = FlatStyle.System;
            lblTitle.Font = new Font("Tahoma", 8.25F, FontStyle.Bold,
                GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(8, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(260, 16);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Title";
            // 
            // picIcon
            // 
            picIcon.Dock = DockStyle.Right;
            picIcon.Image = (Image) resources.GetObject("picIcon.Image");
            picIcon.Location = new Point(268, 6);
            picIcon.Name = "picIcon";
            picIcon.Size = new Size(52, 54);
            picIcon.TabIndex = 3;
            picIcon.TabStop = false;
            // 
            // pnl3dDark
            // 
            pnl3dDark.BackColor = SystemColors.ControlDark;
            pnl3dDark.Dock = DockStyle.Bottom;
            pnl3dDark.Location = new Point(0, 62);
            pnl3dDark.Name = "pnl3dDark";
            pnl3dDark.Size = new Size(324, 1);
            pnl3dDark.TabIndex = 7;
            // 
            // pnl3dBright
            // 
            pnl3dBright.BackColor = Color.White;
            pnl3dBright.Dock = DockStyle.Bottom;
            pnl3dBright.Location = new Point(0, 63);
            pnl3dBright.Name = "pnl3dBright";
            pnl3dBright.Size = new Size(324, 1);
            pnl3dBright.TabIndex = 8;
            // 
            // Header
            // 
            BackColor = SystemColors.Control;
            CausesValidation = false;
            Controls.Add(pnl3dDark);
            Controls.Add(pnl3dBright);
            Controls.Add(pnlDockPadding);
            Name = "Header";
            Size = new Size(324, 64);
            SizeChanged += Header_SizeChanged;
            pnlDockPadding.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void ResizeImageAndText()
        {
            //Resize image 
            picIcon.Size = picIcon.Image.Size;
            //Relocate image according to its size
            picIcon.Top = (Height - picIcon.Height) / 2;
            picIcon.Left = Width - picIcon.Width - 8;
            //Fit text around picture
            lblTitle.Width = picIcon.Left - lblTitle.Left;
            lblDescription.Width = picIcon.Left - lblDescription.Left;
        }
    }
}