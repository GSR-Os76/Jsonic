using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public sealed class JsonElement : IJsonComponent
    {
        public IJsonComponent? Value { get; }

        public JsonType Type { get; }



        public JsonElement() : this(null, JsonType.Null) { } // end constructor
        public JsonElement(bool value) : this(new JsonBoolean(value)) { } // end constructor
        public JsonElement(string value, bool expectEnquoted = false) : this(new JsonString(value, expectEnquoted)) { } // end constructor
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
        } // end AsStringC()



        // using parse this way is inconsistent with it's meaning in JsonString. Maybe make ParseJson/ParseJsonStart/ParseString(JsonString specific)
        public static JsonElement ParseJsonStart(string parse, out string remainder)
        {
            switch (parse[0])
            {
                case 'n':
                    if (parse.Length < JsonUtil.JSON_NULL.Length || !parse[0..JsonUtil.JSON_NULL.Length].Equals(JsonUtil.JSON_NULL))
                        throw new MalformedJsonException($"Couldn't read element at the start of \"{parse}\".");

                    remainder = parse[JsonUtil.JSON_NULL.Length..^0];
                    return new();
                case 'f':
                    if (parse.Length < JsonUtil.JSON_FALSE.Length || !parse[0..JsonUtil.JSON_FALSE.Length].Equals(JsonUtil.JSON_FALSE))
                        throw new MalformedJsonException($"Couldn't read element at the start of \"{parse}\".");

                    remainder = parse[JsonUtil.JSON_FALSE.Length..^0];
                    return new(false);
                case 't':
                    if (parse.Length < JsonUtil.JSON_TRUE.Length || !parse[0..JsonUtil.JSON_TRUE.Length].Equals(JsonUtil.JSON_TRUE))
                        throw new MalformedJsonException($"Couldn't read element at the start of \"{parse}\".");

                    remainder = parse[JsonUtil.JSON_TRUE.Length..^0];
                    return new(true);
                case '"':
                    Match m = new Regex(JsonString.ENQUOTED_REGEX).Match(parse, 0);
                    if(!m.Success)
                        throw new MalformedJsonException($"Couldn't read element at the start of \"{parse}\".");

                    string s = m.Value;
                    remainder = parse[s.Length..^0];
                    return new(s, true);
                case '[':
                    return new(JsonArray.ParseJsonStart(parse, out remainder));
                case '{':
                    return new(JsonObject.ParseJsonStart(parse, out remainder));
                default:
                    m = new Regex(JsonNumber.REGEX).Match(parse, 0);  // why is this variable still in scope? that seems curious, but very valuable
                    if (!m.Success)
                        throw new MalformedJsonException($"Couldn't read element at the start of \"{parse}\".");

                    s = m.Value;
                    remainder = parse[s.Length..^0];
                    return new(new JsonNumber(s));
            }
        } // end ParseJsonStart()

    } // end record class
} // end namespace