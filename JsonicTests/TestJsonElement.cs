using GSR.Jsonic;
using GSR.Jsonic.Formatting;
using System.Data;

namespace GSR.Tests.Jsonic
{
    public static class TestJsonElement
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void ToStringNull()
            {
                Assert.AreEqual("null", new JsonElement(JsonNull.NULL).ToString());
                Assert.AreEqual("null", new JsonElement().ToString());
                Assert.AreEqual("null", new JsonElement(JsonNull.NULL).ToString(JsonFormatting.COMPRESSED));
                Assert.AreEqual("null", new JsonElement().ToString(JsonFormatting.COMPRESSED));
            } // end TestNullValue()

            [TestMethod]
            [DataRow(false, "false")]
            [DataRow(true, "true")]
            public void ToStringBoolean(bool b, string expectation)
            {
                JsonElement j = new(b);
                Assert.AreEqual(expectation, j.ToString());
                Assert.AreEqual(expectation, j.ToString(JsonFormatting.COMPRESSED));
            } // end TestNullValue()

            [TestMethod]
            public void ToStringArray()
            {
                JsonElement j = new(new JsonArray());
                Assert.AreEqual("[]", j.ToString());
                Assert.AreEqual("[]", j.ToString(JsonFormatting.COMPRESSED));

                j.AsArray().Add(true).Add(new JsonArray()).Add(90);
                Assert.AreEqual("[\r\n    true,\r\n    [],\r\n    90\r\n]", j.ToString());
                Assert.AreEqual("[true,[],90]", j.ToString(JsonFormatting.COMPRESSED));
            } // end ToStringArray()

            [TestMethod]
            [DataRow("true", true, true)]
            [DataRow("false", false, true)]
            [DataRow("false", true, false)]
            [DataRow("\"\"", "", true)]
            [DataRow("\"\\r\"", "\r", true)]
            [DataRow("904", 904L, true)]
            [DataRow("0.33e-1", .033f, true)]
            [DataRow("0.33e-1", .033d, true)]
            public void Equality(string json, object b, bool expectation)
                => Assert.AreEqual(expectation, JsonElement.ParseJson(json).Equals(b));

            [TestMethod]
            [DataRow(true, false, false)]
            [DataRow(false, true, false)]
            [DataRow(true, true, true)]
            [DataRow(false, false, true)]
            public void BooleanEquality(bool a, bool b, bool expectation)
                => Assert.AreEqual(expectation, new JsonElement(a).Equals(new JsonElement(b)));

            [TestMethod]
            [DataRow("null", true)]
            [DataRow("false", false)]
            [DataRow("true", false)]
            [DataRow("\"\"", false)]
            [DataRow("\"d\"", false)]
            [DataRow("0", false)]
            [DataRow("819864", false)]
            [DataRow("[]", false)]
            [DataRow("[20E0]", false)]
            [DataRow("{}", false)]
            [DataRow("{\"e\": \"e\"}", false)]
            public void NullEquality(string json, bool expectation)
                => Assert.AreEqual(expectation, JsonElement.ParseJson(json).Equals(null));

            [TestMethod]
            [DataRow("null", "")]
            [DataRow("nullml3", "ml3")]
            [DataRow("null ", " ")]
            [DataRow(" null,", ",")]
            public void ParseJsonNull(string input, string expectedRemained)
            {
                Assert.AreEqual(null, JsonElement.ParseJson(input, out string r).Value);
                Assert.AreEqual(expectedRemained, r);
            } // end ParseJsonNull()

            [TestMethod]
            [DataRow("false", "")]
            [DataRow("falseI(#MForkv", "I(#MForkv")]
            [DataRow(" false ", " ")]
            [DataRow("false,", ",")]
            public void ParseJsonFalse(string input, string expectedRemained)
            {
                Assert.AreEqual(JsonBoolean.FALSE, JsonElement.ParseJson(input, out string r).Value);
                Assert.AreEqual(expectedRemained, r);
            } // end ParseJsonFalse()

            [TestMethod]
            [DataRow("true", "")]
            [DataRow("true;l3Opd-=_", ";l3Opd-=_")]
            [DataRow("true ", " ")]
            [DataRow("true,87", ",87")]
            public void ParseJsonTrue(string input, string expectedRemained)
            {
                Assert.AreEqual(JsonBoolean.TRUE, JsonElement.ParseJson(input, out string r).Value);
                Assert.AreEqual(expectedRemained, r);
            } // end ParseJsonTrue()

