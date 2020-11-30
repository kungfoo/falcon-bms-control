using System.Windows.Forms;


namespace Common.UI.UserControls
{
    /// <summary>
    /// User control that allows selecting a hotkey and (optional) modifier keys such as SHIFT-, ALT-, and CTRL-.
    /// </summary>
	public class ShortcutInput : System.Windows.Forms.UserControl
	{
		#region Public Properties
		public byte CharCode 
		{
			get { return (byte)((string)DdnChars.SelectedItem)[1]; }
			set 
			{
				foreach (object item in DdnChars.Items)
				{
					if (item.ToString() == " " + (char)value)
					{
						DdnChars.SelectedItem = item;
						return;
					}
				}
			}
		}


		public byte Win32Modifiers
		{
			get
			{
				byte toReturn = 0;
				if (CbxShift.Checked)
					toReturn += ModShift;
				if (CbxControl.Checked)
					toReturn += ModControl;
				if (CbxAlt.Checked)
					toReturn += ModAlt;
				return toReturn;
			}
		}


		public Keys Keys
		{
			get
			{
				Keys k = (Keys) CharCode;
				if (CbxShift.Checked)
					k |= Keys.Shift;
				if (CbxControl.Checked)
					k |= Keys.Control;
				if (CbxAlt.Checked)
					k |= Keys.Alt;
				return k;
			}
			set
			{
				Keys k = (Keys) value;
				if (((int)k & (int)Keys.Shift) != 0)
					Shift = true;
				if (((int)k & (int)Keys.Control) !=0)
					Control = true;
				if (((int)k & (int)Keys.Alt) !=0)
					Alt = true;

				CharCode = ShortcutInput.CharCodeFromKeys(k);
			}
		}


		public bool Shift
		{
			get { return CbxShift.Checked; }
			set { CbxShift.Checked = value; }
		}


		public bool Control
		{
			get { return CbxControl.Checked; }
			set { CbxControl.Checked = value; }
		}


		public bool Alt
		{
			get { return CbxAlt.Checked; }
			set { CbxAlt.Checked = value; }
		}


		public byte MinModifiers = 0;


		public bool IsValid
		{
			get
			{
				byte ModCount = 0;
				ModCount += (byte)((Shift) ? 1 : 0);
				ModCount += (byte)((Control) ? 1 : 0);
				ModCount += (byte)((Alt) ? 1 : 0);
                if (ModCount < MinModifiers)
					return false;
				else
					return true;
			}
		}
		#endregion


		private const byte ModAlt = 1, ModControl = 2, ModShift = 4, ModWin = 8;
		private System.Windows.Forms.CheckBox CbxShift;
		private System.Windows.Forms.CheckBox CbxControl;
		private System.Windows.Forms.CheckBox CbxAlt;
		private System.Windows.Forms.ComboBox DdnChars;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		public ShortcutInput()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			for (int i=65; i<91; i++)
				DdnChars.Items.Add(" " + (char)i);

			for (int i=48; i<58; i++)
				DdnChars.Items.Add(" " + (char)i);
			
			DdnChars.SelectedIndex = 0;
		}


		/// <summary>
		/// Calculates the Win32 Modifiers total for a Keys enum
		/// </summary>
		/// <param name="k">An instance of the Keys enumaration</param>
		/// <returns>The Win32 Modifiers total as required by RegisterHotKey</returns>
		public static byte Win32ModifiersFromKeys(Keys k)
		{
            return (byte)(k & Keys.Modifiers);
		}


		/// <summary>
		/// Calculates the character code of alphanumeric key of the Keys enum instance
		/// </summary>
		/// <param name="k">An instance of the Keys enumaration</param>
		/// <returns>The character code of the alphanumeric key</returns>
		public static byte CharCodeFromKeys(Keys k)
		{
			return (byte)(k & Keys.KeyCode);
		}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				Common.Util.DisposeObject(components);
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.CbxShift = new System.Windows.Forms.CheckBox();
            this.CbxControl = new System.Windows.Forms.CheckBox();
            this.CbxAlt = new System.Windows.Forms.CheckBox();
            this.DdnChars = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CbxShift
            // 
            this.CbxShift.Location = new System.Drawing.Point(14, 3);
            this.CbxShift.Name = "CbxShift";
            this.CbxShift.Size = new System.Drawing.Size(56, 24);
            this.CbxShift.TabIndex = 0;
            this.CbxShift.Text = "Shift";
            // 
            // CbxControl
            // 
            this.CbxControl.Location = new System.Drawing.Point(70, 3);
            this.CbxControl.Name = "CbxControl";
            this.CbxControl.Size = new System.Drawing.Size(64, 24);
            this.CbxControl.TabIndex = 1;
            this.CbxControl.Text = "Control";
            // 
            // CbxAlt
            // 
            this.CbxAlt.Location = new System.Drawing.Point(142, 3);
            this.CbxAlt.Name = "CbxAlt";
            this.CbxAlt.Size = new System.Drawing.Size(40, 24);
            this.CbxAlt.TabIndex = 2;
            this.CbxAlt.Text = "Alt";
            // 
            // DdnChars
            // 
            this.DdnChars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DdnChars.Location = new System.Drawing.Point(190, 3);
            this.DdnChars.Name = "DdnChars";
            this.DdnChars.Size = new System.Drawing.Size(40, 21);
            this.DdnChars.TabIndex = 4;
            // 
            // ShortcutInput
            // 
            this.Controls.Add(this.DdnChars);
            this.Controls.Add(this.CbxAlt);
            this.Controls.Add(this.CbxControl);
            this.Controls.Add(this.CbxShift);
            this.Name = "ShortcutInput";
            this.Size = new System.Drawing.Size(248, 29);
            this.ResumeLayout(false);

		}
		#endregion

	}
}
