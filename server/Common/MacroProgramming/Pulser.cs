using System;
using System.Threading;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class Pulser : Chainable
    {
        private DigitalSignal _in;
        private volatile bool _keepRunning;
        private Macro _macro = new Macro();
        private DigitalSignal _out;
        private TimeSpan _timeHigh = TimeSpan.Zero;
        private TimeSpan _timeLow = TimeSpan.Zero;
        [NonSerialized] private Thread _workerThread;

        public Pulser()
        {
            In = new DigitalSignal();
            Out = new DigitalSignal();
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
                _in = value;
                _in.SignalChanged += _in_SignalChanged;
            }
        }

        public Macro Macro
        {
            get => _macro;
            set
            {
                if (value == null)
                {
                    value = new Macro();
                }
                _macro = value;
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

        public TimeSpan TimeHigh
        {
            get => _timeHigh;
            set => _timeHigh = value;
        }

        public TimeSpan TimeLow
        {
            get => _timeLow;
            set => _timeLow = value;
        }

        private void _in_SignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

        private void PulseWork()
        {
            if (_out != null)
            {
                _out.State = false;
            }
            do
            {
                if (_macro != null)
                {
                    _macro.In.State = true;
                }
                Thread.Sleep(_timeHigh);
                if (_macro != null)
                {
                    _macro.In.State = false;
                }
                Thread.Sleep(_timeLow);
            } while (_keepRunning);
            if (_out != null)
            {
                _out.State = true;
            }
        }

        private void Start()
        {
            if (_workerThread != null && _workerThread.IsAlive)
            {
                Util.AbortThread(_workerThread);
            }
            _workerThread = new Thread(PulseWork);
            _workerThread.SetApartmentState(ApartmentState.STA);
            _workerThread.IsBackground = true;
            _keepRunning = true;
            _workerThread.Start();
        }

        private void Stop()
        {
            _keepRunning = false;
            if (_workerThread != null)
            {
                if (_workerThread.IsAlive)
                {
                    Util.AbortThread(_workerThread);
                }
            }
            if (_macro != null)
            {
                _macro.In.State = false;
            }
            if (_out != null)
            {
                _out.State = false;
            }
        }
    }
}