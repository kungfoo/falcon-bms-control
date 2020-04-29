using System;
using System.Threading;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class Macro : Chainable
    {
        private DigitalSignal _in;
        private volatile bool _keepWaiting;
        private DigitalSignal _out;
        private DigitalSignal _toStart;
        private DigitalSignal _waitFor;
        [NonSerialized] private Thread _workerThread;

        public Macro()
        {
            ToStart = new DigitalSignal();
            WaitFor = new DigitalSignal();
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
                value.SignalChanged += InSignalChanged;
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

        public DigitalSignal ToStart
        {
            get => _toStart;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal();
                }
                _toStart = value;
            }
        }

        public DigitalSignal WaitFor
        {
            get => _waitFor;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal();
                }
                value.SignalChanged += WaitForSignalChanged;
                _waitFor = value;
            }
        }

        public void Stop()
        {
            _keepWaiting = false;
            if (_workerThread != null && _workerThread.IsAlive)
            {
                Util.AbortThread(_workerThread);
            }
            _toStart.State = false;
        }

        private void InSignalChanged(object sender, DigitalSignalChangedEventArgs e)
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
            _keepWaiting = true;
            _workerThread.Start();
            _workerThread.Join();
        }

        private void WaitForSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                _keepWaiting = false;
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

        private void Work()
        {
            _waitFor.State = false;
            if (_toStart != null)
            {
                _toStart.State = true;
            }
            while (_keepWaiting)
                Thread.Sleep(20);
            if (_toStart != null) _toStart.State = false;
        }
    }
}