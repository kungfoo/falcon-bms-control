using System.Drawing;
using System.Threading;
using Common.MacroProgramming;

namespace Common.HardwareSupport
{
    public abstract class HardwareSupportModuleBase : IHardwareSupportModule
    {
        public virtual AnalogSignal[] AnalogInputs { get { return null; } }
        public virtual AnalogSignal[] AnalogOutputs { get { return null; } }
        public virtual TextSignal[] TextInputs { get { return null; } }
        public virtual DigitalSignal[] DigitalInputs { get { return null; } }
        public virtual DigitalSignal[] DigitalOutputs { get { return null; } }
        public virtual TextSignal[] TextOutputs { get { return null; } }
        public abstract string FriendlyName { get; }

        public virtual void Render(Graphics g, Rectangle destinationRectangle)
        {
            Thread.Sleep(0);
        }

        public virtual void Synchronize()
        {
            Thread.Sleep(0);
        }
    }
}