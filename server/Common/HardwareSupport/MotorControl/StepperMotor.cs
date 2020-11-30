using System;
using Common.MacroProgramming;

namespace Common.HardwareSupport.MotorControl
{
    [Serializable]
    public class StepperMotor : PositionableMotorWithFeedback
    {
        public StepperMotor()
        {
            NumStepsInRangeOfTravel = 0;
        }

        public int NumStepsInRangeOfTravel { get; set; }

        protected override void OnGoToPositionSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            if (MinPositionReachedSignal != null && MinPositionReachedSignal.State &&
                args.CurrentState <= args.PreviousState)
            {
                return;
            }
            if (MaxPositionReachedSignal != null && MaxPositionReachedSignal.State &&
                args.CurrentState >= args.PreviousState)
            {
                return;
            }
            var changeInValue = args.CurrentState - args.PreviousState;
            PhysicalOutput.State = changeInValue * NumStepsInRangeOfTravel;
            base.OnGoToPositionSignalChanged(sender, args);
        }
    }
}