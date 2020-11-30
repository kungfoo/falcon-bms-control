using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Win32;

namespace Common.UI.UserControls
{
    [Designer(typeof(IPAddressControlDesigner))]
    public class IPAddressControl : UserControl
    {
        public const int FIELD_COUNT = 4;

        private readonly DotControl[] _dotControls = new DotControl[FIELD_COUNT - 1];
        private readonly FieldControl[] _fieldControls = new FieldControl[FIELD_COUNT];
        private readonly TextBox _referenceTextBox = new TextBox();
        private bool _autoHeight = true;
        private bool _backColorChanged;
        private BorderStyle _borderStyle = BorderStyle.Fixed3D;
        private Size _fixed3DOffset = new Size(3, 3);
        private Size _fixedSingleOffset = new Size(2, 2);
        private bool _focused;
        private bool _hasMouse;
        private bool _readOnly;

        public IPAddressControl()
        {
            BackColor = SystemColors.Window;

            ResetBackColorChanged();

            for (var index = 0; index < _fieldControls.Length; ++index)
            {
                _fieldControls[index] = new FieldControl();

                _fieldControls[index].CreateControl();

                _fieldControls[index].FieldIndex = index;
                _fieldControls[index].Name = "FieldControl" + index.ToString(CultureInfo.InvariantCulture);
                _fieldControls[index].Parent = this;

                _fieldControls[index].CedeFocusEvent += OnCedeFocus;
                _fieldControls[index].Click += OnSubControlClicked;
                _fieldControls[index].DoubleClick += OnSubControlDoubleClicked;
                _fieldControls[index].GotFocus += OnFieldGotFocus;
                _fieldControls[index].KeyPress += OnFieldKeyPressed;
                _fieldControls[index].LostFocus += OnFieldLostFocus;
                _fieldControls[index].MouseClick += OnSubControlMouseClicked;
                _fieldControls[index].MouseDoubleClick += OnSubControlMouseDoubleClicked;
                _fieldControls[index].MouseEnter += OnSubControlMouseEntered;
                _fieldControls[index].MouseHover += OnSubControlMouseHovered;
                _fieldControls[index].MouseLeave += OnSubControlMouseLeft;
                _fieldControls[index].MouseMove += OnSubControlMouseMoved;
                _fieldControls[index].TextChangedEvent += OnFieldTextChanged;

                Controls.Add(_fieldControls[index]);

                if (index < FIELD_COUNT - 1)
                {
                    _dotControls[index] = new DotControl();

                    _dotControls[index].CreateControl();

                    _dotControls[index].Name = "DotControl" + index.ToString(CultureInfo.InvariantCulture);
                    _dotControls[index].Parent = this;

                    _dotControls[index].Click += OnSubControlClicked;
                    _dotControls[index].DoubleClick += OnSubControlDoubleClicked;
                    _dotControls[index].MouseClick += OnSubControlMouseClicked;
                    _dotControls[index].MouseDoubleClick += OnSubControlMouseDoubleClicked;
                    _dotControls[index].MouseEnter += OnSubControlMouseEntered;
                    _dotControls[index].MouseHover += OnSubControlMouseHovered;
                    _dotControls[index].MouseLeave += OnSubControlMouseLeft;
                    _dotControls[index].MouseMove += OnSubControlMouseMoved;

                    Controls.Add(_dotControls[index]);
                }
            }

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            _referenceTextBox.AutoSize = true;

            Cursor = Cursors.IBeam;

            Size = MinimumSize;

            DragEnter += IPAddressControl_DragEnter;
            DragDrop += IPAddressControl_DragDrop;
        }

        [Browsable(true)]
        public bool AllowInternalTab
        {
            get { return _fieldControls.Select(fc => fc.TabStop).FirstOrDefault(); }
            set
            {
                foreach (var fc in _fieldControls)
                    fc.TabStop = value;
            }
        }

