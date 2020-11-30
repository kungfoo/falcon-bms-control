using System;

namespace Common.MacroProgramming
{
    [Serializable]
    public class CalibrationPoint
    {
        public CalibrationPoint()
        {
        }

        public CalibrationPoint(double Input, double Output) : this()
        {
            this.Input = Input;
            this.Output = Output;
        }

        public double Input { get; set; }
        public double Output { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CalibrationPoint && (obj as CalibrationPoint).Input == Input && (obj as CalibrationPoint).Output == Output;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"Input:{Input}, Output:{Output}";
        }
    }
}