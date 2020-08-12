/*

* ==============================================================================
*
* Filename: JSModuleTest
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 5:40:14
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using Microsoft.ClearScript;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AutumnBox.JSRT.Tests.JSModule
{
    [TestClass]
    public class JSModuleTest
    {

        [TestMethod]
        private Impl.JSModule NewHelloWorldMoule()
        {
            var text = File.ReadAllText("JSModule/Scripts/JSModuleTest.js");
            return new Impl.JSModule(text);
        }

        [TestMethod]
        public void MetadataTest()
        {
            using var module = NewHelloWorldMoule();
            Assert.AreEqual("zsh2401@163.com", module.Author);
            Assert.AreEqual("Hello World", module.Name);
        }

        [TestMethod]
        public void GlobalVar()
        {
            using var module = NewHelloWorldMoule();

            module.SetOrCreateGlobalVariable("a", 2401);
            Assert.AreEqual(2401, (int)module.GetGlobalVariable("a"));

            module.SetOrCreateGlobalVariable("a", 2402);
            Assert.AreEqual(2402, (int)module.GetGlobalVariable("a"));

            Assert.ThrowsException<InvalidOperationException>(() => module.GetGlobalVariable("b"));

            Assert.AreEqual(2402, (int)module.GetGlobalVariable("a"));
        }

        [TestMethod]
        public void RunMain()
        {
            using var moudle = NewHelloWorldMoule();
            var task = moudle.Start();
            task.Wait();
            Assert.AreEqual(2401, task.Result);
        }
        [TestMethod]
        public void EventTest() {
            using var moudle = NewHelloWorldMoule();
            var task = moudle.RaiseEvent("b",1,2,3);
            task.Wait();
            Assert.AreEqual("b-4",task.Result);
        }
    }
}
