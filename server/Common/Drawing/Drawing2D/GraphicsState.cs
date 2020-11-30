using System;

namespace Common.Drawing.Drawing2D
{
    /// <summary>
    ///     Represents the state of a <see cref="T:Common.Drawing.Graphics" /> object. This object is returned by a call
    ///     to the <see cref="M:Common.Drawing.Graphics.Save" /> methods. This class cannot be inherited.
    /// </summary>
    public sealed class GraphicsState : MarshalByRefObject
    {
        private GraphicsState(System.Drawing.Drawing2D.GraphicsState graphicsState)
        {
            WrappedGraphicsState = graphicsState;
        }

        private System.Drawing.Drawing2D.GraphicsState WrappedGraphicsState { get; }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsState" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.GraphicsState" /> that results from the conversion.</returns>
        /// <param name="graphicsState">The <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator GraphicsState(System.Drawing.Drawing2D.GraphicsState graphicsState)
        {
            return graphicsState == null ? null : new GraphicsState(graphicsState);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.GraphicsState" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.GraphicsState" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> that results from the conversion.</returns>
        /// <param name="graphicsState">The <see cref="T:Common.Drawing.Drawing2D.GraphicsState" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.GraphicsState(GraphicsState graphicsState)
        {
            return graphicsState?.WrappedGraphicsState;
        }
    }
}