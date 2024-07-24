using System.Globalization;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    internal static class JsonUtil
    {
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

    } // end class
} // end namespace