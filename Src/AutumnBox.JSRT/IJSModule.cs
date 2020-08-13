/*

* ==============================================================================
*
* Filename: IJSModule
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 2:56:49
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Threading.Tasks;

namespace AutumnBox.JSRT
{
    public interface IJSModule : IDisposable
    {
        string Name { get; }
        string Author { get; }
        string Id { get; }
        JSModuleStatus Status { get; }
        Task<object> StartAsync();
        Task<object> InvokeAsync(string methodName, params object[] args);
        Task<object> RaiseEventAsync(string eventId, params object[] args);
    }
}
