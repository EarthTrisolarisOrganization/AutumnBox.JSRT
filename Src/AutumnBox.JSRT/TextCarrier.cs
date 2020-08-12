/*

* ==============================================================================
*
* Filename: TextCarrier
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 3:27:58
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Enhancement.ClassTextKit;

namespace AutumnBox.JSRT
{
    [ClassText("not_done","This function is still developing.","zh-cn:此功能仍在开发中")]
    class TextCarrier
    {
        public static ClassTextReader Reader { get; }
        static TextCarrier()
        {
            Reader = ClassTextReaderCache.Acquire<TextCarrier>();
        }
        private TextCarrier() { }
    }
}
