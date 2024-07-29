using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonArrayNullArguments
    {
#pragma warning disable CS8625
#pragma warning disable CS8600

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexerSet1()
        {
            new JsonArray()[0] = (JsonElement)null;
        } // end TestIndexerSet1()

        #region Constructor Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructNull1()
        {
            new JsonArray((JsonElement[])null);
        } // end TestConstructNull1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructNull2()
        {
            new JsonArray((IEnumerable<JsonElement>)null);
        } // end TestConstructNull2()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructNullContainingIEnumerable()
        {
            new JsonArray(new List<JsonElement> { new JsonElement(2), null });
        } // end TestConstructNullContainingIEnumerable()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructNullContainingArray()
        {
            new JsonArray(new JsonElement[] { new JsonElement(2), null });
        } // end TestConstructNullContainingArray()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructNullContainingParams()
        {
            new JsonArray(new JsonElement(2), null);
        } // end TestConstructNullContainingArray()
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd()
        {
            new JsonArray().Add((JsonElement)null);
        } // end TestAdd()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertAt()
        {
            new JsonArray().InsertAt(0, (JsonElement)null);
        } // end TestInsertAt()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson1()
        {
            JsonArray.ParseJson(null, out _);
        } // end TestParseJson1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseJson2()
        {
            JsonArray.ParseJson(null);
        } // end TestParseJson2()
#pragma warning restore CS8625
#pragma warning restore CS8600

    } // end class
} // end namespace