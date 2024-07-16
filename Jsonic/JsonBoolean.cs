namespace GSR.Jsonic
{
#warning could be two singular instances, as the class is immutable and only has two possible states.
    public sealed class JsonBoolean : IJsonComponent
    {
        public bool Value { get; }



        public JsonBoolean(bool value) 
        {
            Value = value;
        } // end constructor



        public string ToCompressedString() => ToString();

        public override string ToString() => Value ? JsonUtil.JSON_TRUE : JsonUtil.JSON_FALSE;

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object? obj) => obj is JsonBoolean b && b.Value == Value;

        public static bool operator ==(JsonBoolean a, JsonBoolean b) => a.Equals(b);

        public static bool operator !=(JsonBoolean a, JsonBoolean b) => !a.Equals(b);


    } // end class
} // end namespace