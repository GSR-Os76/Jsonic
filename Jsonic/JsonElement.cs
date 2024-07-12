namespace GSR.Jsonic
{
    public sealed class JsonElement : JsonComponent
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



        public override string ToString()
        {
            if (Type == JsonType.Null)
                return JsonUtil.JSON_NULL;

            Value.Options = Options;
            return Value.ToString();
        } // end ToString()

    } // end record class
} // end namespace