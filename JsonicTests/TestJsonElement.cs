using GSR.Jsonic;
using System.Data;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonElement
    {
        #region ToString() and ToCompressedString()
        [TestMethod]
        public void TestNullValue()
        {
            Assert.AreEqual("null", new JsonElement(JsonNull.NULL).ToString());
            Assert.AreEqual("null", new JsonElement().ToString());
            Assert.AreEqual("null", new JsonElement(JsonNull.NULL).ToCompressedString());
            Assert.AreEqual("null", new JsonElement().ToCompressedString());
        } // end TestNullValue()

        [TestMethod]
        [DataRow(false, "false")]
        [DataRow(true, "true")]
        public void TestBooleanValue(bool b, string expectation)
        {
            JsonElement j = new(b);
            Assert.AreEqual(expectation, j.ToString());
            Assert.AreEqual(expectation, j.ToCompressedString());
        } // end TestNullValue()

        [TestMethod]
        public void TestArrayToString()
        {
            JsonElement j = new(new JsonArray());
            Assert.AreEqual("[\r\r]", j.ToString());
            Assert.AreEqual("[]", j.ToCompressedString());

            j.AsArray().Add(true).Add(new JsonArray()).Add(90);
            Assert.AreEqual("[\r\ttrue,\r\t[\r\t\r\t],\r\t90\r]", j.ToString());
            Assert.AreEqual("[true,[],90]", j.ToCompressedString());
        } // end TestNullValue()
#warning others
        #endregion

        #region test equality
        [TestMethod]
        [DataRow(true, false, false)]
        [DataRow(false, true, false)]
        [DataRow(true, true, true)]
        [DataRow(false, false, true)]
        public void TestBooleanEquality(bool a, bool b, bool expectation)
        {
            Assert.AreEqual(expectation, new JsonElement(a).Equals(new JsonElement(b)));
        } // end TestBooleanEquality()

        [TestMethod]
        [DataRow("null")]
        [DataRow("false")]
        [DataRow("true")]
        [DataRow("\"\"")]
        [DataRow("\"d\"")]
        [DataRow("0")]
        [DataRow("819864")]
        [DataRow("[]")]
        [DataRow("[20E0]")]
        [DataRow("{}")]
        [DataRow("{\"e\": \"e\"}")]
        public void TestNullEquality(string json)
        {
            Assert.IsFalse(JsonElement.ParseJson(json).Equals(null));
        } // end TestBooleanEquality()

        #endregion


        #region test ParseJsonStart valid
        [TestMethod]
        [DataRow("null", "")]
        [DataRow("nullml3", "ml3")]
        [DataRow("null ", " ")]
        [DataRow(" null,", ",")]
        public void TestParseStartValidNull(string input, string expectedRemained)
        {
            Assert.AreEqual(null, JsonElement.ParseJson(input, out string r).Value);
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidNull()

        [TestMethod]
        [DataRow("false", "")]
        [DataRow("falseI(#MForkv", "I(#MForkv")]
        [DataRow("false ", " ")]
        [DataRow("false,", ",")]
        public void TestParseStartValidFalse(string input, string expectedRemained)
        {
            Assert.AreEqual(JsonBoolean.FALSE, JsonElement.ParseJson(input, out string r).Value);
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidFalse()

        [TestMethod]
        [DataRow("true", "")]
        [DataRow("true;l3Opd-=_", ";l3Opd-=_")]
        [DataRow("true ", " ")]
        [DataRow("true,87", ",87")]
        public void TestParseStartValidTrue(string input, string expectedRemained)
        {
            Assert.AreEqual(JsonBoolean.TRUE, JsonElement.ParseJson(input, out string r).Value);
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidTrue()

        [TestMethod]
        [DataRow("\"\"", "", "")]
        [DataRow("\"\\\"390432\"", "\"390432", "")]
        [DataRow("   \"\\\"390432\"\"", "\"390432", "\"")]
        [DataRow("\"   \"", "   ", "")]
        [DataRow("\"\"e;l3Opd-=_", "", "e;l3Opd-=_")]
        [DataRow("\"\"\"falsetrue ", "", "\"falsetrue ")]
        [DataRow("\r\t\"\"true,87", "", "true,87")]
        [DataRow("\"\",", "", ",")]
        public void TestParseStartValidString(string input, string expectedString, string expectedRemained)
        {
            Assert.AreEqual(new JsonString(expectedString), JsonElement.ParseJson(input, out string r).Value);
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidString()

        [TestMethod]
        [DataRow("33lk", "lk")]
        [DataRow("0.9e1O0k", "O0k")]
        [DataRow("150.3", "")]
        [DataRow("0,", ",")]
        public void TestParseStartValidNumber(string input, string expectedRemained)
        {
            Assert.AreEqual(typeof(JsonNumber), JsonElement.ParseJson(input, out string r).Value?.GetType());
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
            Assert.AreEqual(typeof(JsonArray), JsonElement.ParseJson(input, out string r).Value?.GetType());
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidArray()

        [TestMethod]
        [DataRow("{\"b\": 90}", "")]
        [DataRow("{}", "")]
        [DataRow("{\"_\": \"\", \"RunTime\": 9e1}", "")]
        [DataRow("{\"b\":  90} kl", " kl")]
        [DataRow("{}, ,", ", ,")]
        [DataRow("{\"ooo\": null   , \"RunTime \": 9e1}", "")]
        public void TestParseStartValidObject(string input, string expectedRemained)
        {
            Assert.AreEqual(typeof(JsonObject), JsonElement.ParseJson(input, out string r).Value?.GetType());
            Assert.AreEqual(expectedRemained, r);
        } // end TestParseStartValidObject()
        #endregion

        #region test ParseJsonStart invalid
        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("nul")]
        [DataRow("nulml3")]
        [DataRow("nll ")]
        public void TestParseStartInvalidNull(string input)
        {
            JsonElement.ParseJson(input, out _);
        } // end TestParseStartInvalidNull()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("fase")]
        [DataRow("falsyI(#MForkv")]
        [DataRow("fals e ")]
        [DataRow("fals.e,")]
        public void TestParseStartInvalidFalse(string input)
        {
            JsonElement.ParseJson(input, out _);
        } // end TestParseStartInvalidFalse()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("tue")]
        [DataRow(" trud-=_")]
        [DataRow("tue ")]
        [DataRow("tre,")]
        public void TestParseStartInvalidTrue(string input)
        {
            JsonElement.ParseJson(input, out _);
        } // end TestParseStartInvalidTrue()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("\"")]
        [DataRow("\"\\390432")]
        [DataRow("\"   ")]
        public void TestParseStartInvalidString(string input)
        {
            JsonElement.ParseJson(input, out _);
        } // end TestParseStartInvalidString()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("[")]
        [DataRow("[,{},[],90,\"\",false,null,true")]
        [DataRow("[-")]
        [DataRow(" [--[;[]c")]
        public void TestParseStartInvalidArray(string input)
        {
            JsonElement.ParseJson(input, out _);
        } // end TestParseStartInvalidArray()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("{")]
        [DataRow(" } ")]
        [DataRow("{,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,}")]
        [DataRow("{\"\\ob\": 90}")]
        [DataRow("  {\"keyThat'sDuplicated\": null, \"keyThat'sDuplicated\": null}")]
        [DataRow("{\"90_09\": ")]

        public void TestParseStartInvalidObject(string input)
        {
            JsonElement.ParseJson(input, out _);
        } // end TestParseStartInvalidObject()
        #endregion

        #region Parse

        [TestMethod]
        [DataRow(" false")]
        [DataRow("\r null")]
        public void TestParseStartCanBeWhiteSpace(string json)
        {
            JsonElement.ParseJson(json);
            Assert.IsTrue(true);
        } // end TestParseStartCanBeWhiteSpace()
        #endregion
    } // end class
} // end namespace