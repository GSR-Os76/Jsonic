using GSR.Jsonic;

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
        public void TestBooleanValue()
        {
            Assert.AreEqual(JsonUtil.JSON_FALSE, new JsonElement(false).ToString());
            Assert.AreEqual(JsonUtil.JSON_TRUE, new JsonElement(true).ToString());
        } // end TestNullValue()

#warning others

    } // end class
} // end namespace