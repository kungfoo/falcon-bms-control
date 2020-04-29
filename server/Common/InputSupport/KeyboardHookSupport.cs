using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Common.Win32;

namespace Common.InputSupport
{
    /// <summary>
    ///     This component monitors all mouse activities globally (also outside of the application)
    ///     and provides appropriate events.
    /// </summary>
    public class GlobalEventProvider : Component
    {
        /// <summary>
        ///     This component raises events. The value is always true.
        /// </summary>
        protected override bool CanRaiseEvents => true;

        /// <summary>
        ///     Occurs when a key is preseed.
        /// </summary>
        public event KeyEventHandler KeyDown
        {
            add
            {
                if (m_KeyDown == null)
                {
                    HookManager.KeyDown += HookManager_KeyDown;
                }
                m_KeyDown += value;
            }
            remove
            {
                m_KeyDown -= value;
                if (m_KeyDown == null)
                {
                    HookManager.KeyDown -= HookManager_KeyDown;
                }
            }
        }

        /// <summary>
        ///     Occurs when a key is pressed.
        /// </summary>
        /// <remarks>
        ///     Key events occur in the following order:
        ///     <list type="number">
        ///         <item>KeyDown</item>
        ///         <item>KeyPress</item>
        ///         <item>KeyUp</item>
        ///     </list>
        ///     The KeyPress event is not raised by noncharacter keys; however, the noncharacter keys do raise the KeyDown and
        ///     KeyUp events.
        ///     Use the KeyChar property to sample keystrokes at run time and to consume or modify a subset of common keystrokes.
        ///     To handle keyboard events only in your application and not enable other applications to receive keyboard events,
        ///     set the KeyPressEventArgs.Handled property in your form's KeyPress event-handling method to <b>true</b>.
        /// </remarks>
        public event KeyPressEventHandler KeyPress
        {
            add
            {
                if (m_KeyPress == null)
                {
                    HookManager.KeyPress += HookManager_KeyPress;
                }
                m_KeyPress += value;
            }
            remove
            {
                m_KeyPress -= value;
                if (m_KeyPress == null)
                {
                    HookManager.KeyPress -= HookManager_KeyPress;
                }
            }
        }

        /// <summary>
        ///     Occurs when a key is released.
        /// </summary>
        public event KeyEventHandler KeyUp
        {
            add
            {
                if (m_KeyUp == null)
                {
                    HookManager.KeyUp += HookManager_KeyUp;
                }
                m_KeyUp += value;
            }
            remove
            {
                m_KeyUp -= value;
                if (m_KeyUp == null)
                {
                    HookManager.KeyUp -= HookManager_KeyUp;
                }
            }
        }

        /// <summary>
        ///     Occurs when a click was performed by the mouse.
        /// </summary>
        public event MouseEventHandler MouseClick
        {
            add
            {
                if (m_MouseClick == null)
                {
                    HookManager.MouseClick += HookManager_MouseClick;
                }
                m_MouseClick += value;
            }

            remove
            {
                m_MouseClick -= value;
                if (m_MouseClick == null)
                {
                    HookManager.MouseClick -= HookManager_MouseClick;
                }
            }
        }

        /// <summary>
        ///     Occurs when a click was performed by the mouse.
        /// </summary>
        /// <remarks>
        ///     This event provides extended arguments of type <see cref="MouseEventArgs" /> enabling you to
        ///     supress further processing of mouse click in other applications.
        /// </remarks>
        public event EventHandler<MouseEventExtArgs> MouseClickExt
        {
            add
            {
                if (m_MouseClickExt == null)
                {
                    HookManager.MouseClickExt += HookManager_MouseClickExt;
                }
                m_MouseClickExt += value;
            }

            remove
            {
                m_MouseClickExt -= value;
                if (m_MouseClickExt == null)
                {
                    HookManager.MouseClickExt -= HookManager_MouseClickExt;
                }
            }
        }

        /// <summary>
        ///     Occurs when a double clicked was performed by the mouse.
        /// </summary>
        public event MouseEventHandler MouseDoubleClick
        {
            add
            {
                if (m_MouseDoubleClick == null)
                {
                    HookManager.MouseDoubleClick += HookManager_MouseDoubleClick;
                }
                m_MouseDoubleClick += value;
            }

            remove
            {
                m_MouseDoubleClick -= value;
                if (m_MouseDoubleClick == null)
                {
                    HookManager.MouseDoubleClick -= HookManager_MouseDoubleClick;
                }
            }
        }

        /// <summary>
        ///     Occurs when the mouse a mouse button is pressed.
        /// </summary>
        public event MouseEventHandler MouseDown
        {
            add
            {
                if (m_MouseDown == null)
                {
                    HookManager.MouseDown += HookManager_MouseDown;
                }
                m_MouseDown += value;
            }

            remove
            {
                m_MouseDown -= value;
                if (m_MouseDown == null)
                {
                    HookManager.MouseDown -= HookManager_MouseDown;
                }
            }
        }

        /// <summary>
        ///     Occurs when the mouse pointer is moved.
        /// </summary>
        public event MouseEventHandler MouseMove
        {
            add
            {
                if (MouseMoveEventHandler == null)
                {
                    HookManager.MouseMove += HookManagerMouseMove;
                }
                MouseMoveEventHandler += value;
            }

            remove
            {
                MouseMoveEventHandler -= value;
                if (MouseMoveEventHandler == null)
                {
                    HookManager.MouseMove -= HookManagerMouseMove;
                }
            }
        }

