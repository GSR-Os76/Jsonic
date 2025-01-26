namespace GSR.Jsonic
{
    /// <summary>
    /// Enum for identifying the type of value a json value is.
    /// </summary>
    public enum JsonType
    {
        /// <summary>
        /// A json array.
        /// </summary>
        Array,
        /// <summary>
        /// A json boolean.
        /// </summary>
        Boolean,
        /// <summary>
        /// The null value.
        /// </summary>
        Null,
        /// <summary>
        /// A json number.
        /// </summary>
        Number,
        /// <summary>
        /// A json object.
        /// </summary>
        Object,
        /// <summary>
        /// A json string.
        /// </summary>
        String
    } // end enum
} // end namespace