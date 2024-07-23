using GSR.Jsonic;

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
            new JsonArray(json);
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
            new JsonArray(json);
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
        public void TestParseInvalid(string json) 
        {
            new JsonArray(json);
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
            Assert.AreEqual(expectation, new JsonArray(a).Equals(new JsonArray(b)));
        } // end TestEquality()
        #endregion

        #region ToCompressedString()
        [TestMethod]
        [DataRow("[]")]
        public void ToCompressedStringEmpty(string e)
        {
            Assert.AreEqual(e, new JsonArray().ToCompressedString());
        } // end ToCompressedStringEmpty()

        [TestMethod]
        [DataRow("[[]]")]
        public void ToCompressedStringNested1(string e)
        {
            Assert.AreEqual(e, new JsonArray().Add(new JsonArray()).ToCompressedString());
        } // end ToCompressedStringNested1()

        [TestMethod]
        [DataRow("[[false],true]")]
        public void ToCompressedStringNested2(string e)
        {
            Assert.AreEqual(e, new JsonArray(new JsonElement(new JsonArray(new JsonElement(false))), new JsonElement(true)).ToCompressedString());
        } // end ToCompressedStringNested2()


        [TestMethod]
        [DataRow("[[false],true,\"are\"]")]
        public void ToCompressedStringNested3(string e)
        {
            Assert.AreEqual(e, new JsonArray(new(new JsonArray(new JsonElement(false))), new(true), new("are")).ToCompressedString());
        } // end ToCompressedStringNested3()

        [TestMethod]
        [DataRow("[[false],true,\"are\"]")]
        public void ToCompressedStringNested3_2(string e)
        {
            Assert.AreEqual(e, new JsonArray().Add(new JsonArray().Add(false)).Add(true).Add("are").ToCompressedString());
        } // end ToCompressedStringNested3_2()
        #endregion

        #region ToString
        [TestMethod]
        [DataRow("[\r\r]")]
        public void ToStringEmpty(string e)
        {
            Assert.AreEqual(e, new JsonArray().ToString());
        } // end ToStringEmpty()

        [TestMethod]
        [DataRow("[\r\t[\r\t\r\t]\r]")]
        public void ToStringNested1(string e)
        {
            Assert.AreEqual(e, new JsonArray(new JsonElement(new JsonArray())).ToString());
        } // end ToStringNested1()

        [TestMethod]
        [DataRow("[\r\t[\r\t\tfalse\r\t],\r\ttrue\r]")]
        public void ToStringNested2(string e)
        {
            Assert.AreEqual(e, new JsonArray(new JsonElement(new JsonArray(new JsonElement(false))), new JsonElement(true)).ToString());
        } // end ToStringNested2()

        [TestMethod]
        [DataRow("[\r\t[\r\t\tfalse\r\t],\r\ttrue,\r\t\"are\"\r]")]
        public void ToStringNested3(string e)
        {
            Assert.AreEqual(e, new JsonArray(new JsonElement(new JsonArray(new JsonElement(false))), new JsonElement(true), new JsonElement(new JsonString("are"))).ToString());
        } // end ToStringNested3()
        #endregion

    } // end class
} // end namespace