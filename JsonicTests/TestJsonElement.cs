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
        } // end TestNullValue()

        [TestMethod]
        [DataRow(false, JsonOptions.Default, JsonUtil.JSON_FALSE)]
        [DataRow(true, JsonOptions.Default, JsonUtil.JSON_TRUE)]
        [DataRow(false, JsonOptions.Compress, JsonUtil.JSON_FALSE)]
        [DataRow(true, JsonOptions.Compress, JsonUtil.JSON_TRUE)]
        public void TestBooleanValue(bool b, JsonOptions o, string expectation)
        {
            JsonElement j = new(b);
            j.Options = o;
            Assert.AreEqual(expectation, j.ToString());
        } // end TestNullValue()

        [TestMethod]
        public void TestBooleanValue()
        {
            Assert.AreEqual(JsonUtil.JSON_FALSE, new JsonElement(false).ToString());
            Assert.AreEqual(JsonUtil.JSON_TRUE, new JsonElement(true).ToString());
        } // end TestNullValue()
#warning others

    } // end class
} // end namespace