using System.Text;

namespace GSR.Jsonic
{
    public sealed class JsonObject : IJsonComponent
    {
        private readonly Dictionary<JsonString, JsonElement> _elements = new();



        public int Count => _elements.Count;

        public JsonElement this[string key]
        {
            get => _elements[new JsonString(key.IsNotNull())];
            set => _elements[new JsonString(key.IsNotNull())] = value.IsNotNull();
        } // end indexer

        public JsonElement this[JsonString key]
        {
            get => _elements[key.IsNotNull()];
            set => _elements[key.IsNotNull()] = value.IsNotNull();
        } // end indexer



        public JsonObject() { } // end constructor

        public JsonObject(params KeyValuePair<JsonString, JsonElement>[] elements) : this((IEnumerable<KeyValuePair<JsonString, JsonElement>>)elements) { } // end constructor

        public JsonObject(IEnumerable<KeyValuePair<JsonString, JsonElement>> elements)
        {
            foreach (KeyValuePair<JsonString, JsonElement> kvp in elements.IsNotNull())
                _elements.Add(kvp.Key.IsNotNull(), kvp.Value.IsNotNull());
        } // end constructor


        public JsonString[] GetKeySet() => _elements.Keys.ToArray();

        /// <summary>
        /// Adds a null element to the object.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonObject AddNull(string key) => Add(key, new JsonElement());
        public JsonObject Add(string key, bool value) => Add(key, new JsonBoolean(value));
        public JsonObject Add(string key, string element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, sbyte element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, byte element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, short element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, ushort element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, int element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, uint element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, long element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, ulong element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, float element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, double element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, decimal element) => Add(key, new JsonElement(element));
        public JsonObject Add(string key, JsonNull? value) => Add(key, new JsonElement(value));
        public JsonObject Add(string key, JsonArray value) => Add(key, new JsonElement(value));
        public JsonObject Add(string key, JsonBoolean value) => Add(key, new JsonElement(value));
        public JsonObject Add(string key, JsonNumber value) => Add(key, new JsonElement(value));
        public JsonObject Add(string key, JsonObject value) => Add(key, new JsonElement(value));
        public JsonObject Add(string key, JsonString value) => Add(key, new JsonElement(value));
        public JsonObject Add(string key, JsonElement value) => Add(new JsonString(key), value);
        /// <summary>
        /// Adds a null element to the object.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonObject AddNull(JsonString key) => Add(key, new JsonElement());
        public JsonObject Add(JsonString key, bool value) => Add(key, new JsonElement(value));
        public JsonObject Add(JsonString key, string element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, sbyte element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, byte element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, short element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, ushort element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, int element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, uint element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, long element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, ulong element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, float element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, double element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, decimal element) => Add(key, new JsonElement(element));
        public JsonObject Add(JsonString key, JsonNull? value) => Add(key, new JsonElement(value));
        public JsonObject Add(JsonString key, JsonArray value) => Add(key, new JsonElement(value));
        public JsonObject Add(JsonString key, JsonBoolean value) => Add(key, new JsonElement(value));
        public JsonObject Add(JsonString key, JsonNumber value) => Add(key, new JsonElement(value));
        public JsonObject Add(JsonString key, JsonObject value) => Add(key, new JsonElement(value));
        public JsonObject Add(JsonString key, JsonString value) => Add(key, new JsonElement(value));
        public JsonObject Add(JsonString key, JsonElement value)
        {
            _elements.Add(key.IsNotNull(), value.IsNotNull());
            return this;
        } // end Add()

        public bool ContainsKey(string key) => ContainsKey(new JsonString(key));
        public bool ContainsKey(JsonString key) => _elements.ContainsKey(key.IsNotNull());

        public JsonObject Remove(string key) => Remove(new JsonString(key));
        /// <summary>
        /// Does nothing if not contained, else remove value paired with the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonObject Remove(JsonString key)
        {
            _elements.Remove(key.IsNotNull());
            return this;
        } // end Remove()



        public string ToCompressedString() => AsString(true);

        public override string ToString() => AsString();

        private string AsString(bool compress = false)
        {
            StringBuilder sb = new("{");
            if (!compress)
                sb.Append('\r');

            JsonString[] keys = _elements.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                JsonString key = keys[i];
                sb.Append(compress
                    ? $"{key}:{_elements[key].ToCompressedString()}"
                    : $"{key}: {_elements[key].ToString()}".Entabbed());

                if (i != _elements.Count - 1)
                {
                    sb.Append(',');
                    if (!compress)
                        sb.Append('\r');
                }
            }
            if (!compress)
                sb.Append('\r');

            sb.Append('}');
            return sb.ToString();
        } // end AsString()

        public override bool Equals(object? obj) => obj is JsonObject b && b.Count == Count && b._elements.Keys.All((x) => ContainsKey(x) && b[x].Equals(this[x]));

        public override int GetHashCode() => _elements.GetHashCode();

        public static bool operator ==(JsonObject a, JsonObject b) => a.Equals(b);

        public static bool operator !=(JsonObject a, JsonObject b) => !a.Equals(b);



        /// <summary>
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonObject containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonObject ParseJson(string json, out string remainder)
        {
            string parse = json.IsNotNull().TrimStart();
            JsonObject obj = new();
            if (parse.Length < 2 || parse[0] != '{')
                throw new MalformedJsonException();

            parse = parse[1..].TrimStart();
            if (parse[0] == '}')
            {
                remainder = parse[1..];
                return obj;
            }

            while (parse.Length != 0)
            {
                JsonString key = JsonString.ParseJson(parse, out parse);
                parse = parse.TrimStart();

                if (obj.ContainsKey(key))
                    throw new MalformedJsonException($"Duplicate key encountered: {key}");

                if (parse[0] != ':')
                    throw new MalformedJsonException();

                parse = parse[1..].TrimStart();

                obj.Add(key, JsonElement.ParseJson(parse, out string r));
                parse = r.TrimStart();

                if (parse[0] == '}')
                {
                    remainder = parse[1..];
                    return obj;
                }
                else if (parse[0] != ',')
                    throw new MalformedJsonException();
                else
                    parse = parse[1..].TrimStart();
            }
            throw new MalformedJsonException();
        } // end ParseJson()

        /// <summary>
        /// Reads all of a string as a single Json value with no superfluous non-whitespace characters.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <returns>A JsonObject containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonObject ParseJson(string json) => JsonUtil.RequiredEmptyRemainder(ParseJson, json);

    } // end class
} // end namespace