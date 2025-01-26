using GSR.Jsonic.Formatting;

namespace GSR.Jsonic
{
    /// <summary>
    /// Uninstantiable class for representing a null json value, for the 'instance' use <see cref="NULL"/>.
    /// </summary>
    public sealed class JsonNull : AJsonValue
    {
        internal const string JSON_NULL = "null";

        /// <summary>
        /// The single constant <see cref="JsonNull"/> 'instance'.
        /// </summary>
        public static readonly JsonNull? NULL = null;



        private JsonNull() { }



        /// <inheritdoc/>
        public override string ToString(JsonFormatting formatting) => JSON_NULL;



        /// <summary>
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonNull containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonNull? ParseJson(string json, out string remainder)
        {
#if ASSERT
            json.IsNotNull();
#endif
            string parse = json.TrimStart();
            if (parse.Length < 1 || !parse[0].Equals('n'))
                throw new MalformedJsonException();

            Util.RequireAtStart(JSON_NULL, parse, out remainder);
            return NULL;
        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonNull containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonNull? ParseJson(string json) => Util.RequiredEmptyRemainder(ParseJson, json);
    } // end class
} // end namespace