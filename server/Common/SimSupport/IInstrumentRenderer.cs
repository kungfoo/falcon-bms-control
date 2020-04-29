using System;
using Common.Drawing;

namespace Common.SimSupport
{
    public interface IInstrumentRenderer : IDisposable
    {
        InstrumentStateBase GetState();
        void Render(Graphics destinationGraphics, Rectangle destinationRectangle);
    }
}