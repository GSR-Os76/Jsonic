using GSR.Jsonic.Formatting;

namespace GSR.Jsonic
{
    /// <summary>
    /// Common representation of one of the json values.
    /// </summary>
    public sealed class JsonElement : AJsonValue
    {
        private static readonly JsonElement NULL = new();
        private static readonly JsonElement TRUE = new(true);
        private static readonly JsonElement FALSE = new(false);
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
        public override string ToString(JsonFormatting formatting)
        {
            throw new NotImplementedException();
            bool compress = false;
            if (Type == JsonType.Null)
                return JsonNull.JSON_NULL;

            return Value.ToString(formatting);
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


        /// <inheritdoc/>
        public static implicit operator JsonElement(bool value) => value ? TRUE : FALSE;
        /// <inheritdoc/>
        public static implicit operator JsonElement(sbyte value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(byte value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(short value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(ushort value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(int value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(uint value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(long value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(ulong value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(float value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(double value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(decimal value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(string value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(JsonNull? value) => NULL;
        /// <inheritdoc/>
        public static implicit operator JsonElement(JsonArray value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(JsonBoolean value) => value ? TRUE : FALSE;
        /// <inheritdoc/>
        public static implicit operator JsonElement(JsonNumber value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(JsonObject value) => new(value);
        /// <inheritdoc/>
        public static implicit operator JsonElement(JsonString value) => new(value);



        /// <inheritdoc/>
        public static implicit operator bool(JsonElement value) => (JsonBoolean)value;
        /// <inheritdoc/>
        public static implicit operator sbyte(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator byte(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator short(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator ushort(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator int(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator uint(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator long(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator ulong(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator float(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator double(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator decimal(JsonElement value) => (JsonNumber)value;
        /// <inheritdoc/>
        public static implicit operator string(JsonElement value) => (JsonString)value;
        /// <inheritdoc/>
        public static explicit operator JsonNull?(JsonElement value) => value.AsNull();
        /// <inheritdoc/>
        public static explicit operator JsonArray(JsonElement value) => value.AsArray();
        /// <inheritdoc/>
        public static explicit operator JsonBoolean(JsonElement value) => value.AsBoolean();
        /// <inheritdoc/>
        public static explicit operator JsonNumber(JsonElement value) => value.AsNumber();
        /// <inheritdoc/>
        public static explicit operator JsonObject(JsonElement value) => value.AsObject();
        /// <inheritdoc/>
        public static explicit operator JsonString(JsonElement value) => value.AsString();



        /// <summary>
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonElement containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonElement ParseJson(string json, out string remainder)
        {
#if DEBUG
            json.IsNotNull();
#endif
            string parse = json.TrimStart();
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