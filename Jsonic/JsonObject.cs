using GSR.Jsonic.Formatting;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GSR.Jsonic
{
    /// <summary>
    /// Representation of a json object.
    /// </summary>
    public sealed class JsonObject : AJsonValue, IDictionary<JsonString, JsonElement>
    {
        private readonly IDictionary<JsonString, JsonElement> _elements;



        /// <summary>
        /// Get the current number of key value pairs in the object
        /// </summary>
        public int Count => _elements.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public ICollection<JsonString> Keys => _elements.Keys;

        /// <inheritdoc/>
        public ICollection<JsonElement> Values => _elements.Values;



        /// <summary>
        /// Get or set the value associated with the <paramref name="key"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonElement this[JsonString key]
        {
            get
            {
#if ASSERT
                key.IsNotNull();
#endif
                return _elements[key];
            }
            set
            {
#if ASSERT
                key.IsNotNull();
#endif
                _elements[key] = value ?? (JsonElement)JsonNull.NULL;
            }
        } // end indexer



        /// <inheritdoc/>
        public JsonObject()
        {
            _elements = new Dictionary<JsonString, JsonElement>();
        } // end constructor

        /// <inheritdoc/>
        public JsonObject(params KeyValuePair<JsonString, JsonElement>[] elements) : this((IEnumerable<KeyValuePair<JsonString, JsonElement>>)elements) { } // end constructor

        /// <inheritdoc/>
        public JsonObject(IEnumerable<KeyValuePair<JsonString, JsonElement>> elements)
        {
#if ASSERT
            foreach (KeyValuePair<JsonString, JsonElement> kvp in elements.IsNotNull())
            {
                kvp.Key.IsNotNull();
                kvp.Value.IsNotNull();
            }
#endif
            _elements = new Dictionary<JsonString, JsonElement>(elements);
        } // end constructor



        /// <summary>
        /// Associates the <paramref name="key"/> with null.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonObject Add(JsonString key)
        {
#if ASSERT
            key.IsNotNull();
#endif
            return Add(key, new JsonElement());
        } // end Add()

        /// <summary>
        /// Add a new <paramref name="key"/> <paramref name="value"/> pair to the object.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonObject Add(JsonString key, JsonElement? value)
        {
#if ASSERT
            key.IsNotNull();
#endif
            _elements.Add(key, value ?? (JsonElement)JsonNull.NULL);
            return this;
        } // end Add()

        /// <inheritdoc/>
        public void Add(KeyValuePair<JsonString, JsonElement> item) => _elements.Add(item);

        void IDictionary<JsonString, JsonElement>.Add(JsonString key, JsonElement? value) => _elements.Add(key, value ?? (JsonElement)JsonNull.NULL);

        /// <summary>
        /// Remove all the elements from the array.
        /// </summary>
        /// <returns>this</returns>
        public JsonObject Clear()
        {
            _elements.Clear();
            return this;
        } // end Clear()

        void ICollection<KeyValuePair<JsonString, JsonElement>>.Clear() => _elements.Clear();

        /// <summary>
        /// Check if the object has a propety with the<paramref name="key"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(JsonString key)
        {
#if ASSERT
            key.IsNotNull();
#endif
            return _elements.ContainsKey(key);
        } // end ContainsKey.

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<JsonString, JsonElement> item) => _elements.Contains(item);

        /// <summary>
        /// Does nothing if not contained, else remove value paired with the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>this</returns>
        public JsonObject Remove(JsonString key)
        {
#if ASSERT
            key.IsNotNull();
#endif
            _elements.Remove(key);
            return this;
        } // end Remove()

        bool IDictionary<JsonString, JsonElement>.Remove(JsonString key) => _elements.Remove(key);

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<JsonString, JsonElement> item) => _elements.Remove(item);



        /// <inheritdoc/>
        public bool TryGetValue(JsonString key, [MaybeNullWhen(false)] out JsonElement value) => _elements.TryGetValue(key, out value);

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<JsonString, JsonElement>[] array, int arrayIndex) => _elements.CopyTo(array, arrayIndex);



        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<JsonString, JsonElement>> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();



        /// <inheritdoc/>
        public override string ToString(JsonFormatting formatting)
        {
            StringBuilder sb = new("{");
            if (_elements.Count == 0)
                for (int i = 0; i < formatting.ObjectFormatting.EmptyCollectionNewLining; i++)
                    sb.Append(formatting.NewLineType.Str());
            else
                WriteElements(sb, formatting);
            sb.Append('}');
            string s = sb.ToString();
            return sb.ToString();
        } // end ToString()

        private void WriteElements(StringBuilder stringBuilder, JsonFormatting formatting)
        {
            if (formatting.ObjectFormatting.NewLineProceedingFirstElement)
            {
                stringBuilder.Append(formatting.NewLineType.Str());
                stringBuilder.Append(formatting.ObjectFormatting.Indentation);
            }

            int i = 0;
            foreach (KeyValuePair<JsonString, JsonElement> kvp in _elements)
            {
                stringBuilder.Append(kvp.Key.ToString(formatting));
                stringBuilder.Append(formatting.ObjectFormatting.PostKeyWhitespace);
                stringBuilder.Append(':');
                stringBuilder.Append(formatting.ObjectFormatting.PreValueWhitespace);
                stringBuilder.Append(kvp.Value.ToString(formatting).Entabbed(formatting.NewLineType, formatting.ObjectFormatting.Indentation));


                if (i != _elements.Count - 1)
                {
                    stringBuilder.Append(',');
                    if (formatting.ObjectFormatting.NewLineBetweenElements)
                    {
                        stringBuilder.Append(formatting.NewLineType.Str());
                        stringBuilder.Append(formatting.ObjectFormatting.Indentation);
                    }
                    else
                        stringBuilder.Append(formatting.ObjectFormatting.PostCommaSpacing);
                }
                i++;
            }

            if (formatting.ObjectFormatting.NewLineSucceedingLastElement)
                stringBuilder.Append(formatting.NewLineType.Str());
        } // end WriteElements()

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is JsonObject b && b.Count == Count && b._elements.Keys.All((x) => ContainsKey(x) && b[x].Equals(this[x]));

        /// <inheritdoc/>
        public override int GetHashCode() => _elements.GetHashCode();

        /// <inheritdoc/>
        public static bool operator ==(JsonObject a, JsonObject b) => a.Equals(b);

        /// <inheritdoc/>
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
#if ASSERT
            json.IsNotNull();
#endif
            string parse = json.TrimStart();
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
        public static JsonObject ParseJson(string json) => Util.RequiredEmptyRemainder(ParseJson, json);
    } // end class
} // end namespace