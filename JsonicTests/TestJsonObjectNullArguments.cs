using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonObjectNullArguments
    {
#pragma warning disable CS8625
#pragma warning disable CS8600

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexerGet1()
        {
            object _ = new JsonObject()[(string)null];
        } // end TestIndexerGet1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexerGet2()
        {
            object _ = new JsonObject()[(JsonString)null];
        } // end TestIndexerGet2()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexerSet1()
        {
            new JsonObject()[string.Empty] = (JsonElement)null;
        } // end TestIndexerSet1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexerSet2()
        {
            new JsonObject()[""] = (JsonElement)null;
        } // end TestIndexerSet2()

        #region Constructor Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructNull1()
        {
            new JsonObject((KeyValuePair<JsonString, JsonElement>[])null);
        } // end TestConstructNull1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructNull2()
        {
            new JsonObject((IEnumerable<KeyValuePair<JsonString, JsonElement>>)null);
        } // end TestConstructNull2()
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd1()
        {
            new JsonObject().Add("", (JsonElement)null);
        } // end TestAdd1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd2()
        {
            new JsonObject().Add("", (JsonElement)null);
        } // end TestAdd2()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd3()
        {
            new JsonObject().Add((string)null, new JsonElement());
        } // end TestAdd3()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd4()
        {
            new JsonObject().Add((JsonString)null, new JsonElement());
        } // end TestAdd4()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddNull1()
        {
            new JsonObject().AddNull((string)null);
        } // end TestAddNull1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddNull2()
        {
            new JsonObject().AddNull((JsonString)null);
        } // end TestAddNull2()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsKey1()
        {
            new JsonObject().ContainsKey((string)null);
        } // end TestContainsKey1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsKey2()
        {
            new JsonObject().ContainsKey((JsonString)null);
        } // end TestContainsKey2()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemove1()
        {
            new JsonObject().Remove((string)null);
        } // end TestRemove1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemove2()
        {
            new JsonObject().Remove((JsonString)null);
        } // end TestRemove2()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson1()
        {
            JsonObject.ParseJson(null, out _);
        } // end TestParseJson1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson2()
        {
            JsonObject.ParseJson(null);
        } // end TestParseJson2()
#pragma warning restore CS8625
#pragma warning restore CS8600

    } // end class
} // end namespace