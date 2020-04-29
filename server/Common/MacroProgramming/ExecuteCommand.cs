using System;
using System.Diagnostics;
using log4net;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class ExecuteCommand : Chainable
    {
        private string _args;
        private string _command;
        private DigitalSignal _in;
        private DigitalSignal _out;
        private static readonly ILog _log = LogManager.GetLogger(typeof(ExecuteCommand));
        public ExecuteCommand()
        {
            In = new DigitalSignal();
            Out = new DigitalSignal();
        }

        public string Args
        {
            get => _args;
            set => _args = value;
        }

        public string Command
        {
            get => _command;
            set => _command = value;
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

        private void Execute()
        {
            try
            {
                Process.Start(_command, _args);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }

        private void InSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                if (_out != null)
                {
                    _out.State = false;
                }
                Execute();
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