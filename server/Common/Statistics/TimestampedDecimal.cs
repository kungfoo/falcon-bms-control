using System;

namespace Common.Statistics
{
    [Serializable]
    public struct TimestampedDecimal
    {
        public DateTime Timestamp;
        public double Value;
        public double CorrelatedValue;
    }
}