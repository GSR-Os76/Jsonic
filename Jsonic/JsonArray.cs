using System.Collections;
using System.Text;
using System.Xml.Linq;

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
        public JsonArray Add(JsonArray? element) => Add(new JsonElement(element));
        public JsonArray Add(JsonBoolean? element) => Add(new JsonElement(element));
        public JsonArray Add(JsonNumber? element) => Add(new JsonElement(element));
        public JsonArray Add(JsonObject? element) => Add(new JsonElement(element));
        public JsonArray Add(JsonString? element) => Add(new JsonElement(element));
        public JsonArray Add(JsonElement element) 
        {
            _elements.Add(element);
            return this;
        } // end Add()

        public JsonArray Clear()
        {
            _elements.Clear();
            return this;
        } // end Clear()

        public JsonArray InsertAt(int index, JsonElement element)
        {
            _elements.Insert(index, element);
            return this;
        } // end InsertAt()

        public JsonArray RemoveAt(int index)
        {
            _elements.RemoveAt(index);
            return this;
        } // end RemoveAt()



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