        /// <summary>
        ///     Occurs when the mouse pointer is moved.
        /// </summary>
        /// <remarks>
        ///     This event provides extended arguments of type <see cref="MouseEventArgs" /> enabling you to
        ///     supress further processing of mouse movement in other applications.
        /// </remarks>
        public event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add
            {
                if (m_MouseMoveExt == null)
                {
                    HookManager.MouseMoveExt += HookManager_MouseMoveExt;
                }
                m_MouseMoveExt += value;
            }

            remove
            {
                m_MouseMoveExt -= value;
                if (m_MouseMoveExt == null)
                {
                    HookManager.MouseMoveExt -= HookManager_MouseMoveExt;
                }
            }
        }

        /// <summary>
        ///     Occurs when a mouse button is released.
        /// </summary>
        public event MouseEventHandler MouseUp
        {
            add
            {
                if (m_MouseUp == null)
                {
                    HookManager.MouseUp += HookManager_MouseUp;
                }
                m_MouseUp += value;
            }

            remove
            {
                m_MouseUp -= value;
                if (m_MouseUp == null)
                {
                    HookManager.MouseUp -= HookManager_MouseUp;
                }
            }
        }

        private event KeyEventHandler m_KeyDown;

        private event KeyPressEventHandler m_KeyPress;

        private event KeyEventHandler m_KeyUp;

        private event MouseEventHandler m_MouseClick;

        private event EventHandler<MouseEventExtArgs> m_MouseClickExt;

        private event MouseEventHandler m_MouseDoubleClick;

        private event MouseEventHandler m_MouseDown;


        private event EventHandler<MouseEventExtArgs> m_MouseMoveExt;


        private event MouseEventHandler m_MouseUp;

