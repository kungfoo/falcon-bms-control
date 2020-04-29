using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public enum LogicOperation
    {
        Not,
        And,
        Or,
        Nand,
        Nor,
        Xor,
        XNor,
        False,
        True
    }

    [Serializable]
    public sealed class LogicGate : Chainable
    {
        private DigitalSignal _in1;
        private DigitalSignal _in2;
        private LogicOperation _operation = LogicOperation.False;
        private DigitalSignal _out;

        public LogicGate()
        {
            In1 = new DigitalSignal();
            In2 = new DigitalSignal();
            Out = new DigitalSignal();
        }

        public DigitalSignal In1
        {
            get => _in1;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal();
                }
                value.SignalChanged += _in_SignalChanged;
                _in1 = value;
            }
        }

        public DigitalSignal In2
        {
            get => _in2;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal();
                }
                value.SignalChanged += _in_SignalChanged;
                _in2 = value;
            }
        }

        public LogicOperation Operation
        {
            get => _operation;
            set => _operation = value;
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
            if (_out == null)
            {
                return;
            }
            switch (_operation)
            {
                case LogicOperation.And:
                    _out.State = _in1.State & _in2.State;
                    break;
                case LogicOperation.False:
                    _out.State = !_in1.State && !_in2.State;
                    break;
                case LogicOperation.Nand:
                    _out.State = !(_in1.State & _in2.State);
                    break;
                case LogicOperation.Nor:
                    _out.State = !(_in1.State | _in2.State);
                    break;
                case LogicOperation.Not:
                    _out.State = !_in1.State;
                    break;
                case LogicOperation.Or:
                    _out.State = _in1.State | _in2.State;
                    break;
                case LogicOperation.True:
                    _out.State = _in1.State && _in2.State;
                    break;
                case LogicOperation.XNor:
                    _out.State = !(_in1.State ^ _in2.State);
                    break;
                case LogicOperation.Xor:
                    _out.State = _in1.State ^ _in2.State;
                    break;
            }
        }
    }
}