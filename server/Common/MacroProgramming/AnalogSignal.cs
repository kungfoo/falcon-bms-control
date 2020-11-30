using System;
using System.Xml.Serialization;
using Common.Statistics;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class AnalogSignalChangedEventArgs : EventArgs
    {
        public AnalogSignalChangedEventArgs(double currentState, double previousState,
            double currentCorrelatedState = 0)
        {
            CurrentState = currentState;
            CurrentCorrelatedState = currentCorrelatedState;
            PreviousState = previousState;
        }

        public double CurrentCorrelatedState { get; }

        public double CurrentState { get; }

        public double PreviousState { get; }
    }

    [Serializable]
    public class AnalogSignal : Signal
    {
        public delegate void AnalogSignalChangedEventHandler(object sender, AnalogSignalChangedEventArgs args);

        private TimestampedDecimal _correlatedState = new TimestampedDecimal {Timestamp = DateTime.UtcNow, Value = 0};
        private bool _isCosine;
        private bool _isSine;
        private int _precision = -1; //# decimal places to round values to
        private TimestampedDecimal _previousState = new TimestampedDecimal {Timestamp = DateTime.UtcNow, Value = 0};
        private TimestampedDecimal _state = new TimestampedDecimal {Timestamp = DateTime.UtcNow, Value = 0};

        [XmlIgnore]
        public virtual double CorrelatedState
        {
            get => _correlatedState.Value;
            set
            {
                var newVal =
                    _precision != -1
                        ? System.Math.Round(value, _precision)
                        : value;
                if (double.IsInfinity(newVal) || double.IsNaN(newVal))
                {
                    newVal = 0;
                }
                if (newVal != _correlatedState.Value)
                {
                    _correlatedState = new TimestampedDecimal {Timestamp = DateTime.UtcNow, Value = newVal};
                }
            }
        }

        public override bool HasListeners => SignalChanged != null;

        public bool IsAngle { get; set; }

        public bool IsCosine
        {
            get => _isCosine;
            set
            {
                _isCosine = value;
                _isSine = false;
                IsAngle = true;
                MinValue = -1;
                MaxValue = 1;
            }
        }

        public bool IsPercentage { get; set; }

        public bool IsSine
        {
            get => _isSine;
            set
            {
                _isSine = value;
                _isCosine = false;
                IsAngle = true;
                MinValue = -1;
                MaxValue = 1;
            }
        }

        public bool IsVoltage { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }

        public int Precision
        {
            get => _precision;
            set => _precision = value;
        }

        [XmlIgnore]
        public override string SignalType => "Analog / Numeric";

        [XmlIgnore]
        public virtual double State
        {
            get => _state.Value;
            set
            {
                var newVal =
                    _precision != -1
                        ? System.Math.Round(value, _precision)
                        : value;
                if (double.IsInfinity(newVal) || double.IsNaN(newVal))
                {
                    newVal = 0;
                }
                if (newVal != _state.Value)
                {
                    _previousState = _state;
                    _state = new TimestampedDecimal {Timestamp = DateTime.UtcNow, Value = newVal};
                    UpdateEventListeners();
                }
            }
        }

        public double? TimeConstant { get; set; }

        public virtual TimestampedDecimal TimestampedState => _state;

        [field: NonSerialized]
        public event AnalogSignalChangedEventHandler SignalChanged;

        protected virtual void UpdateEventListeners()
        {
            SignalChanged?.Invoke(this, new AnalogSignalChangedEventArgs(State, _previousState.Value, CorrelatedState));
        }
    }
}