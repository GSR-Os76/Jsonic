using GSR.Jsonic;
using GSR.Jsonic.Formatting;

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
            public void ParseJson(string toParse, string expectation) 
                => Assert.AreEqual(expectation, JsonString.ParseJson(toParse).Value);

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
            [DataRow("\"\\uA6f9\"\t", "꛹", "\t")]
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
            public void ParseJsonRemainderized(string toParse, string expectation, string expectedRemainder)
            {
                Assert.AreEqual(expectation, JsonString.ParseJson(toParse, out string remainder).Value);
                Assert.AreEqual(expectedRemainder, remainder);
            } // end ParseJsonRemainderized()

            [TestMethod]
            [DataRow("", "")]
            [DataRow("/", "/")]
            [DataRow("\0", "\\u0000")]
            [DataRow("\u0000", "\\u0000")]
            [DataRow("\r", "\\r")]
            [DataRow("\n", "\\n")]
            [DataRow("\t", "\\t")]
            [DataRow("\f", "\\f")]
            [DataRow("\b", "\\b")]
            [DataRow("\"", "\\\"")]
            [DataRow("\\", "\\\\")]
            [DataRow("afdsafd", "afdsafd")]
            [DataRow("afdsa\rfd", "afdsa\\rfd")]
            [DataRow("afd怸sa\rfd", "afd怸sa\\rfd")]
            public void ToString(string value, string expectation)
            {
                JsonString v = new(value);
                Assert.AreEqual(value, v.Value);
                Assert.AreEqual(value, (string)v);
                Assert.AreEqual($"\"{expectation}\"", v.ToString());
            } // end ToString()

            [TestMethod]
            [DataRow("", "", false, false)]
            [DataRow("\"", "\\\"", false, false)]
            [DataRow("\"", "\\\"", false, true)]
            [DataRow("\\", "\\\\", false, false)]
            [DataRow("\\", "\\\\", false, true)]
            [DataRow("/", "\\/", false, true)]
            [DataRow("/", "/", false, false)]
            [DataRow("\0", "\\u0000", false, true)]
            [DataRow("\u0000", "\\u0000", false, true)]
#warning should separators be counted too? [DataRow("\u2028", "\\u2028", true)]
            [DataRow("\0", "\\u0000", false, false)]
            [DataRow("\u0000", "\\u0000", false, false)]
            [DataRow("\r", "\\r", false, false)]
            [DataRow("\n", "\\n", false, false)]
            [DataRow("\t", "\\t", false, false)]
            [DataRow("\f", "\\f", false, false)]
            [DataRow("\b", "\\b", false, false)]
            [DataRow("\r", "\\r", false, true)]
            [DataRow("\n", "\\n", false, true)]
            [DataRow("\t", "\\t", false, true)]
            [DataRow("\f", "\\f", false, true)]
            [DataRow("\b", "\\b", false, true)]
            [DataRow("afdsafd", "afdsafd", false, true)]
            public void ConstructedFormattedToString(string value, string expectation, bool preserve, bool solidus)
            {
                JsonFormatting format = new(stringFormatting: new(preserve, solidus));
                JsonString v = new(value);
                Assert.AreEqual(value, v.Value);
                Assert.AreEqual(value, (string)v);
                Assert.AreEqual($"\"{expectation}\"", v.ToString(format));
            } // end ConstructedFormattedToString()

#warning ParsedFormattedToString
            /*
             
            [DataRow("\"\\u9999\"", "香", "")]
            [DataRow("\"\\u6038\"", "怸", "")]
            [DataRow("\"\\u1234\"", "ሴ", "")]
            [DataRow("\"\\uaFaa\"", "꾪", "")]
             */

            [TestMethod]
            [DataRow("\"\"")]
            [DataRow("\"hnjmkwjn4nbhy5uenj nhjby\"")]
            public void NullEquality(string json)
                => Assert.IsFalse(JsonString.ParseJson(json).Equals(null));
        } // end inner NullEquality Valid

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
            public void Parse(string s)
            {
                JsonString.ParseJson(s);
            }// end Parse()        

#pragma warning disable CS8625
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Construct()
            {
                new JsonString(null);
            } // end Construct()

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJson()
            {
                JsonString.ParseJson(null);
            } // end ParseJson()

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJsonRemainderized()
            {
                JsonString.ParseJson(null, out _);
            } // end ParseJsonRemainderized()
#pragma warning restore CS8625
        } // end inner class Invalid
    } // end class
} // end namespace