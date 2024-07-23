namespace GSR.Jsonic
{
    public interface IJsonComponent
    {
        /// <summary>
        /// Write the object to json format without any formatting white space, but without changing the value it represents..
        /// </summary>
        /// <returns></returns>
        string ToCompressedString();
    } // end interface
} // end namespace