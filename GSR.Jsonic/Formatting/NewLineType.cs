namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Flag style enum representation of valid types of newlines.
    /// </summary>
    public enum NewLineType
    {
        /// <summary>
        /// No newline.
        /// </summary>
        NONE = 0b0000,
        /// <summary>
        /// Newlnes as only a carriage return.
        /// </summary>
        CR = 0b0010,
        /// <summary>
        /// Newlines as only a linefeed.
        /// </summary>
        LF = 0b0001,
        /// <summary>
        /// Newlines as a carriage return and then a linefeed.
        /// </summary>
        CRLF = 0b0011
    } // end enum
} // end namespace