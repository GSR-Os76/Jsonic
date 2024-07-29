using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonStringNullArguments
    {
#pragma warning disable CS8625
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructNull()
        {
            new JsonString(null);
        } // end TestConstructNull()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFromUnescapedString()
        {
            JsonString.FromUnescapedString(null);
        } // end TestFromUnescapedString()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson1()
        {
            JsonString.ParseJson(null, out _);
        } // end TestParseJson1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson2()
        {
            JsonString.ParseJson(null);
        } // end TestParseJson2()
#pragma warning restore CS8625

    } // end class
} // end namespace