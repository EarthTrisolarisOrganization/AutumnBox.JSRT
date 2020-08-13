/*

* ==============================================================================
*
* Filename: EventTest
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 7:54:11
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.JSRT.Tests.JSModule
{
    [TestClass]
    public class AsyncTest : JSModuleTestBase
    {
        [TestMethod]
        public void FuncAsync()
        {
            var script = "async function f(){return await 2401}";
            using var module = NewJSModuleWithScript(script);
            module.InvokeAsync("f").ContinueWith(task =>
            {
                Assert.AreEqual(2401, task.Result);
            }).Wait();
        }
        [TestMethod]
        public void FuncExceptionAsync()
        {
            var script = "async function f(){throw \"ERROR\"; return await 2401}";
            using var module = NewJSModuleWithScript(script);
            module.InvokeAsync("f").ContinueWith(task =>
            {
                Assert.IsTrue(task.IsFaulted);
                Assert.AreEqual("ERROR", task.Exception.InnerException.Message);
            }).Wait();
        }

        [TestMethod]
        public void EventAsync()
        {
            using var module = NewJSModuleWithName("EventAsyncTest");
            module.RaiseEventAsync("destory").ContinueWith(task =>
            {
                Assert.AreEqual("destory-0", task.Result);
            }).Wait();
        }

        [TestMethod]
        public void EventSync()
        {
            using var module = NewJSModuleWithName("EventSyncTest");
            module.RaiseEventAsync("destory", 1, 2, 3, 4).ContinueWith(task =>
            {
                Assert.AreEqual("destory-4", task.Result);
            }).Wait();
        }
    }
}
