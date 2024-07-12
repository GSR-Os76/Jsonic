namespace GSR.Jsonic
{
    public sealed class JsonElement : IJsonComponent
    {
        public IJsonComponent? Value { get; }

        public JsonType Type { get; }



        public JsonElement() : this(null, JsonType.Null) { } // end constructor
        public JsonElement(bool value) : this(new JsonBoolean(value)) { } // end constructor
        public JsonElement(string value) : this(new JsonString(value)) { } // end constructor
        public JsonElement(JsonArray? value) : this(value, JsonType.Array) { } // end constructor
        public JsonElement(JsonBoolean? value) : this(value, JsonType.Boolean) { } // end constructor
        public JsonElement(JsonNumber? value) : this(value, JsonType.Number) { } // end constructor
        public JsonElement(JsonObject? value) : this(value, JsonType.Object) { } // end constructor
        public JsonElement(JsonString? value) : this(value, JsonType.String) { } // end constructor
        private JsonElement(IJsonComponent? value, JsonType type)
        {
            Value = value;
            Type = value is null ? JsonType.Null : type;
        } // end constructor



#warning add helper methods for quick casting retrieval?



        public string ToCompressedString() => AsString(true);

        public override string ToString() => AsString();

        private string AsString(bool compress = false) 
        {
            if (Type == JsonType.Null)
                return JsonUtil.JSON_NULL;

            return compress ? Value.ToCompressedString() : Value.ToString();
        } // end AsString()

    } // end record class
} // end namespace