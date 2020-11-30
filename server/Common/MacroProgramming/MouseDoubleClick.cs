using System;
using Common.Win32;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class MouseDoubleClick : Chainable
    {
        private DigitalSignal _in;
        private DigitalSignal _out;
        private MouseButton _toClick = MouseButton.Left;

        public MouseDoubleClick()
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
                        KeyAndMouseFunctions.LeftDoubleClick();
                        break;
                    case MouseButton.Middle:
                        KeyAndMouseFunctions.MiddleDoubleClick();
                        break;
                    case MouseButton.Right:
                        KeyAndMouseFunctions.RightDoubleClick();
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