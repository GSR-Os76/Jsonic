namespace GSR.Jsonic
{
    public sealed class JsonElement
    {
        public object? Element { get; }
        public JsonType Type { get; }



        public JsonElement(object? element)
        {
            Element = element;
            Type = TypeOrThrow(element);
        } // end constructor



        private static JsonType TypeOrThrow(object? element)
        {
            if (element == null)
                return JsonType.Null;
            else if (element.GetType() == typeof(JsonArray))
                return JsonType.Array;
            else if (element.GetType() == typeof(bool))
                return JsonType.Boolean;
            else if (element.GetType() == typeof(JsonNumber))
                return JsonType.Number;
            else if (element.GetType() == typeof(JsonObject))
                return JsonType.Object;
            else if (element.GetType() == typeof(JsonString))
                return JsonType.String;

            throw new MalformedJsonException($"\"{element}\" was not recognized as a valid Json type.");
        } // end TypeOrThrow()

    } // end record class
} // end namespace