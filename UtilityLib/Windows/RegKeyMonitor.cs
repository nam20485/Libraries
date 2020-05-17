using System;
using System.Threading;
using Libraries.UtilityLib.Windows;
using Libraries.UtilityLib.Logger;
using Libraries.UtilityLib.Threading;

namespace Libraries.UtilityLib.Windows
{
    public class RegKeyMonitor : ThreadLoop
    {
        public event RegistryKeyChangedHandler RegistryKeyChanged;

        protected NotifiableRegKey RegKey { get; set; }
        public bool WatchSubtree { get; protected set; }  

        public RegKeyMonitor(IntPtr hKey, string subKey, bool watchSubtree)
        {
            RegKey = new NotifiableRegKey(hKey, subKey);
            WatchSubtree = watchSubtree;
        }

        public override bool Start()
        {
            RegKey.Open();
            return base.Start();
        }

        public override void Stop()
        {
            base.Stop();
            RegKey.Close();
        }

        #region implemented abstract members of Libraries.UtilityLib.ThreadLoopBase
        protected override void Callback()
        {
            if (RegistryKeyChanged != null)
            {
                RegistryKeyChanged(this, new RegistryKeyChangedEventArgs(RegKey));
            }
        }
        #endregion

        protected override bool PreExecuteWaitInit(AutoResetEvent executeEvent)
        {
            Win32API.RegNotifyChangeFilter notifyFilter = (Win32API.RegNotifyChangeFilter.Attributes |
                                                            Win32API.RegNotifyChangeFilter.Name |
                                                            Win32API.RegNotifyChangeFilter.Security |
                                                            Win32API.RegNotifyChangeFilter.Value);

            // http://msdn.microsoft.com/en-us/library/windows/desktop/ms724892(v=vs.85).aspx:
            // This function detects a single change. After the caller receives a notification event, 
            // it should call the function again to receive the next notification.  
            Win32API.Error result = Win32API.RegNotifyChangeKeyValue(RegKey.Handle, 
                                                                     WatchSubtree, 
                                                                     notifyFilter, 
                                                                     executeEvent.SafeWaitHandle.DangerousGetHandle(),                                                                           
                                                                     true);
            return (result == Win32API.Error.Success);
        }
    }   
}

