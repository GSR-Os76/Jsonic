using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonNumber
    {
        #region constructor tests
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
        public void TestValidConstruct(string s)
        {
            new JsonNumber(s);
            Assert.IsTrue(true);
        }// end TestValidConstruct()

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
        public void TestInvalidConstruct(string s)
        {
            new JsonNumber(s);
        }// end TestInvalidConstruct()
        #endregion



        #region sbyte tests
        [TestMethod]
        [DataRow("20", (sbyte)20)]
        [DataRow("12e1", (sbyte)120)]
        [DataRow("5.0", (sbyte)5)]
        [DataRow("1.27e2", (sbyte)127)]
        [DataRow("-8", (sbyte)-8)]
        [DataRow("-93e0", (sbyte)-93)]
        [DataRow("-8.0", (sbyte)-8)]
        [DataRow("-1.0e2", (sbyte)-100)]
        public void TestAsSByteValid(string s, sbyte e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsSignedByte());
        } //  end TestAsSByteValid()

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [DataRow("200000000000004")]
        [DataRow("-80000000000000000000")]
        [DataRow("-93e16")]
        [DataRow("2e908")]
        [DataRow("0.48e-5")]
        [DataRow("-90e-3")]
        [DataRow("-4.5")]
        [DataRow("-0.2")]
        public void TestAsSByteInvalid(string s)
        {
            new JsonNumber(s).AsSignedByte();
        } //  end TestAsByteInvalid()
        #endregion

        #region byte tests
        [TestMethod]
        [DataRow("20", (byte)20)]
        [DataRow("12e1", (byte)120)]
        [DataRow("204.0", (byte)204)]
        [DataRow("2.0e2", (byte)200)]
        public void TestAsByteValid(string s, byte e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsByte());
        } //  end TestAsByteValid()

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [DataRow("-8")]
        [DataRow("-93e4")]
        [DataRow("-8.0")]
        [DataRow("-93.0e4")]
        [DataRow("200000000000004")]
        [DataRow("-80000000000000000000")]
        [DataRow("-93e16")]
        [DataRow("2e908")]
        [DataRow("0.48e-5")]
        [DataRow("-90e-3")]
        [DataRow("-4.5")]
        [DataRow("-0.2")]
        public void TestAsByteInvalid(string s)
        {
            new JsonNumber(s).AsByte();
        } //  end TestAsByteInvalid()
        #endregion

        #region short tests
        [TestMethod]
        [DataRow("2004", (short)2004)]
        [DataRow("-8", (short)-8)]
        [DataRow("-93e2", (short)-9300)]
        [DataRow("2e3", (short)2000)]
        [DataRow("2004.0", (short)2004)]
        [DataRow("-8.0", (short)-8)]
        [DataRow("-93.0e2", (short)-9300)]
        [DataRow("2.0e3", (short)2000)]
        public void TestAsShortValid(string s, short e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsShort());
        } //  end TestAsShortValid()

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [DataRow("200000000000004")]
        [DataRow("-80000000000000000000")]
        [DataRow("-93e16")]
        [DataRow("2e908")]
        [DataRow("0.48e-5")]
        [DataRow("-90e-3")]
        [DataRow("-4.5")]
        [DataRow("-0.2")]
        public void TestAsShortInvalid(string s)
        {
            new JsonNumber(s).AsShort();
        } //  end TestAsShortInvalid()
        #endregion

        #region ushort tests
        [TestMethod]
        [DataRow("2004", (ushort)2004)]
        [DataRow("2e3", (ushort)2000)]
        [DataRow("2004.0", (ushort)2004)]
        [DataRow("2.0e3", (ushort)2000)]
        public void TestAsUShortValid(string s, ushort e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsUnsignedShort());
        } //  end TestAsUShortValid()

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [DataRow("-8")]
        [DataRow("-93e4")]
        [DataRow("-8.0")]
        [DataRow("-93.0e4")]
        [DataRow("200000000000004")]
        [DataRow("-80000000000000000000")]
        [DataRow("-93e16")]
        [DataRow("2e908")]
        [DataRow("0.48e-5")]
        [DataRow("-90e-3")]
        [DataRow("-4.5")]
        [DataRow("-0.2")]
        public void TestAsUShortInvalid(string s)
        {
            new JsonNumber(s).AsUnsignedShort();
        } //  end TestAsUShortInvalid()
        #endregion

        #region int tests
        [TestMethod]
        [DataRow("2004", 2004)]
        [DataRow("-8", -8)]
        [DataRow("-93e4", -930000)]
        [DataRow("2e3", 2000)]
        [DataRow("2004.0", 2004)]
        [DataRow("-8.0", -8)]
        [DataRow("-93.0e4", -930000)]
        [DataRow("2.0e3", 2000)]
        public void TestAsIntValid(string s, int e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsInt());
        } //  end TestAsIntValid()

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [DataRow("200000000000004")]
        [DataRow("-80000000000000000000")]
        [DataRow("-93e16")]
        [DataRow("2e908")]
        [DataRow("0.48e-5")]
        [DataRow("-90e-3")]
        [DataRow("-4.5")]
        [DataRow("-0.2")]
        public void TestAsIntInvalid(string s)
        {
            new JsonNumber(s).AsInt();
        } //  end TestAsIntInvalid()
        #endregion

        #region uint tests
        [TestMethod]
        [DataRow("2004", (uint)2004)]
        [DataRow("2e3", (uint)2000)]
        [DataRow("2004.0", (uint)2004)]
        [DataRow("2.0e3", (uint)2000)]
        public void TestAsUIntValid(string s, uint e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsUnsignedInt());
        } //  end TestAsUIntValid()

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [DataRow("-8")]
        [DataRow("-93e4")]
        [DataRow("-8.0")]
        [DataRow("-93.0e4")]
        [DataRow("200000000000004")]
        [DataRow("-80000000000000000000")]
        [DataRow("-93e16")]
        [DataRow("2e908")]
        [DataRow("0.48e-5")]
        [DataRow("-90e-3")]
        [DataRow("-4.5")]
        [DataRow("-0.2")]
        public void TestAsUIntInvalid(string s)
        {
            new JsonNumber(s).AsUnsignedInt();
        } //  end TestAsUIntInvalid()
        #endregion

        #region long tests
        [TestMethod]
        [DataRow("2004", 2004l)]
        [DataRow("-8", -8l)]
        [DataRow("-93e4", -930000l)]
        [DataRow("2e3", 2000l)]
        [DataRow("2004.0", 2004l)]
        [DataRow("-8.0", -8l)]
        [DataRow("-93.0e4", -930000l)]
        [DataRow("2.0e3", 2000l)]
        public void TestAsLongValid(string s, long e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsLong());
        } //  end TestAsLongValid()

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [DataRow("200000000000004000000")]
        [DataRow("-80000000000000000000")]
        [DataRow("-93e26")]
        [DataRow("2e908")]
        [DataRow("0.48e-5")]
        [DataRow("-90e-3")]
        [DataRow("-4.5")]
        [DataRow("-0.2")]
        public void TestAsLongInvalid(string s)
        {
            new JsonNumber(s).AsLong();
        } //  end TestAsLongInvalid()
        #endregion

        #region ulong tests
        [TestMethod]
        [DataRow("2004", (ulong)2004)]
        [DataRow("2e3", (ulong)2000)]
        [DataRow("2004.0", (ulong)2004)]
        [DataRow("2.0e3", (ulong)2000)]
        public void TestAsULongValid(string s, ulong e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsUnsignedLong());
        } //  end TestAsULongValid()

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [DataRow("-8")]
        [DataRow("-93e4")]
        [DataRow("-8.0")]
        [DataRow("-93.0e4")]
        [DataRow("200000000000004000000")]
        [DataRow("-80000000000000000000")]
        [DataRow("-93e26")]
        [DataRow("2e908")]
        [DataRow("0.48e-5")]
        [DataRow("-90e-3")]
        [DataRow("-4.5")]
        [DataRow("-0.2")]
        public void TestAsULongInvalid(string s)
        {
            new JsonNumber(s).AsUnsignedLong();
        } //  end TestAsULongInvalid()
        #endregion



        #region float tests
        [TestMethod]
        [DataRow("2004", 2004f)]
        [DataRow("-8", -8f)]
        [DataRow("-93e4", -930000f)]
        [DataRow("2e3", 2000f)]
        [DataRow("2004.0", 2004f)]
        [DataRow("-8.0", -8f)]
        [DataRow("-93.0e4", -930000f)]
        [DataRow("2.0e3", 2000f)]
        [DataRow("2000000000000040000000000000000000000000000000000000000000000000000", float.PositiveInfinity)]
        [DataRow("-8000000000000000000000000000000000000000000000000000000000000000000", float.NegativeInfinity)]
        [DataRow("-93e267", float.NegativeInfinity)]
        [DataRow("2e908", float.PositiveInfinity)]
        public void TestAsFloatValid(string s, float e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsFloat());
        } //  end TestAsFloatValid()
        #endregion

        #region double tests
        [TestMethod]
        [DataRow("2004", 2004d)]
        [DataRow("-8", -8d)]
        [DataRow("-93e4", -930000d)]
        [DataRow("2e3", 2000d)]
        [DataRow("2004.0", 2004d)]
        [DataRow("-8.0", -8d)]
        [DataRow("-93.0e4", -930000d)]
        [DataRow("2.0e3", 2000d)]
        [DataRow("-93e367", double.NegativeInfinity)]
        [DataRow("2e908", double.PositiveInfinity)]
        public void TestAsDoubleValid(string s, double e)
        {
            Assert.AreEqual(e, new JsonNumber(s).AsDouble());
        } //  end TestAsDoubleValid()
        #endregion

        #region double tests
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        public void TestAsDecimalValid(int i)
        {
            Tuple<decimal, string>[] s = new Tuple<decimal, string>[] {Tuple.Create(2004m, "2004"), Tuple.Create(-8m, "-8"), Tuple.Create(-930000m, "-93e4") , Tuple.Create(2000m, "2e3") , Tuple.Create(2004m, "2004.0"), Tuple.Create(-8m, "-8.0"), Tuple.Create(-930000m, "-93.0e4"), Tuple.Create(2000m, "2.0e3") };

            Assert.AreEqual(s[i].Item1, new JsonNumber(s[i].Item2).AsDecimal());
        } //  end TestAsDecimalValid()

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [DataRow(0)]
        [DataRow(1)]
        public void TestAsDecimalInvalid(int i)
        {
            string[] s = new string[] { "-93e267", "2e908" };
            new JsonNumber(s[i]).AsDecimal();
        } //  end TestAsDecimalInvalid()
        #endregion

    }  // end class
} // end namespace