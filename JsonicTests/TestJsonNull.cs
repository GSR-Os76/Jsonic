using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    public static class TestJsonNull
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow("null")]
            public void ParseJson(string json)
                => Assert.AreEqual(JsonNull.NULL, JsonNull.ParseJson(json));
        } // end inner class Valid()

        [TestClass]
        public class Invalid
        {
#pragma warning disable CS8625



            [TestMethod]
            [ExpectedException(typeof(MalformedJsonException))]
            [DataRow("nul")]
            [DataRow("Null")]
            public void ParseJsonNull(string json)
                => JsonNull.ParseJson(json);

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJson()
                => JsonNull.ParseJson(null);

            [TestMethod]
            [ExpectedException(typeof(MalformedJsonException))]
            [DataRow("nul")]
            [DataRow("Null")]
            public void ParseJsonRemainderized(string json)
                => JsonNull.ParseJson(json, out _);

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJsonRemainderizedNull()
                => JsonNull.ParseJson(null, out _);
#pragma warning restore CS8625
        } // end inner class Invalid
    } // end class
} // end namespace