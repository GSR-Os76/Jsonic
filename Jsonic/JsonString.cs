using GSR.Jsonic.Formatting;
using System.Text;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    /// <summary>
    /// Representation of a json string.
    /// </summary>
    public sealed class JsonString : AJsonValue
    {
        private static readonly Regex REGEX = new(@"""([^\\""]|(\\([""\\/bfnrt]|(u[0-9a-fA-F]{4}))))*""");

        /// <summary>
        /// The value of the string, without any escaping.
        /// </summary>
        public string Value
        {
            get
            {
                if (_value is not null)
                    return _value;

                string? val;
                if (_valueP is null || !_valueP.TryGetTarget(out val))
                {
                    val = Regex.Unescape(_parsed?[1..^1] ?? throw new InvalidOperationException("Value hypothetically shouldn't possibly be null currently."));
                    if (_valueP is null)
                        _valueP = new(val);
                    else
                        _valueP.SetTarget(val);
                }
                return val ?? throw new InvalidOperationException("Value hypothetically shouldn't possibly be null currently.");
            }
        }

        private WeakReference<string>? _valueP;
        private readonly string? _value;
        private readonly string? _parsed;


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
            : this(null, value)
        {
#if ASSERT
            value.IsNotNull();
#endif
        } // end ctor

        private JsonString(string? parsed, string? value)
        {
            _parsed = parsed;
            _value = value;
        } // end ctor



        /// <inheritdoc/>
        public override string ToString(JsonFormatting formatting)
        {
            if (formatting.StringFormatting.Preserve && _parsed is not null)
                return _parsed;

            bool solidusFlag = formatting.StringFormatting.EscapeSolidi;
            StringBuilder sb = new(Value.Length + 2);
            sb.Append('"');
            foreach (char c in Value)
                if (c == '\\')
                    sb.Append("\\\\");
                else if (c == '"')
                    sb.Append("\\\"");
                else if (c == '\b')
                    sb.Append("\\b");
                else if (c == '\f')
                    sb.Append("\\f");
                else if (c == '\n')
                    sb.Append("\\n");
                else if (c == '\r')
                    sb.Append("\\r");
                else if (c == '\t')
                    sb.Append("\\t");
                else if (char.IsControl(c))
                {
                    sb.Append(@"\u");
                    sb.Append(((int)c).ToString("x4"));
                }
                else if (c == '/' && solidusFlag)
                    sb.Append("\\/");
                else
                    sb.Append(c);

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
#if ASSERT
            json.IsNotNull();
#endif
            string parse = json.TrimStart();
            if (parse.Length < 1 || !parse[0].Equals('"'))
                throw new MalformedJsonException();

            Match m = REGEX.Match(parse, 0);
            if (!m.Success)
                throw new MalformedJsonException($"Couldn't read string at the start of: \"{parse}\".");

            string s = m.Value;
            remainder = parse[s.Length..^0];
            return new(s, null);
        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonString containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonString ParseJson(string json) => Util.RequiredEmptyRemainder(ParseJson, json);

    } // end class
} // end namespace