using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public class JsonNumber
    {
        public const string STRICT_VALIDATOR_REGEX = @"^-?(([1-9][0-9]*)|0)(\.[0-9]+)?([eE][-+]?[0-9]+)?$";

        public string Value { get; }



        public JsonNumber(string value)
        {
            if (!Regex.IsMatch(value, STRICT_VALIDATOR_REGEX))
                throw new MalformedJsonException($"\"{value}\" is not a valid json numeric");

            Value = value;
        } // end JsonNumber



        public override string ToString() => Value;

        //public static explicit operator byte(JsonNumber a) => byte.Parse(a.Value);
    } // end class
} // end namespace