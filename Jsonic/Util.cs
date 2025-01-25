using GSR.Jsonic.Formatting;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    internal static class Util
    {
        internal static readonly Regex WHITESPACE_REGEX = new(@"[ \r\n\t]*");
        internal static readonly Regex ANCHORED_WHITESPACE_REGEX = new(@"^([ \r\n\t]*)$");
        /// <summary>
        /// Increase tab depth by adding a new tab after every newline.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="newLineType"></param>
        /// <param name="entabbing"></param>
        /// <returns></returns>
        internal static string Entabbed(this string s, NewLineType newLineType, string entabbing) =>
            newLineType == NewLineType.NONE
            ? s
            : s.Replace(newLineType.Str(), $"{newLineType.Str()}{entabbing}");
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

        internal static string Str(this NewLineType newLineType) => newLineType switch
        {
            NewLineType.CR => "\r",
            NewLineType.CRLF => "\r\n",
            NewLineType.LF => "\n",
            NewLineType.NONE => "",
            _ => throw new InvalidOperationException()
        };

    } // end class
} // end namespace