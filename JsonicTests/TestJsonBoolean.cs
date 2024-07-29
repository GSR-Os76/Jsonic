using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonBoolean
    {

        [TestMethod]
        public void TestDifferentInstanceMethodEquality()
        {
            Assert.IsTrue(new JsonBoolean(false).Equals(new JsonBoolean(false)));
            Assert.IsTrue(new JsonBoolean(true).Equals(new JsonBoolean(true)));
            Assert.IsFalse(new JsonBoolean(false).Equals(new JsonBoolean(true)));
            Assert.IsFalse(new JsonBoolean(true).Equals(new JsonBoolean(false)));
        } // end TestDifferentInstanceMethodEquality()

        [TestMethod]
        public void TestSameInstanceMethodEquality()
        {
            JsonBoolean f = new(false);
            JsonBoolean t = new(true);
            Assert.IsTrue(f.Equals(f));
            Assert.IsTrue(t.Equals(t));
            Assert.IsFalse(f.Equals(t));
            Assert.IsFalse(t.Equals(f));
        } // end TestSameInstanceMethodEquality()

        [TestMethod]
        public void TestDifferentInstanceOperatorEquality()
        {
            Assert.IsTrue(new JsonBoolean(false) == (new JsonBoolean(false)));
            Assert.IsTrue(new JsonBoolean(true) == (new JsonBoolean(true)));
            Assert.IsFalse(new JsonBoolean(false) == (new JsonBoolean(true)));
            Assert.IsFalse(new JsonBoolean(true) == (new JsonBoolean(false)));
        } // end TestDifferentInstanceOperatorEquality()

        [TestMethod]
        public void TestSameInstanceOperatorEquality()
        {
            JsonBoolean f = new(false);
            JsonBoolean t = new(true);
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
            Assert.IsFalse(new JsonBoolean(false) != (new JsonBoolean(false)));
            Assert.IsFalse(new JsonBoolean(true) != (new JsonBoolean(true)));
            Assert.IsTrue(new JsonBoolean(false) != (new JsonBoolean(true)));
            Assert.IsTrue(new JsonBoolean(true) != (new JsonBoolean(false)));
        } // end TestDifferentInstanceOperatorDisequality()

        [TestMethod]
        public void TestSameInstanceOperatorDisequality()
        {
            JsonBoolean f = new(false);
            JsonBoolean t = new(true);
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsFalse(f != f);
            Assert.IsFalse(t != t);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.IsTrue(f != t);
            Assert.IsTrue(t != f);
        } // end TestSameInstanceOperatorDisequality()

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void TestNullEquality(bool value)
        {
            Assert.IsFalse(new JsonBoolean(value).Equals(null));
        } // end TestDifferentInstanceOperatorDisequality()

    } // end class
} // end namespace