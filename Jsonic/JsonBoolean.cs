namespace GSR.Jsonic
{
    public sealed class JsonBoolean : IJsonComponent
    {
        public const string JSON_TRUE = "true";
        public const string JSON_FALSE = "false";

        public static readonly JsonBoolean TRUE = new(true);
        public static readonly JsonBoolean FALSE = new(false);

        public bool Value { get; }



        public JsonBoolean(bool value)
        {
            Value = value;
        } // end constructor



        /// <inheritdoc/>
        public string ToCompressedString() => ToString();

        public override string ToString() => Value ? JSON_TRUE : JSON_FALSE;

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object? obj) => obj is JsonBoolean b && b.Value == Value;

        public static bool operator ==(JsonBoolean a, JsonBoolean b) => a.Equals(b);

        public static bool operator !=(JsonBoolean a, JsonBoolean b) => !a.Equals(b);



        /// <summary>
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonBoolean containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonBoolean ParseJson(string json, out string remainder)
        {
            string parse = json.TrimStart();
            if (parse.Length < 1)
                throw new MalformedJsonException();

            switch (parse[0])
            {
                case 'f':
                    if (parse.Length < JSON_FALSE.Length || !parse[0..JSON_FALSE.Length].Equals(JSON_FALSE))
                        throw new MalformedJsonException($"Couldn't read element at the start of \"{parse}\".");

                    remainder = parse[JSON_FALSE.Length..^0];
                    return new(false);
                case 't':
                    if (parse.Length < JSON_TRUE.Length || !parse[0..JSON_TRUE.Length].Equals(JSON_TRUE))
                        throw new MalformedJsonException($"Couldn't read element at the start of \"{parse}\".");

                    remainder = parse[JSON_TRUE.Length..^0];
                    return new(true);
                default:
                    throw new MalformedJsonException();
            }
        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonBoolean containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonBoolean ParseJson(string json) => JsonUtil.RequiredEmptyRemainder(ParseJson, json);

    } // end class
} // end namespace