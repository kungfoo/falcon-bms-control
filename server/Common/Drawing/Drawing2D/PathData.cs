using System.Linq;

namespace Common.Drawing.Drawing2D
{
    /// <summary>
    ///     Contains the graphical data that makes up a <see cref="T:Common.Drawing.Drawing2D.GraphicsPath" /> object.
    ///     This class cannot be inherited.
    /// </summary>
    public sealed class PathData
    {
        private PathData(System.Drawing.Drawing2D.PathData pathData)
        {
            WrappedPathData = pathData;
        }

        /// <summary>
        ///     Gets or sets an array of <see cref="T:Common.Drawing.PointF" /> structures that represents the points through
        ///     which the path is constructed.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Common.Drawing.PointF" /> objects that represents the points through which the path
        ///     is constructed.
        /// </returns>
        public PointF[] Points
        {
            get => WrappedPathData.Points.Convert<PointF>().ToArray();
            set => WrappedPathData.Points = value.Convert<System.Drawing.PointF>().ToArray();
        }

        /// <summary>Gets or sets the types of the corresponding points in the path.</summary>
        /// <returns>An array of bytes that specify the types of the corresponding points in the path.</returns>
        public byte[] Types
        {
            get => WrappedPathData.Types;
            set => WrappedPathData.Types = value;
        }

        private System.Drawing.Drawing2D.PathData WrappedPathData { get; }

        /// <summary>
        ///     Converts the specified <see cref="T:System.Drawing.Drawing2D.PathData" /> to a
        ///     <see cref="T:Common.Drawing.Drawing2D.PathData" />.
        /// </summary>
        /// <returns>The <see cref="T:Common.Drawing.Drawing2D.PathData" /> that results from the conversion.</returns>
        /// <param name="pathData">The <see cref="T:System.Drawing.Drawing2D.PathData" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator PathData(System.Drawing.Drawing2D.PathData pathData)
        {
            return pathData == null ? null : new PathData(pathData);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:Common.Drawing.Drawing2D.PathData" /> to a
        ///     <see cref="T:System.Drawing.Drawing2D.PathData" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Drawing.Drawing2D.PathData" /> that results from the conversion.</returns>
        /// <param name="pathData">The <see cref="T:Common.Drawing.Drawing2D.PathData" /> to be converted.</param>
        /// <filterpriority>3</filterpriority>
        public static implicit operator System.Drawing.Drawing2D.PathData(PathData pathData)
        {
            return pathData?.WrappedPathData;
        }
    }
}