        [Browsable(true)]
        public bool AnyBlank
        {
            get { return _fieldControls.Any(fc => fc.Blank); }
        }

        [Browsable(true)]
        public bool AutoHeight
        {
            get => _autoHeight;
            set
            {
                _autoHeight = value;

                if (_autoHeight)
                {
                    AdjustSize();
                }
            }
        }

        [Browsable(false)]
        public int Baseline
        {
            get
            {
                var textMetric = GetTextMetrics(Handle, Font);

                var offset = textMetric.tmAscent + 1;

                switch (BorderStyle)
                {
                    case BorderStyle.Fixed3D:
                        offset += _fixed3DOffset.Height;
                        break;
                    case BorderStyle.FixedSingle:
                        offset += _fixedSingleOffset.Height;
                        break;
                }

                return offset;
            }
        }

        [Browsable(true)]
        public bool Blank
        {
            get { return _fieldControls.All(fc => fc.Blank); }
        }

        [Browsable(true)]
        public new BorderStyle BorderStyle
        {
            get => _borderStyle;
            set
            {
                _borderStyle = value;
                AdjustSize();
                Invalidate();
            }
        }

        [Browsable(false)]
        public override bool Focused
        {
            get { return _fieldControls.Any(fc => fc.Focused); }
        }

        [Browsable(true)]
        public override Size MinimumSize => CalculateMinimumSize();

        [Browsable(true)]
        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;

                foreach (var fc in _fieldControls)
                    fc.ReadOnly = _readOnly;

                foreach (var dc in _dotControls)
                    dc.ReadOnly = _readOnly;

                Invalidate();
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                var sb = new StringBuilder();

                for (var index = 0; index < _fieldControls.Length; ++index)
                {
                    sb.Append(_fieldControls[index].Text);

                    if (index < _dotControls.Length)
                    {
                        sb.Append(_dotControls[index].Text);
                    }
                }