        private event MouseEventHandler MouseMoveEventHandler;

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            m_KeyDown?.Invoke(this, e);
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_KeyPress?.Invoke(this, e);
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            m_KeyUp?.Invoke(this, e);
        }

        private void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            m_MouseClick?.Invoke(this, e);
        }

        private void HookManager_MouseClickExt(object sender, MouseEventExtArgs e)
        {
            m_MouseClickExt?.Invoke(this, e);
        }

        private void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_MouseDoubleClick?.Invoke(this, e);
        }

        private void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            m_MouseDown?.Invoke(this, e);
        }

        private void HookManager_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            m_MouseMoveExt?.Invoke(this, e);
        }

        private void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            m_MouseUp?.Invoke(this, e);
        }

        private void HookManagerMouseMove(object sender, MouseEventArgs e)
        {
            MouseMoveEventHandler?.Invoke(this, e);
        }

        //################################################################

        //################################################################
    }


    public static partial class HookManager
    {
        /// <summary>
        ///     This field is not objectively needed but we need to keep a reference on a delegate which will be
        ///     passed to unmanaged code. To avoid GC to clean it up.
        ///     When passing delegates to unmanaged code, they must be kept alive by the managed application
        ///     until it is guaranteed that they will never be called.
        /// </summary>
        private static HookProc s_MouseDelegate;

        /// <summary>
        ///     Stores the handle to the mouse hook procedure.
        /// </summary>
        private static int s_MouseHookHandle;

        private static int m_OldX;
        private static int m_OldY;

        private static bool _shiftDown;
        private static bool _ctrlDown;
        private static bool _altDown;

        /// <summary>
        ///     This field is not objectively needed but we need to keep a reference on a delegate which will be
        ///     passed to unmanaged code. To avoid GC to clean it up.
        ///     When passing delegates to unmanaged code, they must be kept alive by the managed application
        ///     until it is guaranteed that they will never be called.
        /// </summary>
        private static HookProc s_KeyboardDelegate;

        /// <summary>
        ///     Stores the handle to the Keyboard hook procedure.
        /// </summary>
        private static int s_KeyboardHookHandle;

        private static void EnsureSubscribedToGlobalKeyboardEvents()
        {
            // install Keyboard hook only if it is not installed and must be installed
            if (s_KeyboardHookHandle != 0) return;
            //See comment of this field. To avoid GC to clean it up.
            s_KeyboardDelegate = KeyboardHookProc;
            //install hook
            IntPtr hModule;
            using (var process = Process.GetCurrentProcess())
            using (var module = process.MainModule)
            {
                hModule = NativeMethods.GetModuleHandle(module.ModuleName);
            }
            s_KeyboardHookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, s_KeyboardDelegate, hModule, 0);

            //s_KeyboardHookHandle = SetWindowsHookEx(
            //    WH_KEYBOARD_LL,
            //    s_KeyboardDelegate,
            //    /*
            //    Marshal.GetHINSTANCE(
            //        Assembly.GetExecutingAssembly().GetModules()[0]),
            //     */
            //    IntPtr.Zero,
            //    0);
            //If SetWindowsHookEx fails.
            if (s_KeyboardHookHandle == 0)
            {
                //try setting the hook again using the module parameter
                s_KeyboardHookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, s_KeyboardDelegate, hModule, 0);
                if (s_KeyboardHookHandle == 0)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set.
                    var errorCode = Marshal.GetLastWin32Error();
                    //do cleanup

                    //Initializes and throws a new instance of the Win32Exception class with the specified error.
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static void EnsureSubscribedToGlobalMouseEvents()
        {
            // install Mouse hook only if it is not installed and must be installed
            if (s_MouseHookHandle == 0)
            {
                //See comment of this field. To avoid GC to clean it up.
                s_MouseDelegate = MouseHookProc;
                //install hook
                s_MouseHookHandle = SetWindowsHookEx(
                    WH_MOUSE_LL,
                    s_MouseDelegate,
                    Marshal.GetHINSTANCE(
                        Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);
                //If SetWindowsHookEx fails.
                if (s_MouseHookHandle == 0)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set.
                    var errorCode = Marshal.GetLastWin32Error();
                    //do cleanup

                    //Initializes and throws a new instance of the Win32Exception class with the specified error.
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static void ForceUnsunscribeFromGlobalKeyboardEvents()
        {
            if (s_KeyboardHookHandle == 0) return;
            //uninstall hook
            UnhookWindowsHookEx(s_KeyboardHookHandle);
            //reset invalid handle
            s_KeyboardHookHandle = 0;
            //Free up for GC
            s_KeyboardDelegate = null;
            //if failed and exception must be thrown
            /*
                if (result == 0)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
                 */
        }

        private static void ForceUnsunscribeFromGlobalMouseEvents()
        {
            if (s_MouseHookHandle == 0) return;
            //uninstall hook
            var result = UnhookWindowsHookEx(s_MouseHookHandle);
            //reset invalid handle
            s_MouseHookHandle = 0;
            //Free up for GC
            s_MouseDelegate = null;
            //if failed and exception must be thrown
            if (result == 0)
            {
                //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set.
                var errorCode = Marshal.GetLastWin32Error();
                //Initializes and throws a new instance of the Win32Exception class with the specified error.
                throw new Win32Exception(errorCode);
            }
        }

        /// <summary>
        ///     A callback function which will be called every Time a keyboard activity detected.
        /// </summary>
        /// <param name="nCode">
        ///     [in] Specifies whether the hook procedure must process the message.
        ///     If nCode is HC_ACTION, the hook procedure must process the message.
        ///     If nCode is less than zero, the hook procedure must pass the message to the
        ///     CallNextHookEx function without further processing and must return the
        ///     value returned by CallNextHookEx.
        /// </param>
        /// <param name="wParam">
        ///     [in] Specifies whether the message was sent by the current thread.
        ///     If the message was sent by the current thread, it is nonzero; otherwise, it is zero.
        /// </param>
        /// <param name="lParam">
        ///     [in] Pointer to a CWPSTRUCT structure that contains details about the message.
        /// </param>
        /// <returns>
        ///     If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
        ///     If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx
        ///     and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC
        ///     hooks will not receive hook notifications and may behave incorrectly as a result. If the hook
        ///     procedure does not call CallNextHookEx, the return value should be zero.
        /// </returns>
        private static int KeyboardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            //indicates if any of underlaing events set e.Handled flag
            var handled = false;

            if (nCode >= 0)
            {
                //read structure KeyboardHookStruct at lParam
                var MyKeyboardHookStruct =
                    (KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                //raise KeyDown
                if (s_KeyDown != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    var keyData = (Keys) MyKeyboardHookStruct.VirtualKeyCode;

                    if ((NativeMethods.GetKeyState(NativeMethods.VK_SHIFT) & 0x8000) != 0)
                    {
                        keyData |= Keys.Shift;
                        _shiftDown = true;
                        //SHIFT is pressed
                    }
                    else
                    {
                        _shiftDown = false;
                    }
                    if ((NativeMethods.GetKeyState(NativeMethods.VK_CONTROL) & 0x8000) != 0)
                    {
                        keyData |= Keys.Control;
                        _ctrlDown = true;
                        //CONTROL is pressed
                    }
                    else
                    {
                        _ctrlDown = false;
                    }
                    if ((NativeMethods.GetKeyState(NativeMethods.VK_MENU) & 0x8000) != 0)
                    {
                        keyData |= Keys.Alt;
                        _altDown = true;
                        //ALT is pressed
                    }
                    else
                    {
                        _altDown = false;
                    }
                    var e = new KeyEventArgsExt(keyData, MyKeyboardHookStruct.ScanCode,
                        (MyKeyboardHookStruct.Flags & NativeMethods.LLKHF_EXTENDED) ==
                        NativeMethods.LLKHF_EXTENDED);
                    s_KeyDown?.Invoke(null, e);
                    handled = e.Handled;
                }

                // raise KeyPress
                if (s_KeyPress != null && wParam == WM_KEYDOWN)
                {
                    var isDownShift = (GetKeyState(VK_SHIFT) & 0x80) == 0x80;
                    var isDownCapslock = GetKeyState(VK_CAPITAL) != 0;

                    var keyState = new byte[256];
                    GetKeyboardState(keyState);
                    var inBuffer = new byte[2];
                    if (ToAscii(MyKeyboardHookStruct.VirtualKeyCode,
                            MyKeyboardHookStruct.ScanCode,
                            keyState,
                            inBuffer,
                            MyKeyboardHookStruct.Flags) == 1)
                    {
                        var key = (char) inBuffer[0];
                        if (isDownCapslock ^ isDownShift && char.IsLetter(key)) key = char.ToUpper(key);
                        var e = new KeyPressEventArgs(key);
                        s_KeyPress?.Invoke(null, e);
                        handled = handled || e.Handled;
                    }
                }

                // raise KeyUp
                if (s_KeyUp != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    var keyData = (Keys) MyKeyboardHookStruct.VirtualKeyCode;
                    if ((NativeMethods.GetKeyState(NativeMethods.VK_SHIFT) & 0x8000) == 0 && _shiftDown)
                    {
                        _shiftDown = false;
                        keyData |= Keys.Shift;
                        //SHIFT has been released
                    }
                    if ((NativeMethods.GetKeyState(NativeMethods.VK_CONTROL) & 0x8000) == 0 && _ctrlDown)
                    {
                        _ctrlDown = false;
                        keyData |= Keys.Control;
                        //CONTROL has been released
                    }
                    if ((NativeMethods.GetKeyState(NativeMethods.VK_MENU) & 0x8000) == 0 && _altDown)
                    {
                        _altDown = false;
                        keyData |= Keys.Alt;
                        //ALT has been released
                    }

                    var e = new KeyEventArgsExt(keyData, MyKeyboardHookStruct.ScanCode,
                        (MyKeyboardHookStruct.Flags & NativeMethods.LLKHF_EXTENDED) ==
                        NativeMethods.LLKHF_EXTENDED);
                    s_KeyUp?.Invoke(null, e);
                    handled = handled || e.Handled;
                }
            }

            //if event handled in application do not handoff to other listeners
            if (handled)
            {
                return -1;
            }

            //forward to other application
            return CallNextHookEx(s_KeyboardHookHandle, nCode, wParam, lParam);
        }

        /// <summary>
        ///     A callback function which will be called every Time a mouse activity detected.
        /// </summary>
        /// <param name="nCode">
        ///     [in] Specifies whether the hook procedure must process the message.
        ///     If nCode is HC_ACTION, the hook procedure must process the message.
        ///     If nCode is less than zero, the hook procedure must pass the message to the
        ///     CallNextHookEx function without further processing and must return the
        ///     value returned by CallNextHookEx.
        /// </param>
        /// <param name="wParam">
        ///     [in] Specifies whether the message was sent by the current thread.
        ///     If the message was sent by the current thread, it is nonzero; otherwise, it is zero.
        /// </param>
        /// <param name="lParam">
        ///     [in] Pointer to a CWPSTRUCT structure that contains details about the message.
        /// </param>
        /// <returns>
        ///     If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
        ///     If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx
        ///     and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC
        ///     hooks will not receive hook notifications and may behave incorrectly as a result. If the hook
        ///     procedure does not call CallNextHookEx, the return value should be zero.
        /// </returns>
        private static int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                //Marshall the data from callback.
                var mouseHookStruct = (MouseLLHookStruct) Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

                //detect button clicked
                var button = MouseButtons.None;
                short mouseDelta = 0;
                var clickCount = 0;
                var mouseDown = false;
                var mouseUp = false;

                switch (wParam)
                {
                    case WM_LBUTTONDOWN:
                        mouseDown = true;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case WM_LBUTTONUP:
                        mouseUp = true;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case WM_LBUTTONDBLCLK:
                        button = MouseButtons.Left;
                        clickCount = 2;
                        break;
                    case WM_RBUTTONDOWN:
                        mouseDown = true;
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case WM_RBUTTONUP:
                        mouseUp = true;
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case WM_RBUTTONDBLCLK:
                        button = MouseButtons.Right;
                        clickCount = 2;
                        break;
                    case WM_MOUSEWHEEL:
                        //If the message is WM_MOUSEWHEEL, the high-order word of MouseData member is the wheel delta.
                        //One wheel click is defined as WHEEL_DELTA, which is 120.
                        //(value >> 16) & 0xffff; retrieves the high-order word from the given 32-bit value
                        mouseDelta = (short) ((mouseHookStruct.MouseData >> 16) & 0xffff);

                        break;
                }

                //generate event
                var e = new MouseEventExtArgs(
                    button,
                    clickCount,
                    mouseHookStruct.Point.X,
                    mouseHookStruct.Point.Y,
                    mouseDelta);

                //Mouse up
                if (s_MouseUp != null && mouseUp)
                {
                    s_MouseUp.Invoke(null, e);
                }

                //Mouse down
                if (s_MouseDown != null && mouseDown)
                {
                    s_MouseDown.Invoke(null, e);
                }

                //If someone listens to click and a click is heppened
                if (s_MouseClick != null && clickCount > 0)
                {
                    s_MouseClick.Invoke(null, e);
                }

                //If someone listens to click and a click is heppened
                if (s_MouseClickExt != null && clickCount > 0)
                {
                    s_MouseClickExt.Invoke(null, e);
                }

                //If someone listens to double click and a click is heppened
                if (s_MouseDoubleClick != null && clickCount == 2)
                {
                    s_MouseDoubleClick.Invoke(null, e);
                }

                //Wheel was moved
                if (s_MouseWheel != null && mouseDelta != 0)
                {
                    s_MouseWheel.Invoke(null, e);
                }

                //If someone listens to move and there was a change in coordinates raise move event
                if ((s_MouseMove != null || s_MouseMoveExt != null) &&
                    (m_OldX != mouseHookStruct.Point.X || m_OldY != mouseHookStruct.Point.Y))
                {
                    m_OldX = mouseHookStruct.Point.X;
                    m_OldY = mouseHookStruct.Point.Y;
                    s_MouseMove?.Invoke(null, e);

                    s_MouseMoveExt?.Invoke(null, e);
                }

                if (e.Handled)
                {
                    return -1;
                }
            }

            //call next hook
            return CallNextHookEx(s_MouseHookHandle, nCode, wParam, lParam);
        }

        private static void TryUnsubscribeFromGlobalKeyboardEvents()
        {
            //if no subsribers are registered unsubsribe from hook
            if (s_KeyDown == null &&
                s_KeyUp == null &&
                s_KeyPress == null)
            {
                ForceUnsunscribeFromGlobalKeyboardEvents();
            }
        }

        private static void TryUnsubscribeFromGlobalMouseEvents()
        {
            //if no subsribers are registered unsubsribe from hook
            if (s_MouseClick == null &&
                s_MouseDown == null &&
                s_MouseMove == null &&
                s_MouseUp == null &&
                s_MouseClickExt == null &&
                s_MouseMoveExt == null &&
                s_MouseWheel == null)
            {
                ForceUnsunscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        ///     The CallWndProc hook procedure is an application-defined or library-defined callback
        ///     function used with the SetWindowsHookEx function. The HOOKPROC type defines a pointer
        ///     to this callback function. CallWndProc is a placeholder for the application-defined
        ///     or library-defined function name.
        /// </summary>
        /// <param name="nCode">
        ///     [in] Specifies whether the hook procedure must process the message.
        ///     If nCode is HC_ACTION, the hook procedure must process the message.
        ///     If nCode is less than zero, the hook procedure must pass the message to the
        ///     CallNextHookEx function without further processing and must return the
        ///     value returned by CallNextHookEx.
        /// </param>
        /// <param name="wParam">
        ///     [in] Specifies whether the message was sent by the current thread.
        ///     If the message was sent by the current thread, it is nonzero; otherwise, it is zero.
        /// </param>
        /// <param name="lParam">
        ///     [in] Pointer to a CWPSTRUCT structure that contains details about the message.
        /// </param>
        /// <returns>
        ///     If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
        ///     If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx
        ///     and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC
        ///     hooks will not receive hook notifications and may behave incorrectly as a result. If the hook
        ///     procedure does not call CallNextHookEx, the return value should be zero.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/callwndproc.asp
        /// </remarks>
        private delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        //##############################################################################

        //##############################################################################
    }

    /// <summary>
    ///     This class monitors all mouse activities globally (also outside of the application)
    ///     and provides appropriate events.
    /// </summary>
    public static partial class HookManager
    {
        private static MouseButtons s_PrevClickedButton;

        //The timer to monitor time interval between two clicks.
        private static Timer s_DoubleClickTimer;

        /// <summary>
        ///     Occurs when a key is preseed.
        /// </summary>
        public static event KeyEventHandler KeyDown
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyDown += value;
            }
            remove
            {
                s_KeyDown -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }

        /// <summary>
        ///     Occurs when a key is pressed.
        /// </summary>
        /// <remarks>
        ///     Key events occur in the following order:
        ///     <list type="number">
        ///         <item>KeyDown</item>
        ///         <item>KeyPress</item>
        ///         <item>KeyUp</item>
        ///     </list>
        ///     The KeyPress event is not raised by noncharacter keys; however, the noncharacter keys do raise the KeyDown and
        ///     KeyUp events.
        ///     Use the KeyChar property to sample keystrokes at run time and to consume or modify a subset of common keystrokes.
        ///     To handle keyboard events only in your application and not enable other applications to receive keyboard events,
        ///     set the KeyPressEventArgs.Handled property in your form's KeyPress event-handling method to <b>true</b>.
        /// </remarks>
        public static event KeyPressEventHandler KeyPress
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyPress += value;
            }
            remove
            {
                s_KeyPress -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }

        /// <summary>
        ///     Occurs when a key is released.
        /// </summary>
        public static event KeyEventHandler KeyUp
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyUp += value;
            }
            remove
            {
                s_KeyUp -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }

        /// <summary>
        ///     Occurs when a click was performed by the mouse.
        /// </summary>
        public static event MouseEventHandler MouseClick
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseClick += value;
            }
            remove
            {
                s_MouseClick -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        ///     Occurs when a click was performed by the mouse.
        /// </summary>
        /// <remarks>
        ///     This event provides extended arguments of type <see cref="MouseEventArgs" /> enabling you to
        ///     supress further processing of mouse click in other applications.
        /// </remarks>
        public static event EventHandler<MouseEventExtArgs> MouseClickExt
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseClickExt += value;
            }
            remove
            {
                s_MouseClickExt -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        //The double click event will not be provided directly from hook.
        //To fire the double click event wee need to monitor mouse up event and when it occures
        //Two times during the time interval which is defined in Windows as a doble click time
        //we fire this event.

        /// <summary>
        ///     Occurs when a double clicked was performed by the mouse.
        /// </summary>
        public static event MouseEventHandler MouseDoubleClick
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                if (s_MouseDoubleClick == null)
                {
                    //We create a timer to monitor interval between two clicks
                    s_DoubleClickTimer = new Timer
                    {
                        //This interval will be set to the value we retrive from windows. This is a windows setting from contro planel.
                        Interval = GetDoubleClickTime(),
                        //We do not start timer yet. It will be start when the click occures.
                        Enabled = false
                    };
                    //We define the callback function for the timer
                    s_DoubleClickTimer.Tick += DoubleClickTimeElapsed;
                    //We start to monitor mouse up event.
                    MouseUp += OnMouseUp;
                }
                s_MouseDoubleClick += value;
            }
            remove
            {
                if (s_MouseDoubleClick != null)
                {
                    s_MouseDoubleClick -= value;
                    if (s_MouseDoubleClick == null)
                    {
                        //Stop monitoring mouse up
                        MouseUp -= OnMouseUp;
                        //Dispose the timer
                        s_DoubleClickTimer.Tick -= DoubleClickTimeElapsed;
                        s_DoubleClickTimer = null;
                    }
                }
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        ///     Occurs when the mouse a mouse button is pressed.
        /// </summary>
        public static event MouseEventHandler MouseDown
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseDown += value;
            }
            remove
            {
                s_MouseDown -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        ///     Occurs when the mouse pointer is moved.
        /// </summary>
        public static event MouseEventHandler MouseMove
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseMove += value;
            }

            remove
            {
                s_MouseMove -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        ///     Occurs when the mouse pointer is moved.
        /// </summary>
        /// <remarks>
        ///     This event provides extended arguments of type <see cref="MouseEventArgs" /> enabling you to
        ///     supress further processing of mouse movement in other applications.
        /// </remarks>
        public static event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseMoveExt += value;
            }

            remove
            {
                s_MouseMoveExt -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        ///     Occurs when a mouse button is released.
        /// </summary>
        public static event MouseEventHandler MouseUp
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseUp += value;
            }
            remove
            {
                s_MouseUp -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        ///     Occurs when the mouse wheel moves.
        /// </summary>
        public static event MouseEventHandler MouseWheel
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseWheel += value;
            }
            remove
            {
                s_MouseWheel -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event KeyEventHandler s_KeyDown;

        private static event KeyPressEventHandler s_KeyPress;

        private static event KeyEventHandler s_KeyUp;

        private static event MouseEventHandler s_MouseClick;

        private static event EventHandler<MouseEventExtArgs> s_MouseClickExt;


        private static event MouseEventHandler s_MouseDoubleClick;

        private static event MouseEventHandler s_MouseDown;
        private static event MouseEventHandler s_MouseMove;

        private static event EventHandler<MouseEventExtArgs> s_MouseMoveExt;

        private static event MouseEventHandler s_MouseUp;

        private static event MouseEventHandler s_MouseWheel;

        //This field remembers mouse button pressed because in addition to the short interval it must be also the same button.

        private static void DoubleClickTimeElapsed(object sender, EventArgs e)
        {
            //Timer is alapsed and no second click ocuured
            s_DoubleClickTimer.Enabled = false;
            s_PrevClickedButton = MouseButtons.None;
        }

        /// <summary>
        ///     This method is designed to monitor mouse clicks in order to fire a double click event if interval between
        ///     clicks was short enaugh.
        /// </summary>
        /// <param name="sender">Is always null</param>
        /// <param name="e">Some information about click heppened.</param>
        private static void OnMouseUp(object sender, MouseEventArgs e)
        {
            //This should not heppen
            if (e.Clicks < 1)
            {
                return;
            }
            //If the secon click heppened on the same button
            if (e.Button.Equals(s_PrevClickedButton))
            {
                //Fire double click
                s_MouseDoubleClick?.Invoke(null, e);
                //Stop timer
                s_DoubleClickTimer.Enabled = false;
                s_PrevClickedButton = MouseButtons.None;
            }
            else
            {
                //If it was the firts click start the timer
                s_DoubleClickTimer.Enabled = true;
                s_PrevClickedButton = e.Button;
            }
        }

        //################################################################

        //################################################################
    }

    public static partial class HookManager
    {
        /// <summary>
        ///     The KBDLLHOOKSTRUCT structure contains information about a low-level keyboard input event.
        /// </summary>
        /// <remarks>
        ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookstructures/cwpstruct.asp
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        private struct KeyboardHookStruct
        {
            /// <summary>
            ///     Specifies a virtual-key code. The code must be a value in the range 1 to 254.
            /// </summary>
            public readonly int VirtualKeyCode;

            /// <summary>
            ///     Specifies a hardware scan code for the key.
            /// </summary>
            public readonly int ScanCode;

            /// <summary>
            ///     Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
            /// </summary>
            public readonly int Flags;

            /// <summary>
            ///     Specifies the Time stamp for this message.
            /// </summary>
            public readonly int Time;

            /// <summary>
            ///     Specifies extra information associated with the message.
            /// </summary>
            public readonly int ExtraInfo;
        }

        /// <summary>
        ///     The MSLLHOOKSTRUCT structure contains information about a low-level keyboard input event.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct MouseLLHookStruct
        {
            /// <summary>
            ///     Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates.
            /// </summary>
            public Point Point;

            /// <summary>
            ///     If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta.
            ///     The low-order word is reserved. A positive value indicates that the wheel was rotated forward,
            ///     away from the user; a negative value indicates that the wheel was rotated backward, toward the user.
            ///     One wheel click is defined as WHEEL_DELTA, which is 120.
            ///     If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP,
            ///     or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released,
            ///     and the low-order word is reserved. This value can be one or more of the following values. Otherwise, MouseData is
            ///     not used.
            ///     XBUTTON1
            ///     The first X button was pressed or released.
            ///     XBUTTON2
            ///     The second X button was pressed or released.
            /// </summary>
            public readonly int MouseData;

            /// <summary>
            ///     Specifies the event-injected flag. An application can use the following value to test the mouse Flags. Value
            ///     Purpose
            ///     LLMHF_INJECTED Test the event-injected flag.
            ///     0
            ///     Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.
            ///     1-15
            ///     Reserved.
            /// </summary>
            public readonly int Flags;

            /// <summary>
            ///     Specifies the Time stamp for this message.
            /// </summary>
            public readonly int Time;

            /// <summary>
            ///     Specifies extra information associated with the message.
            /// </summary>
            public readonly int ExtraInfo;
        }

        /// <summary>
        ///     The Point structure defines the X- and Y- coordinates of a point.
        /// </summary>
        /// <remarks>
        ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/gdi/rectangl_0tiq.asp
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            /// <summary>
            ///     Specifies the X-coordinate of the point.
            /// </summary>
            public readonly int X;

            /// <summary>
            ///     Specifies the Y-coordinate of the point.
            /// </summary>
            public readonly int Y;
        }
    }

    public static partial class HookManager
    {
        //values from Winuser.h in Microsoft SDK.
        /// <summary>
        ///     Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events.
        /// </summary>
        private const int WH_MOUSE_LL = 14;

        /// <summary>
        ///     Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard  input events.
        /// </summary>
        private const int WH_KEYBOARD_LL = 13;

        /// <summary>
        ///     The WM_LBUTTONDOWN message is posted when the user presses the left mouse button
        /// </summary>
        private const int WM_LBUTTONDOWN = 0x201;

        /// <summary>
        ///     The WM_RBUTTONDOWN message is posted when the user presses the right mouse button
        /// </summary>
        private const int WM_RBUTTONDOWN = 0x204;

        /// <summary>
        ///     The WM_LBUTTONUP message is posted when the user releases the left mouse button
        /// </summary>
        private const int WM_LBUTTONUP = 0x202;

        /// <summary>
        ///     The WM_RBUTTONUP message is posted when the user releases the right mouse button
        /// </summary>
        private const int WM_RBUTTONUP = 0x205;

        /// <summary>
        ///     The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button
        /// </summary>
        private const int WM_LBUTTONDBLCLK = 0x203;

        /// <summary>
        ///     The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button
        /// </summary>
        private const int WM_RBUTTONDBLCLK = 0x206;

        /// <summary>
        ///     The WM_MOUSEWHEEL message is posted when the user presses the mouse wheel.
        /// </summary>
        private const int WM_MOUSEWHEEL = 0x020A;

        /// <summary>
        ///     The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem
        ///     key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.
        /// </summary>
        private const int WM_KEYDOWN = 0x100;

        /// <summary>
        ///     The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem
        ///     key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed,
        ///     or a keyboard key that is pressed when a window has the keyboard focus.
        /// </summary>
        private const int WM_KEYUP = 0x101;

        /// <summary>
        ///     The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user
        ///     presses the F10 key (which activates the menu bar) or holds down the ALT key and then
        ///     presses another key. It also occurs when no window currently has the keyboard focus;
        ///     in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that
        ///     receives the message can distinguish between these two contexts by checking the context
        ///     code in the lParam parameter.
        /// </summary>
        private const int WM_SYSKEYDOWN = 0x104;

        /// <summary>
        ///     The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user
        ///     releases a key that was pressed while the ALT key was held down. It also occurs when no
        ///     window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent
        ///     to the active window. The window that receives the message can distinguish between
        ///     these two contexts by checking the context code in the lParam parameter.
        /// </summary>
        private const int WM_SYSKEYUP = 0x105;

        private const byte VK_SHIFT = 0x10;
        private const byte VK_CAPITAL = 0x14;

        /// <summary>
        ///     The GetDoubleClickTime function retrieves the current double-click time for the mouse. A double-click is a series
        ///     of two clicks of the
        ///     mouse button, the second occurring within a specified time after the first. The double-click time is the maximum
        ///     number of
        ///     milliseconds that may occur between the first and second click of a double-click.
        /// </summary>
        /// <returns>
        ///     The return value specifies the current double-click time, in milliseconds.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/en-us/library/ms646258(VS.85).aspx
        /// </remarks>
        [DllImport("user32")]
        public static extern int GetDoubleClickTime();

        /// <summary>
        ///     The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain.
        ///     A hook procedure can call this function either before or after processing the hook information.
        /// </summary>
        /// <param name="idHook">Ignored.</param>
        /// <param name="nCode">
        ///     [in] Specifies the hook code passed to the current hook procedure.
        ///     The next hook procedure uses this code to determine how to process the hook information.
        /// </param>
        /// <param name="wParam">
        ///     [in] Specifies the wParam value passed to the current hook procedure.
        ///     The meaning of this parameter depends on the type of hook associated with the current hook chain.
        /// </param>
        /// <param name="lParam">
        ///     [in] Specifies the lParam value passed to the current hook procedure.
        ///     The meaning of this parameter depends on the type of hook associated with the current hook chain.
        /// </param>
        /// <returns>
        ///     This value is returned by the next hook procedure in the chain.
        ///     The current hook procedure must also return this value. The meaning of the return value depends on the hook type.
        ///     For more information, see the descriptions of the individual hook procedures.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(
            int idHook,
            int nCode,
            int wParam,
            IntPtr lParam);

        /// <summary>
        ///     The GetKeyboardState function copies the status of the 256 virtual keys to the
        ///     specified buffer.
        /// </summary>
        /// <param name="pbKeyState">
        ///     [in] Pointer to a 256-byte array that contains keyboard key states.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
        /// </remarks>
        [DllImport("user32")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        /// <summary>
        ///     The GetKeyState function retrieves the status of the specified virtual key. The status specifies whether the key is
        ///     up, down, or toggled
        ///     (on, off—alternating each time the key is pressed).
        /// </summary>
        /// <param name="vKey">
        ///     [in] Specifies a virtual key. If the desired virtual key is a letter or digit (A through Z, a through z, or 0
        ///     through 9), nVirtKey must be set to the ASCII value of that character. For other keys, it must be a virtual-key
        ///     code.
        /// </param>
        /// <returns>
        ///     The return value specifies the status of the specified virtual key, as follows:
        ///     If the high-order bit is 1, the key is down; otherwise, it is up.
        ///     If the low-order bit is 1, the key is toggled. A key, such as the CAPS LOCK key, is toggled if it is turned on. The
        ///     key is off and untoggled if the low-order bit is 0. A toggle key's indicator light (if any) on the keyboard will be
        ///     on when the key is toggled, and off when the key is untoggled.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/en-us/library/ms646301.aspx
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);


        /// <summary>
        ///     The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain.
        ///     You would install a hook procedure to monitor the system for certain types of events. These events
        ///     are associated either with a specific thread or with all threads in the same desktop as the calling thread.
        /// </summary>
        /// <param name="idHook">
        ///     [in] Specifies the type of hook procedure to be installed. This parameter can be one of the following values.
        /// </param>
        /// <param name="lpfn">
        ///     [in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a
        ///     thread created by a different process, the lpfn parameter must point to a hook procedure in a dynamic-link
        ///     library (DLL). Otherwise, lpfn can point to a hook procedure in the code associated with the current process.
        /// </param>
        /// <param name="hMod">
        ///     [in] Handle to the DLL containing the hook procedure pointed to by the lpfn parameter.
        ///     The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by
        ///     the current process and if the hook procedure is within the code associated with the current process.
        /// </param>
        /// <param name="dwThreadId">
        ///     [in] Specifies the identifier of the thread with which the hook procedure is to be associated.
        ///     If this parameter is zero, the hook procedure is associated with all existing threads running in the
        ///     same desktop as the calling thread.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the handle to the hook procedure.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int SetWindowsHookEx(
            int idHook,
            HookProc lpfn,
            IntPtr hMod,
            int dwThreadId);

        /// <summary>
        ///     The ToAscii function translates the specified virtual-key code and keyboard
        ///     state to the corresponding character or characters. The function translates the code
        ///     using the input language and physical keyboard layout identified by the keyboard layout handle.
        /// </summary>
        /// <param name="uVirtKey">
        ///     [in] Specifies the virtual-key code to be translated.
        /// </param>
        /// <param name="uScanCode">
        ///     [in] Specifies the hardware scan code of the key to be translated.
        ///     The high-order bit of this value is set if the key is up (not pressed).
        /// </param>
        /// <param name="lpbKeyState">
        ///     [in] Pointer to a 256-byte array that contains the current keyboard state.
        ///     Each element (byte) in the array contains the state of one key.
        ///     If the high-order bit of a byte is set, the key is down (pressed).
        ///     The low bit, if set, indicates that the key is toggled on. In this function,
        ///     only the toggle bit of the CAPS LOCK key is relevant. The toggle state
        ///     of the NUM LOCK and SCROLL LOCK keys is ignored.
        /// </param>
        /// <param name="lpwTransKey">
        ///     [out] Pointer to the buffer that receives the translated character or characters.
        /// </param>
        /// <param name="fuState">
        ///     [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise.
        /// </param>
        /// <returns>
        ///     If the specified key is a dead key, the return value is negative. Otherwise, it is one of the following values.
        ///     Value Meaning
        ///     0 The specified virtual key has no translation for the current state of the keyboard.
        ///     1 One character was copied to the buffer.
        ///     2 Two characters were copied to the buffer. This usually happens when a dead-key character
        ///     (accent or diacritic) stored in the keyboard layout cannot be composed with the specified
        ///     virtual key to form a single character.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
        /// </remarks>
        [DllImport("user32")]
        private static extern int ToAscii(
            int uVirtKey,
            int uScanCode,
            byte[] lpbKeyState,
            byte[] lpwTransKey,
            int fuState);

        /// <summary>
        ///     The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx
        ///     function.
        /// </summary>
        /// <param name="idHook">
        ///     [in] Handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to
        ///     SetWindowsHookEx.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int UnhookWindowsHookEx(int idHook);
    }

    public class KeyEventArgsExt : KeyEventArgs
    {
        public KeyEventArgsExt(Keys keyData, int scanCode, bool extendedKeyFlag) : base(keyData)
        {
            ScanCode = scanCode;
            ExtendedKeyFlag = extendedKeyFlag;
        }

        public bool ExtendedKeyFlag { get; set; }

        public int ScanCode { get; set; }
    }

    /// <summary>
    ///     Provides data for the MouseClickExt and MouseMoveExt events. It also provides a property Handled.
    ///     Set this property to <b>true</b> to prevent further processing of the event in other applications.
    /// </summary>
    public class MouseEventExtArgs : MouseEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the MouseEventArgs class.
        /// </summary>
        /// <param name="buttons">One of the MouseButtons values indicating which mouse button was pressed.</param>
        /// <param name="clicks">The number of times a mouse button was pressed.</param>
        /// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
        /// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
        /// <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
        public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
            : base(buttons, clicks, x, y, delta)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the MouseEventArgs class.
        /// </summary>
        /// <param name="e">An ordinary <see cref="MouseEventArgs" /> argument to be extended.</param>
        internal MouseEventExtArgs(MouseEventArgs e)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
        }

        /// <summary>
        ///     Set this property to <b>true</b> inside your event handler to prevent further processing of the event in other
        ///     applications.
        /// </summary>
        public bool Handled { get; set; }
    }
}