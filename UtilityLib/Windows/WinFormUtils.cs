using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Libraries.UtilityLib.Windows
{
    public static class WinFormUtils
    {
        public static void ShowCaption(IntPtr hForm, bool show)
        {
            Win32API.WindowStyles style = (Win32API.WindowStyles)
                Win32API.GetWindowLongPtr(hForm, Win32API.GetWindowLongs.GWL_STYLE).ToInt32();

            if (show)
            {
                style |= Win32API.WindowStyles.WS_CAPTION;
            }
            else
            {
                style &= ~Win32API.WindowStyles.WS_CAPTION;
            }
            Win32API.SetWindowLongPtr(hForm, Win32API.GetWindowLongs.GWL_STYLE, (int) style);
        }

        public static bool IsCaptionShowing(IntPtr hForm)
        {
            Win32API.WindowStyles style = (Win32API.WindowStyles) 
                Win32API.GetWindowLongPtr(hForm, Win32API.GetWindowLongs.GWL_STYLE).ToInt32();

            return (style.HasFlag(Win32API.WindowStyles.WS_CAPTION));
        }

        public static string GetEntryAssemblyFilename(bool withExtension)
        {
            string filename = string.Empty;

            Assembly assembly = Assembly.GetEntryAssembly();
            if (assembly != null && assembly.Location != null)
            {
                if (withExtension)
                {
                    filename = Path.GetFileName(assembly.Location);
                }
                else
                {
                    filename = Path.GetFileNameWithoutExtension(assembly.Location);
                }
            }

            return filename;
        }
    }
}
