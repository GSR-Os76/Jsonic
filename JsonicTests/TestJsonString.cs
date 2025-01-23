using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonString
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
            public void TestToString(string value, string expectation) 
            {
                JsonString v = new(value);
                Assert.AreEqual(value, v.Value);
                Assert.AreEqual(value, (string)v);
                Assert.AreEqual($"\"{expectation}\"", v.ToString());
            } // end TestToString()
            
            [TestMethod]
            [DataRow("", "", JsonString.OptionalEscapeCharacters.NONE)]
            [DataRow("\"", "\\\"", JsonString.OptionalEscapeCharacters.NONE)]
            [DataRow("\"", "\\\"", JsonString.OptionalEscapeCharacters.ALL)]
            [DataRow("\\", "\\\\", JsonString.OptionalEscapeCharacters.NONE)]
            [DataRow("\\", "\\\\", JsonString.OptionalEscapeCharacters.ALL)]
            [DataRow("/", "\\/", JsonString.OptionalEscapeCharacters.SOLIDUS)]
            [DataRow("\r", "\\r", JsonString.OptionalEscapeCharacters.CARRIAGE_RETURN)]
            [DataRow("\n", "\\n", JsonString.OptionalEscapeCharacters.LINEFEED)]
            [DataRow("\t", "\\t", JsonString.OptionalEscapeCharacters.HORIZONTAL_TAB)]
            [DataRow("\f", "\\f", JsonString.OptionalEscapeCharacters.FORMFEED)]
            [DataRow("\b", "\\b", JsonString.OptionalEscapeCharacters.BACKSPACE)]
            [DataRow("/", "\\/", JsonString.OptionalEscapeCharacters.ALL)]
            [DataRow("\r", "\\r", JsonString.OptionalEscapeCharacters.ALL)]
            [DataRow("\n", "\\n", JsonString.OptionalEscapeCharacters.ALL)]
            [DataRow("\t", "\\t", JsonString.OptionalEscapeCharacters.ALL)]
            [DataRow("\f", "\\f", JsonString.OptionalEscapeCharacters.ALL)]
            [DataRow("\b", "\\b", JsonString.OptionalEscapeCharacters.ALL)]
            [DataRow("/", "/", JsonString.OptionalEscapeCharacters.NONE)]
            [DataRow("\r", "\r", JsonString.OptionalEscapeCharacters.NONE)]
            [DataRow("\n", "\n", JsonString.OptionalEscapeCharacters.NONE)]
            [DataRow("\t", "\t", JsonString.OptionalEscapeCharacters.NONE)]
            [DataRow("\f", "\f", JsonString.OptionalEscapeCharacters.NONE)]
            [DataRow("\b", "\b", JsonString.OptionalEscapeCharacters.NONE)]
            [DataRow("afdsafd", "afdsafd", JsonString.OptionalEscapeCharacters.ALL)]
            public void TestFormattedToString(string value, string expectation, JsonString.OptionalEscapeCharacters escaping) 
            {
                JsonString v = new(value);
                Assert.AreEqual(value, v.Value);
                Assert.AreEqual(value, (string)v);
                Assert.AreEqual($"\"{expectation}\"", v.ToString(escaping));
            } // end TestFormattedToString()

            [TestMethod]
            [DataRow("\"\"")]
            [DataRow("\"hnjmkwjn4nbhy5uenj nhjby\"")]
            public void TestNullEquality(string json)
            {
                Assert.IsFalse(JsonString.ParseJson(json).Equals(null));
            }// end TestNullEquality()
        } // end inner class Valid

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
        } // end inner class Invalid()
    } // end class
} // end namespace