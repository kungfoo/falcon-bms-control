using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.MacroProgramming
{
    [Serializable]
    public enum MathOperator
    {
        AbsoluteValue,
        Multiply,
        Divide,
        Add,
        Subtract,
        DivRem,
        Min,
        Max,
        Power,
        Sign,
        LeftShift,
        RightShift,
        Negate
    }

    [Serializable]
    public sealed class MathOperation : Chainable
    {
        private List<AnalogSignal> _ins = new List<AnalogSignal>();
        private MathOperator _op = MathOperator.Max;
        private AnalogSignal _out;

        public MathOperation()
        {
            Out = new AnalogSignal();
        }

        public List<AnalogSignal> Ins
        {
            get => _ins;
            set
            {
                if (value == null)
                {
                    value = new List<AnalogSignal>();
                }
                foreach (var t in value)
                    if (t != null)
                    {
                        t.SignalChanged += InSignalChanged;
                    }
                _ins = value;
                CalculateResult();
            }
        }

        public MathOperator Op
        {
            get => _op;
            set
            {
                _op = value;
                CalculateResult();
            }
        }

        public AnalogSignal Out
        {
            get => _out;
            set
            {
                if (value == null)
                {
                    value = new AnalogSignal();
                }
                _out = value;
                CalculateResult();
            }
        }

        private void CalculateResult()
        {
            double result = 0;
            switch (_op)
            {
                case MathOperator.AbsoluteValue:
                    result = System.Math.Abs(_ins[0].State);
                    break;
                case MathOperator.Add:
                    foreach (var t in _ins)
                        try
                        {
                            result += t.State;
                        }
                        catch (OverflowException)
                        {
                        }
                    break;
                case MathOperator.Divide:
                    result = _ins[0].State;
                    for (var i = 1; i < _ins.Count; i++)
                        try
                        {
                            result = result / _ins[i].State;
                        }
                        catch (OverflowException)
                        {
                        }
                        catch (DivideByZeroException)
                        {
                        }
                    break;
                case MathOperator.DivRem:
                    result = _ins[0].State;
                    for (var i = 1; i < _ins.Count; i++)
                        try
                        {
                            var curResult = (int) result;
                            System.Math.DivRem(curResult, (int)_ins[i].State, out var outResult);
                            result = outResult;
                        }
                        catch (OverflowException)
                        {
                        }
                        catch (DivideByZeroException)
                        {
                        }
                    break;
                case MathOperator.LeftShift:
                    result = _ins[0].State;
                    foreach (var t in _ins)
                    {
                        var numPlaces = 1;
                        try
                        {
                            numPlaces = (int) t.State;
                        }
                        catch (InvalidCastException)
                        {
                        }
                        catch (OverflowException)
                        {
                        }
                        result = (long) result << numPlaces;
                    }
                    break;
                case MathOperator.Max:
                    result = _ins[0].State;
                    result = _ins.Aggregate(result, (current, t) => System.Math.Max(current, t.State));
                    break;
                case MathOperator.Min:
                    result = _ins[0].State;
                    result = _ins.Aggregate(result, (current, t) => System.Math.Max((sbyte) current, (sbyte) t.State));
                    break;
                case MathOperator.Multiply:
                    result = _ins[0].State;
                    for (var i = 1; i < _ins.Count; i++)
                        try
                        {
                            result *= _ins[i].State;
                        }
                        catch (OverflowException)
                        {
                        }
                    break;
                case MathOperator.Negate:
                    result = 0;
                    foreach (var t in _ins)
                        try
                        {
                            result -= t.State;
                        }
                        catch (OverflowException)
                        {
                        }
                    break;
                case MathOperator.Power:
                    result = _ins[0].State;
                    for (var i = 1; i < _ins.Count; i++)
                        try
                        {
                            result *= _ins[i].State;
                        }
                        catch (OverflowException)
                        {
                        }
                    break;
                case MathOperator.RightShift:
                    result = _ins[0].State;
                    foreach (var t in _ins)
                    {
                        var numPlaces = 1;
                        try
                        {
                            numPlaces = (int) t.State;
                        }
                        catch (InvalidCastException)
                        {
                        }
                        catch (OverflowException)
                        {
                        }
                        result = (long) result >> numPlaces;
                    }
                    break;
                case MathOperator.Sign:
                    foreach (var t in _ins)
                        try
                        {
                            result += System.Math.Sign(t.State);
                        }
                        catch (OverflowException)
                        {
                        }

                    break;
                case MathOperator.Subtract:
                    result = _ins[0].State;
                    for (var i = 1; i < _ins.Count; i++)
                        try
                        {
                            result -= _ins[i].State;
                        }
                        catch (OverflowException)
                        {
                        }
                    break;
            }
            _out.State = result;
        }

        private void InSignalChanged(object sender, AnalogSignalChangedEventArgs e)
        {
            CalculateResult();
        }
    }
}