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
            new JsonObject(json);
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
            new JsonObject(json);
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
            Assert.AreEqual(expectation, new JsonObject(a).Equals(new JsonObject(b)));
        } // end TestEquality()

#warning test ToString writting, compressed and not.

    } // end class
} // end namespace