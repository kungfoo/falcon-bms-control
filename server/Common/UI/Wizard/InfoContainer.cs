using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Common.UI.Wizard
{
    /// <summary>
    ///     Summary description for UserControl1.
    /// </summary>
    [Designer(typeof(InfoContainerDesigner))]
    public class InfoContainer : UserControl
    {
        private Label lblTitle;
        private PictureBox picImage;

        /// <summary>
        /// </summary>
        public InfoContainer()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }


        /// <summary>
        ///     Gets/Sets the Icon
        /// </summary>
        [Category("Appearance")]
        public Image Image
        {
            get => picImage.Image;
            set => picImage.Image = value;
        }

        /// <summary>
        ///     Get/Set the title for the info page
        /// </summary>
        [Category("Appearance")]
        public string PageTitle
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

        private void InfoContainer_Load(object sender, EventArgs e)
        {
            //Handle really irating resize that doesn't take account of Anchor
            lblTitle.Left = picImage.Width + 8;
            lblTitle.Width = Width - 4 - lblTitle.Left;
        }

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new ResourceManager(typeof(InfoContainer));
            picImage = new PictureBox();
            lblTitle = new Label();
            SuspendLayout();
            // 
            // picImage
            // 
            picImage.Dock = DockStyle.Left;
            picImage.Image = (Image) resources.GetObject("picImage.Image");
            picImage.Location = new Point(0, 0);
            picImage.Name = "picImage";
            picImage.Size = new Size(164, 388);
            picImage.TabIndex = 0;
            picImage.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left
                              | AnchorStyles.Right;
            lblTitle.FlatStyle = FlatStyle.System;
            lblTitle.Font = new Font("Tahoma", 12F, FontStyle.Bold,
                GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(172, 4);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(304, 48);
            lblTitle.TabIndex = 7;
            lblTitle.Text = "Welcome to the / Completing the <Title> Wizard";
            // 
            // InfoContainer
            // 
            BackColor = Color.White;
            Controls.Add(lblTitle);
            Controls.Add(picImage);
            Name = "InfoContainer";
            Size = new Size(480, 388);
            Load += InfoContainer_Load;
            ResumeLayout(false);
        }
    }
}