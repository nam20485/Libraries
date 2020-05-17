using System;

namespace Libraries.UtilityLib.Windows
{
    public delegate void RegistryKeyChangedHandler(object sender, EventArgs e);        

    public class RegistryKeyChangedEventArgs : EventArgs
    {
        public RegKey RegKey { get; protected set; }

        public RegistryKeyChangedEventArgs(RegKey regKey)
        {
            RegKey = regKey;
        }
    }
}

