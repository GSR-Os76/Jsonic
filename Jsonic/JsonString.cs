using System.Text;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    /// <summary>
    /// Representation of a json string.
    /// </summary>
    public sealed class JsonString : IJsonValue
    {
        private const string UNENQUOTED_REGEX = @"([^\\""]|(\\([""\\/bfnrt]|(u[0-9a-fA-F]{4}))))*";
        private const string ENQUOTED_REGEX = @"""" + UNENQUOTED_REGEX + @"""";
        private const string ANCHORED_UNENQUOTED_REGEX = @"^" + UNENQUOTED_REGEX + @"$";
        private const string ANCHORED_ENQUOTED_REGEX = @"^" + ENQUOTED_REGEX + @"$";

        private const OptionalEscapeCharacters DEFUALT_ESCAPES = OptionalEscapeCharacters.BACKSPACE | OptionalEscapeCharacters.FORMFEED | OptionalEscapeCharacters.LINEFEED | OptionalEscapeCharacters.CARRIAGE_RETURN | OptionalEscapeCharacters.HORIZONTAL_TAB;

        /// <summary>
        /// The value of the string, without any escaping.
        /// </summary>
        public string Value { get; }



        /// <summary>
        /// Create a new <see cref="JsonString"/>
        /// </summary>
        public JsonString() : this(string.Empty) { }

        /// <summary>
        /// Constructs a JsonString with a given value.
        /// </summary>
        /// <param name="value">The value to be represented by the <see cref="JsonString"/>, understood as having no escape codes.</param>
        /// <exception cref="MalformedJsonException">String wasn't in valid Json string format.</exception>
        public JsonString(string value)
        {
#if DEBUG
            value.IsNotNull();
#endif
            Value = value;
        } // end constructor()




        /// <inheritdoc/>
        public string ToCompressedString() => ToString();

        /// <inheritdoc/>
        public override string ToString() => ToString(DEFUALT_ESCAPES);

        /// <summary>
        /// Convert the string into json text with the selected <see cref="OptionalEscapeCharacters"/> <paramref name="escapes"/> escaped.
        /// </summary>
        /// <param name="escapes"></param>
        /// <returns></returns>
        public string ToString(OptionalEscapeCharacters escapes)
        {
            StringBuilder sb = new(Value.Length + 2);
            sb.Append('"');
            foreach (char c in Value)
            {
                if (c == '\\')
                    sb.Append("\\\\");
                else if (c == '"')
                    sb.Append("\\\"");
                else if (c == '/' && (escapes & OptionalEscapeCharacters.SOLIDUS) != 0)
                    sb.Append("\\/");
                else if (c == '\b' && (escapes & OptionalEscapeCharacters.BACKSPACE) != 0)
                    sb.Append("\\b");
                else if (c == '\f' && (escapes & OptionalEscapeCharacters.FORMFEED) != 0)
                    sb.Append("\\f");
                else if (c == '\n' && (escapes & OptionalEscapeCharacters.LINEFEED) != 0)
                    sb.Append("\\n");
                else if (c == '\r' && (escapes & OptionalEscapeCharacters.CARRIAGE_RETURN) != 0)
                    sb.Append("\\r");
                else if (c == '\t' && (escapes & OptionalEscapeCharacters.HORIZONTAL_TAB) != 0)
                    sb.Append("\\t");
                else
                    sb.Append(c);
            }
            sb.Append('"');
            return sb.ToString();
        } // end ToString()

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is JsonString other1)
                return other1.Value == Value;
            else if (obj is string other2)
                return other2 == Value;

            return false;
        } // end Equals()


        /// <inheritdoc/>
        public static bool operator ==(JsonString a, JsonString b) => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(JsonString a, JsonString b) => !a.Equals(b);



        /// <inheritdoc/>
        public static implicit operator JsonString(string value) => new(value);
        /// <inheritdoc/>
        public static implicit operator string(JsonString value) => value.Value;



        /// <summary>
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonString containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonString ParseJson(string json, out string remainder)
        {
#if DEBUG
            json.IsNotNull();
#endif
            string parse = json.TrimStart();
            if (parse.Length < 1 || !parse[0].Equals('"'))
                throw new MalformedJsonException();

            Match m = new Regex(ENQUOTED_REGEX).Match(parse, 0);
            if (!m.Success)
                throw new MalformedJsonException($"Couldn't read string at the start of: \"{parse}\".");

            string s = m.Value;
            remainder = parse[s.Length..^0];
            return Regex.Unescape(s[1..^1]);
        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonString containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonString ParseJson(string json) => JsonUtil.RequiredEmptyRemainder(ParseJson, json);




        /// <summary>
        /// Flag style enum representation of characters that are allowed to be escaped or unescaped.
        /// </summary>
        public enum OptionalEscapeCharacters
        {
            /// <summary>
            /// No flags set.
            /// </summary>
            NONE = 0b0000_0000,
            /// <summary>
            /// All flags set.
            /// </summary>
            ALL = 0b0011_1111,


            /*          QUOTATION_MARK,
                        REVERSE_SOLIDUS,*/
            /// <summary>
            /// Forward slash: '\/' 
            /// </summary>
            SOLIDUS = 0b0000_0001,
            /// <summary>
            /// Backspace: '\b'
            /// </summary>
            BACKSPACE = 0b0000_0010,
            /// <summary>
            /// Form feed: '\f'
            /// </summary>
            FORMFEED = 0b0000_0100,
            /// <summary>
            /// Line feed, or newline: '\n'
            /// </summary>
            LINEFEED = 0b0000_1000,
            /// <summary>
            /// Carriage return: '\r'
            /// </summary>
            CARRIAGE_RETURN = 0b0001_0000,
            /// <summary>
            /// Horizontal tab: '\t'
            /// </summary>
            HORIZONTAL_TAB = 0b0010_0000,
            ///// <summary>
            ///// Utf-16 codepoint: '\u[a-fA-F]{4}'
            ///// </summary>
            //UNICODE = 0b0100_0000
        } // end EscapeCharacters
    } // end class
} // end namespace