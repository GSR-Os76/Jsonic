namespace GSR.Jsonic
{
    /// <summary>
    /// Common contract of all GSR.Jsonic json value representations
    /// </summary>
    public interface IJsonValue
    {
#warning replace with .ToString(int indentation);
        /// <summary>
        /// Write the object to json format without any formatting white space, but without changing the value it represents..
        /// </summary>
        /// <returns></returns>
        string ToCompressedString();
    } // end interface
} // end namespace