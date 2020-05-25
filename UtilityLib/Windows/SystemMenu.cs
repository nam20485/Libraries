using System;
using System.Windows.Forms;
using Libraries.UtilityLib.Windows;
//using static Libraries.UtilityLib.Windows.Win32API;

namespace Libraries.UtilityLib.Windows
{
    public static class SystemMenu
    {
        public static bool AddMenuItem(IntPtr hWnd, Win32API.MenuItemInfoType uFlags, uint uIDNewItem, string lpNewItem)
        {
            var hSysMenu = Win32API.GetSystemMenu(hWnd, false);
            if (hSysMenu != IntPtr.Zero)
            {
                return Win32API.AppendMenu(hSysMenu, uFlags, uIDNewItem, lpNewItem);
            }

            return false;
        }

        public static bool AddSystemMenuItem(this Form form, Win32API.MenuItemInfoType uFlags, uint uIDNewItem, string lpNewItem)
        { 
            return AddMenuItem(form.Handle, uFlags, uIDNewItem, lpNewItem);
        }
    }
}
