using System;
using Common.MacroProgramming;

namespace Common.HardwareSupport.TextOutput
{
    [Serializable]
    public class DotMatrixCharacterDisplay : TextDisplay
    {
        public DotMatrixCharacterDisplay(TextSignal displayText) : base(displayText)
        {
        }

        private DotMatrixCharacterDisplay()
        {
        }

        protected override void UpdateOutputSignals()
        {
        }
    }
}