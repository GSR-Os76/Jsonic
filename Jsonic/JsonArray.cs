using System.Collections;
using System.Text;

namespace GSR.Jsonic
{
    public sealed class JsonArray : IJsonComponent, IEnumerable<JsonElement>
    {
        private readonly List<JsonElement> _elements = new List<JsonElement>();



        public int Count => _elements.Count;

        public JsonElement this[int index]
        {
            get => _elements[index];
            set => _elements[index] = value;
        } // end indexer



        public JsonArray() { } // end constructor

        public JsonArray(params JsonElement[] elements) => _elements.AddRange(elements);

        public JsonArray(string json)
        {
#warning parse json array
        } // end constructor



        public void Add(JsonElement element) => _elements.Add(element);
        public void Clear() => _elements.Clear();
        public void InsertAt(int index, JsonElement element) => _elements.Insert(index, element);
        public void RemoveAt(int index) => _elements.RemoveAt(index);



        public IEnumerator<JsonElement> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();



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
                    // this condition seems all off
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

    } // end class
} // end namespace