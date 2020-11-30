using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class RangeEvaluator : Chainable
    {
        private AnalogSignal _in;
        private DigitalSignal _out;
        private Range _range = new Range();

        public RangeEvaluator()
        {
            In = new AnalogSignal();
            Out = new DigitalSignal();
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

        public Range Range
        {
            get => _range;
            set
            {
                if (value == null)
                {
                    value = new Range();
                }
                _range = value;
            }
        }

        private void _in_SignalChanged(object sender, AnalogSignalChangedEventArgs e)
        {
            if (e.CurrentState >= _range.LowerInclusiveBound && e.CurrentState <= _range.UpperInclusiveBound)
            {
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