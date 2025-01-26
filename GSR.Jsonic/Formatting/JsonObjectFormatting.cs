namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonObject"/>.
    /// </summary>
    public sealed class JsonObjectFormatting : JsonCollectionFormatting
    {
        /// <summary>
        /// Whitespace to add after the key in a key value pair.
        /// </summary>
        public string PostKeyWhitespace { get; } = "";

        /// <summary>
        /// Whitespace to add before the value in a key value pair.
        /// </summary>
        public string PreValueWhitespace { get; } = " ";



        /// <inheritdoc/>
        public JsonObjectFormatting(
            int emptyCollectionNewlineCount = 0,
            bool newLineProceedingFirstElement = true,
            bool newLineBetweenElements = true,
            bool newLineSucceedingLastElement = true,
            string indentation = "    ",
            string postCommaSpacing = "",
            string postKeyWhitespace = "",
            string preValueWhitespace = " ") : base(
        emptyCollectionNewlineCount,
        newLineProceedingFirstElement,
        newLineBetweenElements,
        newLineSucceedingLastElement,
        indentation,
        postCommaSpacing)
        {
#if DEBUG
            if (!Util.ANCHORED_WHITESPACE_REGEX.IsMatch(postKeyWhitespace))
                throw new ArgumentException();

            if (!Util.ANCHORED_WHITESPACE_REGEX.IsMatch(preValueWhitespace))
                throw new ArgumentException();
#endif
            PostKeyWhitespace = postKeyWhitespace;
            PreValueWhitespace = preValueWhitespace;
        } // end ctor
    } // end class
} // end namespace