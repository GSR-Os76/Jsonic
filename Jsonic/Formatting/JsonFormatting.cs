namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Definition of various options to determine the style of output json. Such as indentation, or what characters should be escapes, or if numbers should be exponentially notation.
    /// </summary>
    public struct JsonFormatting
    {
        /// <summary>
        /// Type of newlines to use.
        /// </summary>
        public NewLineType NewLineType { get; } = NewLineType.CRLF;



        /// <summary>
        /// How <see cref="JsonArray"/>s should be formatted.
        /// </summary>
        public JsonArrayFormatting ArrayFormatting { get; } = new();

        /// <summary>
        /// How <see cref="JsonNumber"/>s should be formatted.
        /// </summary>
        public JsonNumberFormatting NumberFormatting { get; } = new();

        /// <summary>
        /// How <see cref="JsonObject"/>s should be formatted.
        /// </summary>
        public JsonObjectFormatting ObjectFormatting { get; } = new();

        /// <summary>
        /// How <see cref="JsonString"/>s should be formatted.
        /// </summary>
        public JsonStringFormatting StringFormatting { get; } = new();



        /// <inheritdoc/>
        public JsonFormatting(NewLineType newLineType, JsonArrayFormatting arrayFormatting, JsonNumberFormatting numberFormatting, JsonObjectFormatting objectFormatting, JsonStringFormatting stringFormatting)
        {
            NewLineType = newLineType;
            ArrayFormatting = arrayFormatting;
            NumberFormatting = numberFormatting;
            ObjectFormatting = objectFormatting;
            StringFormatting = stringFormatting;
        } // end ctor

    } // end struct
} // end namespace