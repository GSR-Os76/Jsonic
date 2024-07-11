using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public sealed class JsonNumber
    {
        public const string STRICT_VALIDATOR_REGEX = @"^-?(([1-9][0-9]*)|0)(\.[0-9]+)?([eE][-+]?[0-9]+)?$";

        public string Value { get; }



        public JsonNumber(string json)
        {
            if (!Regex.IsMatch(json, STRICT_VALIDATOR_REGEX))
                throw new MalformedJsonException($"\"{json}\" is not a valid json numeric");

#warning maybe cache orginal value but process out exponent to allow wider casting into integrals, currently Xe+Y will fail even when it's within range for a integer conversion. SIMPLY: make more tolerant in converting to C# integral types
            Value = json;
        } // end JsonNumber



        public override string ToString() => Value;


        public static implicit operator sbyte(JsonNumber a) => sbyte.Parse(a.Value);
        public static explicit operator JsonNumber(sbyte a) => new(a.ToString());
        public static implicit operator byte(JsonNumber a) => byte.Parse(a.Value);
        public static explicit operator JsonNumber(byte a) => new(a.ToString());

        public static implicit operator short(JsonNumber a) => short.Parse(a.Value);
        public static explicit operator JsonNumber(short a) => new(a.ToString());
        public static implicit operator ushort(JsonNumber a) => ushort.Parse(a.Value);
        public static explicit operator JsonNumber(ushort a) => new(a.ToString());

        public static implicit operator int(JsonNumber a) => int.Parse(a.Value);
        public static explicit operator JsonNumber(int a) => new(a.ToString());
        public static implicit operator uint(JsonNumber a) => uint.Parse(a.Value);
        public static explicit operator JsonNumber(uint a) => new(a.ToString());

        public static implicit operator long(JsonNumber a) => long.Parse(a.Value);
        public static explicit operator JsonNumber(long a) => new(a.ToString());
        public static implicit operator ulong(JsonNumber a) => ulong.Parse(a.Value);
        public static explicit operator JsonNumber(ulong a) => new(a.ToString());

        public static implicit operator nint(JsonNumber a) => nint.Parse(a.Value);
        public static explicit operator JsonNumber(nint a) => new(a.ToString());
        public static implicit operator nuint(JsonNumber a) => nuint.Parse(a.Value);
        public static explicit operator JsonNumber(nuint a) => new(a.ToString());



        public static implicit operator float(JsonNumber a) => float.Parse(a.Value);
        public static explicit operator JsonNumber(float a) => new(a.ToString());

        public static implicit operator double(JsonNumber a) => double.Parse(a.Value);
        public static explicit operator JsonNumber(double a) => new(a.ToString());

        public static implicit operator decimal(JsonNumber a) => decimal.Parse(a.Value);
        public static explicit operator JsonNumber(decimal a) => new(a.ToString());

    } // end class
} // end namespace