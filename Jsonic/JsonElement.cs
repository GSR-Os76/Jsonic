namespace GSR.Jsonic
{
    public sealed class JsonElement : IJsonComponent
    {
        // if we're already boxing a boolean sometimes, why not just wrap it in JsonBoolean usurping unboxing? Then eveything is IJsonComponent too. Even storing a bool typed value would not help, as the value property would still just box it. 
        public object? Value { get; }

        public JsonType Type { get; }

        public JsonOptions Options { get; set; } = JsonOptions.Default;



        public JsonElement() : this(null, JsonType.Null) { } // end constructor
        public JsonElement(JsonArray? element) : this(element, JsonType.Array) { } // end constructor
        public JsonElement(bool element) : this(element, JsonType.Boolean) { } // end constructor
        public JsonElement(JsonNumber? element) : this(element, JsonType.Number) { } // end constructor
        public JsonElement(JsonObject? element) : this(element, JsonType.Object) { } // end constructor
        public JsonElement(JsonString? element) : this(element, JsonType.String) { } // end constructor
        private JsonElement(object? element, JsonType type)
        {
            Value = element;
            Type = element is null ? JsonType.Null : type;
        } // end constructor



        public override string ToString()
        {
            switch (Type) 
            {
                case JsonType.Null:
                    return JsonUtil.JSON_NULL;
                case JsonType.Boolean:
                    return ((bool)Value) ? JsonUtil.JSON_TRUE : JsonUtil.JSON_FALSE;
                default:
                    IJsonComponent j = (IJsonComponent)Value;
                    j.Options = Options;
                    return j.ToString();
            }
        } // end ToString()
        //Separate constructors an implement ToString, and json state


    } // end record class
} // end namespace