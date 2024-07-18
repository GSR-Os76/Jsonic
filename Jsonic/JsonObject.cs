﻿using System.Text;
using System.Text.RegularExpressions;

namespace GSR.Jsonic
{
    public sealed class JsonObject : IJsonComponent
    {
        private readonly Dictionary<JsonString, JsonElement> _elements = new();



        public int Count => _elements.Count;

        public JsonElement this[string index]
        {
            get => _elements[new JsonString(index)];
            set => _elements[new JsonString(index)] = value;
        } // end indexer

        public JsonElement this[JsonString index]
        {
            get => _elements[index];
            set => _elements[index] = value;
        } // end indexer



        public JsonObject() { } // end constructor

        public JsonObject(string json)
        {
            Parse(json, out string r).Aggregate(this, (seed, kvp) => seed.Add(kvp.Key, kvp.Value));
            if (!r.Trim().Equals(string.Empty))
                throw new MalformedJsonException();
        } // end constructor



#warning see about indexers for all of theses

        /// <summary>
        /// Adds a null element to the object.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonObject Add(string key) => Add(key, new JsonElement());

        public JsonObject Add(string key, JsonArray? value) => Add(key, new JsonElement(value));

        public JsonObject Add(string key, JsonBoolean? value) => Add(key, new JsonElement(value));

        public JsonObject Add(string key, JsonNumber? value) => Add(key, new JsonElement(value));

        public JsonObject Add(string key, JsonObject? value) => Add(key, new JsonElement(value));

        public JsonObject Add(string key, JsonString? value) => Add(key, new JsonElement(value));

        public JsonObject Add(string key, JsonElement value) => Add(new JsonString(key), value);

        /// <summary>
        /// Adds a null element to the object.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonObject Add(JsonString key) => Add(key, new JsonElement());

        public JsonObject Add(JsonString key, JsonArray? value) => Add(key, new JsonElement(value));

        public JsonObject Add(JsonString key, JsonBoolean? value) => Add(key, new JsonElement(value));

        public JsonObject Add(JsonString key, JsonNumber? value) => Add(key, new JsonElement(value));

        public JsonObject Add(JsonString key, JsonObject? value) => Add(key, new JsonElement(value));

        public JsonObject Add(JsonString key, JsonString? value) => Add(key, new JsonElement(value));

        public JsonObject Add(JsonString key, JsonElement value) 
        {
            _elements[key] = value;
            return this;
        } // end Add()



        // ContainsKey()



        public string ToCompressedString() => AsString(true);

        public override string ToString() => AsString();

        private string AsString(bool compress = false)
        {
            StringBuilder sb = new("{");
            if (!compress)
                sb.Append('\r');

            for (int i = 0; i < _elements.Count; i++)
            {
                if (!compress)
                    sb.Append('\t'); 

                KeyValuePair<JsonString, JsonElement> kvp = _elements.ElementAt(i);
                sb.Append(compress ? kvp.Key.ToCompressedString() : kvp.Key.ToString());

                sb.Append(':');
                if (!compress)
                    sb.Append(' ');

                sb.Append(compress ? kvp.Value.ToCompressedString() : kvp.Value.ToString());

                if (i == _elements.Count - 2)
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

        private static List<KeyValuePair<JsonString, JsonElement>> Parse(string json, out string remainder) 
        {
            List<KeyValuePair<JsonString, JsonElement>> elements = new();
            string parse = json.TrimStart();
            if (parse.Length < 2 || parse[0] != '{')
                throw new MalformedJsonException();

            parse = parse[1..].TrimStart();
            if (parse[0] == '}')
            {
                remainder = parse[1..];
                return elements;
            }

            while (parse.Length != 0)
            {
                string k = Regex.Match(parse, JsonString.ENQUOTED_REGEX).Value;
                parse = parse[k.Length..].TrimStart();
                JsonString key = new(k, true);

                if (elements.Where((x) => x.Key.Equals(key)).Any())
                    throw new MalformedJsonException($"Duplicate key encountered: {key}");

                if (parse[0] != ':')
                    throw new MalformedJsonException();

                parse = parse[1..].TrimStart();

                elements.Add(KeyValuePair.Create(key, JsonElement.ParseJsonStart(parse, out string r)));
                parse = r.TrimStart();

                if (parse[0] == '}')
                {
                    remainder = parse[1..];
                    return elements;
                }
                else if (parse[0] != ',')
                    throw new MalformedJsonException();
                else
                    parse = parse[1..].TrimStart();
            }
            throw new MalformedJsonException();
        } // end Parse()

        public static JsonObject ParseJsonStart(string parse, out string remainder) =>  Parse(parse, out remainder).Aggregate(new JsonObject(), (seed, kvp) => seed.Add(kvp.Key, kvp.Value));

    } // end class
} // end namespace