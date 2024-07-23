using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonObject
    {
        #region constructor tests
        [TestMethod]
        [DataRow("{}")]
        [DataRow("    \t {}")]
        [DataRow("{} \r    ")]
        [DataRow("{  }")]
        [DataRow("{\"s\": false}")]
        [DataRow("{\"\": true}")]
        [DataRow("{\"eighte\": null}")]
        [DataRow("{\"9\": -2.7e4}")]
        [DataRow("{\"string\": \"string\"}")]
        [DataRow("{\"hbhjnbhgyhDS3\": {}}")]
        [DataRow("{\"1Ar\": [0]}")]
        [DataRow("{\"_\": {}}")]
        [DataRow("{\"k\": {\"k\": \"v\"}}")]
        [DataRow("{\"k\": false, \"k2\": \"\"}")]
        [DataRow("{\"90-uj\": null, \"\\\\fjej\\\\\": null}")]
        [DataRow("{\"k\": 0, \"_\": -1}")]
        // and with values
        public void TestConstructSuccess(string json)
        {
            JsonObject.ParseJson(json);
        } // end TestConstructSuccess()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("{")]
        [DataRow("}")]
        [DataRow("h{")]
        [DataRow("tr{}")]
        [DataRow("{}it")]
        [DataRow("{false,]}")]
        [DataRow("{nil,}")]
        [DataRow("{nil}")]
        [DataRow("{93,9,f}")]
        [DataRow("{\"jierg\": g}")]
        [DataRow("{\"key\": }")]
        [DataRow("{\"v\": 9,}")]
        [DataRow("{\"v\": 9, \"v\": 0}")]
        public void TestConstructFail(string json)
        {
            JsonObject.ParseJson(json);
        } // end TestConstructFail()
        #endregion

        [TestMethod]
        [DataRow("{}", "{}", true)]
        [DataRow("{}", "{\"op\": false}", false)]
        [DataRow("{\"key\": 3e3}", "{\"key\"   : 0.3e4}", true)]
        [DataRow("{\"9034\": null, \"key\": 3e3}", "{\"key\"   : 0.3e4, \"9034\": null}", true)]
        [DataRow("{\"key\": [0, 1, 0]}", "{\"n\"   : [0, 1, 0]}", false)]
        public void TestEquality(string a, string b, bool expectation)
        {
            Assert.AreEqual(expectation, JsonObject.ParseJson(a).Equals(JsonObject.ParseJson(b)));
        } // end TestEquality()


        #region test ToStringCompressed()
        [TestMethod]
        public void TestToCompressedStringEmpty()
        {
            Assert.AreEqual("{}", new JsonObject().ToCompressedString());
        } // end TestToCompressedStringEmpty()

        [TestMethod]
        public void TestToCompressedString2()
        {
            Assert.AreEqual("{\"r\":false,\"nrew\":null}", new JsonObject().Add("r", false).AddNull("nrew").ToCompressedString());
        } // end TestToCompressedString2()

        [TestMethod]
        public void TestToCompressedStringNested1()
        {
            Assert.AreEqual("{\"Data\":{},\"nrew\":70}", new JsonObject().Add("Data", new JsonObject()).Add("nrew", new JsonNumber(70)).ToCompressedString());
        } // end TestToCompressedStringNested1()

        [TestMethod]
        public void TestToCompressedStringNested2()
        {
            Assert.AreEqual("{\"position\":[-12,0,403]}", new JsonObject().Add("position", new JsonArray().Add(-12).Add(0).Add(403)).ToCompressedString());
        } // end TestToCompressedStringNested2()
        #endregion

        #region test ToString()
        [TestMethod]
        public void TestToStringEmpty()
        {
            Assert.AreEqual("{\r\r}", new JsonObject().ToString());
        } // end TestToStringEmpty()

        [TestMethod]
        public void TestToString2()
        {
            Assert.AreEqual("{\r\t\"r\": false,\r\t\"nrew\": null\r}", new JsonObject().Add("r", false).AddNull("nrew").ToString());
        } // end TestToString2()

        [TestMethod]
        public void TestToStringNested1()
        {
            Assert.AreEqual("{\r\t\"Data\": {\r\t\r\t},\r\t\"nrew\": 70\r}", new JsonObject().Add("Data", new JsonObject()).Add("nrew", new JsonNumber(70)).ToString());
        } // end TestToStringNested1()

        [TestMethod]
        public void TestToStringNested2()
        {
            Assert.AreEqual("{\r\t\"position\": [\r\t\t-12,\r\t\t0,\r\t\t403\r\t]\r}", new JsonObject().Add("position", new JsonArray().Add(-12).Add(0).Add(403)).ToString());
        } // end TestToStringNested2()
        #endregion

    } // end class
} // end namespace