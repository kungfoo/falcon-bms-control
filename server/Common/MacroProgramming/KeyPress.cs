using System;
using System.Threading;
using System.Windows.Forms;
using Common.Win32;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class KeyPress : Chainable
    {
        private bool _extendedKey;
        private DigitalSignal _in;
        private Keys _keyCode = Keys.None;
        private DigitalSignal _out;
        private bool _press = true;
        private bool _release = true;

        public KeyPress()
        {
            In = new DigitalSignal();
            Out = new DigitalSignal();
        }

        public bool ExtendedKey
        {
            get => _extendedKey;
            set => _extendedKey = value;
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

        public Keys KeyCode
        {
            get => _keyCode;
            set => _keyCode = value;
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

        private void _in_SignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                Out.State = false;
                if (_press)
                {
                    KeyAndMouseFunctions.SendKey(_keyCode, _extendedKey, true, false);
                }
                Thread.Sleep(50);
                if (_release)
                {
                    KeyAndMouseFunctions.SendKey(_keyCode, _extendedKey, false, true);
                }
                Out.State = true;
            }
            else
            {
                Out.State = false;
            }
        }
    }
}