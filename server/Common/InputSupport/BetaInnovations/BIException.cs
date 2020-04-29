using System;
using System.Runtime.Serialization;

namespace Common.InputSupport.BetaInnovations
{
    /// <summary>
    ///     <see cref="BIException" /> represents <see cref="System.Exception">Exception</see>s that occur during calls to the
    ///     BetaInnovations SDK.
    /// </summary>
    [Serializable]
    public class BIException : ApplicationException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BIException" /> class.
        /// </summary>
        public BIException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BIException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BIException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BIException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the <see cref="System.Exception">Exception</see>.</param>
        /// <param name="inner">
        ///     The <see cref="System.Exception" /> that is the cause of the current
        ///     <see cref="System.Exception">Exception</see>, or <see langword="null" /> if no inner
        ///     <see cref="System.Exception">Exception</see> is specified.
        /// </param>
        public BIException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BIException" /> class.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object
        ///     data about the <see cref="System.Exception">Exception</see> being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual
        ///     information about the source or destination.
        /// </param>
        protected BIException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}