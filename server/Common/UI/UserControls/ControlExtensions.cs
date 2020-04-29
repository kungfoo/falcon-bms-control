using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.UI.UserControls
{
    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType()
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }
        public static async Task<TResult> UIThreadAsync<TResult>(this Control control, System.Func<TResult> func)
        {
            return control.InvokeRequired 
                    ? await Task.Factory.FromAsync(control.BeginInvoke(func), _ => {return (TResult)control.EndInvoke(_); })
                    : await Task.Factory.StartNew(func);
        }
        public static TResult UIThread<TResult>(this Control control, System.Func<TResult> func)
        {
            return control.InvokeRequired
                ? (TResult) control.Invoke(func)
                : func();
        }
        public static void UIThread(this Control control, System.Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}