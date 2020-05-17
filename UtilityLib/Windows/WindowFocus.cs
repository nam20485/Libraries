using System;
using System.Runtime.InteropServices;

namespace Libraries.UtilityLib.Windows
{
    public static class WindowFocus
    {
        public static uint GetActiveWindowThreadId()
        {
            IntPtr hActiveWnd = Win32API.GetForegroundWindow();
            if (hActiveWnd != IntPtr.Zero)
            {
                return Win32API.GetWindowThreadProcessId(hActiveWnd, IntPtr.Zero);
            }
            return 0;
        }

        public static IntPtr GetActiveControl(IntPtr hWnd)
        {
            uint threadId = Win32API.GetWindowThreadProcessId(hWnd, IntPtr.Zero);
            if (threadId > 0)
            {
                return GetActiveControl(threadId);
            }
            return IntPtr.Zero;
        }

        public static IntPtr GetActiveControl(uint threadId)
        {        
            Win32API.GUITHREADINFO threadInfo = new Win32API.GUITHREADINFO();
            threadInfo.cbSize = Marshal.SizeOf(threadInfo);
            if (Win32API.GetGUIThreadInfo(threadId, out threadInfo))
            {
                return new IntPtr(threadInfo.hwndActive);
            }
            return IntPtr.Zero;
        }

        public static IntPtr GetActiveWindowActiveControl()
        {
            //return GetActiveControl(GetActiveWindowThreadId());
            return GetActiveControl(0);
        }
    }
}

