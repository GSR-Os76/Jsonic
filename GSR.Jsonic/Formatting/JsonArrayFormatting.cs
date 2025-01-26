namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonArray"/>.
    /// </summary>
    public sealed class JsonArrayFormatting : JsonCollectionFormatting
    {
        /// <inheritdoc/>
        public JsonArrayFormatting(
            int emptyCollectionNewlineCount = 0,
            bool newLineProceedingFirstElement = true,
            bool newLineBetweenElements = true,
            bool newLineSucceedingLastElement = true,
            string indentation = "    ",
            string postCommaSpacing = "") : base(
                emptyCollectionNewlineCount,
                newLineProceedingFirstElement,
                newLineBetweenElements,
                newLineSucceedingLastElement,
                indentation,
                postCommaSpacing)
        { } // end ctor
    } // end class
} // end namespace