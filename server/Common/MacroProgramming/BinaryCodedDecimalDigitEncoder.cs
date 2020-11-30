using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class BinaryCodedDecimalDigitEncoder : Chainable
    {
        private AnalogSignal _in;
        private DigitalSignal[] _out;

        public BinaryCodedDecimalDigitEncoder()
        {
            var newOut = new DigitalSignal[4];
            for (var i = 0; i < newOut.Length; i++)
                newOut[i] = new DigitalSignal();
            Out = newOut;
            In = new AnalogSignal();
            In.SignalChanged += _in_SignalChanged;
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
                Evaluate();
            }
        }


        public DigitalSignal[] Out
        {
            get => _out;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal[4];
                    for (var i = 0; i < value.Length; i++)
                        value[i] = new DigitalSignal();
                }
                _out = value;
                Evaluate();
            }
        }

        private void _in_SignalChanged(object sender, AnalogSignalChangedEventArgs e)
        {
            Evaluate();
        }

        private void Evaluate()
        {
            if (_in == null) return;
            if (_out == null) return;
            long newVal = (int) _in.State;
            for (var i = 0; i < _out.Length; i++)
            {
                var thisMask = (int) System.Math.Pow(2, i);
                if (_out[i] != null)
                {
                    _out[i].State = (newVal & thisMask) == thisMask;
                }
            }
        }
    }
}