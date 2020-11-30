using System;
using System.Xml.Serialization;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class DigitalSignalChangedEventArgs : EventArgs
    {
        public DigitalSignalChangedEventArgs(bool currentState, bool previousState)
        {
            CurrentState = currentState;
            PreviousState = previousState;
        }

        public bool CurrentState { get; }

        public bool PreviousState { get; }
    }

    [Serializable]
    public class DigitalSignal : Signal
    {
        public delegate void SignalChangedEventHandler(object sender, DigitalSignalChangedEventArgs args);

        [NonSerialized] private Inverter _inverter;
        [NonSerialized] private bool _state;

        public override bool HasListeners => SignalChanged != null;

        [XmlIgnore]
        public DigitalSignal Inverse
        {
            get
            {
                if (_inverter == null)
                {
                    _inverter = new Inverter {In = this};
                }
                return _inverter.Out;
            }
        }

        public override string SignalType => "Digital / Boolean";

        [XmlIgnore]
        public bool State
        {
            get => _state;
            set
            {
                if (_state == value) return;
                var previousState = _state;
                _state = value;
                SignalChanged?.Invoke(this, new DigitalSignalChangedEventArgs(value, previousState));
            }
        }

        [field: NonSerialized]
        public event SignalChangedEventHandler SignalChanged;
    }
}