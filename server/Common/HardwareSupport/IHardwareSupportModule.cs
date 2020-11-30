using System.Drawing;
using Common.MacroProgramming;

namespace Common.HardwareSupport
{
    public interface IHardwareSupportModule
    {
        AnalogSignal[] AnalogInputs { get; }
        AnalogSignal[] AnalogOutputs { get; }
        DigitalSignal[] DigitalInputs { get; }
        DigitalSignal[] DigitalOutputs { get; }
        TextSignal[] TextInputs { get; }
        TextSignal[] TextOutputs { get; }
        string FriendlyName { get; }
        void Render(Graphics g, Rectangle destinationRectangle);
        void Synchronize();
    }
}