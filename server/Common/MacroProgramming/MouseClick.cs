using System;
using Common.Win32;

namespace Common.MacroProgramming
{
    [Serializable]
    public enum MouseButton
    {
        Left,
        Middle,
        Right
    }

    [Serializable]
    public sealed class MouseClick : Chainable
    {
        private DigitalSignal _in;
        private DigitalSignal _out;
        private bool _press = true;
        private bool _release = true;
        private MouseButton _toClick = MouseButton.Left;

        public MouseClick()
        {
            In = new DigitalSignal();
            Out = new DigitalSignal();
        }

        public DigitalSignal In
        {
            get => _in;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal();
                }
                value.SignalChanged += _in_SignalChanged;
                _in = value;
            }
        }

        public DigitalSignal Out
        {
            get => _out;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal();
                }
                _out = value;
            }
        }

        public bool Press
        {
            get => _press;
            set => _press = value;
        }

        public bool Release
        {
            get => _release;
            set => _release = value;
        }

        public MouseButton ToClick
        {
            get => _toClick;
            set => _toClick = value;
        }

        private void _in_SignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                if (_out != null)
                {
                    _out.State = false;
                }
                switch (_toClick)
                {
                    case MouseButton.Left:
                        if (_press && _release)
                        {
                            KeyAndMouseFunctions.LeftClick();
                        }
                        else if (_press)
                        {
                            KeyAndMouseFunctions.LeftDown();
                        }
                        else if (_release)
                        {
                            KeyAndMouseFunctions.LeftUp();
                        }
                        break;
                    case MouseButton.Middle:
                        if (_press && _release)
                        {
                            KeyAndMouseFunctions.MiddleClick();
                        }
                        else if (_press)
                        {
                            KeyAndMouseFunctions.MiddleDown();
                        }
                        else if (_release)
                        {
                            KeyAndMouseFunctions.MiddleUp();
                        }
                        break;
                    case MouseButton.Right:
                        if (_press && _release)
                        {
                            KeyAndMouseFunctions.RightClick();
                        }
                        else if (_press)
                        {
                            KeyAndMouseFunctions.RightDown();
                        }
                        else if (_release)
                        {
                            KeyAndMouseFunctions.RightUp();
                        }
                        break;
                }
                if (_out != null)
                {
                    _out.State = true;
                }
            }
            else
            {
                if (_out != null)
                {
                    _out.State = false;
                }
            }
        }
    }
}