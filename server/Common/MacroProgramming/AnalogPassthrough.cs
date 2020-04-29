using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class AnalogPassthrough : Chainable
    {
        private AnalogSignal _in;
        private AnalogSignal _out;

        public AnalogPassthrough()
        {
            In = new AnalogSignal();
            Out = new AnalogSignal();
        }

        public AnalogSignal In
        {
            get => _in;
            set
            {
                if (value == null)
                {
                    value = new AnalogSignal();
                }
                value.SignalChanged += _in_SignalChanged;
                _in = value;
            }
        }

        public AnalogSignal Out
        {
            get => _out;
            set
            {
                if (value == null)
                {
                    value = new AnalogSignal();
                }
                _out = value;
            }
        }

        public void Refresh()
        {
            UpdateOutputValue(_in.State, _in.CorrelatedState);
        }

        private void _in_SignalChanged(object sender, AnalogSignalChangedEventArgs e)
        {
            UpdateOutputValue(e.CurrentState, e.CurrentCorrelatedState);
        }

        private void UpdateOutputValue(double outputState, double correlatedState)
        {
            if (_out != null)
            {
                _out.CorrelatedState = correlatedState;
                _out.State = outputState;
            }
        }
    }
}