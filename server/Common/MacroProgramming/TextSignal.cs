using System;
using System.Xml.Serialization;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class TextSignalChangedEventArgs : EventArgs
    {
        public TextSignalChangedEventArgs(string currentState, string previousState)
        {
            CurrentState = currentState;
            PreviousState = previousState;
        }

        public string CurrentState { get; }

        public string PreviousState { get; }
    }

    [Serializable]
    public class TextSignal : Signal
    {
        public delegate void TextSignalChangedEventHandler(object sender, TextSignalChangedEventArgs args);

        [NonSerialized] private string _state = string.Empty;

        public override bool HasListeners => SignalChanged != null;

        public override string SignalType => "Text / Characters";

        [XmlIgnore]
        public string State
        {
            get => _state;
            set
            {
                if (_state == value) return;
                var previousState = _state;
                _state = value;
                SignalChanged?.Invoke(this, new TextSignalChangedEventArgs(value, previousState));
            }
        }

        [field: NonSerialized]
        public event TextSignalChangedEventHandler SignalChanged;
    }
}