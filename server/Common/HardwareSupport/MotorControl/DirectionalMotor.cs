using System;
using Common.MacroProgramming;

namespace Common.HardwareSupport.MotorControl
{
    [Serializable]
    public class DirectionalMotor : MotorControlBase
    {
        private AnalogSignal _speedAndDirection;

        public DirectionalMotor()
        {
            SpeedAndDirection = new AnalogSignal();
        }

        public AnalogSignal SpeedAndDirection
        {
            get => _speedAndDirection;
            set
            {
                _speedAndDirection = value;
                if (_speedAndDirection != null)
                {
                    _speedAndDirection.SignalChanged += OnSpeedAndDirectionSignalChanged;
                }
            }
        }

        protected virtual void OnSpeedAndDirectionSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            PhysicalOutput.State = args.CurrentState;
        }
    }
}