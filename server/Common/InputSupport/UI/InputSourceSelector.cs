using System;
using System.Windows.Forms;
using Common.InputSupport.DirectInput;
using Common.Win32;

namespace Common.InputSupport.UI
{
    public sealed partial class InputSourceSelector
    {
        private readonly Mediator.PhysicalControlStateChangedEventHandler _mediatorHandler;
        private Mediator _mediator;

        public InputSourceSelector()
        {
            InitializeComponent();
            _mediatorHandler =
                MediatorPhysicalControlStateChanged;
            PreviewKeyDown += PreviewKeyDownHandler;
            KeyDown += Form_KeyDown;
            SelectedControl = new InputControlSelection();
        }

        public Mediator Mediator
        {
            get => _mediator;
            set
            {
                _mediator = value;
                if (_mediator != null)
                {
                    _mediator.PhysicalControlStateChanged += _mediatorHandler;
                    _mediator.RaiseEvents = true;
                }
            }
        }

        public string PromptText { get; set; }
        public InputControlSelection SelectedControl { get; set; }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (
                (keyData & Keys.KeyCode & Keys.Tab) == Keys.Tab
                ||
                (keyData & Keys.KeyCode & Keys.Up) == Keys.Up
                ||
                (keyData & Keys.KeyCode & Keys.Down) == Keys.Down
            )
            {
                rdoKeystroke.Checked = true;
                txtKeystroke.Select();
                UpdateKeyAssignmentData(keyData);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedControl = null;
            Close();
        }

        private InputControlSelection BuildInputControlSelection()
        {
            var toReturn = new InputControlSelection();
            if (rdoJoystick.Checked)
            {
                var device = (DIPhysicalDeviceInfo) cbJoysticks.SelectedItem;
                var control = (DIPhysicalControlInfo) cboJoystickControl.SelectedItem;
                switch (control.ControlType)
                {
                    case ControlType.Axis:
                        toReturn.ControlType = ControlType.Axis;
                        break;
                    case ControlType.Button:
                        toReturn.ControlType = ControlType.Button;
                        break;
                    case ControlType.Pov:
                        toReturn.ControlType = ControlType.Pov;
                        if (rdoPovUp.Checked)
                        {
                            toReturn.PovDirection = PovDirections.Up;
                        }
                        else if (rdoPovUpLeft.Checked)
                        {
                            toReturn.PovDirection = PovDirections.UpLeft;
                        }
                        else if (rdoPovUpRight.Checked)
                        {
                            toReturn.PovDirection = PovDirections.UpRight;
                        }
                        else if (rdoPovRight.Checked)
                        {
                            toReturn.PovDirection = PovDirections.Right;
                        }
                        else if (rdoPovLeft.Checked)
                        {
                            toReturn.PovDirection = PovDirections.Left;
                        }
                        else if (rdoPovDownLeft.Checked)
                        {
                            toReturn.PovDirection = PovDirections.DownLeft;
                        }
                        else if (rdoPovDownRight.Checked)
                        {
                            toReturn.PovDirection = PovDirections.DownRight;
                        }
                        else if (rdoPovDown.Checked)
                        {
                            toReturn.PovDirection = PovDirections.Down;
                        }
                        break;
                }
                toReturn.DirectInputDevice = device;
                toReturn.DirectInputControl = control;
            }
            else if (rdoKeystroke.Checked)
            {
                toReturn.ControlType = ControlType.Key;
                toReturn.Keys = (Keys) Enum.Parse(typeof(Keys), txtKeystroke.Text);
            }
            return toReturn;
        }

        private void cbJoysticks_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedControl.DirectInputDevice = (DIPhysicalDeviceInfo) cbJoysticks.SelectedItem;
            PopulateJoystickControlsComboBox();
            SelectCurrentJoystickControl();
        }

