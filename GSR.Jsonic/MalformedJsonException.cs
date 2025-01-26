using System.Runtime.Serialization;

namespace GSR.Jsonic
{
    /// <summary>
    /// <see cref="Exception"/> indicating the input couldn't be understood as json.
    /// </summary>
    [Serializable]
    public class MalformedJsonException : Exception
    {
        /// <inheritdoc/>
        public MalformedJsonException() { }

        /// <inheritdoc/>
        public MalformedJsonException(string message) : base(message) { }

        /// <inheritdoc/>
        public MalformedJsonException(string message, Exception inner) : base(message, inner) { }

        /// <inheritdoc/>
        protected MalformedJsonException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    } // end class
} // end namespace