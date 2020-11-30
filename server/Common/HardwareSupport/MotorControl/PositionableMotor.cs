using System;
using Common.MacroProgramming;

namespace Common.HardwareSupport.MotorControl
{
    [Serializable]
    public class PositionableMotor : MotorControlBase
    {
        private AnalogSignal _goToPositionSignal;

        public PositionableMotor()
        {
            GoToPositionSignal = new AnalogSignal();
        }

        public AnalogSignal GoToPositionSignal
        {
            get => _goToPositionSignal;
            set
            {
                _goToPositionSignal = value;
                if (_goToPositionSignal == null) return;
                if (PhysicalOutput != null)
                {
                    PhysicalOutput.State = 1.0;
                }
                _goToPositionSignal.SignalChanged += OnGoToPositionSignalChanged;
            }
        }

        protected virtual void OnGoToPositionSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            PhysicalOutput.State = args.CurrentState;
        }
    }
}