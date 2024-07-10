using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonNumber
    {
        [TestMethod]
        [DataRow("45")]
        [DataRow("99999999999999999999999999999999999999999999999999999")]
        [DataRow("1")]
        [DataRow("4000")]
        [DataRow("5.8099230")]
        [DataRow("3.0")]
        [DataRow("3.0e0")]
        [DataRow("3.0E-0")]
        [DataRow("3.4e+09")]
        [DataRow("567.3302480e-12")]
        [DataRow("90e2")]
        [DataRow("-10")]
        [DataRow("-2")]
        [DataRow("-9.4")]
        [DataRow("-36.0")]
        [DataRow("-8E2")]
        [DataRow("-3e-2")]
        [DataRow("-9e+89584")]
        [DataRow("-3e-2")]
        [DataRow("-3e+03")]
        [DataRow("-4.2e-8")]
        public void TestValid(string s)
        {
            new JsonNumber(s);
            Assert.IsTrue(true);
        }// end TestValid

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("00")]
        [DataRow("+0")]
        [DataRow("098")]
        [DataRow("-0873")]
        [DataRow("+1")]
        [DataRow("E4")]
        [DataRow(".67")]
        [DataRow("l")]
        [DataRow("")]
        [DataRow("0 .67")]
        [DataRow("23.11E 0")]
        [DataRow("1e-")]
        [DataRow("320l")]
        [DataRow("-E+3")]


        public void TestInvalid(string s)
        {
            new JsonNumber(s);
        }// end TestValid


    }  // end class
} // end namespace