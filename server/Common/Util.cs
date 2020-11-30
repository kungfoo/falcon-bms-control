using System;
using log4net;

namespace Common
{
    public static class Util
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Util));

        public static void DisposeObject(object obj)
        {
            if (obj == null) return;
            try
            {
                var disposable = obj as IDisposable;
                disposable?.Dispose();
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }

        public static byte SetBit(byte bits, int index, bool newVal)
        {
            var toReturn = bits;
            if (newVal)
            {
                toReturn |= (byte) (int) System.Math.Pow(2, index);
            }
            else
            {
                toReturn &= (byte) ~(int) System.Math.Pow(2, index);
            }
            return toReturn;
        }
    }
}