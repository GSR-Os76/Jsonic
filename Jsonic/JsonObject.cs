using System.Text;

namespace GSR.Jsonic
{
    public sealed class JsonObject : IJsonComponent
    {
        private readonly Dictionary<JsonString, JsonElement> _elements = new();





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