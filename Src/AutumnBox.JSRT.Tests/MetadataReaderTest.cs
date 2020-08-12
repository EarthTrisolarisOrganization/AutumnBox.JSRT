/*

* ==============================================================================
*
* Filename: HeaderReaderTest
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 5:12:37
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.JSRT.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.JSRT.Tests
{
    [TestClass]
    public class MetadataReaderTest
    {
        [TestMethod]
        public void BasicParse()
        {
            string header =
                "// @name test\n" +
                "// @description test desc\n" +
                "// @url https://www.atmb.top\n" +
                "// @opensource https://github.com/zsh2401/AutumnBox \n";
            var reader = new MetadataReader(header);
            Assert.AreEqual("test", reader["name"]);
            Assert.AreEqual("test desc", reader["description"]);
            Assert.AreEqual("https://www.atmb.top", reader["url"]);
            Assert.AreEqual("https://github.com/zsh2401/AutumnBox ", reader["opensource"]);
        }

        [TestMethod]
        public void ParseWithNoise()
        {
            string header =
                "// sadsadasasad \n" +
                "// @name test\n" +
                "// =====AutumnBox Script\n" +
                "// @description test desc\n" +
                "// @url https://www.atmb.top\n" +
                "// @opensource https://github.com/zsh2401/AutumnBox \n";
            var reader = new MetadataReader(header);
            Assert.AreEqual("test", reader["name"]);
            Assert.AreEqual("test desc", reader["description"]);
            Assert.AreEqual("https://www.atmb.top", reader["url"]);
            Assert.AreEqual("https://github.com/zsh2401/AutumnBox ", reader["opensource"]);
        }

        [TestMethod]
        public void InvaildStatement()
        {
            string header =
            "// sadsadasasad \n" +
            "//@name test\n" +
            "// @description test desc\n" +
            "// @url https://www.atmb.top\n" +
            "// @opensource https://github.com/zsh2401/AutumnBox \n";

            var reader = new MetadataReader(header);
            Assert.AreEqual("test desc", reader["description"]);
            Assert.AreEqual("https://www.atmb.top", reader["url"]);
            Assert.AreEqual("https://github.com/zsh2401/AutumnBox ", reader["opensource"]);
            Assert.ThrowsException<KeyNotFoundException>(() => _ = reader["name"]);
        }

        [TestMethod]
        public void WithRealJavaScript()
        {
            string header =
                "// @name test\n" +
                "// @description test desc\n" +
                "// @url https://www.atmb.top\n" +
                "// @opensource https://github.com/zsh2401/AutumnBox \n" +
                "function sayHello(){" +
                "   console.log(\"Hello World!\");" +
                "}" +
                "sayHello()";
            var reader = new MetadataReader(header);

            Assert.AreEqual("test", reader["name"]);
            Assert.AreEqual("test desc", reader["description"]);
            Assert.AreEqual("https://www.atmb.top", reader["url"]);
            Assert.AreEqual("https://github.com/zsh2401/AutumnBox ", reader["opensource"]);
        }
    }
}