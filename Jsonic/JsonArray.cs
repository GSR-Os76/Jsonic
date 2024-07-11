using System.Collections;

namespace GSR.Jsonic
{
    public sealed class JsonArray : IEnumerable<JsonElement>
    {
        private IList<JsonElement> _elements = new List<JsonElement>();


        public JsonArray() { } // end constructor

        public JsonArray(string json)
        {
#warning parse json array
        } // end constructor



        public IEnumerator<JsonElement> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    } // end class
} // end namespace