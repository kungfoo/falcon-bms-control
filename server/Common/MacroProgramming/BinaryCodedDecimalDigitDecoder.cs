using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class BinaryCodedDecimalDigitDecoder : Chainable
    {
        private DigitalSignal[] _in;
        private AnalogSignal _out;

        public BinaryCodedDecimalDigitDecoder()
        {
            var newIn = new DigitalSignal[4];
            for (var i = 0; i < newIn.Length; i++)
                newIn[i] = new DigitalSignal();
            In = newIn;
            Out = new AnalogSignal();
        }


        public DigitalSignal[] In
        {
            get => _in;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal[4];
                    for (var i = 0; i < value.Length; i++)
                        value[i] = new DigitalSignal();
                }
                foreach (var t in value)
                    if (t != null)
                    {
                        t.SignalChanged += InSignalChanged;
                    }
                _in = value;
                Evaluate();
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
                Evaluate();
            }
        }

        private void Evaluate()
        {
            if (_in == null) return;
            if (_out == null) return;
            var newVal = 0;

            for (var i = 0; i < _in.Length; i++)
                if (_in[i].State)
                {
                    newVal |= (int) System.Math.Pow(2, i);
                }
            _out.State = newVal;
        }

        private void InSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            Evaluate();
        }
    }
}