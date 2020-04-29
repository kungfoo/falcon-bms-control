using Common.MacroProgramming;

namespace Common.HardwareSupport.MotorControl
{
    public static class MotorControlFactory
    {
        public static DirectionalMotor CreateDirectionalMotor
        (
            AnalogSignal speedAndDirection,
            AnalogSignal outputLine
        )
        {
            var motor = new DirectionalMotor {SpeedAndDirection = speedAndDirection, PhysicalOutput = outputLine};
            return motor;
        }

        public static PositionableMotor CreatePositionableMotor
        (
            AnalogSignal goToPositionSignal,
            AnalogSignal outputLine
        )
        {
            var toReturn = new PositionableMotor
            {
                GoToPositionSignal = goToPositionSignal,
                PhysicalOutput = outputLine
            };
            return toReturn;
        }

        public static PositionableMotorWithFeedback CreateServoMechanism
        (
            AnalogSignal goToPositionSignal,
            AnalogSignal absolutePositionSensingSignal,
            Range minPositionRange,
            Range maxPositionRange,
            AnalogSignal outputLine
        )
        {
            var motor = new PositionableMotorWithFeedback {GoToPositionSignal = goToPositionSignal};
            var minPositionReachedRE = new RangeEvaluator {Range = minPositionRange, In = goToPositionSignal};
            motor.MinPositionReachedSignal = minPositionReachedRE.Out;
            var maxPositionReachedRE = new RangeEvaluator {In = goToPositionSignal};
            motor.MaxPositionReachedSignal = maxPositionReachedRE.Out;
            motor.PhysicalOutput = outputLine;
            return motor;
        }

        public static StepperMotor CreateStepperMotorControlWithAbsolutePositionSensing
        (
            AnalogSignal goToPositionSignal,
            AnalogSignal absolutePositionSensingSignal,
            Range minPositionRange,
            Range maxPositionRange,
            int numStepsInTravelRange,
            AnalogSignal outputLine
        )
        {
            var stepper = new StepperMotor
            {
                GoToPositionSignal = goToPositionSignal,
                NumStepsInRangeOfTravel = numStepsInTravelRange
            };
            var minPositionReachedRE = new RangeEvaluator {Range = minPositionRange, In = goToPositionSignal};
            stepper.MinPositionReachedSignal = minPositionReachedRE.Out;
            var maxPositionReachedRE = new RangeEvaluator {In = goToPositionSignal};
            stepper.MaxPositionReachedSignal = maxPositionReachedRE.Out;
            stepper.PhysicalOutput = outputLine;
            return stepper;
        }

        public static StepperMotor CreateStepperMotorControlWithBoundaryPositionSensing
        (
            AnalogSignal goToPositionSignal,
            int numStepsInTravelRange,
            DigitalSignal minPositionReachedSignal,
            DigitalSignal maxPositionReachedSignal,
            AnalogSignal outputLine
        )
        {
            var stepper = new StepperMotor
            {
                GoToPositionSignal = goToPositionSignal,
                NumStepsInRangeOfTravel = numStepsInTravelRange,
                MinPositionReachedSignal = minPositionReachedSignal,
                MaxPositionReachedSignal = maxPositionReachedSignal,
                PhysicalOutput = outputLine
            };
            return stepper;
        }

        public static StepperMotor CreateStepperMotorControlWithPhysicalBoundary(
            AnalogSignal goToPositionSignal,
            int numStepsInTravelRange,
            AnalogSignal outputLine
        )
        {
            var stepper = new StepperMotor
            {
                GoToPositionSignal = goToPositionSignal,
                NumStepsInRangeOfTravel = numStepsInTravelRange,
                PhysicalOutput = outputLine
            };
            return stepper;
        }
    }
}