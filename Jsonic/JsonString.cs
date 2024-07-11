using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public sealed class JsonString
    {
        public const string STRICT_VALIDATOR_REGEX = @"^""([^\\""]|(\\([""\\/bfnrt]|(u[0-9a-fA-F]{4}))))*""$";

        public string Value { get; }



        public JsonString(string json)
        {
            if (!Regex.IsMatch(json, STRICT_VALIDATOR_REGEX))
                throw new MalformedJsonException($"\"{json}\" is not a valid json string");

            Value = json;
        } // end constructor()


        public override string ToString() => Value;


        public static implicit operator string(JsonString a) => a.Value;
        public static explicit operator JsonString(string a) => new(a);
    } // end class
} // end namespace