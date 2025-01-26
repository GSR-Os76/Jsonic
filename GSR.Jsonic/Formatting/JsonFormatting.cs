namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Definition of various options to determine the style of output json. Such as indentation, or what characters should be escapes, or if numbers should be exponentially notation.
    /// </summary>
    public sealed class JsonFormatting
    {
        /// <summary>
        /// Formatting without any non-coding white space. Keeps numbers and strings in form they were parsed from.
        /// </summary>
        public static readonly JsonFormatting COMPRESSED = new(
            NewLineType.NONE,
            new JsonArrayFormatting(0, false, false, false, "", ""),
            new JsonNumberFormatting(preserve: true),
            new JsonObjectFormatting(0, false, false, false, "", "", "", ""),
            new JsonStringFormatting(preserve: true));

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
        public JsonFormatting() { } // end ctor

        /// <summary>
        /// Construct the object - nulls indicate use of the default values
        /// </summary>
        /// <param name="newLineType"></param>
        /// <param name="arrayFormatting"></param>
        /// <param name="numberFormatting"></param>
        /// <param name="objectFormatting"></param>
        /// <param name="stringFormatting"></param>
        public JsonFormatting(
            NewLineType newLineType = NewLineType.CRLF,
            JsonArrayFormatting? arrayFormatting = null,
            JsonNumberFormatting? numberFormatting = null,
            JsonObjectFormatting? objectFormatting = null,
            JsonStringFormatting? stringFormatting = null)
        {
            NewLineType = newLineType;
            ArrayFormatting = arrayFormatting ?? new();
            NumberFormatting = numberFormatting ?? new();
            ObjectFormatting = objectFormatting ?? new();
            StringFormatting = stringFormatting ?? new();
        } // end ctor

    } // end struct
} // end namespace