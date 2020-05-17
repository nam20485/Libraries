using System;

namespace Libraries.UtilityLib.Threading
{
    public class ThreadDelegate : ThreadProc
    {
        public delegate void ThreadProcCallback();
        public delegate void ThreadProcExceptionHandler(Exception e);

        protected ThreadProcCallback OnCallback;
        protected ThreadProcExceptionHandler OnThreadProcException;

        protected ThreadDelegate()
        {
            // do nothing, hide ctor from public access,
            // ctor'able from the static method
        }

        protected ThreadDelegate(ThreadProcCallback callback, ThreadProcExceptionHandler threadProcExceptionHandler = null)
        {
            OnCallback = callback;            
            OnThreadProcException = threadProcExceptionHandler;
        }

        public static ThreadDelegate Create(ThreadProcCallback callback, ThreadProcExceptionHandler threadProcExceptionHandler = null)
        {
            return new ThreadDelegate(callback, threadProcExceptionHandler);
        }

        public static ThreadDelegate Start(ThreadProcCallback callback, ThreadProcExceptionHandler threadProcExceptionHandler = null)
        {
            ThreadDelegate workerThread = Create(callback, threadProcExceptionHandler);
            workerThread.Start();
            return workerThread;
        }

        #region implemented abstract members of Libraries.UtilityLib.ThreadLoop
        protected override void Callback()
        {
            if (OnCallback != null)
            {
                OnCallback();
            }
        }

        protected override void HandleThreadProcException(Exception e)
        {
            if (OnThreadProcException != null)
            {
                OnThreadProcException(e);
            }
        }
        #endregion
    }
}

