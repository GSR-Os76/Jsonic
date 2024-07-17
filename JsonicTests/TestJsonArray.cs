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
        public void TestConstructFails(string json)
        {
            new JsonArray(json);
        } // end TestConstructFails()

        // test elements that were parsed are as was expected
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