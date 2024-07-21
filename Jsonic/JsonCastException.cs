namespace GSR.Jsonic
{
    [Serializable]
    public class InvalidJsonCastException : Exception
    {
        public InvalidJsonCastException(JsonType actual, JsonType expected) : this($"Unable to cast json value of type \"{actual}\" to the type \"{expected}\".") { }

        public InvalidJsonCastException() { }

        public InvalidJsonCastException(string message) : base(message) { }

        public InvalidJsonCastException(string message, Exception inner) : base(message, inner) { }

        protected InvalidJsonCastException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
} // end namespace