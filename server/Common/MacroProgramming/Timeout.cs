using System;
using System.Threading;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class Timeout : Chainable
    {
        private DigitalSignal _in;
        private Macro _macro = new Macro();
        private DigitalSignal _out;
        private TimeSpan _runningTime = TimeSpan.Zero;
        [NonSerialized] private Thread _workerThread;

        public Timeout()
        {
            _in = new DigitalSignal();
            _out = new DigitalSignal();
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

        public TimeSpan RunningTime
        {
            get => _runningTime;
            set => _runningTime = value;
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

        private void Start()
        {
            if (_workerThread != null && _workerThread.IsAlive)
            {
                Util.AbortThread(_workerThread);
            }
            _workerThread = new Thread(Work);
            _workerThread.SetApartmentState(ApartmentState.STA);
            _workerThread.IsBackground = true;
            _workerThread.Start();
            _workerThread.Join(_runningTime);
            Util.AbortThread(_workerThread);

            if (_macro != null)
            {
                _macro.In.State = false;
            }
            if (_out != null)
            {
                _out.State = true;
            }
        }

        private void Stop()
        {
            if (_workerThread != null && _workerThread.IsAlive)
            {
                Util.AbortThread(_workerThread);
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

        private void Work()
        {
            if (_out != null)
            {
                _out.State = false;
            }
            if (_macro != null)
            {
                _macro.In.State = true;
            }
            if (_out != null)
            {
                _out.State = true;
            }
        }
    }
}