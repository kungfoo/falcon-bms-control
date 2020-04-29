using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Common.Win32;
using log4net;

namespace Common.UI.Screen
{
    public static class Util
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Util));

        public static Bitmap CaptureScreenRectangle(Rectangle sourceRectangle)
        {
            if (sourceRectangle == Rectangle.Empty)
            {
                return null;
            }
            Bitmap toReturn = null;
            try
            {
                toReturn = new Bitmap(sourceRectangle.Width, sourceRectangle.Height);
                using (var g = Graphics.FromImage(toReturn))
                {
                    g.CopyFromScreen(sourceRectangle.X, sourceRectangle.Y, 0, 0, sourceRectangle.Size);
                }
            }
            catch (Win32Exception e)
            {
                Log.Debug(e.Message, e);
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e);
            }
            return toReturn;
        }

        public static string CleanDeviceName(string deviceName)
        {
            if (deviceName == null) return null;
            var firstNull = deviceName.IndexOf('\0');
            return firstNull >= 0 ? deviceName.Substring(0, firstNull).Trim() : deviceName;
        }

        public static System.Windows.Forms.Screen FindScreen(string deviceName)
        {
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
                if (CleanDeviceName(screen.DeviceName) == CleanDeviceName(deviceName))
                {
                    return screen;
                }
            return System.Windows.Forms.Screen.PrimaryScreen;
        }

        public static void OpenFormOnSpecificMonitor(Form formToOpen, System.Windows.Forms.Screen screen, Point point,
            Size size,
            bool hideFromTaskBar, bool hideFromAltTab)
        {
            OpenFormOnSpecificMonitor(formToOpen, screen, point, size, hideFromTaskBar, hideFromAltTab, false);
        }

        public static void OpenFormOnSpecificMonitor(Form formToOpen,
            System.Windows.Forms.Screen screen, Point point, Size size,
            bool hideFromTaskBar, bool hideFromAltTab,
            bool runAsUIThread)
        {
            OpenFormOnSpecificMonitor(formToOpen, screen, point, size );
            if (hideFromTaskBar)
            {
                formToOpen.ShowInTaskbar = false;
            }
            if (hideFromAltTab)
            {
                formToOpen.ShowIcon = false;
                formToOpen.ShowInTaskbar = false;
            }
            if (hideFromAltTab)
            {
                NativeMethods.SetWindowLong(formToOpen.Handle, NativeMethods.GWL_EXSTYLE,
                    (NativeMethods.GetWindowLong(formToOpen.Handle,
                         NativeMethods.GWL_EXSTYLE) |
                     NativeMethods.WS_EX_TOOLWINDOW) & ~NativeMethods.WS_EX_APPWINDOW);
            }
        }

        public static void OpenFormOnSpecificMonitor(Form formToOpen, System.Windows.Forms.Screen screen, Point point,
            Size size)
        {
            OpenFormOnSpecificMonitor(formToOpen, screen, ref point, ref size, false, false, false);
        }

        private static void OpenFormOnSpecificMonitor(Form formToOpen, System.Windows.Forms.Screen screen,
            ref Point point, ref Size size, bool hideFromTaskBar, bool hideFromAltTab, bool runAsUIThread)
        {
            // Set the StartPosition to Manual otherwise the system will assign an automatic start position
            formToOpen.StartPosition = FormStartPosition.Manual;
            // Set the form location so it appears at Location (x, y) on the specified screen 
            var l = screen.Bounds.Location;
            l.Offset(point);
            formToOpen.DesktopLocation = l;
            // Show the form
            formToOpen.Size = size;
            if (runAsUIThread)
            {
                Application.UseWaitCursor = false;
                Application.VisualStyleState = VisualStyleState.NoneEnabled;
                Application.Run(formToOpen);
            }
            else
            {
                formToOpen.Show();
                formToOpen.UseWaitCursor = false;
                formToOpen.Cursor = Cursors.Default;
            }
            formToOpen.Size = size;
            formToOpen.Owner = Application.OpenForms.Count == 0 ? null : Application.OpenForms[0];
            if (hideFromTaskBar)
            {
                formToOpen.ShowInTaskbar = false;
            }
            if (hideFromAltTab)
            {
                formToOpen.ShowIcon = false;
                formToOpen.ShowInTaskbar = false;
            }
            if (hideFromAltTab)
            {
                NativeMethods.SetWindowLong(formToOpen.Handle, NativeMethods.GWL_EXSTYLE,
                                            (NativeMethods.GetWindowLong(formToOpen.Handle,
                                                                         NativeMethods.GWL_EXSTYLE) |
                                             NativeMethods.WS_EX_TOOLWINDOW) & ~NativeMethods.WS_EX_APPWINDOW);
            }
        }
    }
}