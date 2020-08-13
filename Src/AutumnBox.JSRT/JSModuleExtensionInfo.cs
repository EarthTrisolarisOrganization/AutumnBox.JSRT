/*

* ==============================================================================
*
* Filename: JSModuleExtensionInfo
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 8:46:14
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AutumnBox.JSRT
{
    class JSModuleExtensionInfo : IExtensionInfo
    {
        private readonly IJSModule module;

        public string Id => module.Id;

        public IReadOnlyDictionary<string, ValueReader> Metadata { get; }

        public JSModuleExtensionInfo(IJSModule module)
        {
            this.module = module ?? throw new ArgumentNullException(nameof(module));
            var tmpDic = new Dictionary<string, ValueReader>();
            foreach (var kv in module.Metadata)
            {
                tmpDic.Add(kv.Key, () => kv.Value);
            }
            Metadata = tmpDic;
        }

        public bool Equals([AllowNull] IExtensionInfo other)
        {
            if (other is JSModuleExtensionInfo extInf)
            {
                return extInf.Id == this.Id;
            }
            return false;
        }

        public IExtensionProcedure OpenProcedure()
        {
            return new JSModuleProcedure(module);
        }

        private class JSModuleProcedure : IExtensionProcedure
        {
            private readonly IJSModule module;

            public ILake? Source { get; set; }

            public Dictionary<string, object>? Args { get; set; }

            public JSModuleProcedure(IJSModule module)
            {
                this.module = module ?? throw new ArgumentNullException(nameof(module));
            }

            public void Dispose()
            {
                module.RaiseEventAsync("status_change", "idle");
            }

            public object? Run()
            {
                module.RaiseEventAsync("status_change", "busy");
                module.RaiseEventAsync("args_map_received", Args ?? new Dictionary<string, object>());
                module.RaiseEventAsync("lake_received", Source ?? LakeProvider.Lake);
                var task = module.StartAsync();
                task.Wait();
                return task.Result;
            }
        }
    }
}
