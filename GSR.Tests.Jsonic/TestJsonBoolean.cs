using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    public class TestJsonBoolean
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void MethodEquality()
            {
                Assert.IsTrue(JsonBoolean.FALSE.Equals(JsonBoolean.FALSE));
                Assert.IsTrue(JsonBoolean.TRUE.Equals(JsonBoolean.TRUE));
                Assert.IsFalse(JsonBoolean.FALSE.Equals(JsonBoolean.TRUE));
                Assert.IsFalse(JsonBoolean.TRUE.Equals(JsonBoolean.FALSE));
            } // end MethodEquality()

            [TestMethod]
            public void OperatorEquality()
            {
#pragma warning disable CS1718
                Assert.IsTrue(JsonBoolean.FALSE == JsonBoolean.FALSE);
                Assert.IsTrue(JsonBoolean.TRUE == JsonBoolean.TRUE);
#pragma warning restore CS1718
                Assert.IsFalse(JsonBoolean.FALSE == JsonBoolean.TRUE);
                Assert.IsFalse(JsonBoolean.TRUE == JsonBoolean.FALSE);
            } // end OperatorEquality()

            [TestMethod]
            public void OperatorDisequality()
            {
#pragma warning disable CS1718
                Assert.IsFalse(JsonBoolean.FALSE != JsonBoolean.FALSE);
                Assert.IsFalse(JsonBoolean.TRUE != JsonBoolean.TRUE);
#pragma warning restore CS1718
                Assert.IsTrue(JsonBoolean.FALSE != JsonBoolean.TRUE);
                Assert.IsTrue(JsonBoolean.TRUE != JsonBoolean.FALSE);
            } // end OperatorDisequality()

            [TestMethod]
            public void NullEquality()
            {
                Assert.IsFalse(JsonBoolean.FALSE.Equals(null));
                Assert.IsFalse(JsonBoolean.TRUE.Equals(null));
            } // end NullEquality()

            [TestMethod]
            [DataRow(true, "true")]
            [DataRow(false, "false")]
            public void ToString(bool value, string expectation) => Assert.AreEqual(expectation, ((JsonBoolean)value).ToString());
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
#pragma warning disable CS8625
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJson() => JsonBoolean.ParseJson(null);

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ParseJsonRemainderized() => JsonBoolean.ParseJson(null, out _);
#pragma warning restore CS8625
        } // end inner class Invalid()
    } // end class
} // end namespace