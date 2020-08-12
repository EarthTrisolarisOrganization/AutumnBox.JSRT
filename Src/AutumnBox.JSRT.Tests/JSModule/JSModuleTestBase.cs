/*

* ==============================================================================
*
* Filename: JSModuleTestBase
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 6:30:33
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/


using System.IO;

namespace AutumnBox.JSRT.Tests.JSModule
{
    public class JSModuleTestBase
    {
        protected Impl.JSModule NewJSModule()
        {
            var name = this.GetType().Name;
            var script = File.ReadAllText($"JSModule/Scripts/{name}.js");
            return new Impl.JSModule(script);
        }
    }
}
