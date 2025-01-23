using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    /// <summary>
    /// Representation of a json number.
    /// </summary>
    public sealed class JsonNumber : IJsonValue
    {
        private const string REGEX = @"-?(([1-9][0-9]*)|0)(\.[0-9]+)?([eE][-+]?[0-9]+)?";
        private const string ANCHORED_REGEX = @"^" + REGEX + @"$";
        private const NumberStyles PARSING_STYLE = NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

        /// <summary>
        /// Json text of the number
        /// </summary>
#warning rename to Text
        public string Value { get; }
#warning don't directly expose the lazy, cleaner to just show =>value
        /// <summary>
        /// Lazy that contains, or will calculate, the significand of the number.
        /// </summary>
        public Lazy<string> Significand { get; }

        /// <summary>
        /// Lazy that contains, or will calculate, the exponent of the number.
        /// </summary>
        public Lazy<int> Exponent { get; }



        /// <inheritdoc/>
        public JsonNumber(sbyte value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(byte value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(short value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(ushort value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(int value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(uint value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(long value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(ulong value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(float value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(double value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(decimal value) : this(value.ToString()) { }
        /// <inheritdoc/>
        public JsonNumber(string value)
        {
#if DEBUG
            value.IsNotNull();
#endif
            if (!Regex.IsMatch(value, ANCHORED_REGEX))
                throw new MalformedJsonException($"\"{value}\" is not a valid json numeric");

            Value = value;
            Significand = new(() => SignificandOf(value));
            Exponent = new(() => ExponentOf(value));
        } // end JsonNumber



        /// <summary>
        /// Attempt to convert the value to a <see cref="sbyte"/>.
        /// </summary>
        /// <returns></returns>
        public sbyte AsSignedByte() => sbyte.Parse(Value, PARSING_STYLE);
        /// <summary>
        /// Attempt to convert the value to a <see cref="byte"/>.
        /// </summary>
        /// <returns></returns>
        public byte AsByte() => byte.Parse(Value, PARSING_STYLE);

        /// <summary>
        /// Attempt to convert the value to a <see cref="short"/>.
        /// </summary>
        /// <returns></returns>
        public short AsShort() => short.Parse(Value, PARSING_STYLE);
        /// <summary>
        /// Attempt to convert the value to a <see cref="ushort"/>.
        /// </summary>
        /// <returns></returns>
        public ushort AsUnsignedShort() => ushort.Parse(Value, PARSING_STYLE);

        /// <summary>
        /// Attempt to convert the value to a <see cref="int"/>.
        /// </summary>
        /// <returns></returns>
        public int AsInt() => int.Parse(Value, PARSING_STYLE);
        /// <summary>
        /// Attempt to convert the value to a <see cref="uint"/>.
        /// </summary>
        /// <returns></returns>
        public uint AsUnsignedInt() => uint.Parse(Value, PARSING_STYLE);

        /// <summary>
        /// Attempt to convert the value to a <see cref="long"/>.
        /// </summary>
        /// <returns></returns>
        public long AsLong() => long.Parse(Value, PARSING_STYLE);
        /// <summary>
        /// Attempt to convert the value to a <see cref="ulong"/>.
        /// </summary>
        /// <returns></returns>
        public ulong AsUnsignedLong() => ulong.Parse(Value, PARSING_STYLE);



        /// <summary>
        /// Attempt to convert the value to a <see cref="float"/>.
        /// </summary>
        /// <returns></returns>
        public float AsFloat() => float.Parse(Value);
        /// <summary>
        /// Attempt to convert the value to a <see cref="double"/>.
        /// </summary>
        /// <returns></returns>
        public double AsDouble() => double.Parse(Value);
        /// <summary>
        /// Attempt to convert the value to a <see cref="decimal"/>.
        /// </summary>
        /// <returns></returns>
        public decimal AsDecimal() => decimal.Parse(Value, PARSING_STYLE);



        /// <inheritdoc/>
        public string ToCompressedString() => ToString();

        /// <inheritdoc/>
        public override string ToString() => Value;

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is JsonNumber b && b.Significand.Value.Equals(Significand.Value) && b.Exponent.Value.Equals(Exponent.Value);

        /// <inheritdoc/>
        public static bool operator ==(JsonNumber a, JsonNumber b) => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(JsonNumber a, JsonNumber b) => !a.Equals(b);



        /// <inheritdoc/>
        public static implicit operator JsonNumber(sbyte value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(byte value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(short value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(ushort value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(int value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(uint value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(long value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(ulong value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(float value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(double value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonNumber(decimal value) => new(value);

        /// <inheritdoc/>
        public static implicit operator sbyte(JsonNumber value) => value.AsSignedByte();
        /// <inheritdoc/>
        public static implicit operator byte(JsonNumber value) => value.AsByte();
        /// <inheritdoc/>
        public static implicit operator short(JsonNumber value) => value.AsShort();
        /// <inheritdoc/>
        public static implicit operator ushort(JsonNumber value) => value.AsUnsignedShort();
        /// <inheritdoc/>
        public static implicit operator int(JsonNumber value) => value.AsInt();
        /// <inheritdoc/>
        public static implicit operator uint(JsonNumber value) => value.AsUnsignedInt();
        /// <inheritdoc/>
        public static implicit operator long(JsonNumber value) => value.AsLong();
        /// <inheritdoc/>
        public static implicit operator ulong(JsonNumber value) => value.AsUnsignedLong();
        /// <inheritdoc/>
        public static implicit operator float(JsonNumber value) => value.AsFloat();
        /// <inheritdoc/>
        public static implicit operator double(JsonNumber value) => value.AsDouble();
        /// <inheritdoc/>
        public static implicit operator decimal(JsonNumber value) => value.AsDecimal();



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