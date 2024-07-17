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
        public JsonNumber(string json)
        {
            if (!Regex.IsMatch(json, ANCHORED_REGEX))
                throw new MalformedJsonException($"\"{json}\" is not a valid json numeric");

            Value = json;
            Significand = new(() => SignificandOf(json));
            Exponent = new(() => ExponentOf(json));
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

#warning remove trailing zeros from significand.


        private static string SignificandOf(string number)
        {
            string s = number.Split('e', 'E')[0];
            string[] ss = s.Split('.');
            return ss[0].Concat(ss.Length == 1 ? "" : WithoutTrailingZeros(ss[1])).Aggregate(new StringBuilder(s.Length), (seed, c) => seed.Append(c)).ToString();
        } // end SignificandOf()

        private static int ExponentOf(string number)
        {
            string[] s = number.Split('e', 'E');
            string[] ss = s[0].Split('.');
            int expShift = ss.Length == 1 ? 0 : (-WithoutTrailingZeros(ss[1]).Count());
            int sciNot = s.Length == 1 ? 0 : int.Parse(s[1]);
            return sciNot + expShift;
        } // end ExponentOf()

        private static IEnumerable<char> WithoutTrailingZeros(IEnumerable<char> s) => s.Reverse().Aggregate(new StringBuilder(s.Count()), (seed, c) => seed.Append(seed.Length == 0 && c == '0' ? "" : c)).ToString().Reverse();

    } // end class
} // end namespace