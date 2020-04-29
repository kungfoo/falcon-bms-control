using System;

namespace Common.Drawing.Drawing2D
{
    /// <summary>
    ///     Represents the internal data of a graphics container. This class is used when saving the state of a
    ///     <see cref="T:Common.Drawing.Graphics" /> object using the <see cref="M:Common.Drawing.Graphics.BeginContainer" />
    ///     and <see cref="M:Common.Drawing.Graphics.EndContainer(Common.Drawing.Drawing2D.GraphicsContainer)" /> methods. This
    ///     class cannot be inherited.
    /// </summary>
    public sealed class GraphicsContainer : MarshalByRefObject
    {
        private GraphicsContainer(System.Drawing.Drawing2D.GraphicsContainer graphicsContainer)
        {
            WrappedGraphicsContainer = graphicsContainer;
        }

        private System.Drawing.Drawing2D.GraphicsContainer WrappedGraphicsContainer { get; }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.GraphicsContainer" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.GraphicsContainer" /> that results from the conversion.</returns>
        /// <param name="graphicsContainer">The <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator GraphicsContainer(System.Drawing.Drawing2D.GraphicsContainer graphicsContainer)
        {
            return graphicsContainer == null ? null : new GraphicsContainer(graphicsContainer);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.GraphicsContainer" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that results from the conversion.</returns>
        /// <param name="graphicsContainer">The <see cref="T:Common.Drawing.Drawing2D.GraphicsContainer" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.GraphicsContainer(GraphicsContainer graphicsContainer)
        {
            return graphicsContainer?.WrappedGraphicsContainer;
        }
    }
}