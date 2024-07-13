using System.Globalization;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public sealed class JsonNumber : IJsonComponent
    {
        public const string STRICT_VALIDATOR_REGEX = @"^-?(([1-9][0-9]*)|0)(\.[0-9]+)?([eE][-+]?[0-9]+)?$";
        private const NumberStyles PARSING_STYLE = NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

        public string Value { get; }



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
            if (!Regex.IsMatch(json, STRICT_VALIDATOR_REGEX))
                throw new MalformedJsonException($"\"{json}\" is not a valid json numeric");

            Value = json;
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

    } // end class
} // end namespace