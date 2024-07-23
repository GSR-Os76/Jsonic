using System.Text;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public sealed class JsonString : IJsonComponent
    {
        public const string UNENQUOTED_REGEX = @"([^\\""]|(\\([""\\/bfnrt]|(u[0-9a-fA-F]{4}))))*";
        public const string ENQUOTED_REGEX = @"""" + UNENQUOTED_REGEX + @"""";
        public const string ANCHORED_UNENQUOTED_REGEX = @"^" + UNENQUOTED_REGEX + @"$";
        public const string ANCHORED_ENQUOTED_REGEX = @"^" + ENQUOTED_REGEX + @"$";

        public static readonly JsonString EMPTY = new();

        public string Value { get; }



        public JsonString() : this(string.Empty) { }

        /// <summary>
        /// Constructs a JsonString with a given value.
        /// </summary>
        /// <param name="value">The Json compatibly formatted string the Json string represents.</param>
        /// <exception cref="MalformedJsonException">String wasn't in valid Json string format.</exception>
        public JsonString(string value)
        {
            if (!Regex.IsMatch(value, ANCHORED_UNENQUOTED_REGEX))
                throw new MalformedJsonException($"Couldn't construct a Json string with the of value: \"{value}\", maybe try using: \"{nameof(FromEscapedString)}\"");

            Value = value;
        } // end constructor()




        /// <summary>
        /// Unescapes escaped characters, turning the Json string into the string it represents.
        /// </summary>
        /// <returns></returns>
        public string ToUnescapedString() => Value.Replace("\\\"", "\"").Replace("\\/", "/").Replace("\\b", "\b").Replace("\\f", "\f").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t").UnescapeUnicodeCharacters().Replace("\\\\", "\\");

        /// <summary>
        /// Escapes all unescaped characters necessary, turning it into an equivalent string that represents it.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static JsonString FromEscapedString(string s)
        {
            StringBuilder sb = new(s.Length + 2);
            foreach (char c in s)
                sb.Append(c == '"' ? "\\\"" : c == '\\' ? "\\\\" : c);

            return new(sb.ToString());
        } // end Parse()



        /// <inheritdoc/>
        public string ToCompressedString() => ToString();

        public override string ToString() => $"\"{Value}\"";

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object? obj) => obj is JsonString b && b.Value == Value;

        public static bool operator ==(JsonString a, JsonString b) => a.Equals(b);

        public static bool operator !=(JsonString a, JsonString b) => !a.Equals(b);



        /// <summary>
        /// Parses a Json string string into a JsonString representation of it.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        /// <exception cref="MalformedJsonException"></exception>
        public static JsonString Parse(string json)
        {
            string parse = json.Trim();
            if (!Regex.IsMatch(parse, ANCHORED_ENQUOTED_REGEX))
                throw new MalformedJsonException($"Could read JsonString from value: \"{parse}\"");

            return new(parse[1..^1]);
        } // end Parse()
    } // end class
} // end namespace