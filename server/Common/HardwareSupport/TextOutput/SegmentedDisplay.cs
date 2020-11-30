using System;
using Common.MacroProgramming;

namespace Common.HardwareSupport.TextOutput
{
    [Serializable]
    public abstract class SegmentedDisplay : TextDisplay
    {
        protected SegmentedDisplay()
        {
        }

        protected SegmentedDisplay(TextSignal displayText) : base(displayText)
        {
        }
    }
}