            [TestMethod]
            [DataRow("            \"\"", "", "")]
            [DataRow("\"\\\"390432\"", "\"390432", "")]
            [DataRow("   \"\\\"390432\"\"", "\"390432", "\"")]
            [DataRow("\"   \"", "   ", "")]
            [DataRow("\"\"e;l3Opd-=_", "", "e;l3Opd-=_")]
            [DataRow("\"\"\"falsetrue ", "", "\"falsetrue ")]
            [DataRow("\r\t\"\"true,87", "", "true,87")]
            [DataRow("\"\",", "", ",")]
            public void ParseJsonString(string input, string expectedString, string expectedRemained)
            {
                Assert.AreEqual(new JsonString(expectedString), JsonElement.ParseJson(input, out string r).Value);
                Assert.AreEqual(expectedRemained, r);
            } // end ParseJsonString()

            [TestMethod]
            [DataRow("33lk", "lk")]
            [DataRow("0.9e1O0k", "O0k")]
            [DataRow("150.3", "")]
            [DataRow("0,", ",")]
            public void ParseJsonNumber(string input, string expectedRemained)
            {
                Assert.AreEqual(typeof(JsonNumber), JsonElement.ParseJson(input, out string r).Value?.GetType());
                Assert.AreEqual(expectedRemained, r);
            } // end ParseJsonNumber()

            [TestMethod]
            [DataRow("[]", "")]
            [DataRow("[[],{},[],90,\"\",false,null,true]", "")]
            [DataRow("\t\t[[[0,1,1]],  [],90,\"\" ,false ,null,true]", "")]
            [DataRow("[\r\t[\r\t]\r]", "")]
            [DataRow("[]   ", "   ")]
            [DataRow("[[],{},[],90,\"\",false,null,true] f", " f")]
            [DataRow("[[[0,1,1]],  [],90,\"\" ,false ,null,true]arbeitnung", "arbeitnung")]
            [DataRow("[\r\t[\r\t]\r], false", ", false")]
            [DataRow("[]f", "f")]
            public void ParseJsonArray(string input, string expectedRemained)
            {
                Assert.AreEqual(typeof(JsonArray), JsonElement.ParseJson(input, out string r).Value?.GetType());
                Assert.AreEqual(expectedRemained, r);
            } // end ParseJsonArray()

            [TestMethod]
            [DataRow("{\"b\": 90}", "")]
            [DataRow("\r{}", "")]
            [DataRow("{\"_\": \"\", \"RunTime\": 9e1}", "")]
            [DataRow("{\"b\":  90} kl", " kl")]
            [DataRow("{}, ,", ", ,")]
            [DataRow("{\"ooo\": null   , \"RunTime \": 9e1}", "")]
            public void ParseJsonObject(string input, string expectedRemained)
            {
                Assert.AreEqual(typeof(JsonObject), JsonElement.ParseJson(input, out string r).Value?.GetType());
                Assert.AreEqual(expectedRemained, r);
            } // end ParseJsonObject()
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(MalformedJsonException))]
            [DataRow("nul")]
            [DataRow("nulml3")]
            [DataRow("nll ")]
            [DataRow("fase")]
            [DataRow("falsyI(#MForkv")]
            [DataRow("fals e ")]
            [DataRow("fals.e,")]
            [DataRow("tue")]
            [DataRow(" trud-=_")]
            [DataRow("tue ")]
            [DataRow("tre,")]
            [DataRow("\"")]
            [DataRow("\"\\390432")]
            [DataRow("\"   ")]
            [DataRow("[")]
            [DataRow("[,{},[],90,\"\",false,null,true")]
            [DataRow("[-")]
            [DataRow(" [--[;[]c")]
            [DataRow("{")]
            [DataRow(" } ")]
            [DataRow("{,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,}")]
            [DataRow("{\"\\ob\": 90}")]
            [DataRow("  {\"keyThat'sDuplicated\": null, \"keyThat'sDuplicated\": null}")]
            [DataRow("{\"90_09\": ")]
            public void ParseJson(string input)
                => JsonElement.ParseJson(input, out _);

#pragma warning disable CS8625
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJsonRemainderized()
                => JsonElement.ParseJson(null, out _);

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJson2()
                => JsonElement.ParseJson(null);
#pragma warning restore CS8625
        } // end inner class Invalid
    } // end class
} // end namespace