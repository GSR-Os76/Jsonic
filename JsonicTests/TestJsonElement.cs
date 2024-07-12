using GSR.Jsonic;
using System.Data;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonElement
    {
        [TestMethod]
        public void TestNullValue() 
        {
            Assert.AreEqual(JsonUtil.JSON_NULL, new JsonElement((JsonObject?)null).ToString());
            Assert.AreEqual(JsonUtil.JSON_NULL, new JsonElement().ToString());
            Assert.AreEqual(JsonUtil.JSON_NULL, new JsonElement((JsonObject?)null).ToCompressedString());
            Assert.AreEqual(JsonUtil.JSON_NULL, new JsonElement().ToCompressedString());
        } // end TestNullValue()

        [TestMethod]
        [DataRow(false, JsonUtil.JSON_FALSE)]
        [DataRow(true, JsonUtil.JSON_TRUE)]
        public void TestBooleanValue(bool b, string expectation)
        {
            JsonElement j = new(b);
            Assert.AreEqual(expectation, j.ToString());
            Assert.AreEqual(expectation, j.ToCompressedString());
        } // end TestNullValue()




#warning others

    } // end class
} // end namespace