using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class FlipFlop : Chainable
    {
        private DigitalSignal _in;
        private DigitalSignal _out;
        private DigitalSignal _reset;

        public FlipFlop()
        {
            In = new DigitalSignal();
            Reset = new DigitalSignal();
            Out = new DigitalSignal();
        }

        public bool CurrentValue { get; private set; }

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

        public DigitalSignal Reset
        {
            get => _reset;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal();
                }
                value.SignalChanged += ResetSignalChanged;
                _reset = value;
            }
        }

        private void InSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                CurrentValue = !CurrentValue;
                if (_out != null)
                {
                    _out.State = CurrentValue;
                }
            }
        }

        private void ResetSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                CurrentValue = false;
            }
        }
    }
}