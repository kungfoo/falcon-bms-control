using System;
using System.Threading;
using Common.Win32;

namespace Common.MacroProgramming
{
    [Serializable]
    public sealed class MouseRotator : Chainable
    {
        private int _degreesRotation;
        private DigitalSignal _in;
        private Macro _macro = new Macro();
        private int _numSteps;
        private DigitalSignal _out;
        private int _radius;
        private int _startAngle;
        private TimeSpan _stepDelay = TimeSpan.Zero;
        private int _xCenter;
        private int _yCenter;

        public MouseRotator()
        {
            In = new DigitalSignal();
            Out = new DigitalSignal();
        }

        public int DegreesRotation
        {
            get => _degreesRotation;
            set => _degreesRotation = value;
        }

        public DigitalSignal In
        {
            get => _in;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal();
                }
                value.SignalChanged += InSignalChanged;
                _in = value;
            }
        }

        public Macro Macro
        {
            get => _macro;
            set
            {
                if (value == null)
                {
                    value = new Macro();
                }
                _macro = value;
            }
        }

        public int NumSteps
        {
            get => _numSteps;
            set => _numSteps = value;
        }

        public DigitalSignal Out
        {
            get => _out;
            set
            {
                if (value == null)
                {
                    value = new DigitalSignal();
                }
                _out = value;
            }
        }

        public int Radius
        {
            get => _radius;
            set => _radius = value;
        }

        public int StartAngle
        {
            get => _startAngle;
            set => _startAngle = value;
        }

        public TimeSpan StepDelay
        {
            get => _stepDelay;
            set => _stepDelay = value;
        }

        public int XCenter
        {
            get => _xCenter;
            set => _xCenter = value;
        }

        public int YCenter
        {
            get => _yCenter;
            set => _yCenter = value;
        }

        private void InSignalChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            if (e.CurrentState)
            {
                if (_out != null)
                {
                    _out.State = false;
                }
                double degStartAngle = _startAngle;
                while (degStartAngle >= 360)
                    degStartAngle -= 360;
                const double radiansPerDegree = 2 * System.Math.PI / 360;
                var radStartAngle = degStartAngle * radiansPerDegree;
                var radDesiredRotation = _degreesRotation * radiansPerDegree;
                var radiansPerStep = 2 * System.Math.PI / _numSteps;
                if (_degreesRotation < 0)
                {
                    radiansPerStep = 0 - radiansPerStep;
                }
                var firstStep = true;
                for (var radAngle = radStartAngle;
                    System.Math.Abs(radAngle - radStartAngle) <
                    System.Math.Abs(radDesiredRotation);
                    radAngle += radiansPerStep)
                {
                    var x = _xCenter + (int) (_radius * System.Math.Sin(System.Math.PI - radAngle));
                    var y = _yCenter + (int) (_radius * System.Math.Cos(System.Math.PI - radAngle));
                    if (_macro != null && !firstStep)
                    {
                        _macro.In.State = true;
                    }
                    if (firstStep)
                    {
                        firstStep = false;
                    }
                    KeyAndMouseFunctions.MouseMoveAbsolute(x, y);
                    if (_macro != null)
                    {
                        _macro.In.State = false;
                    }
                    Thread.Sleep(_stepDelay);
                }
                if (_out != null)
                {
                    _out.State = true;
                }
            }
            else
            {
                if (_macro != null)
                {
                    _macro.In.State = false;
                }
                if (_out != null)
                {
                    _out.State = false;
                }
            }
        }
    }
}