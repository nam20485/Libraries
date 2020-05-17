using System;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Libraries.UtilityLib.Windows
{
    public delegate void WindowActivatedHandler(WindowFocusWatcher sender, WindowActivatedEventArgs e);
    public delegate void WindowControlFocussedHandler(WindowFocusWatcher sender, WindowControlFocussedEventArgs e);   

    public class WindowFocusWatcher
    {
        public event WindowActivatedHandler WindowActivated;
        public event WindowControlFocussedHandler WindowControlFocussed;

        public static readonly IntPtr FOREGROUND_WINDOW = IntPtr.Zero;
        protected const int POLL_SLEEP_INTERVAL_MS = 500;
        protected bool exiting = false;

        public readonly IntPtr Window = IntPtr.Zero;       
        public readonly NativeWindow NativeWindow;
        //public readonly Form Form;

        public WindowFocusWatcher()
            : this(FOREGROUND_WINDOW)
        {}

        public WindowFocusWatcher(IntPtr hWnd)
        {
            this.Window = hWnd;
            NativeWindow = NativeWindow.FromHandle(hWnd);
            //Form = Form.FromHandle(hWnd);
        }

        protected Thread pollingThread = null;

        public void Start()
        {
            //WaitCallback pollActiveWindowCB = new WaitCallback(PollActiveWindow);
            //return ThreadPool.QueueUserWorkItem(pollActiveWindowCB, null);
            pollingThread = new Thread(new ThreadStart(PollActiveWindow));
            pollingThread.Start();
        }

        public void Stop()
        {
            exiting = true;
            if (pollingThread != null)
            {
                pollingThread.Join();
                pollingThread = null;
            }
        }

        protected IntPtr lastActiveWindow = IntPtr.Zero;
        protected IntPtr lastActiveControl = IntPtr.Zero;

        protected void PollActiveWindow()
        {
            while (!exiting)
            {
                IntPtr currentActiveWindow = Win32API.GetForegroundWindow();
                if (currentActiveWindow != IntPtr.Zero)
                {
                    if (lastActiveWindow != currentActiveWindow)
                    {                   
                        lastActiveWindow = currentActiveWindow;

                        uint threadId = Win32API.GetWindowThreadProcessId(lastActiveWindow, IntPtr.Zero);
                        FireWindowActivated(lastActiveWindow, threadId);
                    } 
                    else if (lastActiveWindow != IntPtr.Zero)
                    {
                        uint threadId = Win32API.GetWindowThreadProcessId(lastActiveWindow, IntPtr.Zero);
                        Win32API.GUITHREADINFO threadInfo = new Win32API.GUITHREADINFO();
                        threadInfo.cbSize = Marshal.SizeOf(threadId);
                        if (Win32API.GetGUIThreadInfo(threadId, out threadInfo))
                        {
                            IntPtr hwndActive = new IntPtr(threadInfo.hwndActive);
                            if (lastActiveControl != hwndActive)
                            {
                                lastActiveControl = hwndActive;
                                FireWindowControlFocussed(lastActiveWindow, lastActiveControl);
                            }                        
                        }
                    }
                }

                Thread.Sleep(POLL_SLEEP_INTERVAL_MS);
            }
        }

        protected void FireWindowActivated(IntPtr hActiveWnd, uint threadId)
        {
            if (WindowActivated != null)
            {
                WindowActivated(this, new WindowActivatedEventArgs(hActiveWnd, threadId));
            }
        }

        protected void FireWindowControlFocussed(IntPtr hWnd, IntPtr hControl)
        {
            if (WindowControlFocussed != null)
            {
                WindowControlFocussed(this, new WindowControlFocussedEventArgs(hWnd, hControl));
            }
        }
    }

    public class WindowActivatedEventArgs : EventArgs
    {
        public readonly IntPtr ActiveWindow;
        public readonly uint ThreadId;

        public  WindowActivatedEventArgs(IntPtr activeWindow, uint threadId)
        {
            ActiveWindow = activeWindow;
            ThreadId = threadId;
        }
    }

    public struct WindowControlFocussedEventArgs
    {
        public readonly IntPtr Window; 
        public readonly IntPtr FocussedControl;

        public WindowControlFocussedEventArgs(IntPtr window, IntPtr focussedControl)
        {
            Window = window;
            FocussedControl = focussedControl;
        }
    }
}

