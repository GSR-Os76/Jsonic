using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonArray
    {
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
            Assert.AreEqual(e, new JsonArray(new JsonElement(new JsonArray())).ToCompressedString());
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
            Assert.AreEqual(e, new JsonArray(new JsonElement(new JsonArray(new JsonElement(false))), new JsonElement(true), new JsonElement(new JsonString("\"are\""))).ToCompressedString());
        } // end ToCompressedStringNested3()
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
            Assert.AreEqual(e, new JsonArray(new JsonElement(new JsonArray(new JsonElement(false))), new JsonElement(true), new JsonElement(new JsonString("\"are\""))).ToString());
        } // end ToStringNested3()
        #endregion

    } // end class
} // end namespace