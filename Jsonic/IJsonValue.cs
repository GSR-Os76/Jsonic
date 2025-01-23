using GSR.Jsonic.Formatting;

namespace GSR.Jsonic
{
    /// <summary>
    /// Common contract of all GSR.Jsonic json value representations
    /// </summary>
    public interface IJsonValue
    {
        /// <summary>
        /// Write the object to json format with the following <paramref name="formatting"/>.
        /// </summary>
        /// <returns></returns>
        string ToString(JsonFormatting formatting);
    } // end interface
} // end namespace