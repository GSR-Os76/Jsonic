using GSR.Jsonic;

namespace GSR.Tests.Jsonic
{
    public static class TestJsonNumber
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow("3e4", "3")]
            [DataRow("3", "3")]
            [DataRow("30", "3")]
            [DataRow("4.2e4", "42")]
            [DataRow("7.4560e42", "7456")]
            [DataRow("0", "0")]
            [DataRow("-55", "-55")]
            [DataRow("-9.83", "-983")]
            [DataRow("-77e2", "-77")]
            [DataRow("-6.3e4", "-63")]
            [DataRow("-700e-5", "-7")]
            [DataRow("-390.001", "-390001")]
            [DataRow("0.7e1", "7")]
            [DataRow("-0.7e1", "-7")]
            [DataRow("-0e100", "0")]
            [DataRow("-0e12", "0")]
            public void Significand(string json, string expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).Significand);

            [TestMethod]
            [DataRow("3.2e3", 2)]
            [DataRow("32e2", 2)]
            [DataRow("3.00002e3", -2)]
            [DataRow("47.1000", -1)]
            [DataRow("84", 0)]
            [DataRow("954.0", 0)]
            [DataRow("-3.2e3", 2)]
            [DataRow("-32e2", 2)]
            [DataRow("-3.00002e3", -2)]
            [DataRow("-47.1000", -1)]
            [DataRow("-84", 0)]
            [DataRow("-954.0", 0)]
            [DataRow("0.002", -3)]
            [DataRow("0.1e1", 0)]
            [DataRow("300", 2)]
            [DataRow("1102", 0)]
            [DataRow("0.07000", -2)]
            [DataRow("-0e100", 0)]
            [DataRow("-0e12", 0)]
            [DataRow("-0e-60", 0)]
            [DataRow("0e100", 0)]
            [DataRow("0e12", 0)]
            [DataRow("0e-60", 0)]
            public void Exponent(string json, int expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).Exponent);

            [TestMethod]
            [DataRow("20", (sbyte)20)]
            [DataRow("12e1", (sbyte)120)]
            [DataRow("5.0", (sbyte)5)]
            [DataRow("1.27e2", (sbyte)127)]
            [DataRow("-8", (sbyte)-8)]
            [DataRow("-93e0", (sbyte)-93)]
            [DataRow("-8.0", (sbyte)-8)]
            [DataRow("-1.0e2", (sbyte)-100)]
            public void AsSignedByte(string json, sbyte expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsSignedByte());

            [TestMethod]
            [DataRow("20", (byte)20)]
            [DataRow("12e1", (byte)120)]
            [DataRow("204.0", (byte)204)]
            [DataRow("2.0e2", (byte)200)]
            public void AsByte(string json, byte expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsByte());

            [TestMethod]
            [DataRow("2004", (short)2004)]
            [DataRow("-8", (short)-8)]
            [DataRow("-93e2", (short)-9300)]
            [DataRow("2e3", (short)2000)]
            [DataRow("2004.0", (short)2004)]
            [DataRow("-8.0", (short)-8)]
            [DataRow("-93.0e2", (short)-9300)]
            [DataRow("2.0e3", (short)2000)]
            public void AsShort(string json, short expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsShort());

            [TestMethod]
            [DataRow("2004", (ushort)2004)]
            [DataRow("2e3", (ushort)2000)]
            [DataRow("2004.0", (ushort)2004)]
            [DataRow("2.0e3", (ushort)2000)]
            public void AsUnsignedShort(string json, ushort expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsUnsignedShort());

            [TestMethod]
            [DataRow("2004", 2004)]
            [DataRow("-8", -8)]
            [DataRow("-93e4", -930000)]
            [DataRow("2e3", 2000)]
            [DataRow("2004.0", 2004)]
            [DataRow("-8.0", -8)]
            [DataRow("-93.0e4", -930000)]
            [DataRow("2.0e3", 2000)]
            public void AsInt(string json, int expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsInt());

            [TestMethod]
            [DataRow("2004", (uint)2004)]
            [DataRow("2e3", (uint)2000)]
            [DataRow("2004.0", (uint)2004)]
            [DataRow("2.0e3", (uint)2000)]
            public void AsUnsignedInt(string json, uint expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsUnsignedInt());

            [TestMethod]
            [DataRow("2004", 2004L)]
            [DataRow("-8", -8L)]
            [DataRow("-93e4", -930000L)]
            [DataRow("2e3", 2000L)]
            [DataRow("2004.0", 2004L)]
            [DataRow("-8.0", -8L)]
            [DataRow("-93.0e4", -930000L)]
            [DataRow("2.0e3", 2000L)]
            public void AsLong(string json, long expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsLong());

            [TestMethod]
            [DataRow("2004", (ulong)2004)]
            [DataRow("2e3", (ulong)2000)]
            [DataRow("2004.0", (ulong)2004)]
            [DataRow("2.0e3", (ulong)2000)]
            public void AsUnsignedLong(string json, ulong expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsUnsignedLong());

            [TestMethod]
            [DataRow("2004", 2004f)]
            [DataRow("-8", -8f)]
            [DataRow("-93e4", -930000f)]
            [DataRow("2e3", 2000f)]
            [DataRow("2004.0", 2004f)]
            [DataRow("-8.0", -8f)]
            [DataRow("-93.0e4", -930000f)]
            [DataRow("2.0e3", 2000f)]
            public void AsFloat(string json, float expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsFloat());

            [TestMethod]
            [DataRow("2004", 2004d)]
            [DataRow("-8", -8d)]
            [DataRow("-93e4", -930000d)]
            [DataRow("2e3", 2000d)]
            [DataRow("2004.0", 2004d)]
            [DataRow("-8.0", -8d)]
            [DataRow("-93.0e4", -930000d)]
            [DataRow("2.0e3", 2000d)]
            public void AsDouble(string json, double expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(json).AsDouble());

            [TestMethod]
            [DataRow(0)]
            [DataRow(1)]
            [DataRow(2)]
            [DataRow(3)]
            [DataRow(4)]
            [DataRow(5)]
            [DataRow(6)]
            [DataRow(7)]
            public void AsDecimal(int dataIndex)
            {
                Tuple<decimal, string>[] s = new Tuple<decimal, string>[] {
                    Tuple.Create(2004m, "2004"),
                    Tuple.Create(-8m, "-8"),
                    Tuple.Create(-930000m, "-93e4"),
                    Tuple.Create(2000m, "2e3"),
                    Tuple.Create(2004m, "2004.0"),
                    Tuple.Create(-8m, "-8.0"),
                    Tuple.Create(-930000m, "-93.0e4"),
                    Tuple.Create(2000m, "2.0e3") };

                Assert.AreEqual(s[dataIndex].Item1, JsonNumber.ParseJson(s[dataIndex].Item2).AsDecimal());
            } // end TestAsDecimalValid()

            [TestMethod]
            [DataRow("0", "0", true, false, false, false, false, 0)]
            [DataRow("0", "0", true, true, true, false, false, 0)]
            [DataRow("0e0", "0e0", true, false, true, false, false, 0)]
            [DataRow("-0.8e+2340", "-0.8e+2340", true, false, true, false, false, 0)]
            [DataRow("-0.8e+2340", "-0.8e+2340", true, false, true, false, false, 12)]
            [DataRow("0.8e+2340", "0.8e+2340", true, false, true, false, true, 12)]
            [DataRow("-8e+2", "-800", false, false, true, false, false, 0)]
            [DataRow("-8e+2", "-800", false, false, true, false, false, 0)]
            [DataRow("8e+2", "-800", false, false, true, true, false, -10)]
            [DataRow("80", "0.8E2", false, true, true, false, false, -1)]
            [DataRow("80", "0.8E2", false, true, true, false, false, -3)]
            [DataRow("80", "0.800E2", false, true, true, false, true, -3)]
            [DataRow("80", "80.000e2", false, true, false, false, true, -3)]
            [DataRow("80", "800e-1", false, true, false, false, true, 3)]
            [DataRow("80", "800e-1", false, true, false, false, false, 3)]
            [DataRow("123.4", "123.40", false, false, false, false, true, 2)]
            [DataRow("123.0", "123.00", false, false, false, false, true, 2)]
            [DataRow("123.00", "123.00", false, false, false, false, true, 2)]
            [DataRow("123", "123.00", false, false, false, false, true, 2)]
            [DataRow("167", "167", false, false, false, false, true, 0)]
            [DataRow("17.0", "17", false, false, false, false, true, 0)]
            public void ToString(string json, string expectation, 
                bool preserve, bool placeExponent, bool capitalizeExponent, 
                bool explicitlySignExponent, bool allowInsignificantDigits, int decimalPositioning)
            {
                Assert.AreEqual(expectation, JsonNumber.ParseJson(json).ToString(new(numberFormatting: new(preserve, placeExponent, capitalizeExponent, explicitlySignExponent, allowInsignificantDigits, decimalPositioning))));
            } // end ToString()

            [TestMethod]
            [DataRow("3.2e3", "32e2", true)]
            [DataRow("1", "-1", false)]
            [DataRow("-20", "-0.2e2", true)]
            [DataRow("6.00", "6", true)]
            [DataRow("19e0", "19.00000000000e0", true)]
            [DataRow("5", "5", true)]
            public void HomotypicEquals(string a, string b, bool expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(a).Equals(JsonNumber.ParseJson(b)));
            
            [TestMethod]
            [DataRow("3.2e3", "32e2", false)]
            [DataRow("1", "-1", false)]
            [DataRow("-20", "-0.2e2", false)]
            [DataRow("6.00", "6", false)]
            [DataRow("19e0", "19.00000000000e0", false)]
            [DataRow("5", "5", false)]
            [DataRow("5", (byte)5, true)]
            [DataRow("34352345", 34352345, true)]
            [DataRow("12.444E-2", .12444f, true)]
            [DataRow("12.444E-3", .12444f, false)]
            public void HeterotypicEquals(string a, object b, bool expectation) => Assert.AreEqual(expectation, JsonNumber.ParseJson(a).Equals(b));

