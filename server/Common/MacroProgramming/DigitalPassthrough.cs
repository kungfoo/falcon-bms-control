using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class DigitalPassthrough : Chainable
    {
        private DigitalSignal _in;
        private DigitalSignal _out;

        public DigitalPassthrough()
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

        public void Refresh()
        {
            UpdateOutputValue(_in.State);
        }

        private void _in_SignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            UpdateOutputValue(e.CurrentState);
        }

        private void UpdateOutputValue(bool outputValue)
        {
            if (_out != null)
            {
                _out.State = outputValue;
            }
        }
    }
}