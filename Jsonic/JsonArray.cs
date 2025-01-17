﻿using System.Collections;
using System.Text;

namespace GSR.Jsonic
{
    public sealed class JsonArray : IJsonComponent, IEnumerable<JsonElement>
    {
        private readonly List<JsonElement> _elements = new();



        public int Count => _elements.Count;

        public JsonElement this[int index]
        {
            get => _elements[index];
            set => _elements[index] = value.IsNotNull();
        } // end indexer



        public JsonArray() { } // end constructor

        public JsonArray(params JsonElement[] elements) : this((IEnumerable<JsonElement>)elements) { } // end constructor

        public JsonArray(IEnumerable<JsonElement> elements)
        {
            foreach (JsonElement element in elements.IsNotNull())
                element.IsNotNull();

            _elements.AddRange(elements);
        } // end constructor()



        public JsonArray Add(bool element) => Add(new JsonElement(element));
        public JsonArray Add(string element) => Add(new JsonElement(element));
        public JsonArray Add(sbyte element) => Add(new JsonElement(element));
        public JsonArray Add(byte element) => Add(new JsonElement(element));
        public JsonArray Add(short element) => Add(new JsonElement(element));
        public JsonArray Add(ushort element) => Add(new JsonElement(element));
        public JsonArray Add(int element) => Add(new JsonElement(element));
        public JsonArray Add(uint element) => Add(new JsonElement(element));
        public JsonArray Add(long element) => Add(new JsonElement(element));
        public JsonArray Add(ulong element) => Add(new JsonElement(element));
        public JsonArray Add(float element) => Add(new JsonElement(element));
        public JsonArray Add(double element) => Add(new JsonElement(element));
        public JsonArray Add(decimal element) => Add(new JsonElement(element));
        public JsonArray Add(JsonNull? element) => Add(new JsonElement(element));
        public JsonArray Add(JsonArray element) => Add(new JsonElement(element));
        public JsonArray Add(JsonBoolean element) => Add(new JsonElement(element));
        public JsonArray Add(JsonNumber element) => Add(new JsonElement(element));
        public JsonArray Add(JsonObject element) => Add(new JsonElement(element));
        public JsonArray Add(JsonString element) => Add(new JsonElement(element));
        public JsonArray Add(JsonElement element)
        {
            _elements.Add(element.IsNotNull());
            return this;
        } // end Add()

        public JsonArray Clear()
        {
            _elements.Clear();
            return this;
        } // end Clear()

        public JsonArray InsertAt(int index, bool element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, string element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, sbyte element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, byte element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, short element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, ushort element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, int element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, uint element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, long element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, ulong element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, float element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, double element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, decimal element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, JsonNull? element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, JsonArray element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, JsonBoolean element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, JsonNumber element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, JsonObject element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, JsonString element) => InsertAt(index, new JsonElement(element));
        public JsonArray InsertAt(int index, JsonElement element)
        {
            _elements.Insert(index, element.IsNotNull());
            return this;
        } // end InsertAt()

        public JsonArray RemoveAt(int index)
        {
            _elements.RemoveAt(index);
            return this;
        } // end RemoveAt()



        public IEnumerator<JsonElement> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        /// <inheritdoc/>
        public string ToCompressedString() => AsString(true);

        public override string ToString() => AsString();

        private string AsString(bool compress = false)
        {
            StringBuilder sb = new("[");
            if (!compress)
                sb.Append('\r');

            for (int i = 0; i < _elements.Count; i++)
            {
                sb.Append(compress ? _elements[i].ToCompressedString() : _elements[i].ToString().Entabbed());

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

        public override bool Equals(object? obj)
        {
            if (obj is not JsonArray b || b.Count != Count)
                return false;

            for (int i = 0; i < Count; i++)
                if (b[i] != this[i])
                    return false;

            return true;
        } // end Equals()

        public override int GetHashCode() => _elements.GetHashCode();

        public static bool operator ==(JsonArray a, JsonArray b) => a.Equals(b);

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
            string parse = json.IsNotNull().TrimStart();
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