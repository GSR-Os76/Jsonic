namespace GSR.Jsonic
{
    public sealed class JsonNull : IJsonComponent
    {
        public const string JSON_NULL = "null";

        public static readonly JsonNull? NULL = null;



        private JsonNull() { }



        public string ToCompressedString() => ToString();

        public override string ToString() => JSON_NULL;


        /// <summary>
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonNUll containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonNull? ParseJson(string json, out string remainder)
        {
            string parse = json.TrimStart();
            if (parse.Length < 1 || !parse[0].Equals('n'))
                throw new MalformedJsonException();

            if (parse.Length < JSON_NULL.Length || !parse[0..JSON_NULL.Length].Equals(JSON_NULL))
                throw new MalformedJsonException($"Couldn't read element at the start of \"{parse}\".");

            remainder = parse[JSON_NULL.Length..^0];
            return NULL;

        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonNull containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonNull? ParseJson(string json)
        {
            JsonNull? x = ParseJson(json, out string r);
            if (!r.Trim().Equals(string.Empty))
                throw new MalformedJsonException();

            return x;
        } // end ParseJson()

    } // end class
} // end namespace