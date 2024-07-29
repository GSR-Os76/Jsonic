using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonBooleanNullArguments
    {
#pragma warning disable CS8625
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson1()
        {
            JsonBoolean.ParseJson(null, out _);
        } // end TestParseJson1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson2()
        {
            JsonBoolean.ParseJson(null);
        } // end TestParseJson2()
#pragma warning restore CS8625
    } // end class
} // end namespace