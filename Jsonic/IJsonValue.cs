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

        /// <summary>
        /// Write the json value with defualt <see cref="JsonFormatting"/>.
        /// </summary>
        /// <returns></returns>
        string ToString() => ToString(new());
    } // end interface
} // end namespace