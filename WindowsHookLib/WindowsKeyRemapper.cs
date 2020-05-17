using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libraries.UtilityLib.Keys;

namespace Libraries.WindowsHookLib
{
    public class WindowsKeyRemapper : KeyRemapper
    {
        public WindowsKeyRemapper(KeyPress substitute)
        {            
            this[substitute] = KeyPress.LEFT_WINDOWS_KEY;
        }
    }
}
