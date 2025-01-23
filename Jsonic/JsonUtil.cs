using System.Globalization;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    internal static class JsonUtil
    {
        internal static readonly Regex WHITESPACE_REGEX = new(@"[ \r\n\t]*");
        internal static readonly Regex ANCHORED_WHITESPACE_REGEX = new(@"^([ \r\n\t]*)$");
        internal static string Entabbed(this string s) => $"\t{s}".Replace("\r", "\r\t");
        internal static string UnescapeUnicodeCharacters(this string s) => Regex.Replace(s, @"\\u[0-9a-fA-F]{4}", (x) => ((char)int.Parse(x.Value[2..], NumberStyles.HexNumber)).ToString());



        internal delegate T ParseJson<T>(string json, out string remainder);
        internal static T RequiredEmptyRemainder<T>(this ParseJson<T> remainderFunc, string json)
        {
            T t = remainderFunc(json, out string r);
            if (!r.Trim().Equals(string.Empty))
                throw new MalformedJsonException();

            return t;
        } // end RequiredEmptyRemainder()

        internal static void RequireAtStart(string value, string input, out string remainder)
        {
            if (input.Length < value.Length || !input[0..value.Length].Equals(value))
                throw new MalformedJsonException($"Couldn't read element at the start of: \"{input}\".");

            remainder = input[value.Length..^0];
        } // end RequireAtStart()

    } // end class
} // end namespace