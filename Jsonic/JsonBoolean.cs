using GSR.Jsonic.Formatting;

namespace GSR.Jsonic
{
    /// <summary>
    /// Representation of a json number. No constuctor provided, use: <see cref="TRUE"/> or <see cref="FALSE"/>.
    /// </summary>
    public sealed class JsonBoolean : AJsonValue
    {
        private const string JSON_TRUE = "true";
        private const string JSON_FALSE = "false";

        /// <summary>
        /// Static true <see cref="JsonBoolean"/> instance reference.
        /// </summary>
        public static readonly JsonBoolean TRUE = new(true);
        /// <summary>
        /// Static false <see cref="JsonBoolean"/> instance reference.
        /// </summary>
        public static readonly JsonBoolean FALSE = new(false);

        /// <summary>
        /// Boolean value of the <see cref="JsonBoolean"/>.
        /// </summary>
        public bool Value { get; }



        private JsonBoolean(bool value)
        {
            Value = value;
        } // end constructor



        /// <inheritdoc/>
        public override string ToString(JsonFormatting formatting) => Value ? JSON_TRUE : JSON_FALSE;

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is JsonBoolean b && b.Value == Value;

        /// <inheritdoc/>
        public static bool operator ==(JsonBoolean a, JsonBoolean b) => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(JsonBoolean a, JsonBoolean b) => !a.Equals(b);



        /// <inheritdoc/>
        public static implicit operator JsonBoolean(bool value) => value ? TRUE : FALSE;
        /// <inheritdoc/>
        public static implicit operator bool(JsonBoolean value) => value.Value;




        /// <summary>
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonBoolean containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonBoolean ParseJson(string json, out string remainder)
        {
#if DEBUG
            json.IsNotNull();
#endif
            string parse = json.TrimStart();
            if (parse.Length < 1)
                throw new MalformedJsonException();

            switch (parse[0])
            {
                case 'f':
                    JsonUtil.RequireAtStart(JSON_FALSE, parse, out remainder);
                    return FALSE;
                case 't':
                    JsonUtil.RequireAtStart(JSON_TRUE, parse, out remainder);
                    return TRUE;
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