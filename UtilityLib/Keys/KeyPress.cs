using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libraries.UtilityLib.Windows;

namespace Libraries.UtilityLib.Keys
{
    public class KeyPress
    {
        public static readonly KeyPress LEFT_WINDOWS_KEY = new KeyPress(Win32API.VirtualKey.LWIN, Win32API.ScanCode.LWIN, true);
        public static readonly KeyPress LOGITECH_REWIND_KEY = new KeyPress(Win32API.VirtualKey.LEFT, Win32API.ScanCode.NUMPAD4, true);
        public static readonly KeyPress SCROLL_LOCK_KEY = new KeyPress(Win32API.VirtualKey.SCROLL, Win32API.ScanCode.CANCEL, false);
        public static readonly KeyPress LEFT_CONTROL_KEY = new KeyPress(Win32API.VirtualKey.LCONTROL, Win32API.ScanCode.LCONTROL, false);
        public static readonly KeyPress LOGITECH_MENU_KEY = new KeyPress(Win32API.VirtualKey.F1, Win32API.ScanCode.F1, false);

        public Win32API.VirtualKey VkCode   { get; private set; }
        public Win32API.ScanCode ScanCode   { get; private set; }
        public bool Extended                { get; private set; }

        public KeyPress(Win32API.VirtualKey vkCode, Win32API.ScanCode scanCode, bool extended)
        {
            VkCode = vkCode;
            ScanCode = scanCode;
            Extended = extended;
        }              
    }
}
