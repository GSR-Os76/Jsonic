using GSR.Jsonic.Formatting;
using System.Collections;
using System.Text;

namespace GSR.Jsonic
{
#warning implement IList<
    /// <summary>
    /// Representation of a json array.
    /// </summary>
    public sealed class JsonArray : AJsonValue, IEnumerable<JsonElement>
    {
        private readonly List<JsonElement> _elements = new();



        /// <summary>
        /// Number of elements in the array.
        /// </summary>
        public int Count => _elements.Count;

        /// <summary>
        /// Get or set the value at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public JsonElement this[int index]
        {
            get => _elements[index];
            set
            {
#if DEBUG
                value.IsNotNull();
#endif
                _elements[index] = value;
            }
        } // end indexer



        /// <inheritdoc/>
        public JsonArray() { } // end constructor

        /// <inheritdoc/>
        public JsonArray(params JsonElement[] elements) : this((IEnumerable<JsonElement>)elements) { } // end constructor

        /// <inheritdoc/>
        public JsonArray(IEnumerable<JsonElement> elements)
        {
#if DEBUG
            foreach (JsonElement element in elements.IsNotNull())
                element.IsNotNull();
#endif
            _elements.AddRange(elements);
        } // end constructor()



        /// <summary>
        /// Add a new <paramref name="element"/> to the end of the array.
        /// </summary>
        /// <param name="element">this</param>
        public JsonArray Add(JsonElement element)
        {
#if DEBUG
            element.IsNotNull();
#endif
            _elements.Add(element);
            return this;
        } // end Add()

        /// <summary>
        /// Remove all the elements from the array.
        /// </summary>
        /// <returns>this</returns>
        public JsonArray Clear()
        {
            _elements.Clear();
            return this;
        } // end Clear()

        /// <summary>
        /// Insert the <paramref name="element"/> at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="element"></param>
        /// <returns>this</returns>
        public JsonArray InsertAt(int index, JsonElement element)
        {
#if DEBUG
            element.IsNotNull();
#endif
            _elements.Insert(index, element);
            return this;
        } // end InsertAt()

        /// <summary>
        /// Remove the element at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>this</returns>
        public JsonArray RemoveAt(int index)
        {
            _elements.RemoveAt(index);
            return this;
        } // end RemoveAt()



        /// <inheritdoc/>
        public IEnumerator<JsonElement> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();



        /// <inheritdoc/>
        public override string ToString(JsonFormatting formatting) 
        {
            throw new NotImplementedException();
            bool compress = false;
            StringBuilder sb = new("[");
            if (!compress)
                sb.Append('\r');

            for (int i = 0; i < _elements.Count; i++)
            {
                sb.Append(compress ? _elements[i].ToString(formatting) : _elements[i].ToString().Entabbed());

                if (i != _elements.Count - 1)
                {
                    sb.Append(',');
                    if (!compress)
                        sb.Append('\r');
                }
            }
            if (!compress)
                sb.Append('\r');

            sb.Append(']');
            return sb.ToString();
        } // end AsString()

        /// <inheritdoc/>
        public override int GetHashCode() => _elements.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is not JsonArray b || b.Count != Count)
                return false;

            for (int i = 0; i < Count; i++)
                if (b[i] != this[i])
                    return false;

            return true;
        } // end Equals()

        /// <inheritdoc/>
        public static bool operator ==(JsonArray a, JsonArray b) => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(JsonArray a, JsonArray b) => !a.Equals(b);



        /// <summary>
        /// Parse the element at the beginning of a string.
        /// </summary>
        /// <param name="json">The input string.</param>
        /// <param name="remainder">The unmodified section of string trailing the leading value.</param>
        /// <returns>A JsonArray containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">A value couldn't be recognized at the string's beginning, or an error occured while parsing the predicted value.</exception>
        public static JsonArray ParseJson(string json, out string remainder)
        {
#if DEBUG
            json.IsNotNull();
#endif
            string parse = json.TrimStart();
            JsonArray array = new();
            if (parse.Length < 2 || parse[0] != '[')
                throw new MalformedJsonException();

            parse = parse[1..].TrimStart();
            if (parse[0] == ']')
            {
                remainder = parse[1..];
                return array;
            }

            while (parse.Length != 0)
            {
                array.Add(JsonElement.ParseJson(parse, out string r));
                parse = r.TrimStart();

                if (parse.Length == 0)
                    throw new MalformedJsonException();

                if (parse[0] == ']')
                {
                    remainder = parse[1..];
                    return array;
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
        /// <returns>A JsonArray containing the parsed Json value.</returns>
        /// <exception cref="MalformedJsonException">If parsing of a value wasn't possible, or there were trailing characters.</exception>
        public static JsonArray ParseJson(string json) => JsonUtil.RequiredEmptyRemainder(ParseJson, json);

    } // end class
} // end namespace