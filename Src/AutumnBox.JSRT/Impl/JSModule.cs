/*

* ==============================================================================
*
* Filename: JSModule
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 5:30:09
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.JSRT.Util;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.JSRT.Impl
{
    public class JSModule : IJSModule
    {
        public const string EVENT_HANDLER_NAME = "atmbEventHandler";
        public const string MAIN_METHOD = "atmbMain";

        private readonly string js;
        public string Name => metadata["name"];

        public string Author => metadata["author"];

        public string Id => metadata["id"];

        public JSModuleStatus Status { get; private set; } = JSModuleStatus.Idle;

        public readonly V8ScriptEngine engine;
        private readonly MetadataReader metadata;

        public JSModule(string js)
        {
            this.js = js ?? throw new ArgumentNullException(nameof(js));
            metadata = new MetadataReader(js);
            engine = new V8ScriptEngine();
            InitializeJSEngineEnv();
            _ = engine.Script;
            engine.Evaluate(js);
        }

        private void InitializeJSEngineEnv()
        {
            engine.AddHostType(typeof(Console));
        }

        public ValueType GetGlobalVariable(string name)
        {
            try
            {
                return (ValueType)engine.Evaluate($"{name}");
            }
            catch (ScriptEngineException e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public void SetOrCreateGlobalVariable(string name, ValueType value)
        {
            string script = "try{" + name + " = " + value + " }catch(e){ var " + name + " = " + value + " }";
            engine.Evaluate(script);
        }

        public Task<object> CallMethod(string methodName, params object[] args)
        {
            return Task.Run(() => engine.Invoke(methodName, args));
        }

        public void Dispose()
        {
            RaiseEvent("dispose").ContinueWith((task) =>
            {
                engine.Dispose();
            });
        }

        public Task<object> RaiseEvent(string eventId, params object[] args)
        {
            return CallMethod(EVENT_HANDLER_NAME, new object[] { eventId }.Concat(args).ToArray());
        }

        public Task<object> Start()
        {
            return CallMethod(MAIN_METHOD);
        }
    }
}
