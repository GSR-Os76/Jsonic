﻿using System.Text;
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
            if (!Regex.IsMatch(value.IsNotNull(), ANCHORED_UNENQUOTED_REGEX))
                throw new MalformedJsonException($"Couldn't construct a Json string with the of value: \"{value}\", maybe try using: \"{nameof(FromUnescapedString)}\"");

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
        public static JsonString FromUnescapedString(string s)
        {
            StringBuilder sb = new(s.IsNotNull().Length + 2);
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
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonString containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonString ParseJson(string json, out string remainder)
        {
            string parse = json.IsNotNull().TrimStart();
            if (parse.Length < 1 || !parse[0].Equals('"'))
                throw new MalformedJsonException();

            Match m = new Regex(ENQUOTED_REGEX).Match(parse, 0);
            if (!m.Success)
                throw new MalformedJsonException($"Couldn't read string at the start of: \"{parse}\".");

            string s = m.Value;
            remainder = parse[s.Length..^0];
            return new(s[1..^1]);
        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonString containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonString ParseJson(string json) => JsonUtil.RequiredEmptyRemainder(ParseJson, json);

    } // end class
} // end namespace