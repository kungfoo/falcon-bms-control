using System;
using System.Linq;
using Common.Drawing;

namespace Common.SimSupport
{
    public interface IInstrumentRendererBase
    {
        InstrumentStateBase GetState();
        void Render(Graphics destinationGraphics, Rectangle destinationRectangle);
    }

    public abstract class InstrumentRendererBase : IInstrumentRenderer, IInstrumentRendererBase
    {
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public InstrumentStateBase GetState()
        {
            var props = GetType().GetProperties();
            InstrumentStateBase state = null;
            foreach (var prop in props.Where(prop => prop.Name == "InstrumentState"))
                state = (InstrumentStateBase) prop.GetGetMethod().Invoke(this, null);
            return state;
        }

        public abstract void Render(Graphics destinationGraphics, Rectangle destinationRectangle);

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}