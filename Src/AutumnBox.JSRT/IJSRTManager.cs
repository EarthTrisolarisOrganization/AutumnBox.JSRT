/*

* ==============================================================================
*
* Filename: IJSRTManager
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 2:55:19
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;

namespace AutumnBox.JSRT
{
    public interface IJSRTManager : IDisposable
    {
        void Initialize();
        IEnumerable<IJSModule> Loaded { get; }
        IJSModule Load(string jsCode);
        void Unload(IJSModule module);
    }
}
