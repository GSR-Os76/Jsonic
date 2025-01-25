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
        public float AsFloat()
        {
            float f = float.Parse(_value);
            if (f == float.PositiveInfinity || f == float.NegativeInfinity)
                throw new OverflowException("Value was either too large or too small for a float");
            return f;
        } // end AsFloat()
        /// <summary>
        /// Attempt to convert the value to a <see cref="double"/>.
        /// </summary>
        /// <returns></returns>
        public double AsDouble()
        {
            double f = double.Parse(_value);
            if (f == double.PositiveInfinity || f == double.NegativeInfinity)
                throw new OverflowException("Value was either too large or too small for a double");
            return f;
        } // end AsDouble()
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

            bool aid = formatting.NumberFormatting.AllowInsignificantDigits;
            int dp = formatting.NumberFormatting.DecimalPositioning;
            StringBuilder significand = new(Significand);
            bool negative = false;
            if (significand[0] == '-')
            {
                negative = true;
                significand.Remove(0, 1);
            }
            int exponent = Exponent;

            if (formatting.NumberFormatting.PlaceExponent)
            {
                if (dp > 0)
                    exponent += PositionDecimalLeft(significand, dp);
                else if (dp < 0)
                    exponent += PositionDecimalRight(significand, -dp, aid);

                // add the exponent after the processed significand
                significand.Append(formatting.NumberFormatting.CapitalizeExponent ? 'E' : 'e');
                if (formatting.NumberFormatting.ExplicitlySignExponent && exponent >= 0)
                    significand.Append('+');
                significand.Append(exponent);
            }
            else // not placing exponent
            {
                if (exponent < 0)
                    UnexponentializeNegativeExponent(significand, exponent, aid ? -dp : 0);
                else
                {
                    if (exponent > 0) // postive exponent means number of times to multiply by ten, so add that many trailing 0s
                        significand.Append('0', exponent);

                    if (aid && dp < 0) // being of positive or 0 exponent will not contain a decimal - add as many insignificant 0s as necessary to satisy DecimalPlacement.
                    {
                        significand.Append('.');
                        significand.Append('0', -dp);
                    }
                }
            }

            if (negative)
                significand.Insert(0, "-");

            return significand.ToString();
        } // end ToString()

        /// <summary>
        /// Places the a decimal point in the significand, putting <paramref name="decimalPositioning"/> values to the left. 
        /// Decimal point might be imagined, factional part is insignificant.
        /// </summary>
        /// <param name="unsignedSignificand"></param>
        /// <param name="decimalPositioning"></param>
        /// <returns>The power that the expondent has been shifted by</returns>
        private static int PositionDecimalLeft(StringBuilder unsignedSignificand, int decimalPositioning)
        {
            if (unsignedSignificand.Length < decimalPositioning) // less digits in significand than desired count, pad with 0s and adjust exponent accordingly
            {
                int unfulfilled = decimalPositioning - unsignedSignificand.Length;
                unsignedSignificand.Append('0', unfulfilled);
                return -unfulfilled;
            }
            else if (unsignedSignificand.Length > decimalPositioning) // signficiand has more digits than the desire, and decimal will need to be placed inside it.
            {
                int exponent = unsignedSignificand.Length - decimalPositioning; // exponent adjust is difference because it's the number of values being moved to the right of the decimal point.
                unsignedSignificand.Insert(decimalPositioning, "."); // position coincides with insertion index.
                return exponent;
            }
            return 0; // length was equal to desire, and no adjustment is needed.
        } // end PositionDecimalLeft()

        /// <summary>
        /// Places the a decimal point in the significand, putting <paramref name="decimalPositioning"/> values to the right. 
        /// Decimal point might be imagined, factional part is insignificant.
        /// </summary>
        /// <param name="unsignedSignificand"></param>
        /// <param name="decimalPositioning"></param>
        /// <param name="allowInsignificantDigits"></param>
        /// <returns>The power that the expondent has been shifted by</returns>
        private static int PositionDecimalRight(StringBuilder unsignedSignificand, int decimalPositioning, bool allowInsignificantDigits)
        {
            if (unsignedSignificand.Length <= decimalPositioning) // less digits in significand than desired count, will require 0s for padding.
            {
                int significantLength = unsignedSignificand.Length;
                int unfulfille = decimalPositioning - significantLength; // get number of 0s to add

                if (allowInsignificantDigits)
                    unsignedSignificand.Append('0', unfulfille); // if insignificant 0s are allowed prefer them over significant 0s
                else
                    unsignedSignificand.Insert(0, "0", unfulfille);

                unsignedSignificand.Insert(0, "0.");
                return allowInsignificantDigits ? significantLength : decimalPositioning;
            }

            int pos = unsignedSignificand.Length - decimalPositioning;
            unsignedSignificand.Insert(pos, ".");
            return decimalPositioning;
        } // end PositionDecimalRight()

        /// <summary>
        /// Convert a signficand and exponent, where the exponent is negative, to a single number.
        /// </summary>
        /// <param name="unsignedSignificand"></param>
        /// <param name="exponent"></param>
        /// <param name="decimalPositioning"></param>
        private static void UnexponentializeNegativeExponent(StringBuilder unsignedSignificand, int exponent, int decimalPositioning)
        {
            if (unsignedSignificand.Length <= -exponent) // significant has less or equal digits than the exponent, thus all significant digits will be right of decimal.
            {
                unsignedSignificand.Insert(0, "0", -exponent - unsignedSignificand.Length); // add proceeding 0s to fulfill exponent shift
                SatisfyInsignificantZeros(unsignedSignificand, decimalPositioning, unsignedSignificand.Length);
                unsignedSignificand.Insert(0, "0.");
            }
            else // more digits than exponent shift, thus decimal is within the significand.
            {
                int pos = unsignedSignificand.Length + exponent;
                SatisfyInsignificantZeros(unsignedSignificand, decimalPositioning, unsignedSignificand.Length - pos);
                unsignedSignificand.Insert(pos, "."); // all insignficant 0's are after pos, so it should still be valid as the decimal index.
            }
        } // end UnexponentializeNegativeExponent()

        /// <summary>
        /// Add insignificant 0s if required to meet a deired number of digits to the right of the decimal.
        /// </summary>
        /// <param name="significand"></param>
        /// <param name="decimalPositioning"></param>
        /// <param name="rightHandLength"></param>
        private static void SatisfyInsignificantZeros(StringBuilder significand, int decimalPositioning, int rightHandLength)
        {
            if (decimalPositioning > 0)
            {
                int unfullfilled = decimalPositioning - rightHandLength; // calculate how many digits to the right of the decimal positionings aren't occupied, but are desired to be
                if (unfullfilled > 0)
                    significand.Append('0', unfullfilled);
            }
        } // end SatisfyInsignificantZeros()



        /// <inheritdoc/>
        public override int GetHashCode() => _value.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is JsonNumber other1)
                return other1._significand.Value.Equals(_significand.Value)
                && other1._exponent.Value.Equals(_exponent.Value);

            try
            {
                if (obj is sbyte other2)
                    return other2 == (sbyte)this;
                else if (obj is byte other3)
                    return other3 == (byte)this;
                else if (obj is short other4)
                    return other4 == (short)this;
                else if (obj is ushort other5)
                    return other5 == (ushort)this;
                else if (obj is int other6)
                    return other6 == (int)this;
                else if (obj is uint other7)
                    return other7 == (uint)this;
                else if (obj is long other8)
                    return other8 == (long)this;
                else if (obj is ulong other9)
                    return other9 == (ulong)this;
                else if (obj is float other10)
                    return other10 == (float)this;
                else if (obj is double other11)
                    return other11 == (double)this;
                else if (obj is decimal other12)
                    return other12 == (decimal)this;
            }
            catch (OverflowException) { }
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
        public static JsonNumber ParseJson(string json) => Util.RequiredEmptyRemainder(ParseJson, json);
    } // end class
} // end namespace