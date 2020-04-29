namespace Common.Networking
{
    /// <summary>
    ///     Enumeration of possible networking modes that an application instance can operate under
    /// </summary>
    public enum NetworkMode
    {
        /// <summary>
        ///     Standalone (non-networked) mode
        /// </summary>
        Standalone,

        /// <summary>
        ///     Server mode (provides images to remote clients in addition to providing local images)
        /// </summary>
        Server,

        /// <summary>
        ///     Client mode (receives images from a remote server)
        /// </summary>
        Client
    }
}