#warning heterotypic equality


#warning tostring

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
            public void ParseJson(string json) => JsonNumber.ParseJson(json);
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
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
            public void AsSignedByte(string json) => JsonNumber.ParseJson(json).AsSignedByte();

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
            public void AsByte(string json) => JsonNumber.ParseJson(json).AsByte();

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
            public void AsShort(string json) => JsonNumber.ParseJson(json).AsShort();

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
            public void AsUnsignedShort(string json) => JsonNumber.ParseJson(json).AsUnsignedShort();

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
            public void AsInt(string json) => JsonNumber.ParseJson(json).AsInt();

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
            public void AsUnsignedInt(string json) => JsonNumber.ParseJson(json).AsUnsignedInt();

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
            public void AsLong(string json) => JsonNumber.ParseJson(json).AsLong();

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
            public void AsUnsignedLong(string json) => JsonNumber.ParseJson(json).AsUnsignedLong();

            [TestMethod]
            [ExpectedException(typeof(OverflowException))]
            [DataRow("93e267")]
            [DataRow("2e908")]
            [DataRow("2000000000000040000000000000000000000000000000000000000000000000000")]
            [DataRow("-8000000000000000000000000000000000000000000000000000000000000000000")]
            [DataRow("-93e267")]
            public void AsFloat(string json) => JsonNumber.ParseJson(json).AsFloat();

            [TestMethod]
            [ExpectedException(typeof(OverflowException))]
            [DataRow("93e267")]
            [DataRow("2e908")]
            [DataRow("-93e367")]
            public void AsDouble(string json) => JsonNumber.ParseJson(json).AsDouble();

            [TestMethod]
            [ExpectedException(typeof(OverflowException))]
            [DataRow("93e267")]
            [DataRow("2e908")]
            public void AsDecimal(string json) => JsonNumber.ParseJson(json).AsDecimal();

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
            public void ParseJson(string json) => JsonNumber.ParseJson(json);

#pragma warning disable CS8625
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void TestParseJson() => JsonNumber.ParseJson(null);

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void TestParseJsonRemainderized() => JsonNumber.ParseJson(null, out _);
#pragma warning restore CS8625
        } // end inner class Invalid
    }  // end class
} // end namespace