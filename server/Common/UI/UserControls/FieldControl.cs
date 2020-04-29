using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Common.UI.UserControls
{
    internal enum Direction
    {
        Forward,
        Reverse
    }

    internal enum Selection
    {
        None,
        All
    }

    internal enum Action
    {
        None,
        Trim,
        Home,
        End
    }

    internal class FieldControl : TextBox
    {
        public const byte MINIMUM_VALUE = 0;
        public const byte MAXIMUM_VALUE = 255;

        private const TextFormatFlags TEXT_FORMAT_FLAGS = TextFormatFlags.HorizontalCenter |
                                                          TextFormatFlags.SingleLine | TextFormatFlags.NoPadding;

        private byte _rangeLower; // = MinimumValue;  // this is removed for FxCop approval
        private byte _rangeUpper = MAXIMUM_VALUE;

        public FieldControl()
        {
            BorderStyle = BorderStyle.None;
            MaxLength = 3;
            Size = MinimumSize;
            TabStop = false;
            TextAlign = HorizontalAlignment.Center;
        }

        public bool Blank => TextLength == 0;

        public int FieldIndex { get; set; } = -1;

        public override Size MinimumSize
        {
            get
            {
                Size minimumSize;
                using (var g = Graphics.FromHwnd(Handle))
                {
                    minimumSize = TextRenderer.MeasureText(g,
                        "222", Font, Size,
                        TEXT_FORMAT_FLAGS);
                }
                return minimumSize;
            }
        }

        public byte RangeLower
        {
            get => _rangeLower;
            set
            {
                if (value < MINIMUM_VALUE)
                {
                    _rangeLower = MINIMUM_VALUE;
                }
                else if (value > _rangeUpper)
                {
                    _rangeLower = _rangeUpper;
                }
                else
                {
                    _rangeLower = value;
                }

                if (Value < _rangeLower)
                {
                    Text = _rangeLower.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public byte RangeUpper
        {
            get => _rangeUpper;
            set
            {
                if (value < _rangeLower)
                {
                    _rangeUpper = _rangeLower;
                }
                else if (value > MAXIMUM_VALUE)
                {
                    _rangeUpper = MAXIMUM_VALUE;
                }
                else
                {
                    _rangeUpper = value;
                }

                if (Value > _rangeUpper)
                {
                    Text = _rangeUpper.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public byte Value
        {
            get
            {
                byte result;

                if (!byte.TryParse(Text, out result))
                {
                    result = RangeLower;
                }

                return result;
            }
        }

        public event EventHandler<CedeFocusEventArgs> CedeFocusEvent;
        public event EventHandler<TextChangedEventArgs> TextChangedEvent;

        public void TakeFocus(Action action)
        {
            Focus();

            switch (action)
            {
                case Action.Trim:

                    if (TextLength > 0)
                    {
                        var newLength = TextLength - 1;
                        Text = Text.Substring(0, newLength);
                    }

                    SelectionStart = TextLength;

                    return;

                case Action.Home:

                    SelectionStart = 0;
                    SelectionLength = 0;

                    return;

                case Action.End:

                    SelectionStart = TextLength;

                    return;
            }
        }

        public void TakeFocus(Direction direction, Selection selection)
        {
            Focus();

            if (selection == Selection.All)
            {
                SelectionStart = 0;
                SelectionLength = TextLength;
            }
            else
            {
                SelectionStart = direction == Direction.Forward ? 0 : TextLength;
            }
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Home:
                    SendCedeFocusEvent(Action.Home);
                    return;

                case Keys.End:
                    SendCedeFocusEvent(Action.End);
                    return;
            }

            if (IsCedeFocusKey(e))
            {
                SendCedeFocusEvent(Direction.Forward, Selection.All);
                e.SuppressKeyPress = true;
                return;
            }
            if (IsForwardKey(e))
            {
                if (e.Control)
                {
                    SendCedeFocusEvent(Direction.Forward, Selection.All);
                    return;
                }
                if (SelectionLength == 0 && SelectionStart == TextLength)
                {
                    SendCedeFocusEvent(Direction.Forward, Selection.None);
                }
            }
            else if (IsReverseKey(e))
            {
                if (e.Control)
                {
                    SendCedeFocusEvent(Direction.Reverse, Selection.All);
                    return;
                }
                if (SelectionLength == 0 && SelectionStart == 0)
                {
                    SendCedeFocusEvent(Direction.Reverse, Selection.None);
                }
            }
            else if (IsBackspaceKey(e))
            {
                HandleBackspaceKey(e);
            }
            else if (!IsNumericKey(e) &&
                     !IsEditKey(e))
            {
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            BackColor = Parent.BackColor;
        }

        protected override void OnParentForeColorChanged(EventArgs e)
        {
            base.OnParentForeColorChanged(e);
            ForeColor = Parent.ForeColor;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Size = MinimumSize;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (!Blank)
            {
                int value;
                if (!int.TryParse(Text, out value))
                {
                    Text = string.Empty;
                }
                else
                {
                    if (value > RangeUpper)
                    {
                        Text = RangeUpper.ToString(CultureInfo.InvariantCulture);
                        SelectionStart = 0;
                    }
                    else if (TextLength == MaxLength && value < RangeLower)
                    {
                        Text = RangeLower.ToString(CultureInfo.InvariantCulture);
                        SelectionStart = 0;
                    }
                    else
                    {
                        var originalLength = TextLength;
                        var newSelectionStart = SelectionStart;

                        Text = value.ToString(CultureInfo.InvariantCulture);

                        if (TextLength < originalLength)
                        {
                            newSelectionStart -= originalLength - TextLength;
                            SelectionStart = System.Math.Max(0, newSelectionStart);
                        }
                    }
                }
            }

            if (null != TextChangedEvent)
            {
                var args = new TextChangedEventArgs {FieldIndex = FieldIndex, Text = Text};
                TextChangedEvent(this, args);
            }

            if (TextLength == MaxLength && Focused && SelectionStart == TextLength)
            {
                SendCedeFocusEvent(Direction.Forward, Selection.All);
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            if (!Blank)
            {
                if (Value < RangeLower)
                {
                    Text = RangeLower.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x007b: // WM_CONTEXTMENU
                    return;
            }

            base.WndProc(ref m);
        }

        private void HandleBackspaceKey(KeyEventArgs e)
        {
            if (!ReadOnly && (TextLength == 0 || SelectionStart == 0 && SelectionLength == 0))
            {
                SendCedeFocusEvent(Action.Trim);
                e.SuppressKeyPress = true;
            }
        }

        private static bool IsBackspaceKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                return true;
            }

            return false;
        }

        private bool IsCedeFocusKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod ||
                e.KeyCode == Keys.Decimal ||
                e.KeyCode == Keys.Space)
            {
                if (TextLength != 0 && SelectionLength == 0 && SelectionStart != 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsEditKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back ||
                e.KeyCode == Keys.Delete)
            {
                return true;
            }
            if (e.Modifiers == Keys.Control &&
                (e.KeyCode == Keys.C ||
                 e.KeyCode == Keys.V ||
                 e.KeyCode == Keys.X))
            {
                return true;
            }

            return false;
        }

        private static bool IsForwardKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Down)
            {
                return true;
            }

            return false;
        }

        private static bool IsNumericKey(KeyEventArgs e)
        {
            if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
            {
                if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsReverseKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Up)
            {
                return true;
            }

            return false;
        }

        private void SendCedeFocusEvent(Action action)
        {
            if (null != CedeFocusEvent)
            {
                var args = new CedeFocusEventArgs {FieldIndex = FieldIndex, Action = action};
                CedeFocusEvent(this, args);
            }
        }

        private void SendCedeFocusEvent(Direction direction, Selection selection)
        {
            if (null != CedeFocusEvent)
            {
                var args = new CedeFocusEventArgs
                {
                    FieldIndex = FieldIndex,
                    Action = Action.None,
                    Direction = direction,
                    Selection = selection
                };
                CedeFocusEvent(this, args);
            }
        }
    }

    internal class CedeFocusEventArgs : EventArgs
    {
        public Action Action { get; set; }

        public Direction Direction { get; set; }
        public int FieldIndex { get; set; }

        public Selection Selection { get; set; }
    }

    internal class TextChangedEventArgs : EventArgs
    {
        public int FieldIndex { get; set; }

        public string Text { get; set; }
    }
}