﻿using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public sealed class JsonNumber : IJsonComponent
    {
        public const string REGEX = @"-?(([1-9][0-9]*)|0)(\.[0-9]+)?([eE][-+]?[0-9]+)?";
        public const string ANCHORED_REGEX = @"^" + REGEX + @"$";
        private const NumberStyles PARSING_STYLE = NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

        public string Value { get; }

        public Lazy<string> Significand { get; }
        public Lazy<int> Exponent { get; }



        public JsonNumber(sbyte value) : this(value.ToString()) { }
        public JsonNumber(byte value) : this(value.ToString()) { }
        public JsonNumber(short value) : this(value.ToString()) { }
        public JsonNumber(ushort value) : this(value.ToString()) { }
        public JsonNumber(int value) : this(value.ToString()) { }
        public JsonNumber(uint value) : this(value.ToString()) { }
        public JsonNumber(long value) : this(value.ToString()) { }
        public JsonNumber(ulong value) : this(value.ToString()) { }
        public JsonNumber(float value) : this(value.ToString()) { }
        public JsonNumber(double value) : this(value.ToString()) { }
        public JsonNumber(decimal value) : this(value.ToString()) { }
        public JsonNumber(string value)
        {
            if (!Regex.IsMatch(value.IsNotNull(), ANCHORED_REGEX))
                throw new MalformedJsonException($"\"{value}\" is not a valid json numeric");

            Value = value;
            Significand = new(() => SignificandOf(value));
            Exponent = new(() => ExponentOf(value));
        } // end JsonNumber



        public sbyte AsSignedByte() => sbyte.Parse(Value, PARSING_STYLE);
        public byte AsByte() => byte.Parse(Value, PARSING_STYLE);

        public short AsShort() => short.Parse(Value, PARSING_STYLE);
        public ushort AsUnsignedShort() => ushort.Parse(Value, PARSING_STYLE);

        public int AsInt() => int.Parse(Value, PARSING_STYLE);
        public uint AsUnsignedInt() => uint.Parse(Value, PARSING_STYLE);

        public long AsLong() => long.Parse(Value, PARSING_STYLE);
        public ulong AsUnsignedLong() => ulong.Parse(Value, PARSING_STYLE);



        public float AsFloat() => float.Parse(Value);
        public double AsDouble() => double.Parse(Value);
        public decimal AsDecimal() => decimal.Parse(Value, PARSING_STYLE);



        /// <inheritdoc/>
        public string ToCompressedString() => ToString();

        public override string ToString() => Value;

        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Compares by string, not by the represented value.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj) => obj is JsonNumber b && b.Significand.Value.Equals(Significand.Value) && b.Exponent.Value.Equals(Exponent.Value);

        public static bool operator ==(JsonNumber a, JsonNumber b) => a.Equals(b);

        public static bool operator !=(JsonNumber a, JsonNumber b) => !a.Equals(b);

        private static string SignificandOf(string number)
        {
            string w = WithoutInsignificantZeros(new string(number.Split('e', 'E')[0].Where((c) => c != '.').ToArray()));
            return (number[0].Equals('-') && !w.Equals(string.Empty) ? "-" : string.Empty) + (w.Equals(string.Empty) ? "0" : w);
        } // end SignificandOf()

        private static int ExponentOf(string number)
        {
            string[] s = number.Split('e', 'E');
            string[] ss = s[0].Split('.');
            int decShift = ss.Length == 1 ? 0 : (-WithoutTrailingZeros(ss[1]).Count());
            int intShift = decShift == 0 ? (ss[0].Length - WithoutTrailingZeros(ss[0]).Count()) : 0;
            int sciNot = s.Length == 1 ? 0 : int.Parse(s[1]);
            return decShift == 0 && Regex.IsMatch(ss[0], @"^-?0$") ? 0 : sciNot + decShift + intShift;
        } // end ExponentOf()

        private static IEnumerable<char> WithoutTrailingZeros(IEnumerable<char> s) => s.Reverse().Aggregate(new StringBuilder(s.Count()), (seed, c) => seed.Append(seed.Length == 0 && c == '0' ? "" : c)).ToString().Reverse();

        private static string WithoutInsignificantZeros(string s) => Regex.Match(s, @"[1-9]([0-9]*[1-9])?").Value;



        /// <summary>
        /// Parse the number at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonNumber containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonNumber ParseJson(string json, out string remainder)
        {
            string parse = json.IsNotNull().TrimStart();
            if (parse.Length < 1)
                throw new MalformedJsonException();

            Match m = new Regex(REGEX).Match(parse, 0);
            if (!m.Success)
                throw new MalformedJsonException($"Couldn't read element at the start of: \"{parse}\".");

            string s = m.Value;
            remainder = parse[s.Length..^0];
            return new JsonNumber(s);
        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonNumber containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonNumber ParseJson(string json) => JsonUtil.RequiredEmptyRemainder(ParseJson, json);

    } // end class
} // end namespace