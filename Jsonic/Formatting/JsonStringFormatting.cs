namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonString"/>.
    /// </summary>
    public struct JsonStringFormatting
    {
        // public bool Preserve { get; }
#warning preserve
        /// <summary>
        /// Should forward slashes be escaped: '\/'.
        /// </summary>
        public bool EscapeSolidi { get; } = false;

#warning could escape unicode code points inside a specified range.



        /// <inheritdoc/>
        public JsonStringFormatting(
            bool escapeSolidus = false) 
        {
            EscapeSolidi = escapeSolidus;
        } // end ctor
    } // end struct
} // end namespace