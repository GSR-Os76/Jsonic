namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonString"/>.
    /// </summary>
    public sealed class JsonStringFormatting
    {
        /// <summary>
        /// Write to string exactly as how it was parsed. Overrides all the other settings. Ignored if string wasn't parsed.
        /// </summary>
        public bool Preserve { get; }

        /// <summary>
        /// Should forward slashes be escaped: '\/'.
        /// </summary>
        public bool EscapeSolidi { get; }

#warning could escape unicode code points inside a specified range. 
#warning captial/lowercase hex



        /// <inheritdoc/>
        public JsonStringFormatting(
            bool preserve = true,
            bool escapeSolidus = false)
        {
            Preserve = preserve;
            EscapeSolidi = escapeSolidus;
        } // end ctor
    } // end class
} // end namespace