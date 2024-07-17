using System.Text;

namespace GSR.Jsonic
{
    public sealed class JsonObject : IJsonComponent
    {
        private readonly Dictionary<JsonString, JsonElement> _elements = new();




        /*
            List<JsonElement> elements = new();
            string parse = json.TrimStart();
            if (parse.Length < 2 || parse[0] != '[')
                throw new MalformedJsonException();

            parse = parse[1..].TrimStart();
            while (parse.Length != 0)
            {
                if (parse[0] == ']')
                {
                    remainder = parse[1..];
                    return elements;
                }
                string k = Regex.Match(parse, JsonString.ENQUOTED_REGEX).Value; // assure at begining
                parse = parse[k.Length..^0].TrimStart();
                JsonString key = new(k);

                if (parse.Length < 1 && parse[0] != ':')
                    throw new MalformedJsonException();

                parse = parse[1..^0].TrimStart();
                elements.Add(JsonElement.ParseJsonStart(parse, out string r));
                parse = r.TrimStart();

                if (parse[0] == ']')
                {
                    remainder = parse[1..];
                    return elements;
                }
                else if (parse[0] != ',')
                    throw new MalformedJsonException();
            }
         */


        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="MalformedJsonException">Provided key wasn't a valid json string.</exception>
        /// value didn't exist
        private JsonElement GetElement(string key) 
        {
            
        }

        indexer 

        add 

            remove

            containsKey
        */

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


        public static JsonObject ParseJsonStart(string parse, out string remainder)
        {
            throw new NotImplementedException();
        } // end ParseJsonStart()

    } // end class
} // end namespace