using System;

namespace Libraries.UtilityLib.Windows
{
    public class NotifiableRegKey : RegKey
    {
        protected const Win32API.RegSAM NotifySam = (Win32API.RegSAM.Notify|
                                                      Win32API.RegSAM.Read|
                                                      Win32API.RegSAM.QueryValue);

        public NotifiableRegKey(IntPtr hKey, string subKey)
            : base(hKey, subKey)
        {
        }

        public static IntPtr OpenSubKey(IntPtr hKey, string subKey)
        {
            return OpenSubKey(hKey, subKey, NotifySam);
        }

        public void Open()
        {
            Open(NotifySam);
        }

        public static RegKey Open(IntPtr hRootKey, string subKey)
        {
            var regKey = new NotifiableRegKey(hRootKey, subKey);
            regKey.Open();
            return regKey;
        }

       
    }
}

