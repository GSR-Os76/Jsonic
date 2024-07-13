using System.Globalization;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public static class JsonUtil
    {
        public const string JSON_NULL = "null";
        public const string JSON_TRUE = "true";
        public const string JSON_FALSE = "false";



        internal static string UnescapeUnicodeCharacters(this string s) => Regex.Replace(s, @"\\u[0-9a-fA-F]{4}", (x) => ((char)int.Parse(x.Value[2..], NumberStyles.HexNumber)).ToString());
        internal static string Entabbed(this string s) => $"\t{s}".Replace("\r", "\r\t");

    } // end class
} // end namespace