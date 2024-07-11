﻿using System.Text;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public sealed class JsonString
    {
        public const string STRICT_VALIDATOR_REGEX = @"^""([^\\""]|(\\([""\\/bfnrt]|(u[0-9a-fA-F]{4}))))*""$";

        public string Value { get; }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <exception cref="MalformedJsonException">sString wasn't in valid Json string format.</exception>
        public JsonString(string json)
        {
            if (!Regex.IsMatch(json, STRICT_VALIDATOR_REGEX))
                throw new MalformedJsonException($"\"{json}\" is not a valid json string");

            Value = json;
        } // end constructor()



        /// <summary>
        /// Unescapes escaped characters, turning it into the string it represents.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Value[1..^1].Replace("\\\"", "\"").Replace("\\\\", "\\").Replace("\\/", "/").Replace("\\b", "\b").Replace("\\f", "\f").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t").UnescapeUnicodeCharacters();

        /// <summary>
        /// Escapes all unescaped characters necessary, turning it into an equivalent string that represents it.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static JsonString Parse(string s)
        {
            StringBuilder sb = new(s.Length + 2);
            sb.Append("\"");
            foreach(char c in s)
                sb.Append(c == '"' ? "\\\"" : c == '\\' ? "\\\\" : c);

            return new(sb.Append("\"").ToString());
        } // end Parse()           // TODON'T: would be unecessary and oppressive: TODO, maybe also escape \ in the script tag context, maybe also do: .Replace("\b", "\\b").Replace("\f", "\\f").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t")

    } // end class
} // end namespace