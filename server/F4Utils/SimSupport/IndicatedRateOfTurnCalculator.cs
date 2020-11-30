using System;
using System.Collections.Generic;
using Common.Math;
using Common.Statistics;

namespace F4Utils.SimSupport
{
    public interface IIndicatedRateOfTurnCalculator
    {
        float DetermineIndicatedRateOfTurn(float currentMagneticHeadingDecimalDegrees);
        void Reset();
    }

    public class IndicatedRateOfTurnCalculator : IIndicatedRateOfTurnCalculator
    {
        public const float MAX_INDICATED_RATE_OF_TURN_DECIMAL_DEGREES_PER_SECOND = 3.0f;
        private TimestampedDecimal _lastHeadingSample = new TimestampedDecimal {Timestamp = DateTime.UtcNow, Value = 180};
        private List<TimestampedDecimal> _lastInstantaneousRatesOfTurn = new List<TimestampedDecimal>();
        private readonly IMedianOfSamplesCalculator _medianOfSamplesCalculator = new MedianOfSamplesCalculator();
        private float _lastIndicatedRateOfTurn = -0.01f;

        public float DetermineIndicatedRateOfTurn(float currentMagneticHeadingDecimalDegrees)
        {
            //capture the current time
            var curTime = DateTime.UtcNow;

            //determine how many seconds it's been since our last "current heading" datum snapshot?
            var dT = (float) curTime.Subtract(_lastHeadingSample.Timestamp).TotalMilliseconds;
            if (dT < 100) return _lastIndicatedRateOfTurn;
            //determine the change in heading since our last snapshot
            var currentHeading = currentMagneticHeadingDecimalDegrees;
            var headingDelta = Util.AngleDelta((float) _lastHeadingSample.Value, currentHeading);

            //now calculate the instantaneous rate of turn
            var currentInstantaneousRateOfTurn = headingDelta / dT * 1000;
            if (Math.Abs(currentInstantaneousRateOfTurn) > 30 || float.IsInfinity(currentInstantaneousRateOfTurn) || float.IsNaN(currentInstantaneousRateOfTurn))
            {
                currentInstantaneousRateOfTurn = 0; //noise
            }

            if (currentInstantaneousRateOfTurn == 0) return _lastIndicatedRateOfTurn;

            if (Math.Abs(currentInstantaneousRateOfTurn) > MAX_INDICATED_RATE_OF_TURN_DECIMAL_DEGREES_PER_SECOND + 0.75f)
            {
                currentInstantaneousRateOfTurn = (MAX_INDICATED_RATE_OF_TURN_DECIMAL_DEGREES_PER_SECOND + 0.75f) * Math.Sign(currentInstantaneousRateOfTurn);
            }

            var sample = new TimestampedDecimal {Timestamp = curTime, Value = currentInstantaneousRateOfTurn};

            //cull historic rate-of-turn samples older than n seconds
            var replacementList = new List<TimestampedDecimal>();
            for (var i = 0; i < _lastInstantaneousRatesOfTurn.Count; i++)
            {
                if (!(Math.Abs(curTime.Subtract(_lastInstantaneousRatesOfTurn[i].Timestamp).TotalMilliseconds) > 1000)) { replacementList.Add(_lastInstantaneousRatesOfTurn[i]); }
            }

            _lastInstantaneousRatesOfTurn = replacementList;

            _lastInstantaneousRatesOfTurn.Add(sample);

            var medianRateOfTurn = (float) Math.Round(_medianOfSamplesCalculator.MedianSampleValue(_lastInstantaneousRatesOfTurn), 1);
            var indicatedRateOfTurn = _lastIndicatedRateOfTurn;
            const float minIncrement = 0.1f;
            while (medianRateOfTurn < indicatedRateOfTurn - minIncrement) indicatedRateOfTurn -= minIncrement;
            while (medianRateOfTurn > indicatedRateOfTurn + minIncrement) indicatedRateOfTurn += minIncrement;

            if (Math.Round(medianRateOfTurn, 1) == 0) { indicatedRateOfTurn = 0; }
            else if (medianRateOfTurn == indicatedRateOfTurn - minIncrement) { indicatedRateOfTurn = medianRateOfTurn; }
            else if (medianRateOfTurn == indicatedRateOfTurn + minIncrement) { indicatedRateOfTurn = medianRateOfTurn; }

            _lastHeadingSample = new TimestampedDecimal {Timestamp = curTime, Value = currentMagneticHeadingDecimalDegrees};
            _lastIndicatedRateOfTurn = LimitRateOfTurn(indicatedRateOfTurn);
            return indicatedRateOfTurn;
        }

        public void Reset()
        {
            _lastInstantaneousRatesOfTurn = new List<TimestampedDecimal>();
            _lastIndicatedRateOfTurn = 0;
        }

        private static float LimitRateOfTurn(float rateOfTurnDegreesPerSecond)
        {
            var indicatedRateOfTurnDegreesPerSecond = rateOfTurnDegreesPerSecond;

            /*  LIMIT INDICATED RATE OF TURN TO BE WITHIN CERTAIN OUTER BOUNDARIES */
            const float maxIndicatedRateOfTurnDegreesPerSecond = MAX_INDICATED_RATE_OF_TURN_DECIMAL_DEGREES_PER_SECOND + 0.75f;
            const float minIndicatedRateOfTurnDegreesPerSecond = -maxIndicatedRateOfTurnDegreesPerSecond;

            if (rateOfTurnDegreesPerSecond < minIndicatedRateOfTurnDegreesPerSecond) { indicatedRateOfTurnDegreesPerSecond = minIndicatedRateOfTurnDegreesPerSecond; }
            else if (rateOfTurnDegreesPerSecond > maxIndicatedRateOfTurnDegreesPerSecond) { indicatedRateOfTurnDegreesPerSecond = maxIndicatedRateOfTurnDegreesPerSecond; }
            return indicatedRateOfTurnDegreesPerSecond;
        }
    }
}