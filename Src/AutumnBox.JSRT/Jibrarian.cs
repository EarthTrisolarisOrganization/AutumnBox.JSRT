
/*
Copyright (c) 2020 EarthTrisolarisOrganization
AutumnBox.JSRT is licensed under Mulan PSL v2.
You can use this software according to the terms and conditions of the Mulan PSL v2.
You may obtain a copy of Mulan PSL v2 at:
         http://license.coscl.org.cn/MulanPSL2
THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND,
EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT,
MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
See the Mulan PSL v2 for more details.
*/

/*

* ==============================================================================
*
* Filename: Jibrarian
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 3:10:06
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.JSRT.Impl;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutumnBox.JSRT
{
    class Jibrarian : AssemblyLibrarian
    {
        const string STORAGE_ID = "AutumnBox.JSRT_STORAGE";

        public static Jibrarian Instance { get; private set; }

        public override string Name => "AutumnBox JSRT Manager";

        public override int MinApiLevel => 11;

        public override int TargetApiLevel => 11;

        public IStorage Storage { get; private set; }
#pragma warning disable IDE0044 // 添加只读修饰符
#pragma warning disable IDE0051 // 删除未使用的私有成员
        [AutoInject]
        public IRegisterableLake RLake { get; private set; }

        [AutoInject]
        public IAppManager AppManager { get; private set; }

        [AutoInject]
        public ILibsManager LibsManager { get; private set; }

        [AutoInject] IStorageManager storageManager;

#pragma warning restore IDE0051 // 删除未使用的私有成员
#pragma warning restore IDE0044 // 添加只读修饰符

        public List<IJSModule> Modules { get; } = new List<IJSModule>();
        public override void Ready()
        {
            base.Ready();
            SLogger<Jibrarian>.Info("Ready");
            Instance = this;
            this.Storage = storageManager.Open(STORAGE_ID);
            StartJSRTService();
            Regiser(new JSModule("" +
                "// @name Test\n" +
                "// @author zsh2401\n" +
                "function atmbMain(){ throw \"ERROR\" }"));
        }
        public void Regiser(IJSModule module)
        {
            LibsManager.Registry.Add(new JSModuleExtensionInfo(module));
        }
        public void Unregister(IJSModule module)
        {
            var targetToRemove = LibsManager.Registry.Where(r => r.Id == module.Id).FirstOrDefault();
            LibsManager.Registry.Remove(targetToRemove);
        }
        void StartJSRTService() { }
        void FreeJSRTService() { }
        public override void Destory()
        {
            base.Destory();
            this.Storage.ClearCache();
            Modules.ForEach(m => m.RaiseEventAsync("destory"));
            FreeJSRTService();
        }
    }
}
