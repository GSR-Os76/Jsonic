using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonBoolean
    {

        [TestMethod]
        public void TestDifferentInstanceMethodEquality()
        {
            Assert.IsTrue(JsonBoolean.FALSE.Equals(JsonBoolean.FALSE));
            Assert.IsTrue(JsonBoolean.TRUE.Equals(JsonBoolean.TRUE));
            Assert.IsFalse(JsonBoolean.FALSE.Equals(JsonBoolean.TRUE));
            Assert.IsFalse(JsonBoolean.TRUE.Equals(JsonBoolean.FALSE));
        } // end TestDifferentInstanceMethodEquality()

        [TestMethod]
        public void TestSameInstanceMethodEquality()
        {
            JsonBoolean f = JsonBoolean.FALSE;
            JsonBoolean t = JsonBoolean.TRUE;
            Assert.IsTrue(f.Equals(f));
            Assert.IsTrue(t.Equals(t));
            Assert.IsFalse(f.Equals(t));
            Assert.IsFalse(t.Equals(f));
        } // end TestSameInstanceMethodEquality()

        [TestMethod]
        public void TestDifferentInstanceOperatorEquality()
        {
#pragma warning disable CS1718
            Assert.IsTrue(JsonBoolean.FALSE == JsonBoolean.FALSE);
            Assert.IsTrue(JsonBoolean.TRUE == JsonBoolean.TRUE);
#pragma warning restore CS1718
            Assert.IsFalse(JsonBoolean.FALSE == JsonBoolean.TRUE);
            Assert.IsFalse(JsonBoolean.TRUE == JsonBoolean.FALSE);
        } // end TestDifferentInstanceOperatorEquality()

        [TestMethod]
        public void TestSameInstanceOperatorEquality()
        {
            JsonBoolean f = JsonBoolean.FALSE;
            JsonBoolean t = JsonBoolean.TRUE;
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsTrue(f == f);
            Assert.IsTrue(t == t);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.IsFalse(f == t);
            Assert.IsFalse(t == f);
        } // end TestSameInstanceOperatorEquality()

        [TestMethod]
        public void TestDifferentInstanceOperatorDisequality()
        {
#pragma warning disable CS1718
            Assert.IsFalse(JsonBoolean.FALSE != JsonBoolean.FALSE);
            Assert.IsFalse(JsonBoolean.TRUE != JsonBoolean.TRUE);
#pragma warning restore CS1718
            Assert.IsTrue(JsonBoolean.FALSE != JsonBoolean.TRUE);
            Assert.IsTrue(JsonBoolean.TRUE != JsonBoolean.FALSE);
        } // end TestDifferentInstanceOperatorDisequality()

        [TestMethod]
        public void TestSameInstanceOperatorDisequality()
        {
            JsonBoolean f = JsonBoolean.FALSE;
            JsonBoolean t = JsonBoolean.TRUE;
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsFalse(f != f);
            Assert.IsFalse(t != t);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.IsTrue(f != t);
            Assert.IsTrue(t != f);
        } // end TestSameInstanceOperatorDisequality()

        [TestMethod]
        public void TestNullEquality()
        {
            Assert.IsFalse(JsonBoolean.FALSE.Equals(null));
            Assert.IsFalse(JsonBoolean.TRUE.Equals(null));
        } // end TestDifferentInstanceOperatorDisequality()

    } // end class
} // end namespace