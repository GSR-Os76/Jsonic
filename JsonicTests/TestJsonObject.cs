using GSR.Jsonic;
using GSR.Jsonic.Formatting;

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

        [TestMethod]
        public void TestKVPConstructSuccess()
        {
            JsonObject a = new(new List<KeyValuePair<JsonString, JsonElement>>() {
                KeyValuePair.Create(new JsonString("A"), new JsonElement()),
                KeyValuePair.Create(new JsonString("BetaCapionssr3gwty"), new JsonElement(false)),
                KeyValuePair.Create(new JsonString(""), new JsonElement(new JsonArray())),
                KeyValuePair.Create(new JsonString("_"), new JsonElement())
            });
            Assert.AreEqual(JsonNull.NULL, a["A"].Value);
            Assert.AreEqual(new JsonArray(), a[""].Value);
            Assert.AreEqual(JsonNull.NULL, a["A"].Value);
            Assert.AreEqual(JsonNull.NULL, a["_"].Value);
            Assert.AreEqual(JsonBoolean.FALSE, a["BetaCapionssr3gwty"].Value);
        } // end TestKVPConstructSuccess()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestKVPConstructDuplicateFail()
        {
            new JsonObject(new List<KeyValuePair<JsonString, JsonElement>>() {
                KeyValuePair.Create(new JsonString("A"), new JsonElement()),
                KeyValuePair.Create(new JsonString("BetaCapionssr3gwty"), new JsonElement()),
                KeyValuePair.Create(new JsonString("A"), new JsonElement(new JsonArray()))
            });
        } // end TestKVPConstructDuplicateFail()
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

        [TestMethod]
        [DataRow("{}")]
        [DataRow("{\"\": false}")]
        public void TestNullEquality(string json)
        {
            Assert.IsFalse(JsonObject.ParseJson(json).Equals(null));
        } // end TestNullEquality()



        #region test ToString()
        [TestMethod]
        [DataRow("{\r\r}", NewLineType.CR, 2, false, false, false, "", "", "", "")]
        [DataRow("{\r\r}", NewLineType.CR, 2, true, true, true, "", "", "", "")]
        [DataRow("{\n\n\n\n}", NewLineType.LF, 4, false, false, false, " \t ", "", " ", " ")]
        public void ToStringEmpty(string expectation, NewLineType newLineType, int emptySpacing,
            bool newLineProceedingFirstElement,
            bool newLineBetweenElements,
            bool newLineSucceedingLastElement,
            string indentation,
            string postCommaSeparation,
            string postkeySpacing,
            string preValueSpacing)
        {
            JsonObject data = new();
            Assert.AreEqual(expectation, data.ToString(new(
                newLineType: newLineType,
                objectFormatting: new(
                    emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation,
                    postkeySpacing,
                    preValueSpacing))));
        } // end ToStringEmpty()

        [TestMethod]
        [DataRow("{\"r\" : false,\"nrew\" : null}", NewLineType.CRLF, 2, false, false, false, "", "", " ", " ")]
        [DataRow("{\r\n\"r\" : false,\"nrew\" : null}", NewLineType.CRLF, 2, true, false, false, "", "", " ", " ")]
        [DataRow("{\"r\" : false, \"nrew\" : null}", NewLineType.CRLF, 2, false, false, false, "", " ", " ", " ")]
        [DataRow("{\r\n\"r\" : false, \"nrew\" : null}", NewLineType.CRLF, 2, true, false, false, "", " ", " ", " ")]
        [DataRow("{\r\n\"r\" : false,\r\n\"nrew\" : null\r\n}", NewLineType.CRLF, 2, true, true, true, "", " ", " ", " ")]
        [DataRow("{\r\n   \"r\" : false,\r\n   \"nrew\" : null\r\n}", NewLineType.CRLF, 2, true, true, true, "   ", " ", " ", " ")]
        public void ToString1(string expectation, NewLineType newLineType, int emptySpacing,
            bool newLineProceedingFirstElement,
            bool newLineBetweenElements,
            bool newLineSucceedingLastElement,
            string indentation,
            string postCommaSeparation,
            string postkeySpacing,
            string preValueSpacing)
        {
            JsonObject data = new JsonObject().Add("r", false).Add("nrew");
            Assert.AreEqual(expectation, data.ToString(new(
                newLineType: newLineType,
                objectFormatting: new(
                    emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation,
                    postkeySpacing,
                    preValueSpacing))));
        } // end ToString1()


        [TestMethod]
        [DataRow("{\r\t\"Data\": {\r\t\r\t},\r\t\"nrew\": 70\r}", NewLineType.CR, 2, true, true, true, "\t", "", "", " ")]
        [DataRow("{\r\t\"Data\":{\r\t\r\t},\r\t\"nrew\":70\r}", NewLineType.CR, 2, true, true, true, "\t", "", "", "")]
        public void ToStringNested1(string expectation, NewLineType newLineType, int emptySpacing,
            bool newLineProceedingFirstElement,
            bool newLineBetweenElements,
            bool newLineSucceedingLastElement,
            string indentation,
            string postCommaSeparation,
            string postkeySpacing,
            string preValueSpacing)
        {
            JsonObject data = new JsonObject().Add("Data", new JsonObject()).Add("nrew", 70);
            Assert.AreEqual(expectation, data.ToString(new(
                newLineType: newLineType,
                objectFormatting: new(
                    emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation,
                    postkeySpacing,
                    preValueSpacing))));
        } // end ToStringNested1()

        [TestMethod]
        [DataRow("{\r\t\"position\":[\r\t\t-12,\r\t\t0,\r\t\t403\r\t]\r}", NewLineType.CR, 2, true, true, true, "\t", "", "", "")]
        [DataRow("{\"position\":[-12,0,403]}", NewLineType.NONE, 2, true, true, true, "", "", "", "")]
        public void ToStringNested2(string expectation, NewLineType newLineType, int emptySpacing,
            bool newLineProceedingFirstElement,
            bool newLineBetweenElements,
            bool newLineSucceedingLastElement,
            string indentation,
            string postCommaSeparation,
            string postkeySpacing,
            string preValueSpacing)
        {
            JsonObject data = new JsonObject().Add("position", new JsonArray().Add(-12).Add(0).Add(403));
            Assert.AreEqual(expectation, data.ToString(new(
                newLineType: newLineType,
                arrayFormatting: new(emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation),
                objectFormatting: new(
                   emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation,
                    postkeySpacing,
                    preValueSpacing))));
            //Assert.AreEqual(, new JsonObject().Add("position", new JsonArray().Add(-12).Add(0).Add(403)).ToString());
        } // end ToStringNested2()
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddDuplicateFail()
        {
            new JsonObject()
                .Add(new JsonString("A"), new JsonElement())
                .Add(new JsonString("BetaCapionssr3gwty"), new JsonElement())
                .Add("A", new JsonElement(new JsonArray()));
        } // end TestAddDuplicateFail()

    } // end class
} // end namespace