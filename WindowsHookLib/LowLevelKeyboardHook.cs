using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Libraries.UtilityLib;
using Libraries.UtilityLib.Logger;

namespace Libraries.WindowsHookLib
{     
	public delegate IntPtr LowLevelKeyboardHookProc(HookCode hc, KeyEvent keyEvent, KeyEventInfo info);           

    public class LowLevelKeyboardHook : WindowsHook
    {
        public static readonly IntPtr HANDLED = new IntPtr(-1);

        protected new readonly LowLevelKeyboardHookProc pfnClientHookProc = null;		

        public LowLevelKeyboardHook(LowLevelKeyboardHookProc pfnClientHookProc)          
            : base(HookType.WH_KEYBOARD_LL)
        {
            this.pfnClientHookProc = pfnClientHookProc;
        }

        // http://msdn.microsoft.com/en-us/library/windows/desktop/ms644985%28v=vs.85%29.aspx:
        // If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
        // If nCode is greater than or equal to zero, and the hook procedure did not process the message, it is highly 
        // recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that 
        // have installed WH_KEYBOARD_LL hooks will not receive hook notifications and may behave incorrectly as a result. 
        // If the hook procedure processed the message, it may return a nonzero value to prevent the system from passing 
        // the message to the rest of the hook chain or the target window procedure. 	
        
        protected override IntPtr InternalHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            IntPtr result = IntPtr.Zero;

            // protect this instance from ever calling InternalHookProc
            // at the same time          
            lock (lockObj)
            {
                if (code >= 0)
                {
                    if (pfnClientHookProc != null)
                    {
                        try
                        {
                            // protected ourselves from exceptions thrown in client hook procedures
                            LowLevelKeyboardHookProcArgs args = new LowLevelKeyboardHookProcArgs(code, wParam, lParam);
                            result = pfnClientHookProc(args.HookCode, args.KeyEvent, args.Info);                       
                        } 
                        catch (Exception e)
                        {
                            LocalStaticLogger.WriteLine(e.ToString());
                        }
                    }
                }
           
                try
                {
                    // try to ensure CallNextHookEx is always called, even if client 
                    // hook proc code or MarhsalToStructure except
                    if (result == IntPtr.Zero)
                    {
                        result = Win32HookAPI.CallNextHookEx(hHook, code, wParam, lParam);
                    }                
                } 
                catch (Exception e)
                {
                    LocalStaticLogger.WriteLine(e.ToString());
                }
            }

            return result; 
        }        

        protected class LowLevelKeyboardHookProcArgs
        {
            public HookCode HookCode { get; set; }
            public KeyEvent KeyEvent { get; set; }
            public KeyEventInfo Info { get; set; }

            public LowLevelKeyboardHookProcArgs(int code, IntPtr wParam, IntPtr lParam)
            {
                HookCode = (HookCode) code;	            	            
	            KeyEvent = (KeyEvent) wParam.ToInt32();
	            Info =  (KeyEventInfo) Marshal.PtrToStructure(lParam, typeof(KeyEventInfo));
            }
        }      
    }
}
