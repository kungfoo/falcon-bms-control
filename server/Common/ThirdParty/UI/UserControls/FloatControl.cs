using System;

using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;


// Requires unmanaged code
[assembly: SecurityPermissionAttribute(SecurityAction.RequestMinimum, UnmanagedCode = true)]
// Requires ability to create any window type
[assembly: UIPermissionAttribute(SecurityAction.RequestMinimum, Window = UIPermissionWindow.AllWindows)]

namespace Common.UI.UserControls
{


    /// <summary>
    /// An version of a <see cref="System.Windows.Forms.Control"/> 
    /// which can be shown floating, like a tooltip.  If you
    /// want to use this control in conjunction with mouse events
    /// then you must ensure that the mouse is never in any part of the 
    /// control when it is shown (like a tooltip). Otherwise, 
    /// <see cref="System.Windows.Forms.MouseEnter"/> and 
    /// <see cref="System.Windows.Forms.MouseLeave"/> events
    /// are broken, and the Forms Message Filter
    /// goes into a continuous loop when attempting to show
    /// the control.
    /// </summary>
    public class FloatControl : Control
    {

        [DllImport("user32")]
        private static extern int SetParent(
            IntPtr hWndChild,
            IntPtr hWndNewParent);
        [DllImport("user32")]
        private static extern IntPtr GetParent(
            IntPtr hWndChild);

        [DllImport("user32")]
        private static extern int ShowWindow(
            IntPtr hWnd,
            int nCmdShow);

        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_EX_TOPMOST = 0x00000008;

        private const int WM_NCHITTEST = 0x0084;
        private const int HTTRANSPARENT = (-1);


        /// <summary>
        /// Shows the control as a floating Window child 
        /// of the desktop.  To hide the control again,
        /// use the <see cref="Visible"/> property.
        /// </summary>
        public void ShowFloating()
        {
            if (this.Handle == IntPtr.Zero)
            {
                base.CreateControl();
            }
            Console.WriteLine("{0}", GetParent(base.Handle));
            SetParent(base.Handle, IntPtr.Zero);
            ShowWindow(base.Handle, 1);
        }

        /// <summary>
        /// Get the <see cref="System.Windows.Forms.CreateParams"/>
        /// used to create the control.  This override adds the
        /// <code>WS_EX_NOACTIVATE</code>, <code>WS_EX_TOOLWINDOW</code>
        /// and <code>WS_EX_TOPMOST</code> extended styles to make
        /// the Window float on top.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
                p.Parent = IntPtr.Zero;
                return p;
            }
        }


        /// <summary>
        /// Overrides the standard Window Procedure to ensure the
        /// window is transparent to all mouse events.
        /// </summary>
        /// <param name="m">Windows message to process.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        }


        /// <summary>
        /// Overrides the standard painting procedure to render
        /// the text associated with the control.
        /// </summary>
        /// <param name="e">PaintEvent Arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (base.Text.Length > 0)
            {
                Brush br = new SolidBrush(this.ForeColor);
                e.Graphics.DrawString(base.Text, this.Font, br, new PointF(1F, 1F));
                br.Dispose();
            }
            e.Graphics.DrawRectangle(SystemPens.ControlDarkDark,
                0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
        }

        /// <summary>
        /// Constructs a new instance of this control.
        /// </summary>
        public FloatControl()
        {
            // intentionally blank
        }
    }

}
