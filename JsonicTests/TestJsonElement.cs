using GSR.Jsonic;
using System.Data;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonElement
    {
        [TestMethod]
        public void TestNullValue() 
        {
            Assert.AreEqual(JsonUtil.JSON_NULL, new JsonElement((JsonObject?)null).ToString());
            Assert.AreEqual(JsonUtil.JSON_NULL, new JsonElement().ToString());
            Assert.AreEqual(JsonUtil.JSON_NULL, new JsonElement((JsonObject?)null).ToCompressedString());
            Assert.AreEqual(JsonUtil.JSON_NULL, new JsonElement().ToCompressedString());
        } // end TestNullValue()

        [TestMethod]
        [DataRow(false, JsonUtil.JSON_FALSE)]
        [DataRow(true, JsonUtil.JSON_TRUE)]
        public void TestBooleanValue(bool b, string expectation)
        {
            JsonElement j = new(b);
            Assert.AreEqual(expectation, j.ToString());
            Assert.AreEqual(expectation, j.ToCompressedString());
        } // end TestNullValue()
#warning others



        #region test ParseJsonStart valid
        [TestMethod]
        [DataRow("null", "")]
        [DataRow("nullml3", "ml3")]
        [DataRow("null ", " ")]
        [DataRow("null,", ",")]
        public void TestParseStartValidNull(string input, string expectedRemained) 
        {
            Assert.AreEqual(null, JsonElement.ParseJsonStart(input, out string r).Value);
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidNull()

        [TestMethod]
        [DataRow("false", "")]
        [DataRow("falseI(#MForkv", "I(#MForkv")]
        [DataRow("false ", " ")]
        [DataRow("false,", ",")]
        public void TestParseStartValidFalse(string input, string expectedRemained)
        {
            Assert.AreEqual(new JsonBoolean(false), JsonElement.ParseJsonStart(input, out string r).Value);
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidFalse()

        [TestMethod]
        [DataRow("true", "")]
        [DataRow("true;l3Opd-=_", ";l3Opd-=_")]
        [DataRow("true ", " ")]
        [DataRow("true,87", ",87")]
        public void TestParseStartValidTrue(string input, string expectedRemained)
        {
            Assert.AreEqual(new JsonBoolean(true), JsonElement.ParseJsonStart(input, out string r).Value);
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidTrue()

        [TestMethod]
        [DataRow("\"\"", "", "")]
        [DataRow("\"\\\"390432\"", "\\\"390432", "")]
        [DataRow("\"\\\"390432\"\"", "\\\"390432", "\"")]
        [DataRow("\"   \"", "   ", "")]
        [DataRow("\"\"e;l3Opd-=_", "", "e;l3Opd-=_")]
        [DataRow("\"\"\"falsetrue ", "", "\"falsetrue ")]
        [DataRow("\"\"true,87", "", "true,87")]
        [DataRow("\"\",", "", ",")]
        public void TestParseStartValidString(string input, string expectedString, string expectedRemained)
        {
            Assert.AreEqual(new JsonString(expectedString), JsonElement.ParseJsonStart(input, out string r).Value);
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidString()

        [TestMethod]
        [DataRow("33lk", "lk")]
        [DataRow("0.9e1O0k", "O0k")]
        [DataRow("150.3", "")]
        [DataRow("0,", ",")]
        public void TestParseStartValidNumber(string input, string expectedRemained)
        {
            Assert.AreEqual(typeof(JsonNumber), JsonElement.ParseJsonStart(input, out string r).Value?.GetType());
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidNumber()

        [TestMethod]
        [DataRow("[]", "")]
        [DataRow("[[],{},[],90,\"\",false,null,true]", "")]
        [DataRow("[[[0,1,1]],  [],90,\"\" ,false ,null,true]", "")]
        [DataRow("[\r\t[\r\t]\r]", "")]
        [DataRow("[]   ", "   ")]
        [DataRow("[[],{},[],90,\"\",false,null,true] f", " f")]
        [DataRow("[[[0,1,1]],  [],90,\"\" ,false ,null,true]arbeitnung", "arbeitnung")]
        [DataRow("[\r\t[\r\t]\r], false", ", false")]
        [DataRow("[]f", "f")]
        public void TestParseStartValidArray(string input, string expectedRemained)
        {
            Assert.AreEqual(typeof(JsonArray), JsonElement.ParseJsonStart(input, out string r).Value?.GetType());
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidArray()
        #endregion



        #region test ParseJsonStart invalid
        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("nul")]
        [DataRow("nulml3")]
        [DataRow("nll ")]
        public void TestParseStartInvalidNull(string input)
        {
            JsonElement.ParseJsonStart(input, out _);
        } // end TestParseStartInvalidNull()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("fase")]
        [DataRow("falsyI(#MForkv")]
        [DataRow("fals e ")]
        [DataRow("fals.e,")]
        public void TestParseStartInvalidFalse(string input)
        {
            JsonElement.ParseJsonStart(input, out _);
        } // end TestParseStartInvalidFalse()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("tue")]
        [DataRow("trud-=_")]
        [DataRow("tue ")]
        [DataRow("tre,")]
        public void TestParseStartInvalidTrue(string input)
        {
            JsonElement.ParseJsonStart(input, out _);
        } // end TestParseStartInvalidTrue()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("\"")]
        [DataRow("\"\\390432")]
        [DataRow("\"   ")]
        public void TestParseStartInvalidString(string input)
        {
            JsonElement.ParseJsonStart(input, out _);
        } // end TestParseStartInvalidString()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("[")]
        [DataRow("[,{},[],90,\"\",false,null,true")]
        [DataRow("[-")]
        [DataRow(" [--[;[]c")]
        public void TestParseStartInvalidArray(string input)
        {
            JsonElement.ParseJsonStart(input, out _);
        } // end TestParseStartInvalidArray()



        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        [DataRow("{")]
        [DataRow("{\"b\": 90}")]
        [DataRow("{}")]
        public void TestParseStartObject(string input)
        {
            JsonElement.ParseJsonStart(input, out string _);
        } // end TestParseStartObject()
        #endregion
        // invalids including unimplement expection expectation, bad enclosures or too short, etc

    } // end class
} // end namespace