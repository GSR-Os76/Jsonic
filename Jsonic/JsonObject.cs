namespace GSR.Jsonic
{
    public sealed class JsonObject : IJsonComponent
    {
        private Dictionary<JsonString, JsonElement> _kvps;

        public JsonOptions Options { get; set; } = JsonOptions.Default;


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

        add 

            remove

            exists is used
        */



    } // end class
} // end namespace