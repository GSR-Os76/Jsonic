namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonObject"/>.
    /// </summary>
    public struct JsonObjectFormatting
    {
        /// <summary>
        /// Collectionwise formatting of the object.
        /// </summary>
        public JsonCollectionFormatting Formatting { get; }

        /// <summary>
        /// Whitespace to add after the key in a key value pair.
        /// </summary>
        public string PostKeyWhitespace { get; } = "";

        /// <summary>
        /// Whitespace to add before the value in a key value pair.
        /// </summary>
        public string PreValueWhitespace { get; } = " ";



        /// <inheritdoc/>
        public JsonObjectFormatting(JsonCollectionFormatting formatting, string postKeyWhitespace, string preValueWhitespace)
        {
#if DEBUG
            if (!JsonUtil.ANCHORED_WHITESPACE_REGEX.IsMatch(postKeyWhitespace))
                throw new ArgumentException();
            
            if (!JsonUtil.ANCHORED_WHITESPACE_REGEX.IsMatch(preValueWhitespace))
                throw new ArgumentException();
#endif
            Formatting = formatting;
            PostKeyWhitespace = postKeyWhitespace;
            PreValueWhitespace = preValueWhitespace;
        } // end ctor
    } // end struct
} // end namespace