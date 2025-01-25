using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonArray
    {
        #region constructor tests
        [TestMethod]
        [DataRow("[]")]
        [DataRow("     []")]
        [DataRow("[]     ")]
        [DataRow("[  ]")]
        [DataRow("[false]")]
        [DataRow("[true]")]
        [DataRow("[null]")]
        [DataRow("[-2.7e4]")]
        [DataRow("[\"string\"]")]
        [DataRow("[[]]")]
        [DataRow("[[0]]")]
        [DataRow("[{}]")]
        [DataRow("[{\"k\": \"v\"}]")]
        [DataRow("[false, \"\"]")]
        [DataRow("[null, null]")]
        [DataRow("[0, -1]")]
        // and with values
        public void TestConstructSuccess(string json)
        {
            JsonArray.ParseJson(json);
        } // end TestConstructSuccess()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("[")]
        [DataRow("]")]
        [DataRow("a[")]
        [DataRow("_[]")]
        [DataRow("[]0")]
        [DataRow("[false,]")]
        [DataRow("[nil,]")]
        [DataRow("[nil]")]
        [DataRow("[93,9,f]")]
        public void TestConstructFail(string json)
        {
            JsonArray.ParseJson(json);
        } // end TestConstructFail()

        // test elements that were parsed are as was expected
        #endregion

        #region ParseTests
        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("[0,0.0,0e3")]
        [DataRow("[{]")]
        [DataRow("[--0e3]")]
        [DataRow("[[00], \"\"]")]
        [DataRow("[null,")]
        [DataRow("[6.3, false, false,]")]
        public void TestParseInvalid(string json)
        {
            JsonArray.ParseJson(json);
        } // end TestParseInvalid()
        #endregion

        #region equality tests
        [TestMethod]
        [DataRow("[]", "[]", true)]
        [DataRow("[false]", "[\r\tfalse   ]", true)]
        [DataRow("[true]", "[false]", false)]
        [DataRow("[\"a\", 0e1]", "[\"a\", 0e3]", true)]
        [DataRow("[\"a\", 19]", "[\"a\", 1.9e1]", true)]
        [DataRow("[\"a\", null, \"b\"]", "[\"b\", null, \"a\"]", false)]
        [DataRow("[[], []]", "[[], []]", true)]
        [DataRow("[{}, null]", "[{}, null]", true)]
        [DataRow("[{\"aaaa\":[0]},null]", "[{\"aaaa\" :  [0]}, null ]", true)]
        public void TestEquality(string a, string b, bool expectation)
        {
            Assert.AreEqual(expectation, JsonArray.ParseJson(a).Equals(JsonArray.ParseJson(b)));
        } // end TestEquality()

        [TestMethod]
        public void TestNullEquality()
        {
            Assert.AreEqual(false, new JsonArray().Equals(null));
        } // end TestEquality()
        #endregion

        #region ToString
        [TestMethod]
        [DataRow("[\r\r]", NewLineType.CR, 2, false, false, false, "\t", "")]
        [DataRow("[\r\n\r\n]", NewLineType.CRLF, 2, false, false, false, "\t", "")]
        [DataRow("[\r\n\r\n]", NewLineType.CRLF, 2, true, true, true, "  ", "")]
        [DataRow("[]", NewLineType.NONE, 2, false, false, false, "", "")]
        [DataRow("[]", NewLineType.NONE, 0, false, false, false, "   \t", "")]
        [DataRow("[]", NewLineType.LF, 0, false, false, false, "", "")]
        [DataRow("[]", NewLineType.LF, 0, true, true, true, "", "     ")]
        [DataRow("[\n]", NewLineType.LF, 1, false, false, false, "\t", "")]
        [DataRow("[\n\n\n\n]", NewLineType.LF, 4, false, false, false, " \t ", "")]
        public void ToStringEmpty(string expectation, NewLineType newLineType, int emptySpacing,
            bool newLineProceedingFirstElement,
            bool newLineBetweenElements,
            bool newLineSucceedingLastElement,
            string indentation,
            string postCommaSeparation)
        {
            JsonArray data = new();
            Assert.AreEqual(expectation, data.ToString(new(
                newLineType:newLineType,
                arrayFormatting:new(new(emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation)))));
        } // end ToStringEmpty()

        [TestMethod]
        [DataRow("[\r\t[\r\t\r\t]\r]", NewLineType.CR, 2, true, true, true, "\t", "")]
        [DataRow("[\r[\r\r]\r]", NewLineType.CR, 2, true, true, true, "", "")]
        [DataRow("[\r[]\r]", NewLineType.CR, 0, true, true, true, "", "")]
        [DataRow("[\n[]\n]", NewLineType.LF, 0, true, true, true, "", "")]
        [DataRow("[\n    []\n]", NewLineType.LF, 0, true, true, true, "    ", "")]
        [DataRow("[\n[]]", NewLineType.LF, 0, true, true, false, "", "")]
        [DataRow("[[]\n]", NewLineType.LF, 0, false, true, true, "", "")]
        public void ToStringNested1(string expectation, NewLineType newLineType, int emptySpacing,
            bool newLineProceedingFirstElement,
            bool newLineBetweenElements,
            bool newLineSucceedingLastElement,
            string indentation,
            string postCommaSeparation)
        {
            JsonArray data = new((JsonElement)new JsonArray());
            Assert.AreEqual(expectation, data.ToString(new(
                            newLineType: newLineType,
                            arrayFormatting: new(new(emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation)))));
        } // end ToStringNested1()

        [TestMethod]
        [DataRow("[\r\t[\r\t\tfalse\r\t],\r\ttrue\r]", NewLineType.CR, 2, true, true, true, "\t", "")]
        [DataRow("[\r\n    [\r\n        false\r\n    ],\r\n    true\r\n]", NewLineType.CRLF, 2, true, true, true, "    ", "")]
        [DataRow("[\r\n    [\r\n        false\r\n    ],true\r\n]", NewLineType.CRLF, 2, true, false, true, "    ", "")]
        [DataRow("[\r\n    [\r\n        false\r\n    ],  true\r\n]", NewLineType.CRLF, 2, true, false, true, "    ", "  ")]
        public void ToStringNested2(string expectation, NewLineType newLineType, int emptySpacing,
            bool newLineProceedingFirstElement,
            bool newLineBetweenElements,
            bool newLineSucceedingLastElement,
            string indentation,
            string postCommaSeparation)
        {
            JsonArray data = new(new JsonArray(false), true);
            Assert.AreEqual(expectation, data.ToString(new(
                            newLineType: newLineType,
                            arrayFormatting: new(new(emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation)))));
        } // end ToStringNested2()

        [TestMethod]
        [DataRow("[\r\t[\r\t\tfalse\r\t],\r\ttrue,\r\t\"ar\\re\"\r]", NewLineType.CR, 2, true, true, true, "\t", "")]
        [DataRow("[[false],true,\"ar\\re\"]", NewLineType.CR, 1, false, false, false, "\t", "")]
        [DataRow("[[false], true, \"ar\\re\"]", NewLineType.CR, 1, false, false, false, "\t", " ")]
        [DataRow("[[false],\t true,\t \"ar\\re\"]", NewLineType.CR, 1, false, false, false, "\t", "\t ")]
        public void ToStringNested3(string expectation, NewLineType newLineType, int emptySpacing,
            bool newLineProceedingFirstElement,
            bool newLineBetweenElements,
            bool newLineSucceedingLastElement,
            string indentation,
            string postCommaSeparation)
        {
            JsonArray data = new(new JsonArray(false), true, "ar\re");
            Assert.AreEqual(expectation, data.ToString(new(
                            newLineType: newLineType,
                            arrayFormatting: new(new(emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation)))));
        } // end ToStringNested3()
        #endregion

    } // end class
} // end namespace