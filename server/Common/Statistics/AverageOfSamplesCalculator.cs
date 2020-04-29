using System.Collections.Generic;

namespace Common.Statistics
{
    internal interface IAverageOfSamplesCalculator
    {
        double AverageSampleValue(List<TimestampedDecimal> values);
    }

    internal class AverageOfSamplesCalculator : IAverageOfSamplesCalculator
    {
        public double AverageSampleValue(List<TimestampedDecimal> values)
        {
            var sum = 0.0;
            for (var i = 0; i < values.Count; i++)
                sum += values[i].Value;

            var avg = values.Count > 0 ? sum / values.Count : 0;
            return avg;
        }
    }
}