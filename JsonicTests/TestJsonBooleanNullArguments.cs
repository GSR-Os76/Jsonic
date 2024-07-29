using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonBooleanNullArguments
    {
#pragma warning disable CS8625
#pragma warning disable CS8600
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
#pragma warning restore CS8600
    } // end class
} // end namespace