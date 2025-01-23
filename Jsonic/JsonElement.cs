namespace GSR.Jsonic
{
    public sealed class JsonElement : IJsonValue
    {
        /// <summary>
        /// Underlying value of the <see cref="JsonElement"/>.
        /// </summary>
        public IJsonValue? Value { get; }

        /// <summary>
        /// Type of the <see cref="Value"/> of the <see cref="JsonElement"/>.
        /// </summary>
        public JsonType Type { get; }



        /// <summary>
        /// Create a null containing <see cref="JsonElement"/>
        /// </summary>
        public JsonElement() : this(null, JsonType.Null) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(bool value) : this(value ? JsonBoolean.TRUE : JsonBoolean.FALSE) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(string value) : this(new JsonString(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(sbyte value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(byte value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(short value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(ushort value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(int value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(uint value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(long value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(ulong value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(float value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(double value) : this(new JsonNumber(value)) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(decimal value) : this(new JsonNumber(value)) { } // end constructor


        /// <inheritdoc/>
        public JsonElement(JsonNull? value) : this(value, JsonType.Null) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(JsonArray value) : this(value, JsonType.Array) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(JsonBoolean value) : this(value, JsonType.Boolean) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(JsonNumber value) : this(value, JsonType.Number) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(JsonObject value) : this(value, JsonType.Object) { } // end constructor
        /// <inheritdoc/>
        public JsonElement(JsonString value) : this(value, JsonType.String) { } // end constructor
        private JsonElement(IJsonValue? value, JsonType type)
        {
            Value = value;
            Type = value is null ? JsonType.Null : type;
        } // end constructor



        /// <summary>
        /// Get the underlying <see cref="Value"/> as a <see cref="JsonNull"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidJsonCastException"></exception>
        public JsonNull? AsNull() => Type == JsonType.Null ? (JsonNull?)Value : throw new InvalidJsonCastException(Type, JsonType.Null);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable value
#pragma warning disable CS8603 // Possible null reference return.
        /// <summary>
        /// Get the underlying <see cref="Value"/> as a <see cref="JsonArray"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidJsonCastException"></exception>
        public JsonArray AsArray() => Type == JsonType.Array ? (JsonArray)Value : throw new InvalidJsonCastException(Type, JsonType.Array);
        /// <summary>
        /// Get the underlying <see cref="Value"/> as a <see cref="JsonBoolean"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidJsonCastException"></exception>
        public JsonBoolean AsBoolean() => Type == JsonType.Boolean ? (JsonBoolean)Value : throw new InvalidJsonCastException(Type, JsonType.Boolean);
        /// <summary>
        /// Get the underlying <see cref="Value"/> as a <see cref="JsonNumber"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidJsonCastException"></exception>
        public JsonNumber AsNumber() => Type == JsonType.Number ? (JsonNumber)Value : throw new InvalidJsonCastException(Type, JsonType.Number);
        /// <summary>
        /// Get the underlying <see cref="Value"/> as a <see cref="JsonObject"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidJsonCastException"></exception>
        public JsonObject AsObject() => Type == JsonType.Object ? (JsonObject)Value : throw new InvalidJsonCastException(Type, JsonType.Object);
        /// <summary>
        /// Get the underlying <see cref="Value"/> as a <see cref="JsonString"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidJsonCastException"></exception>
        public JsonString AsString() => Type == JsonType.String ? (JsonString)Value : throw new InvalidJsonCastException(Type, JsonType.String);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable value
#pragma warning restore CS8603 // Possible null reference return.



        /// <inheritdoc/>
        public string ToCompressedString() => AsStringC(true);

        /// <inheritdoc/>
        public override string ToString() => AsStringC();

        private string AsStringC(bool compress = false)
        {
            if (Type == JsonType.Null)
                return JsonNull.JSON_NULL;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
            return compress ? Value.ToCompressedString() : Value.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8603 // Possible null reference return.

        } // end AsStringC()

        /// <inheritdoc/>
        public override int GetHashCode() => Tuple.Create(Value, Type).GetHashCode();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is JsonElement b && b.Type == Type && (Type == JsonType.Null || b.Value.Equals(Value)); // apparently C# == doesn't look up type. using it here, even though overridden in the actual type, fails. Presumably because Value is stored as IJsonComponent? not the realized type?
#pragma warning restore CS8602 // Dereference of a possibly null reference.


        /// <inheritdoc/>
        public static bool operator ==(JsonElement a, JsonElement b) => a.Equals(b);

        /// <inheritdoc/>
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
            string parse = json.IsNotNull().TrimStart();
            if (parse.Length < 1)
                throw new MalformedJsonException();

            switch (parse[0])
            {
                case 'n':
                    return new(JsonNull.ParseJson(parse, out remainder));
                case 'f':
                case 't':
                    return new(JsonBoolean.ParseJson(parse, out remainder));
                case '"':
                    return new(JsonString.ParseJson(parse, out remainder));
                case '[':
                    return new(JsonArray.ParseJson(parse, out remainder));
                case '{':
                    return new(JsonObject.ParseJson(parse, out remainder));
                default:
                    return new(JsonNumber.ParseJson(parse, out remainder));
            }
        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonElement containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonElement ParseJson(string json) => JsonUtil.RequiredEmptyRemainder(ParseJson, json);

    } // end record class
} // end namespace