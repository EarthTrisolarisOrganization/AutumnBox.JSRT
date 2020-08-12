/*

* ==============================================================================
*
* Filename: EScriptManager
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 3:05:45
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.JSRT.User
{
    [ExtName("脚本管理器")]
    [ExtAuth("zsh2401")]
    [ExtMinApi(11)]
    [ExtIcon("Resources.jsrtm.png")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtPriority(int.MaxValue)]
    public class EScriptManager : LeafExtensionBase
    {
        [LMain]
        public void Main(IUx ux)
        {
            ux.Message(TextCarrier.Reader.RxGet("not_done"));
        }
    }
}