        private void cboJoystickControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedControl.DirectInputControl = (DIPhysicalControlInfo) cboJoystickControl.SelectedItem;
            EnableDisableControls();
        }

        private void ClearAllPovRadioButtons()
        {
            rdoPovUp.Checked = false;
            rdoPovUpLeft.Checked = false;
            rdoPovUpRight.Checked = false;
            rdoPovRight.Checked = false;
            rdoPovDownLeft.Checked = false;
            rdoPovDownRight.Checked = false;
            rdoPovDown.Checked = false;
            rdoPovLeft.Checked = false;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            var valid = ValidateSelections();
            if (valid)
            {
                StoreSelectedControlInfo();
                Close();
            }
        }

        private void EnableDisableControls()
        {
            rdoJoystick.Enabled = cbJoysticks.Items.Count != 0;

            if (rdoKeystroke.Checked)
            {
                gbPovDirections.Enabled = false;
                txtKeystroke.Enabled = true;
                lblDeviceName.Enabled = false;
                cbJoysticks.Enabled = false;
                lblJoystickControl.Enabled = false;
                cboJoystickControl.Enabled = false;
            }
            else if (rdoJoystick.Checked)
            {
                lblDeviceName.Enabled = true;
                cbJoysticks.Enabled = true;
                lblJoystickControl.Enabled = true;
                cboJoystickControl.Enabled = true;
                txtKeystroke.Enabled = false;

                SelectCurrentJoystick();
                SelectCurrentJoystickControl();

                var control = (DIPhysicalControlInfo) cboJoystickControl.SelectedItem;
                if (control != null)
                {
                    switch (control.ControlType)
                    {
                        case ControlType.Axis:
                            gbPovDirections.Enabled = false;
                            break;
                        case ControlType.Button:
                            gbPovDirections.Enabled = false;
                            break;
                        case ControlType.Pov:
                            gbPovDirections.Enabled = true;
                            break;
                    }
                }
                else
                {
                    cboJoystickControl.SelectedIndex = 0;
                }
            }
            else if (rdoNotAssigned.Checked)
            {
                gbPovDirections.Enabled = false;
                txtKeystroke.Enabled = false;
                lblDeviceName.Enabled = false;
                cbJoysticks.Enabled = false;
                lblJoystickControl.Enabled = false;
                cboJoystickControl.Enabled = false;
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_mediator != null)
            {
                _mediator.PhysicalControlStateChanged -= _mediatorHandler;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (!ContainsFocus) return;
            if (
                (e.KeyCode & Keys.KeyCode & Keys.Tab) == Keys.Tab
                ||
                (e.KeyCode & Keys.KeyCode & Keys.Up) == Keys.Up
                ||
                (e.KeyCode & Keys.KeyCode & Keys.Down) == Keys.Down
            )
            {
                e.Handled |= rdoKeystroke.Checked;
            }
            else
            {
                rdoKeystroke.Checked = true;
                txtKeystroke.Select();
            }
            UpdateKeyAssignmentData(e.KeyCode);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            txtHelpText.Lines = new[]
            {
                @"Press and release the desired " +
                @"keystroke/combination, or press and release " +
                @"the desired joystick input, to assign it to this control."
            };
            PopulateJoysticksComboBox();
            PopulateJoystickControlsComboBox();
            LoadSelectedControlInfo();
            EnableDisableControls();
        }

        private static string GetKeyName(Keys key)
        {
            return key.ToString();
        }

        private void LoadSelectedControlInfo()
        {
            var thisControlSelection = SelectedControl;
            lblPromptText.Text = PromptText;
            switch (thisControlSelection.ControlType)
            {
                case ControlType.Key:
                    txtKeystroke.Text = thisControlSelection.Keys.ToString();
                    rdoKeystroke.Checked = true;
                    break;
                case ControlType.Button:
                    cbJoysticks.SelectedItem = thisControlSelection.DirectInputDevice;
                    cboJoystickControl.SelectedItem = thisControlSelection.DirectInputControl;
                    rdoJoystick.Checked = true;
                    break;
                case ControlType.Pov:
                    ClearAllPovRadioButtons();
                    cbJoysticks.SelectedItem = thisControlSelection.DirectInputDevice;
                    cboJoystickControl.SelectedItem = thisControlSelection.DirectInputControl;
                    switch (thisControlSelection.PovDirection)
                    {
                        case PovDirections.Up:
                            rdoPovUp.Checked = true;
                            break;
                        case PovDirections.UpRight:
                            rdoPovUpRight.Checked = true;
                            break;
                        case PovDirections.Right:
                            rdoPovRight.Checked = true;
                            break;
                        case PovDirections.DownRight:
                            rdoPovDownRight.Checked = true;
                            break;
                        case PovDirections.Down:
                            rdoPovDown.Checked = true;
                            break;
                        case PovDirections.DownLeft:
                            rdoPovDownLeft.Checked = true;
                            break;
                        case PovDirections.Left:
                            rdoPovLeft.Checked = true;
                            break;
                        case PovDirections.UpLeft:
                            rdoPovUpLeft.Checked = true;
                            break;
                    }
                    rdoJoystick.Checked = true;
                    break;
                case ControlType.Axis:
                    cbJoysticks.SelectedItem = thisControlSelection.DirectInputDevice;
                    cboJoystickControl.SelectedItem = thisControlSelection.DirectInputControl;
                    rdoJoystick.Checked = true;
                    break;
                case ControlType.Unknown:
                    rdoNotAssigned.Checked = true;
                    break;
            }
            EnableDisableControls();
        }

        private void MediatorPhysicalControlStateChanged(object sender, PhysicalControlStateChangedEventArgs e)
        {
            if (e.Control.ControlType == ControlType.Button || e.Control.ControlType == ControlType.Pov)
            {
                rdoJoystick.Checked = true;
                var control = (DIPhysicalControlInfo) e.Control;
                var device = (DIPhysicalDeviceInfo) control.Parent;
                cbJoysticks.SelectedItem = device;
                cboJoystickControl.SelectedItem = control;
                if (control.ControlType != ControlType.Pov) return;
                var currentDegrees = e.CurrentState / 100.0f;
                if (e.CurrentState == -1) currentDegrees = -1;
                /*  POV directions in degrees
                              0
                        337.5  22.5   
                       315         45
                     292.5           67.5
                    270                90
                     247.5           112.5
                      225          135
                        202.5  157.5
                            180
                     */
                if (currentDegrees > 337.5 && currentDegrees <= 360 ||
                    currentDegrees >= 0 && currentDegrees <= 22.5)
                {
                    rdoPovUp.Checked = true;
                }
                else if (currentDegrees > 22.5 && currentDegrees <= 67.5)
                {
                    rdoPovUpRight.Checked = true;
                }
                else if (currentDegrees > 67.5 && currentDegrees <= 112.5)
                {
                    rdoPovRight.Checked = true;
                }
                else if (currentDegrees > 112.5 && currentDegrees <= 157.5)
                {
                    rdoPovDownRight.Checked = true;
                }
                else if (currentDegrees > 157.5 && currentDegrees <= 202.5)
                {
                    rdoPovDown.Checked = true;
                }
                else if (currentDegrees > 202.5 && currentDegrees <= 247.5)
                {
                    rdoPovDownLeft.Checked = true;
                }
                else if (currentDegrees > 247.5 && currentDegrees <= 292.5)
                {
                    rdoPovLeft.Checked = true;
                }
                else rdoPovUpLeft.Checked |= (currentDegrees > 292.5 && currentDegrees <= 337.5);
            }
        }

        private void PopulateJoystickControlsComboBox()
        {
            var thisDevice = SelectedControl.DirectInputDevice;
            cboJoystickControl.Items.Clear();
            if (thisDevice?.Controls != null)
            {
                foreach (var control in thisDevice.Controls)
                    if (control.ControlType == ControlType.Button || control.ControlType == ControlType.Pov)
                    {
                        cboJoystickControl.Items.Add(control);
                    }
            }
            cboJoystickControl.DisplayMember = "Alias";
        }

        private void PopulateJoysticksComboBox()
        {
            cbJoysticks.Items.Clear();
            if (Mediator != null)
            {
                foreach (var pair in Mediator.DeviceMonitors)
                    cbJoysticks.Items.Add(pair.Value.DeviceInfo);
            }
            cbJoysticks.DisplayMember = "Alias";
        }

        private void PreviewKeyDownHandler(object sender, PreviewKeyDownEventArgs e)
        {
            if (
                (e.KeyCode & Keys.KeyCode & Keys.Tab) == Keys.Tab
                ||
                (e.KeyCode & Keys.KeyCode & Keys.Up) == Keys.Up
                ||
                (e.KeyCode & Keys.KeyCode & Keys.Down) == Keys.Down
            )
            {
                e.IsInputKey |= rdoKeystroke.Checked;
            }
        }

        private void rdoJoystick_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableControls();
        }

        private void rdoKeystroke_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableControls();
        }

        private void SelectCurrentJoystick()
        {
            var thisDevice = SelectedControl.DirectInputDevice;
            if (thisDevice != null)
            {
                cbJoysticks.SelectedItem = thisDevice;
                if (cbJoysticks.SelectedIndex < 0) cbJoysticks.SelectedIndex = 0;
            }
            else
            {
                cbJoysticks.SelectedIndex = 0;
            }
        }

        private void SelectCurrentJoystickControl()
        {
            var curControl = SelectedControl.DirectInputControl;
            if (curControl != null)
            {
                cboJoystickControl.SelectedItem = curControl;
                if (cboJoystickControl.SelectedIndex < 0) cboJoystickControl.SelectedIndex = 0;
            }
            else
            {
                cboJoystickControl.SelectedIndex = 0;
            }
        }

        private void StoreSelectedControlInfo()
        {
            SelectedControl = BuildInputControlSelection();
        }

        private void UpdateKeyAssignmentData(Keys key)
        {
            if ((NativeMethods.GetKeyState(NativeMethods.VK_SHIFT) & 0x8000) != 0)
            {
                key |= Keys.Shift;
                //SHIFT is pressed
            }
            if ((NativeMethods.GetKeyState(NativeMethods.VK_CONTROL) & 0x8000) != 0)
            {
                key |= Keys.Control;
                //CONTROL is pressed
            }
            if ((NativeMethods.GetKeyState(NativeMethods.VK_MENU) & 0x8000) != 0)
            {
                key |= Keys.Alt;
                //ALT is pressed
            }

            txtKeystroke.Text = GetKeyName(key);
        }

        private bool ValidateSelections()
        {
            var valid = true;
            if (rdoJoystick.Checked)
            {
                var control = (DIPhysicalControlInfo) cboJoystickControl.SelectedItem;
                if (control.ControlType == ControlType.Pov)
                {
                    if (!rdoPovDown.Checked && !rdoPovDownLeft.Checked && !rdoPovDownRight.Checked &&
                        !rdoPovLeft.Checked && !rdoPovRight.Checked && !rdoPovUp.Checked && !rdoPovUpLeft.Checked &&
                        !rdoPovUpRight.Checked)
                    {
                        MessageBox.Show(this,
                            @"A point-of-view (POV) hat control is selected, but no position on the hat has been selected.
Please choose a hat position or change the assigned input control.",
                            Application.ProductName, MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        valid = false;
                    }
                }
            }
            else if (rdoKeystroke.Checked && (string.IsNullOrEmpty(txtKeystroke.Text) || txtKeystroke.Text == @"None"))
            {
                rdoNotAssigned.Checked = true;
            }
            SelectedControl = BuildInputControlSelection();
            return valid;
        }
    }
}