namespace GSR.Jsonic
{
    public abstract class JsonComponent : IJsonComponent
    {
        public JsonOptions Options { get; set; } = JsonOptions.Default;
    } // end class
} // end namespace