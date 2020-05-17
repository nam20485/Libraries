using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Libraries.UtilityLib.Logger;

namespace Libraries.WindowsHookLib
{
    public class AsyncWindowsHook : WindowsHook
    {        
		//public new event WindowHookProc WindowHookEvent;

		protected AsyncWindowsHook (HookType hookType)
			: base(hookType)
		{
		}

        public AsyncWindowsHook(HookType hookType, WindowHookProc pfnClientHookProc)
            : base(hookType, pfnClientHookProc)
        {
        }

        protected override IntPtr InternalHookProc (int code, IntPtr wParam, IntPtr lParam)
		{			
            try
            {                
                bool processed = ThreadPool.QueueUserWorkItem(new WaitCallback(ClientHookProcInvoker), new WindowHookProcArgs(code, wParam, lParam));	
            }
            catch (NotSupportedException nse)
            {
                LocalStaticLogger.WriteLine(nse.ToString());
            }            

            return Win32HookAPI.CallNextHookEx(hHook, code, wParam, lParam);
        }

		private class WindowHookProcArgs
		{
			public int Code { get; set; }
			public IntPtr WParam { get; set; }
			public IntPtr LParam { get; set; }			

			public WindowHookProcArgs(int code, IntPtr wParam, IntPtr lParam)
			{
				Code = code;
				WParam = wParam;
				LParam = lParam;				
			}
		}
		
		protected virtual void ClientHookProcInvoker (object data)
		{            
			WindowHookProcArgs args = data as WindowHookProcArgs;
			if (args != null)
			{
				if (pfnClientHookProc != null)
				{
                    try
                    {
                        // no way to pass return code back
                        IntPtr result = pfnClientHookProc(args.Code, args.WParam, args.LParam);	
                    }
                    catch (System.Exception ex)
                    {
                        LocalStaticLogger.WriteLine(ex.ToString());
                    }
				}				
			}
		}
    }
}
