/*

* ==============================================================================
*
* Filename: JSRTManagerImpl
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 3:22:50
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.JSRT.Impl
{
    public class JSRTManagerImpl : IJSRTManager
    {
        public IEnumerable<IJSModule> Loaded => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public IJSModule Load(string jsCode)
        {
            throw new NotImplementedException();
        }

        public void Unload(IJSModule module)
        {
            throw new NotImplementedException();
        }
    }
}
