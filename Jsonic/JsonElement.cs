using System.Text.RegularExpressions;

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



        public object? AsNull => Type == JsonType.Null ? null : throw new InvalidJsonCastException(Type, JsonType.Null);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable value
#pragma warning disable CS8603 // Possible null reference return.
        public JsonArray AsArray() => Type == JsonType.Array ? (JsonArray)Value : throw new InvalidJsonCastException(Type, JsonType.Array);
        public JsonBoolean AsBoolean() => Type == JsonType.Boolean ? (JsonBoolean)Value : throw new InvalidJsonCastException(Type, JsonType.Boolean);
        public JsonNumber AsNumber() => Type == JsonType.Number ? (JsonNumber)Value : throw new InvalidJsonCastException(Type, JsonType.Number);
        public JsonObject AsObject() => Type == JsonType.Object ? (JsonObject)Value : throw new InvalidJsonCastException(Type, JsonType.Object);
        public JsonString AsString() => Type == JsonType.String ? (JsonString)Value : throw new InvalidJsonCastException(Type, JsonType.String);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable value
#pragma warning restore CS8603 // Possible null reference return.



        /// <inheritdoc/>
        public string ToCompressedString() => AsStringC(true);

        public override string ToString() => AsStringC();

        private string AsStringC(bool compress = false)
        {
            if (Type == JsonType.Null)
                return JsonUtil.JSON_NULL;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
            return compress ? Value.ToCompressedString() : Value.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8603 // Possible null reference return.

        } // end AsStringC()

        public override bool Equals(object? obj) => obj is JsonElement b && b.Type == Type && (Type == JsonType.Null || b.Value.Equals(Value)); // apparently C# == doesn't look up type. using it here, even though overridden in the actual type, fails. Presumably because Value is stored as IJsonComponent? not the realized type?

        public override int GetHashCode() => Tuple.Create(Value, Type).GetHashCode();

        public static bool operator ==(JsonElement a, JsonElement b) => a.Equals(b);

        public static bool operator !=(JsonElement a, JsonElement b) => !a.Equals(b);



        /// <summary>
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonElement containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonElement ParseJson(string json, out string remainder)
        {
            string parse = json.TrimStart();
            if (parse.Length < 1)
                throw new MalformedJsonException();

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
                    return new(JsonString.ParseJson(parse, out remainder));
                case '[':
                    return new(JsonArray.ParseJsonStart(parse, out remainder));
                case '{':
                    return new(JsonObject.ParseJsonStart(parse, out remainder));
                default:
                    return new(JsonNumber.ParseJson(parse, out remainder));
            }
        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonElement containing the parse Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonElement ParseJson(string json) 
        {
            JsonElement e = ParseJson(json, out string r);
            if (!r.Trim().Equals(string.Empty))
                throw new MalformedJsonException();

            return e;
        } // end ParseJson()

    } // end record class
} // end namespace