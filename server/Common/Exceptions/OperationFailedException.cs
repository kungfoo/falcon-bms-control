using System;
using System.Runtime.Serialization;

namespace Common.Exceptions
{
    /// <summary>
    ///     Represents an error that occurs when performing a Win32 or Interop operation.
    /// </summary>
    [Serializable]
    public class OperationFailedException : ApplicationException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationFailedException" /> class.
        /// </summary>
        public OperationFailedException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationFailedException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OperationFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationFailedException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">
        ///     The exception that is the cause of the current exception, or <see langword="null" /> if no inner
        ///     exception is specified.
        /// </param>
        public OperationFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationFailedException" /> class.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object
        ///     data about the <see cref="System.Exception" /> being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual
        ///     information about the source or destination.
        /// </param>
        protected OperationFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}