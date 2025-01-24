using GSR.Jsonic.Formatting;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    /// <summary>
    /// Representation of a json number.
    /// </summary>
    public sealed class JsonNumber : AJsonValue
    {
        private static readonly Regex REGEX = new(@"-?(([1-9][0-9]*)|0)(\.[0-9]+)?([eE][-+]?[0-9]+)?");
        private const NumberStyles PARSING_STYLE = NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;



        /// <summary>
        /// Significant figures of the number.
        /// </summary>
        public string Significand => _significand.Value;
        /// <summary>
        /// Number of decimal places to shift the <see cref="Significand"/>'s decimal to get the number.
        /// </summary>
        public int Exponent => _exponent.Value;

        private readonly string _value;
        private readonly Lazy<string> _significand;
        private readonly Lazy<int> _exponent;



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
        private JsonNumber(string value)
        {
            _value = value;
            _significand = new(() => SignificandOf(value));
            _exponent = new(() => ExponentOf(value));
        } // end JsonNumber



        /// <summary>
        /// Attempt to convert the value to a <see cref="sbyte"/>.
        /// </summary>
        /// <returns></returns>
        public sbyte AsSignedByte() => sbyte.Parse(_value, PARSING_STYLE);
        /// <summary>
        /// Attempt to convert the value to a <see cref="byte"/>.
        /// </summary>
        /// <returns></returns>
        public byte AsByte() => byte.Parse(_value, PARSING_STYLE);

        /// <summary>
        /// Attempt to convert the value to a <see cref="short"/>.
        /// </summary>
        /// <returns></returns>
        public short AsShort() => short.Parse(_value, PARSING_STYLE);
        /// <summary>
        /// Attempt to convert the value to a <see cref="ushort"/>.
        /// </summary>
        /// <returns></returns>
        public ushort AsUnsignedShort() => ushort.Parse(_value, PARSING_STYLE);

        /// <summary>
        /// Attempt to convert the value to a <see cref="int"/>.
        /// </summary>
        /// <returns></returns>
        public int AsInt() => int.Parse(_value, PARSING_STYLE);
        /// <summary>
        /// Attempt to convert the value to a <see cref="uint"/>.
        /// </summary>
        /// <returns></returns>
        public uint AsUnsignedInt() => uint.Parse(_value, PARSING_STYLE);

        /// <summary>
        /// Attempt to convert the value to a <see cref="long"/>.
        /// </summary>
        /// <returns></returns>
        public long AsLong() => long.Parse(_value, PARSING_STYLE);
        /// <summary>
        /// Attempt to convert the value to a <see cref="ulong"/>.
        /// </summary>
        /// <returns></returns>
        public ulong AsUnsignedLong() => ulong.Parse(_value, PARSING_STYLE);



        /// <summary>
        /// Attempt to convert the value to a <see cref="float"/>.
        /// </summary>
        /// <returns></returns>
        public float AsFloat() => float.Parse(_value);
        /// <summary>
        /// Attempt to convert the value to a <see cref="double"/>.
        /// </summary>
        /// <returns></returns>
        public double AsDouble() => double.Parse(_value);
        /// <summary>
        /// Attempt to convert the value to a <see cref="decimal"/>.
        /// </summary>
        /// <returns></returns>
        public decimal AsDecimal() => decimal.Parse(_value, PARSING_STYLE);



        /// <inheritdoc/>
        public override string ToString(JsonFormatting formatting)
        {
            if (formatting.NumberFormatting.Preserve)
                return _value;

            int dp = formatting.NumberFormatting.DecimalPositioning;
            if (formatting.NumberFormatting.PlaceExponent)
            {
                StringBuilder significand = new(Significand);
                bool negative = false;
                if (significand[0] == '-')
                {
                    negative = true;
                    significand.Remove(0, 1);
                }
                int exponent = Exponent;
                if (dp > 0)
                {
                    // significand is always positive, and currently as only digits - thus if is shorter than preferred 0 must be added, with exponent adjustment
                    if (significand.Length < dp)
                    {
                        int unfulfilled = dp - significand.Length;
                        significand.Append('0', unfulfilled);
                        exponent -= unfulfilled;
                    }
                    else if (significand.Length > dp)
                    {
                        int pos = Math.Min(dp, significand.Length); // get position to insert at, either 'dp' or if that would be outside string bounds the string's end
                        exponent += significand.Length - pos; // adjust exponent to reflect shift, number has became smaller by some power of 10 so exponent should be too.
                                                                // significand never contains a decimal, so by subtracting the position we get the number of digits now to the right of the decimal
                        significand = significand.Insert(pos, "."); // shift after updating exponent so decimal isn't count as a digit.
                    }
                }
                else if (dp < 0)
                {
                    int pos = significand.Length + Math.Max(dp, -significand.Length); // get position to insert at, either 'dp' or if that would be outside string bounds the string's end, invert direction so decimal is place from right to left
                    int unfulfilled = Math.Max(0, -dp - significand.Length);
                    exponent += (significand.Length - pos);
                    significand = significand.Insert(pos, ".");
                    if (pos == 0)
                        significand.Insert(0, "0"); // add proceeding 0
                    if(formatting.NumberFormatting.AllowInsignificantDigits)
                        significand.Append('0', unfulfilled);
                }
                if (negative)
                    significand.Insert(0, "-");
                StringBuilder sb = new(significand.Length + 20);
                sb.Append(significand);
                sb.Append(formatting.NumberFormatting.CapitalizeExponent ? 'E' : 'e');
                if (formatting.NumberFormatting.ExplicitlySignExponent && Exponent >= 0)
                    sb.Append('+');
                sb.Append(exponent);
                return sb.ToString();
            }
            throw new NotImplementedException();
            //else if (formatting.NumberFormatting.AllowInsignificantDigits) 
            {

            }
            /*            PlaceExponent
                        CapitalizeExponent
                        ExplicitlySignExponent
                        AllowInsignificantDigits
                        DecimalPositioning*/
        } // end ToString()

        /// <inheritdoc/>
        public override int GetHashCode() => _value.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
#warning Note: might be better to call corresponding AsX method, and catch the overflow exception
            if (obj is JsonNumber other1)
                return other1._significand.Value.Equals(_significand.Value)
                && other1._exponent.Value.Equals(_exponent.Value);
            else if (obj is sbyte other2)
                return ((JsonNumber)other2)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other2)._exponent.Value.Equals(_exponent.Value);
            else if (obj is byte other3)
                return ((JsonNumber)other3)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other3)._exponent.Value.Equals(_exponent.Value);
            else if (obj is short other4)
                return ((JsonNumber)other4)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other4)._exponent.Value.Equals(_exponent.Value);
            else if (obj is ushort other5)
                return ((JsonNumber)other5)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other5)._exponent.Value.Equals(_exponent.Value);
            else if (obj is int other6)
                return ((JsonNumber)other6)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other6)._exponent.Value.Equals(_exponent.Value);
            else if (obj is uint other7)
                return ((JsonNumber)other7)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other7)._exponent.Value.Equals(_exponent.Value);
            else if (obj is long other8)
                return ((JsonNumber)other8)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other8)._exponent.Value.Equals(_exponent.Value);
            else if (obj is ulong other9)
                return ((JsonNumber)other9)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other9)._exponent.Value.Equals(_exponent.Value);
            else if (obj is float other10)
                return ((JsonNumber)other10)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other10)._exponent.Value.Equals(_exponent.Value);
            else if (obj is double other11)
                return ((JsonNumber)other11)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other11)._exponent.Value.Equals(_exponent.Value);
            else if (obj is decimal other12)
                return ((JsonNumber)other12)._significand.Value.Equals(_significand.Value)
                && ((JsonNumber)other12)._exponent.Value.Equals(_exponent.Value);

            return false;
        } // end equals


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
#if DEBUG
            json.IsNotNull();
#endif
            string parse = json.TrimStart();
            if (parse.Length < 1)
                throw new MalformedJsonException();

            Match m = REGEX.Match(parse, 0);
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