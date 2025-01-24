namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonString"/>.
    /// </summary>
    public struct JsonStringFormatting
    {
#warning "or control characters", unescaped control characters are not optional
#warning preserve
        /// <summary>
        /// Should forward slashes be escaped: '\/'.
        /// </summary>
        public bool EscapeSolidi { get; } = false;
        /// <summary>
        /// Should backspace be escaped: '\b'.
        /// </summary>
        public bool EscapeBackspaces { get; } = true;
        /// <summary>
        /// Should formfeeds be escaped: '\f'.
        /// </summary>
        public bool EscapeFormfeeds { get; } = true;
        /// <summary>
        /// Should linefeeds be escaped: '\n'.
        /// </summary>
        public bool EscapeLinefeeds { get; } = true;
        /// <summary>
        /// Should carriage returns being escaped: '\r'.
        /// </summary>
        public bool EscapeCarriageReturns { get; } = true;
        /// <summary>
        /// Should horizontal tabs be escaped: '\t'.
        /// </summary>
        public bool EscapeHorizontalTabs { get; } = true;

#warning could escape unicode code points inside a specified range.



        /// <inheritdoc/>
        public JsonStringFormatting(
            bool escapeSolidus = false,
            bool escapedBackspaces = true,
            bool escapeFormfeeds = true, 
            bool escapeLinefeeds = true, 
            bool escapedCarriageReturns = true, 
            bool escapeHorizontalTabs = true) 
        {
            EscapeSolidi = escapeSolidus;
            EscapeBackspaces = escapedBackspaces;
            EscapeFormfeeds = escapeFormfeeds;
            EscapeLinefeeds = escapeLinefeeds;
            EscapeCarriageReturns = escapedCarriageReturns;
            EscapeHorizontalTabs = escapeHorizontalTabs;
        } // end ctor
    } // end struct
} // end namespace