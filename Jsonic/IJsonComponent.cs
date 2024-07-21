namespace GSR.Jsonic
{
    public interface IJsonComponent
    {
        /// <summary>
        /// Write the object to json format without any formatting white space.
        /// </summary>
        /// <returns></returns>
        string ToCompressedString();
    } // end interface
} // end namespace