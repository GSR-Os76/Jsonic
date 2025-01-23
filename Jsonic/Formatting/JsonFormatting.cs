namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Definition of various options to determine the style of output json. Such as indentation, or what characters should be escapes, or if numbers should be exponentially notation.
    /// </summary>
    public struct JsonFormatting
    {
        /// <summary>
        /// How to write newlines inside of collections.
        /// </summary>
        public NewLineMode NewLineMode { get; }
        /// <summary>
        /// White space to use as indentation after a newline inside a collection, will be repeated to match the depth.
        /// 
        /// Note: Only used when newline mode isn't <see cref="NewLineMode.NONE"/>
        /// </summary>
        public string Identation { get; } = "";



        /// <summary>
        /// How <see cref="JsonString"/>s should be formatted.
        /// </summary>
        public JsonStringFormatting StringFormatting { get; } = new();
    } // end struct
} // end namespace