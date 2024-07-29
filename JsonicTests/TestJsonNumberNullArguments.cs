using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonNumberNullArguments
    {
#pragma warning disable CS8625
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructNull()
        {
            new JsonNumber(null);
        } // end TestConstructNull()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson1()
        {
            JsonNumber.ParseJson(null, out _);
        } // end TestParseJson1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson2()
        {
            JsonNumber.ParseJson(null);
        } // end TestParseJson2()
#pragma warning restore CS8625

    } // end class
} // end namespace