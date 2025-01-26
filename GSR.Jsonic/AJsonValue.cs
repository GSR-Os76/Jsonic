using GSR.Jsonic.Formatting;

namespace GSR.Jsonic
{
    /// <summary>
    /// Simple <see cref="IJsonValue"/> abstract base class that implements <see cref="ToString()"/>.
    /// </summary>
    public abstract class AJsonValue : IJsonValue
    {
        /// <summary>
        /// Write the json value with defualt <see cref="JsonFormatting"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToString(new());

        /// <inheritdoc/>
        public abstract string ToString(JsonFormatting formatting);
    } // end class
} // end namespace