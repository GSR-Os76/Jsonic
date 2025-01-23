namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonArray"/>.
    /// </summary>
    public struct JsonArrayFormatting
    {
        /// <summary>
        /// Collectionwise formatting of the array.
        /// </summary>
        public JsonCollectionFormatting Formatting { get; }



        /// <inheritdoc/>
        public JsonArrayFormatting(JsonCollectionFormatting formatting)
        {
            Formatting = formatting;
        } // end ctor

    } // end struct
} // end namespace