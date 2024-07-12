namespace GSR.Jsonic
{
    public sealed class JsonBoolean : IJsonComponent
    {
        public bool Value { get; }



        public JsonBoolean(bool value) 
        {
            Value = value;
        } // end constructor



        public string ToCompressedString() => ToString();

        public override string ToString() => Value ? JsonUtil.JSON_TRUE : JsonUtil.JSON_FALSE;

    } // end class
} // end namespace