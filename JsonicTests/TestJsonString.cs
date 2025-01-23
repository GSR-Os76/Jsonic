using GSR.Jsonic;
using GSR.Jsonic.Formatting;
using System.Data;

namespace GSR.Tests.Jsonic
{
    public static class TestJsonString
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow("\"\"", "")]
            [DataRow("\"\\\"\"", "\"")]
            [DataRow("\"\\\\\"", "\\")]
            [DataRow("\"\\/\"", "/")]
            [DataRow("\"\\b\"", "\b")]
            [DataRow("\"\\f\"", "\f")]
            [DataRow("\"\\n\"", "\n")]
            [DataRow("\"\\r\"", "\r")]
            [DataRow("\"\\t\"", "\t")]
            [DataRow("\"\\u0000\"", "\0")]
            [DataRow("\"\\u9999\"", "香")]
            [DataRow("\"\\u6038\"", "怸")]
            [DataRow("\"\\u1234\"", "ሴ")]
            [DataRow("\"\\uaFaa\"", "꾪")]
            [DataRow("\"\\uA6f9\"\t", "꛹")]
            [DataRow("\"\\uaB7C\"", "ꭼ")]
            [DataRow("\"\\u463D\"", "䘽")]
            [DataRow("\"\\u463DF\"", "䘽F")]
            [DataRow("\"\\u463DF001\"", "䘽F001")]
            [DataRow("\"Char \\\\\\u463DF001\"", "Char \\䘽F001")]
            [DataRow("  \"/\"", "/")]
            [DataRow("\"Just some text\"", "Just some text")]
            [DataRow("\"-==fjeiwo8njuvg\"", "-==fjeiwo8njuvg")]
            [DataRow("\"b\"", "b")]
            [DataRow("\"f\"", "f")]
            [DataRow("\"u98A3\"", "u98A3")]
            [DataRow("\"n\"", "n")]
            public void TestParseJson(string toParse, string expectation)
            {
                Assert.AreEqual(expectation, JsonString.ParseJson(toParse).Value);
            }// end TestParseJson()

            [TestMethod]
            [DataRow("\"\"", "", "")]
            [DataRow("\"\\\"\"", "\"", "")]
            [DataRow("\"\\\\\"", "\\", "")]
            [DataRow("\"\\/\"", "/", "")]
            [DataRow("\"\\b\"", "\b", "")]
            [DataRow("\"\\f\"", "\f", "")]
            [DataRow("\"\\n\"", "\n", "")]
            [DataRow("\"\\r\"", "\r", "")]
            [DataRow("\"\\t\"", "\t", "")]
            [DataRow("\"\\u0000\"", "\0", "")]
            [DataRow("\"\\u9999\"", "香", "")]
            [DataRow("\"\\u6038\"", "怸", "")]
            [DataRow("\"\\u1234\"", "ሴ", "")]
            [DataRow("\"\\uaFaa\"", "꾪", "")]
            [DataRow("\"\\uA6f9\"\t", "꛹", "")]
            [DataRow("\"\\uaB7C\"", "ꭼ", "")]
            [DataRow("\"\\u463D\"", "䘽", "")]
            [DataRow("\"\\u463DF\"", "䘽F", "")]
            [DataRow("\"\\u463DF001\"", "䘽F001", "")]
            [DataRow("\"Char \\\\\\u463DF001\"", "Char \\䘽F001", "")]
            [DataRow("  \"/\"", "/", "")]
            [DataRow("\"Just some text\"", "Just some text", "")]
            [DataRow("\"-==fjeiwo8njuvg\"", "-==fjeiwo8njuvg", "")]
            [DataRow("\"b\"", "b", "")]
            [DataRow("\"f\"", "f", "")]
            [DataRow("\"u98A3\"", "u98A3", "")]
            [DataRow("\"n\"", "n", "")]
            [DataRow("\"n\": value }", "n", ": value }")]
            [DataRow("\"n\"n", "n", "n")]
            [DataRow("\"n\"\"", "n", "\"")]
            public void TestParseJsonRemainder(string toParse, string expectation, string expectedRemainder)
            {
                Assert.AreEqual(expectation, JsonString.ParseJson(toParse, out string remainder).Value);
            } // end TestParseJsonRemainder()

            [TestMethod]
            [DataRow("", "")]
            [DataRow("\r", "\r")]
            [DataRow("\n", "\n")]
            [DataRow("\t", "\t")]
            [DataRow("\f", "\f")]
            [DataRow("\b", "\b")]
            [DataRow("\"", "\\\"")]
            [DataRow("\\", "\\\\")]
            [DataRow("afdsafd", "afdsafd")]
            [DataRow("afdsa\rfd", "afdsa\rfd")]
            [DataRow("afd怸sa\rfd", "afd怸sa\rfd")]
            public void TestToString(string value, string expectation)
            {
                JsonString v = new(value);
                Assert.AreEqual(value, v.Value);
                Assert.AreEqual(value, (string)v);
                Assert.AreEqual($"\"{expectation}\"", v.ToString());
            } // end TestToString()

            [TestMethod]
            [DataRow("", "", false, false, false, false, false, false)]
            [DataRow("\"", "\\\"", false, false, false, false, false, false)]
            [DataRow("\"", "\\\"", true, true, true, true, true, true)]
            [DataRow("\\", "\\\\", false, false, false, false, false, false)]
            [DataRow("\\", "\\\\", true, true, true, true, true, true)]
            [DataRow("/", "\\/", true, false, false, false, false, false)]
            [DataRow("\r", "\\r", false, false, false, false, true, false)]
            [DataRow("\n", "\\n", false, false, false, true, false, false)]
            [DataRow("\t", "\\t", false, false, false, false, false, true)]
            [DataRow("\f", "\\f", false, false, true, false, false, false)]
            [DataRow("\b", "\\b", false, true, false, false, false, false)]
            [DataRow("/", "\\/", true, true, true, true, true, true)]
            [DataRow("\r", "\\r", true, true, true, true, true, true)]
            [DataRow("\n", "\\n", true, true, true, true, true, true)]
            [DataRow("\t", "\\t", true, true, true, true, true, true)]
            [DataRow("\f", "\\f", true, true, true, true, true, true)]
            [DataRow("\b", "\\b", true, true, true, true, true, true)]
            [DataRow("/", "/", false, false, false, false, false, false)]
            [DataRow("\r", "\r", false, false, false, false, false, false)]
            [DataRow("\n", "\n", false, false, false, false, false, false)]
            [DataRow("\t", "\t", false, false, false, false, false, false)]
            [DataRow("\f", "\f", false, false, false, false, false, false)]
            [DataRow("\b", "\b", false, false, false, false, false, false)]
            [DataRow("afdsafd", "afdsafd", true, true, true, true, true, true)]
            public void TestFormattedToString(string value, string expectation, bool solidus, bool backspace, bool formfeed, bool linefeeds, bool carriageReturns, bool horizontalTabs)
            {
                JsonFormatting format = new(stringFormatting: new(solidus, backspace, formfeed, linefeeds, carriageReturns, horizontalTabs));
                JsonString v = new(value);
                Assert.AreEqual(value, v.Value);
                Assert.AreEqual(value, (string)v);
                Assert.AreEqual($"\"{expectation}\"", v.ToString(format));
            } // end TestFormattedToString()

            [TestMethod]
            [DataRow("\"\"")]
            [DataRow("\"hnjmkwjn4nbhy5uenj nhjby\"")]
            public void TestNullEquality(string json)
            {
                Assert.IsFalse(JsonString.ParseJson(json).Equals(null));
            }// end TestNullEquality()
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(MalformedJsonException))]
            [DataRow("")]
            [DataRow("mkjw;rw\"")]
            [DataRow("\"sfakl")]
            [DataRow("\"")]
            [DataRow("\"\"\"")]
            [DataRow("\"\\\"")]
            [DataRow("\"\\]\"")]
            [DataRow("\"\\y\"")]
            [DataRow("\"\\U78A3\"")]
            [DataRow("\"\\U78A\"")]
            [DataRow("\"\\U734F8A3\"")]
            [DataRow("\"\\U04-=\"")]
            [DataRow("\"\\u000-\"")]
            [DataRow("\"\\uL\"")]
            [DataRow("\"\\u\"")]
            [DataRow("\"\\u8A3\"")]
            [DataRow("\"\\8F3\"")]
            [DataRow("\"\\a\"")]
            [DataRow("\"\\0\"")]
            [DataRow("\"\\Uaaaa\"")]
            [DataRow("\"\\Uaaa00bba\"")]
            [DataRow("sdf \"\"")]
            public void TestParse(string s)
            {
                JsonString.ParseJson(s);
            }// end TestParse()        

#pragma warning disable CS8625
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void TestConstructNull()
            {
                new JsonString(null);
            } // end TestConstructNull()

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void TestParseJson1()
            {
                JsonString.ParseJson(null, out _);
            } // end TestParseJson1()

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void TestParseJson2()
            {
                JsonString.ParseJson(null);
            } // end TestParseJson2()
#pragma warning restore CS8625
        } // end inner class Invalid()
    } // end class
} // end namespace