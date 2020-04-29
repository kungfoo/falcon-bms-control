using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class Repeater : Chainable
    {
        private DigitalSignal _in;
        private Macro _macro = new Macro();

        private int _numRepetitions = 1;
        private DigitalSignal _out;

        public Repeater()
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

        public Macro Macro
        {
            get => _macro;
            set
            {
                if (value == null)
                {
                    value = new Macro();
                }
                _macro = value;
            }
        }

        public int NumRepetitions
        {
            get => _numRepetitions;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _numRepetitions = value;
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

        private void _in_SignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                if (_out != null)
                {
                    _out.State = false;
                }
                for (var i = 0; i < NumRepetitions; i++)
                    if (_macro != null)
                    {
                        _macro.In.State = true;
                    }
                if (_macro != null)
                {
                    _macro.In.State = false;
                }
                if (_out != null)
                {
                    _out.State = true;
                }
            }
            else
            {
                if (_macro != null)
                {
                    _macro.In.State = false;
                }
                if (_out != null)
                {
                    _out.State = false;
                }
            }
        }
    }
}