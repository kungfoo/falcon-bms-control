using System;

namespace Common.InputSupport.Phcc
{
    public struct PHCCInputState : ICloneable
    {
        public short[] AnalogInputs;
        public bool[] DigitalInputs;

        public PHCCInputState(bool[] digitalInputs, short[] analogInputs) : this()
        {
            DigitalInputs = digitalInputs;
            AnalogInputs = analogInputs;
        }

        public object Clone()
        {
            var clone = new PHCCInputState
            {
                DigitalInputs = (bool[]) DigitalInputs.Clone(),
                AnalogInputs = (short[]) AnalogInputs.Clone()
            };
            return clone;
        }
    }
}