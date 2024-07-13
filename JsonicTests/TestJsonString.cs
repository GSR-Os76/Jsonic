﻿using GSR.Jsonic;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace GSR.Tests.Jsonic
{
    [TestClass]
    public class TestJsonString
    {
        #region enquoted
        [TestMethod]
        [DataRow("\"\"")]
        [DataRow("\"\\\"\"")]
        [DataRow("\"\\\\\"")]
        [DataRow("\"\\/\"")]
        [DataRow("\"\\b\"")]
        [DataRow("\"\\f\"")]
        [DataRow("\"\\n\"")]
        [DataRow("\"\\r\"")]
        [DataRow("\"\\t\"")]
        [DataRow("\"\\u0000\"")]
        [DataRow("\"\\u9999\"")]
        [DataRow("\"\\u6038\"")]
        [DataRow("\"\\u1234\"")]
        [DataRow("\"\\uffff\"")]
        [DataRow("\"\\uaaaa\"")]
        [DataRow("\"\\uA6f9\"")]
        [DataRow("\"\\uaB7C\"")]
        [DataRow("\"\\u463D\"")]
        [DataRow("\"\\uABCD\"")]
        [DataRow("\"/\"")]
        [DataRow("\"Just some test\"")]
        [DataRow("\"-==fjeiwo8njuvg\"")]
        [DataRow("\"b\"")]
        [DataRow("\"f\"")]
        [DataRow("\"u98A3\"")]
        [DataRow("\"n\"")]
        public void TestValidEnquoted(string s)
        {
            new JsonString(s, true);
            Assert.IsTrue(true);
        }// end TestValidEnquoted()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("")]
        [DataRow("mkjw;rw\"")]
        [DataRow("\"sfakl")]
        [DataRow("\"")]
        [DataRow("\"\"\"")]
        [DataRow("\"\\\"")]
        [DataRow("\"\\]\"")]
        [DataRow("\"\\y\"")]
        [DataRow("\"\\U78A3\"")]
        [DataRow("\"\\U78A\"")]
        [DataRow("\"\\U734F8A3\"")]
        [DataRow("\"\\U04-=\"")]
        [DataRow("\"\\u000-\"")]
        [DataRow("\"\\uL\"")]
        [DataRow("\"\\u\"")]
        [DataRow("\"\\u8A3\"")]
        [DataRow("\"\\8F3\"")]
        public void TestInvalidEnqouted(string s)
        {
            new JsonString(s, true);
        }// end TestInvalidEnqouted()

        [TestMethod]
        [DataRow("\"\"", "")]
        [DataRow("\"fd\"", "fd")]
        [DataRow("\"\\\"\"", "\"")]
        [DataRow("\"\\\\\"", "\\")]
        [DataRow("\"\\/\"", "/")]
        [DataRow("\"\\b\"", "\b")]
        [DataRow("\"\\f\"", "\f")]
        [DataRow("\"\\n\"", "\n")]
        [DataRow("\"\\r\"", "\r")]
        [DataRow("\"\\t\"", "\t")]
        [DataRow("\"\\u0021\"", "!")]
        [DataRow("\"\\u00A9\"", "©")]
        [DataRow("\"\\u0152\"", "Œ")]
        [DataRow("\"\\u0489\"", "҉")]
        [DataRow("\":erwojofe\"", ":erwojofe")]
        [DataRow("\"\\\\ge4\\b\"", "\\ge4\b")]
        [DataRow("\"\\\\\\b\"", "\\\b")]
        public void TestToRepresentedStringEnquoted(string a, string b)
        {
            Assert.AreEqual(b, new JsonString(a, true).ToRepresentedString());
        }// end TestToRepresentedStringEnquoted()
#endregion



        [TestMethod]
        [DataRow("", "")]
        [DataRow("\"", "\\\"")]
        [DataRow("\"\"", "\\\"\\\"")]
        [DataRow("A3d_", "A3d_")]
        [DataRow("\t", "\t")]
        [DataRow("\\f", "\\\\f")]
        [DataRow("\t Hi there, \n\rhow're you? \t", "\t Hi there, \n\rhow're you? \t")]
        [DataRow("j\\bS", "j\\\\bS")]
        public void TestParse(string a, string b)
        {
            Assert.AreEqual(b, JsonString.Parse(a).Value);
        }// end TestParse()

        [TestMethod]
        [DataRow("", "\"\"")]
        [DataRow("\"", "\"\\\"\"")]
        [DataRow("\"\"", "\"\\\"\\\"\"")]
        [DataRow("A3d_", "\"A3d_\"")]
        [DataRow("\t", "\"\t\"")]
        [DataRow("\\f", "\"\\\\f\"")]
        [DataRow("\t Hi there, \n\rhow're you? \t", "\"\t Hi there, \n\rhow're you? \t\"")]
        [DataRow("j\\bS", "\"j\\\\bS\"")]
        public void TestParseToString(string a, string b)
        {
            Assert.AreEqual(b, JsonString.Parse(a).ToString());
            Assert.AreEqual(b, JsonString.Parse(a).ToCompressedString());
        }// end TestParseToString()

    } // end class
} // end namespace