                return sb.ToString();
            }
            set => Parse(value);
        }

        private bool HasMouse => DisplayRectangle.Contains(PointToClient(MousePosition));

        public event EventHandler<FieldChangedEventArgs> FieldChangedEvent;

        public void Clear()
        {
            foreach (var fc in _fieldControls)
                fc.Clear();
        }

        public byte[] GetAddressBytes()
        {
            var bytes = new byte[FIELD_COUNT];

            for (var index = 0; index < FIELD_COUNT; ++index)
                bytes[index] = _fieldControls[index].Value;

            return bytes;
        }

        public void SetAddressBytes(byte[] bytes)
        {
            Clear();

            if (bytes == null)
            {
                return;
            }

            var length = System.Math.Min(FIELD_COUNT, bytes.Length);

            for (var i = 0; i < length; ++i)
                _fieldControls[i].Text = bytes[i].ToString(CultureInfo.InvariantCulture);
        }

        public void SetFieldFocus(int fieldIndex)
        {
            if (fieldIndex >= 0 && fieldIndex < FIELD_COUNT)
            {
                _fieldControls[fieldIndex].TakeFocus(Direction.Forward, Selection.All);
            }
        }

        public void SetFieldRange(int fieldIndex, byte rangeLower, byte rangeUpper)
        {
            if (fieldIndex >= 0 && fieldIndex < FIELD_COUNT)
            {
                _fieldControls[fieldIndex].RangeLower = rangeLower;
                _fieldControls[fieldIndex].RangeUpper = rangeUpper;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var index = 0; index < FIELD_COUNT; ++index)
            {
                sb.Append(_fieldControls[index]);

                if (index < _dotControls.Length)
                {
                    sb.Append(_dotControls[index]);
                }
            }

            return sb.ToString();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _backColorChanged = true;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            AdjustSize();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _focused = true;
            _fieldControls[0].TakeFocus(Direction.Forward, Selection.All);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (!Focused)
            {
                _focused = false;
                base.OnLostFocus(e);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!_hasMouse)
            {
                _hasMouse = true;
                base.OnMouseEnter(e);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!HasMouse)
            {
                base.OnMouseLeave(e);
                _hasMouse = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var backColor = BackColor;

            if (!_backColorChanged)
            {
                if (!Enabled || ReadOnly)
                {
                    backColor = SystemColors.Control;
                }
            }

            using (var backgroundBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, ClientRectangle);
            }

            var rectBorder = new Rectangle(ClientRectangle.Left, ClientRectangle.Top,
                ClientRectangle.Width - 1, ClientRectangle.Height - 1);

            switch (BorderStyle)
            {
                case BorderStyle.Fixed3D:

                    if (Application.RenderWithVisualStyles)
                    {
                        ControlPaint.DrawVisualStyleBorder(e.Graphics, rectBorder);
                    }
                    else
                    {
                        ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Sunken);
                    }
                    break;

                case BorderStyle.FixedSingle:

                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                        SystemColors.WindowFrame, ButtonBorderStyle.Solid);
                    break;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AdjustSize();
        }

        private void AdjustSize()
        {
            var newSize = MinimumSize;

            if (Width > newSize.Width)
            {
                newSize.Width = Width;
            }

            if (Height > newSize.Height)
            {
                newSize.Height = Height;
            }

            Size = AutoHeight ? new Size(newSize.Width, MinimumSize.Height) : newSize;

            LayoutControls();
        }

        private Size CalculateMinimumSize()
        {
            var minimumSize = new Size(0, 0);

            foreach (var fc in _fieldControls)
            {
                minimumSize.Width += fc.Width;
                minimumSize.Height = System.Math.Max(minimumSize.Height, fc.Height);
            }

            foreach (var dc in _dotControls)
            {
                minimumSize.Width += dc.Width;
                minimumSize.Height = System.Math.Max(minimumSize.Height, dc.Height);
            }

            switch (BorderStyle)
            {
                case BorderStyle.Fixed3D:
                    minimumSize.Width += 6;
                    minimumSize.Height += GetSuggestedHeight() - minimumSize.Height;
                    break;
                case BorderStyle.FixedSingle:
                    minimumSize.Width += 4;
                    minimumSize.Height += GetSuggestedHeight() - minimumSize.Height;
                    break;
            }

            return minimumSize;
        }

        private int GetSuggestedHeight()
        {
            _referenceTextBox.BorderStyle = BorderStyle;
            _referenceTextBox.Font = Font;
            return _referenceTextBox.Height;
        }

        private static NativeMethods.TEXTMETRIC GetTextMetrics(IntPtr hwnd, Font font)
        {
            var hdc = NativeMethods.GetWindowDC(hwnd);

            NativeMethods.TEXTMETRIC textMetric;
            var hFont = font.ToHfont();

            try
            {
                var hFontPreviouse = NativeMethods.SelectObject(hdc, hFont);
                NativeMethods.GetTextMetrics(hdc, out textMetric);
                NativeMethods.SelectObject(hdc, hFontPreviouse);
            }
            finally
            {
                NativeMethods.ReleaseDC(hwnd, hdc);
                NativeMethods.DeleteObject(hFont);
            }

            return textMetric;
        }

        private void IPAddressControl_DragDrop(object sender, DragEventArgs e)
        {
            Text = e.Data.GetData(DataFormats.Text).ToString();
        }

        private static void IPAddressControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.Text) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void LayoutControls()
        {
            SuspendLayout();

            var difference = Width - MinimumSize.Width;

            Debug.Assert(difference >= 0);

            var numOffsets = _fieldControls.Length + _dotControls.Length + 1;

            var div = difference / numOffsets;
            var mod = difference % numOffsets;

            var offsets = new int[numOffsets];

            for (var index = 0; index < numOffsets; ++index)
            {
                offsets[index] = div;

                if (index < mod)
                {
                    ++offsets[index];
                }
            }

            var x = 0;
            var y = 0;

            switch (BorderStyle)
            {
                case BorderStyle.Fixed3D:
                    x = _fixed3DOffset.Width;
                    y = _fixed3DOffset.Height;
                    break;
                case BorderStyle.FixedSingle:
                    x = _fixedSingleOffset.Width;
                    y = _fixedSingleOffset.Height;
                    break;
            }

            var offsetIndex = 0;

            x += offsets[offsetIndex++];

            for (var i = 0; i < _fieldControls.Length; ++i)
            {
                _fieldControls[i].Location = new Point(x, y);

                x += _fieldControls[i].Width;

                if (i < _dotControls.Length)
                {
                    x += offsets[offsetIndex++];
                    _dotControls[i].Location = new Point(x, y);
                    x += _dotControls[i].Width;
                    x += offsets[offsetIndex++];
                }
            }

            ResumeLayout(false);
        }

        private void OnCedeFocus(object sender, CedeFocusEventArgs e)
        {
            switch (e.Action)
            {
                case Action.Home:

                    _fieldControls[0].TakeFocus(Action.Home);
                    return;

                case Action.End:

                    _fieldControls[FIELD_COUNT - 1].TakeFocus(Action.End);
                    return;

                case Action.Trim:

                    if (e.FieldIndex == 0)
                    {
                        return;
                    }

                    _fieldControls[e.FieldIndex - 1].TakeFocus(Action.Trim);
                    return;
            }

            if (e.Direction == Direction.Reverse && e.FieldIndex == 0 ||
                e.Direction == Direction.Forward && e.FieldIndex == FIELD_COUNT - 1)
            {
                return;
            }

            var fieldIndex = e.FieldIndex;

            if (e.Direction == Direction.Forward)
            {
                ++fieldIndex;
            }
            else
            {
                --fieldIndex;
            }

            _fieldControls[fieldIndex].TakeFocus(e.Direction, e.Selection);
        }

        private void OnFieldGotFocus(object sender, EventArgs e)
        {
            if (!_focused)
            {
                _focused = true;
                base.OnGotFocus(EventArgs.Empty);
            }
        }

        private void OnFieldKeyPressed(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void OnFieldLostFocus(object sender, EventArgs e)
        {
            if (!Focused)
            {
                _focused = false;
                base.OnLostFocus(EventArgs.Empty);
            }
        }

        private void OnFieldTextChanged(object sender, TextChangedEventArgs e)
        {
            if (null != FieldChangedEvent)
            {
                var args = new FieldChangedEventArgs {FieldIndex = e.FieldIndex, Text = e.Text};
                FieldChangedEvent(this, args);
            }

            OnTextChanged(EventArgs.Empty);
        }

        private void OnSubControlClicked(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void OnSubControlDoubleClicked(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }

        private void OnSubControlMouseClicked(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        private void OnSubControlMouseDoubleClicked(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        private void OnSubControlMouseEntered(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void OnSubControlMouseHovered(object sender, EventArgs e)
        {
            OnMouseHover(e);
        }

        private void OnSubControlMouseLeft(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void OnSubControlMouseMoved(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void Parse(string text)
        {
            Clear();

            if (null == text)
            {
                return;
            }

            var textIndex = 0;

            int index;

            for (index = 0; index < _dotControls.Length; ++index)
            {
                var findIndex = text.IndexOf(_dotControls[index].Text, textIndex, StringComparison.OrdinalIgnoreCase);

                if (findIndex >= 0)
                {
                    _fieldControls[index].Text = text.Substring(textIndex, findIndex - textIndex);
                    textIndex = findIndex + _dotControls[index].Text.Length;
                }
                else
                {
                    break;
                }
            }

            _fieldControls[index].Text = text.Substring(textIndex);
        }

        // a hack to remove an FxCop warning
        private void ResetBackColorChanged()
        {
            _backColorChanged = false;
        }
    }

    public class FieldChangedEventArgs : EventArgs
    {
        public int FieldIndex { get; set; }

        public string Text { get; set; }
    }
}