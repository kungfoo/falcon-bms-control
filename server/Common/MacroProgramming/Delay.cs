using System;
using System.Threading;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class Delay : Chainable
    {
        private TimeSpan _delayTime = TimeSpan.MinValue;
        private DigitalSignal _in;
        private DigitalSignal _out;

        public Delay()
        {
            In = new DigitalSignal();
            Out = new DigitalSignal();
        }

        public TimeSpan DelayTime
        {
            get => _delayTime;
            set => _delayTime = value;
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

        private void _in_SignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                if (_out != null)
                {
                    _out.State = false;
                }
                Thread.Sleep(_delayTime);
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