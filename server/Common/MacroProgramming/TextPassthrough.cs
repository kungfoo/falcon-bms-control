using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class TextPassthrough : Chainable
    {
        private TextSignal _in;
        private TextSignal _out;

        public TextPassthrough()
        {
            In = new TextSignal();
            Out = new TextSignal();
        }

        public TextSignal In
        {
            get => _in;
            set
            {
                if (value == null)
                {
                    value = new TextSignal();
                }
                value.SignalChanged += _in_SignalChanged;
                _in = value;
            }
        }

        public TextSignal Out
        {
            get => _out;
            set
            {
                if (value == null)
                {
                    value = new TextSignal();
                }
                _out = value;
            }
        }

        public void Refresh()
        {
            UpdateOutputValue(_in.State);
        }

        private void _in_SignalChanged(object sender, TextSignalChangedEventArgs e)
        {
            UpdateOutputValue(e.CurrentState);
        }

        private void UpdateOutputValue(string outputValue)
        {
            if (_out != null)
            {
                _out.State = outputValue;
            }
        }
    }
}