namespace Common.Drawing.Drawing2D
{
    /// <summary>Specifies the system to use when evaluating coordinates.</summary>
    public enum CoordinateSpace
    {
        /// <summary>
        ///     Specifies that coordinates are in the world coordinate context. World coordinates are used in a nonphysical
        ///     environment, such as a modeling environment.
        /// </summary>
        World,

        /// <summary>
        ///     Specifies that coordinates are in the page coordinate context. Their units are defined by the
        ///     <see cref="P:Common.Drawing.Graphics.PageUnit" /> property, and must be one of the elements of the
        ///     <see cref="T:Common.Drawing.GraphicsUnit" /> enumeration.
        /// </summary>
        Page,

        /// <summary>
        ///     Specifies that coordinates are in the device coordinate context. On a computer screen the device coordinates
        ///     are usually measured in pixels.
        /// </summary>
        Device
    }
}