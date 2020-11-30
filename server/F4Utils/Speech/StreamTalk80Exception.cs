using System;
using System.Runtime.Serialization;

namespace F4Utils.Speech
{
    /// <summary>
    ///   Represents an error that occurs when calling the StreamTalk DLL
    /// </summary>
    [Serializable]
    public class StreamTalk80Exception : ApplicationException
    {
        #region Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "StreamTalk80Exception" /> class.
        /// </summary>
        public StreamTalk80Exception()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "StreamTalk80Exception" /> class.
        /// </summary>
        /// <param name = "message">The message that describes the error.</param>
        public StreamTalk80Exception(string message)
            : base(message)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "StreamTalk80Exception" /> class.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <param name = "inner">The exception that is the cause of the current exception, or <see langword = "null" /> if no inner exception is specified.</param>
        public StreamTalk80Exception(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "StreamTalk80Exception" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the <see cref = "System.Exception" /> being thrown.</param>
        /// <param name = "context">The <see cref = "System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected StreamTalk80Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}