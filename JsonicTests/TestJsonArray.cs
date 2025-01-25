using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonArray
    {
        [TestClass]
        public class Valid
        {
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
                    newLineType: newLineType,
                    arrayFormatting: new(emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation))));
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
                                arrayFormatting: new(emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation))));
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
                                arrayFormatting: new(emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation))));
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
                                arrayFormatting: new(emptySpacing, newLineProceedingFirstElement, newLineBetweenElements, newLineSucceedingLastElement, indentation, postCommaSeparation))));
            } // end ToStringNested3()

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
            public void Equality(string a, string b, bool expectation)
                => Assert.AreEqual(expectation, JsonArray.ParseJson(a).Equals(JsonArray.ParseJson(b)));

            [TestMethod]
            public void NullEquality()
                => Assert.AreEqual(false, new JsonArray().Equals(null));

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
            public void ParseJson(string json)
                => JsonArray.ParseJson(json);

            [TestMethod]
            public void Add()
            {
                JsonArray arr = new JsonArray().Add(null);
                Assert.AreEqual(new JsonElement(), arr[0]);
            } // end Add()

/*            [TestMethod]
            public void IndexerSet()
            {
                JsonArray arr = new();
#pragma warning disable CS8625
                arr[0] = null;
#pragma warning restore CS8625 
                Assert.AreEqual(new JsonElement(), arr[0]);
            } // end IndexerSet()*/

            [TestMethod]
            public void InsertAt()
            {
                JsonArray arr = new JsonArray().InsertAt(0, null);
                Assert.AreEqual(new JsonElement(), arr[0]);
            } // end InsertAt()
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
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
            [DataRow("[0,0.0,0e3")]
            [DataRow("[{]")]
            [DataRow("[--0e3]")]
            [DataRow("[[00], \"\"]")]
            [DataRow("[null,")]
            [DataRow("[6.3, false, false,]")]
            public void ParseJson(string json)
                => JsonArray.ParseJson(json);

#pragma warning disable CS8625
#pragma warning disable CS8600

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ConstructNull1()
                => new JsonArray((JsonElement[])null);

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ConstructNull2()
                => new JsonArray((IEnumerable<JsonElement>)null);

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ConstructNullContainingIEnumerable()
                => new JsonArray(new List<JsonElement> { new JsonElement(2), null });

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ConstructNullContainingArray()
                => new JsonArray(new JsonElement[] { new JsonElement(2), null });

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ConstructNullContainingParams()
                => new JsonArray(2, null);

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJson()
                => JsonArray.ParseJson(null);

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJsonRemainderized()
                => JsonArray.ParseJson(null, out _);
#pragma warning restore CS8625
#pragma warning restore CS8600
        } // end inner class Invalid
    } // end class
} // end namespace