using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonElementNullArguments
    {
#pragma warning disable CS8625
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson1()
        {
            JsonElement.ParseJson(null, out _);
        } // end TestParseJson1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson2()
        {
            JsonElement.ParseJson(null);
        } // end TestParseJson2()
#pragma warning restore CS8625
    } // end class
} // end namespace