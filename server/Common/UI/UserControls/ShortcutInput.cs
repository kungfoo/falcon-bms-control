using System.Drawing;
using System.Windows.Forms;

namespace Common.UI.UserControls
{
    /// <summary>
    ///     User control that allows selecting a hotkey and (optional) modifier keys such as SHIFT-, ALT-, and CTRL-.
    /// </summary>
    public class ShortcutInput : UserControl
    {
        private const byte MOD_ALT = 1, MOD_CONTROL = 2, MOD_SHIFT = 4;

        private CheckBox _cbxAlt;
        private CheckBox _cbxControl;
        private CheckBox _cbxShift;
        private ComboBox _ddnChars;

        public byte MinModifiers;


        public ShortcutInput()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            for (var i = 65; i < 91; i++)
                _ddnChars.Items.Add(" " + (char) i);

            for (var i = 48; i < 58; i++)
                _ddnChars.Items.Add(" " + (char) i);

            _ddnChars.SelectedIndex = 0;
        }


        public bool Alt
        {
            get => _cbxAlt.Checked;
            set => _cbxAlt.Checked = value;
        }

        public byte CharCode
        {
            get { return (byte) ((string) _ddnChars.SelectedItem)[1]; }
            set
            {
                foreach (var item in _ddnChars.Items)
                    if (item.ToString() == " " + (char) value)
                    {
                        _ddnChars.SelectedItem = item;
                        return;
                    }
            }
        }


        public bool Control
        {
            get => _cbxControl.Checked;
            set => _cbxControl.Checked = value;
        }


        public bool IsValid
        {
            get
            {
                byte modCount = 0;
                modCount += (byte) (Shift ? 1 : 0);
                modCount += (byte) (Control ? 1 : 0);
                modCount += (byte) (Alt ? 1 : 0);
                if (modCount < MinModifiers)
                {
                    return false;
                }
                return true;
            }
        }


        public Keys Keys
        {
            get
            {
                var k = (Keys) CharCode;
                if (_cbxShift.Checked)
                {
                    k |= Keys.Shift;
                }
                if (_cbxControl.Checked)
                {
                    k |= Keys.Control;
                }
                if (_cbxAlt.Checked)
                {
                    k |= Keys.Alt;
                }
                return k;
            }
            set
            {
                var k = value;
                if (((int) k & (int) Keys.Shift) != 0)
                {
                    Shift = true;
                }
                if (((int) k & (int) Keys.Control) != 0)
                {
                    Control = true;
                }
                if (((int) k & (int) Keys.Alt) != 0)
                {
                    Alt = true;
                }

                CharCode = CharCodeFromKeys(k);
            }
        }


        public bool Shift
        {
            get => _cbxShift.Checked;
            set => _cbxShift.Checked = value;
        }


        public byte Win32Modifiers
        {
            get
            {
                byte toReturn = 0;
                if (_cbxShift.Checked)
                {
                    toReturn += MOD_SHIFT;
                }
                if (_cbxControl.Checked)
                {
                    toReturn += MOD_CONTROL;
                }
                if (_cbxAlt.Checked)
                {
                    toReturn += MOD_ALT;
                }
                return toReturn;
            }
        }


        /// <summary>
        ///     Calculates the character code of alphanumeric key of the Keys enum instance
        /// </summary>
        /// <param name="k">An instance of the Keys enumaration</param>
        /// <returns>The character code of the alphanumeric key</returns>
        public static byte CharCodeFromKeys(Keys k)
        {
            return (byte) (k & Keys.KeyCode);
        }


        /// <summary>
        ///     Calculates the Win32 Modifiers total for a Keys enum
        /// </summary>
        /// <param name="k">An instance of the Keys enumaration</param>
        /// <returns>The Win32 Modifiers total as required by RegisterHotKey</returns>
        public static byte Win32ModifiersFromKeys(Keys k)
        {
            return (byte) (k & Keys.Modifiers);
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
            _cbxShift = new CheckBox();
            _cbxControl = new CheckBox();
            _cbxAlt = new CheckBox();
            _ddnChars = new ComboBox();
            SuspendLayout();
            // 
            // _cbxShift
            // 
            _cbxShift.Location = new Point(14, 3);
            _cbxShift.Name = "_cbxShift";
            _cbxShift.Size = new Size(61, 24);
            _cbxShift.TabIndex = 0;
            _cbxShift.Text = "Shift";
            // 
            // _cbxControl
            // 
            _cbxControl.Location = new Point(93, 3);
            _cbxControl.Name = "_cbxControl";
            _cbxControl.Size = new Size(88, 24);
            _cbxControl.TabIndex = 1;
            _cbxControl.Text = "Control";
            // 
            // _cbxAlt
            // 
            _cbxAlt.Location = new Point(187, 3);
            _cbxAlt.Name = "_cbxAlt";
            _cbxAlt.Size = new Size(60, 24);
            _cbxAlt.TabIndex = 2;
            _cbxAlt.Text = "Alt";
            // 
            // _ddnChars
            // 
            _ddnChars.DropDownStyle = ComboBoxStyle.DropDownList;
            _ddnChars.Location = new Point(253, 3);
            _ddnChars.Name = "_ddnChars";
            _ddnChars.Size = new Size(40, 24);
            _ddnChars.TabIndex = 4;
            // 
            // ShortcutInput
            // 
            Controls.Add(_ddnChars);
            Controls.Add(_cbxAlt);
            Controls.Add(_cbxControl);
            Controls.Add(_cbxShift);
            Name = "ShortcutInput";
            Size = new Size(360, 29);
            ResumeLayout(false);
        }
    }
}