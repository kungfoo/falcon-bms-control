using System;
using Common.MacroProgramming;

namespace Common.HardwareSupport.MotorControl
{
    [Serializable]
    public abstract class MotorControlBase : CompositeControl
    {
        public AnalogSignal PhysicalOutput { get; set; }
    }
}