using System;
using System.Collections.Generic;

namespace Common.Statistics
{
    public interface IMedianOfSamplesCalculator
    {
        double MedianSampleValue(List<TimestampedDecimal> values);
    }

    public class MedianOfSamplesCalculator : IMedianOfSamplesCalculator
    {
        public double MedianSampleValue(List<TimestampedDecimal> values)
        {
            if (values.Count == 0)
            {
                return 0;
            }

            var justTheValues = new double[values.Count];
            for (var i = 0; i < values.Count; i++)
                justTheValues[i] = values[i].Value;

            Array.Sort(justTheValues);

            var itemIndex = justTheValues.Length / 2;

            if (justTheValues.Length % 2 == 0)
            {
                // Even number of items.
                return (justTheValues[itemIndex] + justTheValues[itemIndex - 1]) / 2;
            }
            // Odd number of items.
            return justTheValues[itemIndex];
        }
    }
}