using System;
using Libraries.UtilityLib.Windows;
using Libraries.UtilityLib.Disposeable;

namespace Libraries.UtilityLib.Windows
{
    public class RegKey : NativeResource
    {
        public IntPtr Handle { get; protected set; }

        public IntPtr HRootKey { get; protected set; }
        public string SubKey { get; protected set; }

        public RegKey(IntPtr hRootKey, string subKey)
        {
            HRootKey = hRootKey;
            SubKey = subKey;
        }

        public void Close()
        {
            CloseKey(Handle);
            Handle = IntPtr.Zero;
            Resource = IntPtr.Zero;
        }

        public void Open(Win32API.RegSAM samDesired)
        {
            if (Handle == IntPtr.Zero)
            {
                Handle = OpenSubKey(HRootKey, SubKey, samDesired);

                // for disposal later
                Resource = Handle;
            }
        }

        public static RegKey Open(IntPtr hRootKey, string subKey, Win32API.RegSAM samDesired)
        {
            var regKey = new RegKey(hRootKey, subKey);
            regKey.Open(samDesired);
            return regKey;
        }

        public static IntPtr OpenSubKey(IntPtr hKey, string subKey, Win32API.RegSAM samDesired)
        {
            IntPtr hResult = IntPtr.Zero;

            Win32API.Error result = Win32API.RegOpenKeyEx(hKey, subKey, 0, samDesired, out hResult);
            if (result == Win32API.Error.Success)
            {
                // do nothing            
            }

            return hResult;
        }

        public static void CloseKey(IntPtr hKey)
        {
            if (hKey != IntPtr.Zero)
            {
                Win32API.RegCloseKey(hKey);
                //hKey = IntPtr.Zero;
            }
        }

        protected override void FreeNativeResources()
        {
            Close();
        }
    }
}

