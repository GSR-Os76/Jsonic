namespace GSR.Jsonic
{
    /// <summary>
    /// <see cref="Exception"/> indicating that a <see cref="JsonElement"/> conversion couldn't be complete as the underlying value was a different type.
    /// </summary>
    [Serializable]
    public class InvalidJsonCastException : Exception
    {
        /// <inheritdoc/>
        public InvalidJsonCastException(JsonType actual, JsonType expected) : this($"Unable to cast json value of type \"{actual}\" to the type \"{expected}\".") { }

        /// <inheritdoc/>
        public InvalidJsonCastException() { }

        /// <inheritdoc/>
        public InvalidJsonCastException(string message) : base(message) { }

        /// <inheritdoc/>
        public InvalidJsonCastException(string message, Exception inner) : base(message, inner) { }

        /// <inheritdoc/>
        protected InvalidJsonCastException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } // end class
} // end namespace