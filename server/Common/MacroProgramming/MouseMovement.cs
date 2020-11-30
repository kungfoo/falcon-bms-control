using System;
using Common.Win32;

namespace Common.MacroProgramming
{
    [Serializable]
    public enum MouseMoveMode
    {
        Relative,
        Absolute
    }

    [Serializable]
    public sealed class MouseMovement : Chainable
    {
        private DigitalSignal _in;
        private MouseMoveMode _mode = MouseMoveMode.Relative;
        private DigitalSignal _out;
        private int _x;
        private int _y;

        public MouseMovement()
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
                value.SignalChanged += InSignalChanged;
                _in = value;
            }
        }

        public MouseMoveMode Mode
        {
            get => _mode;
            set => _mode = value;
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

        public int X
        {
            get => _x;
            set
            {
                if (_mode == MouseMoveMode.Absolute)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }
                }
                _x = value;
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                if (_mode == MouseMoveMode.Absolute)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }
                }
                _y = value;
            }
        }

        private void InSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                if (_out != null)
                {
                    _out.State = false;
                }
                switch (_mode)
                {
                    case MouseMoveMode.Relative:
                        KeyAndMouseFunctions.MouseMoveRelative(_x, _y);
                        break;
                    case MouseMoveMode.Absolute:
                        KeyAndMouseFunctions.MouseMoveAbsolute(_x, _y);
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