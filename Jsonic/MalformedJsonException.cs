using System.Runtime.Serialization;

namespace GSR.Jsonic
{
    [Serializable]
    public class MalformedJsonException : Exception
    {
        public MalformedJsonException() { }

        public MalformedJsonException(string message) : base(message) { }

        public MalformedJsonException(string message, Exception inner) : base(message, inner) { }

        protected MalformedJsonException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    } // end class
} // end namespace