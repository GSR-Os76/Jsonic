namespace GSR.Jsonic
{
    public sealed class JsonElement : IJsonComponent
    {
        public IJsonComponent? Value { get; }

        public JsonType Type { get; }



        public JsonElement() : this(null, JsonType.Null) { } // end constructor
        public JsonElement(bool value) : this(new JsonBoolean(value)) { } // end constructor
        public JsonElement(string value) : this(new JsonString(value)) { } // end constructor
        public JsonElement(sbyte value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(byte value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(short value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(ushort value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(int value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(uint value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(long value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(ulong value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(float value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(double value) : this(new JsonNumber(value)) { } // end constructor
        public JsonElement(decimal value) : this(new JsonNumber(value)) { } // end constructor

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



        public JsonArray? AsArray() => (JsonArray?)Value;
        public JsonBoolean? AsBoolean() => (JsonBoolean?)Value;
        public JsonNumber? AsNumber() => (JsonNumber?)Value;
        public JsonObject? AsObject() => (JsonObject?)Value;
        public JsonString? AsString() => (JsonString?)Value;



        public string ToCompressedString() => AsStringC(true);

        public override string ToString() => AsStringC();

        private string AsStringC(bool compress = false) 
        {
            if (Type == JsonType.Null)
                return JsonUtil.JSON_NULL;

            return compress ? Value.ToCompressedString() : Value.ToString();
        } // end AsString()

    } // end record class
} // end namespace