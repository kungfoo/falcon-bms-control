using System;

namespace Common.SimSupport
{
    public class UnitTimeTickEventArgs : EventArgs
    {
        public UnitTimeTickEventArgs()
        {
        }

        public UnitTimeTickEventArgs(bool currentSignalLineState)
        {
            CurrentSignalLineState = currentSignalLineState;
        }

        public bool CurrentSignalLineState { get; set; }
    }
}