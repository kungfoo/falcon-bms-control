using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class Join : Chainable
    {
        private List<DigitalSignal> _ins = new List<DigitalSignal>();
        private DigitalSignal _out;

        public Join()
        {
            Out = new DigitalSignal();
        }

        public List<DigitalSignal> Ins
        {
            get => _ins;
            set
            {
                if (value == null)
                {
                    value = new List<DigitalSignal>();
                }
                foreach (var t in value.Where(t => t != null))
                    t.SignalChanged += InSignalChanged;
                _ins = value;
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

        public DigitalSignal CreateAdditionalIn()
        {
            var newSig = new DigitalSignal();
            _ins.Add(newSig);
            return newSig;
        }

        private void InSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                var bPulse = true;
                if (_ins != null)
                {
                    bPulse = _ins.All(t => t.State);
                }
                if (_out != null)
                {
                    _out.State = bPulse;
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