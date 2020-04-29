using System;
using System.Collections.Generic;
using System.Threading;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class Split : Chainable
    {
        private DigitalSignal _in;
        private List<DigitalSignal> _outs = new List<DigitalSignal>();

        public Split()
        {
            _in = new DigitalSignal();
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
                value.SignalChanged += InSignalChanged;
                _in = value;
            }
        }

        public List<DigitalSignal> Outs
        {
            get => _outs;
            set
            {
                if (value == null)
                {
                    value = new List<DigitalSignal>();
                }
                _outs = value;
            }
        }

        public DigitalSignal CreateAdditionalOut()
        {
            var newSig = new DigitalSignal();
            _outs.Add(newSig);
            return newSig;
        }

        private void InSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                if (_outs != null)
                {
                    foreach (var t1 in _outs)
                    {
                        if (t1 == null) continue;
                        //_outs[i].State = true;
                        var sigVal = new SignalValue {Signal = t1, Value = true};
                        var t = new Thread(SendSignal);
                        t.SetApartmentState(ApartmentState.STA);
                        t.IsBackground = true;
                        t.Start(sigVal);
                    }
                }
            }
            else
            {
                if (_outs != null)
                {
                    foreach (var t1 in _outs)
                    {
                        if (t1 == null) continue;
                        //_outs[i].State = false;
                        var sigVal = new SignalValue {Signal = t1, Value = false};
                        var t = new Thread(SendSignal);
                        t.SetApartmentState(ApartmentState.STA);
                        t.IsBackground = true;
                        t.Start(sigVal);
                    }
                }
            }
        }

        private static void SendSignal(object s)
        {
            var sigVal = (SignalValue) s;
            sigVal.Signal.State = sigVal.Value;
        }

        private struct SignalValue
        {
            public DigitalSignal Signal;
            public bool Value;
        }
    }
}