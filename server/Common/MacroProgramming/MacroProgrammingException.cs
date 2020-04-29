using System;
using System.Runtime.Serialization;

namespace Common.MacroProgramming
{
    /// <summary>
    ///     Base <see cref="System.Exception">Exception</see> class for all exceptions thrown in the Common.MacroProgramming
    ///     namespace.
    /// </summary>
    [Serializable]
    public class MacroProgrammingException : ApplicationException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MacroProgrammingException" /> class.
        /// </summary>
        public MacroProgrammingException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MacroProgrammingException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MacroProgrammingException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MacroProgrammingException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">
        ///     The exception that is the cause of the current exception, or <see langword="null" /> if no inner
        ///     exception is specified.
        /// </param>
        public MacroProgrammingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MacroProgrammingException" /> class.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object
        ///     data about the <see cref="System.Exception" /> being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual
        ///     information about the source or destination.
        /// </param>
        protected MacroProgrammingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}