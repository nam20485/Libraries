using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Libraries.UtilityLib.Disposeable;
using Libraries.UtilityLib.Logger;

namespace Libraries.WindowsHookLib
{
    public class WindowsHook : DisposeableResource<IntPtr>
    {     
        //public event WindowHookProc WindowHookEvent;  

        protected object lockObj = new object();

        public static readonly IntPtr UNHANDLED = IntPtr.Zero;

        protected readonly HookType hookType;
        public HookType HookType
        {
            get { return hookType; }
        }

		protected readonly WindowHookProc pfnClientHookProc;

        protected IntPtr hHook = IntPtr.Zero;

		protected WindowsHook (HookType hookType)
		{
			this.hookType = hookType;            
            SetWindowsHook();
		}

        // passed-in routine used for call-back
        public WindowsHook(HookType hookType, WindowHookProc pfnClientHookProc)                           
        {
            this.hookType = hookType;
            this.pfnClientHookProc = pfnClientHookProc;
            SetWindowsHook();
        }   

        #region Instantiated-class routines

        protected void SetWindowsHook()
        {
            hHook = SetWindowsHook(HookType, InternalHookProc);
        }

        protected bool UnhookWindowsHook()
        {            
            return UnhookWindowsHook(hHook);
        }

        protected virtual IntPtr InternalHookProc(int code, IntPtr wParam, IntPtr lParam)
        {          
            IntPtr result = UNHANDLED;

            if (pfnClientHookProc != null)
            {
                try
                {
                    // try to ensure CallNextHookEx is always called, even if client 
                    // hook proc code or MarhsalToStructure except
                    result = pfnClientHookProc(code, wParam, lParam);                    
                }
                catch (System.Exception ex)
                {
                    LocalStaticLogger.WriteLine(ex.ToString());
                }                    
            }                      

            try
            {
                // only call CallNextHook if client hook proc did not run or did run but didn't return handled
                if (result == UNHANDLED)
                {
                    result = Win32HookAPI.CallNextHookEx(hHook, code, wParam, lParam);
                }                
            }
            catch (System.Exception ex)
            {
                LocalStaticLogger.WriteLine(ex.ToString());
            }

            return result;    
        }

        #endregion // Instantiated-class routines

        #region Static routines

        public static bool UnhookWindowsHook(IntPtr hHook)
        {
            bool result = false;

            try
            {
                result = Win32HookAPI.UnhookWindowsHookEx(hHook);
            }
            catch (System.Exception ex)
            {
                LocalStaticLogger.WriteLine(ex.ToString());
            }

            return result;           
        }

        public static IntPtr SetWindowsHook(HookType hookType, WindowHookProc lpfnHookProc)
        {
            try
            {
                using (var currentProcess = Process.GetCurrentProcess())
                {
                    using (var currentModule = currentProcess.MainModule)
                    {
                        IntPtr hModule = Win32HookAPI.GetModuleHandle(currentModule.ModuleName);
                        if (hModule != IntPtr.Zero)
                        {
                            return Win32HookAPI.SetWindowsHookEx(hookType, lpfnHookProc, hModule, 0);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LocalStaticLogger.WriteLine(ex.ToString());
            }           

            return IntPtr.Zero;
        }

        #endregion // Static routines

        #region DisposeResource implementation

       protected override void FreeNativeResources()
        {
            if (hHook != IntPtr.Zero)
            {
                bool unhooked = UnhookWindowsHook(hHook);
                hHook = IntPtr.Zero;
            }
        }

        #endregion // IDisposeable implementation     
    }
}
