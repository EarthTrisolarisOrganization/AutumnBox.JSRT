/*

* ==============================================================================
*
* Filename: ESTest
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 6:28:49
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using Microsoft.ClearScript.JavaScript;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.JSRT.Tests.JSModule
{
    [TestClass]
    public class ESTest : JSModuleTestBase
    {
        [TestMethod]
        public void ConstMethodIsNotGlobal()
        {
            using var module = NewJSModule();
            module.InvokeAsync("f").ContinueWith(task =>
            {
                Assert.IsTrue(task.IsFaulted);
                Trace.WriteLine(task.Exception);
            }).Wait();
        }

        [TestMethod]
        public void IsSupportPromise()
        {
            using var module = NewJSModule();

            module.GetGlobalVariable("promise").ToTask().ContinueWith(task =>
            {
                Assert.AreEqual(2401, task.Result);
            }).Wait();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                Task<object> promise = module.GetGlobalVariable("notPromise").ToTask();
            }, "The object is not a promise.");
        }

        [TestMethod]
        public void AsyncResult()
        {
            using var module = NewJSModule();
            module.InvokeAsync("after").ContinueWith(task =>
            {
                Assert.AreEqual("c", task.Result);
            }).Wait();
        }
    }
}
