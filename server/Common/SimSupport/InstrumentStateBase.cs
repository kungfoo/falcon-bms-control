using System;

namespace Common.SimSupport
{
    [Serializable]
    public abstract class InstrumentStateBase
    {
        public override bool Equals(object obj)
        {
            return obj is InstrumentStateBase && obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Serialization.Util.ToRawBytes(this);
        }
    }
}