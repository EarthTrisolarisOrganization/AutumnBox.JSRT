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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.JSRT.Tests.JSModule
{
    [TestClass]
    public class ESTest : JSModuleTestBase
    {
        [TestMethod]
        public void ConstMethodIsNotGlobal()
        {
            using var module = NewJSModule();
            var task = module.CallMethod("f");
            Assert.ThrowsException<AggregateException>(() => task.Wait());
            Assert.ThrowsException<AggregateException>(() => task.Result);
            //Assert.AreEqual(10, task.Result);
        }
    }
}
