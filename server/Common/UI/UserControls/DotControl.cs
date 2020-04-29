using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.UI.UserControls
{
    /// <summary>
    ///     Represents the "dot" in an IP Address composite user control
    /// </summary>
    public class DotControl : Control
    {
        private const TextFormatFlags TEXT_FORMAT_FLAGS = TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPrefix |
                                                          TextFormatFlags.SingleLine | TextFormatFlags.NoPadding;

        private bool _backColorChanged;
        private bool _readOnly;

        public DotControl()
        {
            Text = ".";

            BackColor = SystemColors.Window;
            Size = MinimumSize;
            TabStop = false;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        public override Size MinimumSize
        {
            get
            {
                Size minimumSize;
                using (var g = Graphics.FromHwnd(Handle))
                {
                    minimumSize = TextRenderer.MeasureText(g,
                        Text, Font, Size,
                        TEXT_FORMAT_FLAGS);
                }
                return minimumSize;
            }
        }

        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
                Invalidate();
            }
        }

        public override string ToString()
        {
            return Text;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Size = MinimumSize;
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

            var textColor = ForeColor;

            if (!Enabled)
            {
                textColor = SystemColors.GrayText;
            }
            else if (ReadOnly)
            {
                if (!_backColorChanged)
                {
                    textColor = SystemColors.WindowText;
                }
            }

            using (var backgroundBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, ClientRectangle);
            }

            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle,
                textColor, TEXT_FORMAT_FLAGS);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            BackColor = Parent.BackColor;
            _backColorChanged = true;
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
    }
}