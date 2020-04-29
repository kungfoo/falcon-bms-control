using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class AnalogSignalNormalizer : Chainable
    {
        private AnalogSignal _in;
        private Range _inputRange = new Range();
        private AnalogSignal _out;
        private Range _outputRange = new Range();

        public AnalogSignalNormalizer()
        {
            In = new AnalogSignal();
            InputRange = new Range {LowerInclusiveBound = 0.0, UpperInclusiveBound = 1.0};
            OutputRange = new Range {LowerInclusiveBound = 0.0, UpperInclusiveBound = 1.0};
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
                value.SignalChanged += InSignalChanged;
                _in = value;
                Evaluate(_in.State);
            }
        }

        public Range InputRange
        {
            get => _inputRange;
            set
            {
                if (value == null)
                {
                    value = new Range();
                }
                _inputRange = value;
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
                if (_in != null)
                {
                    Evaluate(_in.State);
                }
            }
        }

        public Range OutputRange
        {
            get => _outputRange;
            set
            {
                if (value == null)
                {
                    value = new Range();
                }
                _outputRange = value;
            }
        }

        private void Evaluate(double newVal)
        {
            if (newVal > _inputRange.LowerInclusiveBound && newVal < _inputRange.UpperInclusiveBound)
            {
                if (_out != null)
                {
                    var newValAsPercentageOfInputRange = (newVal - _inputRange.LowerInclusiveBound) /
                                                         _inputRange.Width;
                    _out.State = newValAsPercentageOfInputRange * _outputRange.Width +
                                 _outputRange.LowerInclusiveBound;
                }
            }
            else if (newVal <= _inputRange.LowerInclusiveBound)
            {
                if (_out != null)
                {
                    _out.State = _outputRange.LowerInclusiveBound;
                }
            }
            else if (newVal >= _inputRange.UpperInclusiveBound)
            {
                if (_out != null)
                {
                    _out.State = _outputRange.UpperInclusiveBound;
                }
            }
            else
            {
                if (_out != null)
                {
                    _out.State = _outputRange.LowerInclusiveBound;
                }
            }
        }

        private void InSignalChanged(object sender, AnalogSignalChangedEventArgs e)
        {
            Evaluate(e.CurrentState);
        }
    }
}