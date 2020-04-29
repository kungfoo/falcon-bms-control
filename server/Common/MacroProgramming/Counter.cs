using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class Counter : Chainable
    {
        private DigitalSignal _in;
        private long _increment = 1;
        private AnalogSignal _out;
        private DigitalSignal _reset;

        public Counter()
        {
            In = new DigitalSignal();
            Reset = new DigitalSignal();
            Out = new AnalogSignal();
        }

        public long CurrentValue { get; private set; }

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

        public long Increment
        {
            get => _increment;
            set => _increment = value;
        }

        public long InitialValue { get; } = 0;

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
            if (!e.CurrentState) return;
            try
            {
                CurrentValue += _increment;
            }
            catch (OverflowException)
            {
                CurrentValue = CurrentValue == long.MaxValue ? long.MinValue : long.MaxValue;
            }
            if (_out != null)
            {
                _out.State = CurrentValue;
            }
        }

        private void ResetCounter()
        {
            CurrentValue = InitialValue;
        }

        private void ResetSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                ResetCounter();
            }
        }
    }
}