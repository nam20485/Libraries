using System.Threading;

namespace Libraries.UtilityLib.Threading
{
    public abstract class ThreadLoop : ThreadDelegate
    {
        private readonly AutoResetEvent ExecuteEvent;
        private readonly AutoResetEvent ExitEvent;

        private readonly WaitHandleList WaitHandles = new WaitHandleList();

        protected volatile bool exiting = false;

        protected ThreadLoop()
            : this(new AutoResetEvent(false))
        {
        }

        protected ThreadLoop(AutoResetEvent executeEvent)
        {
            ExecuteEvent = executeEvent;
            ExitEvent = new AutoResetEvent(false);

            WaitHandles.AddRange(new WaitHandle[] { ExecuteEvent, ExitEvent });
        }

        public override void Stop()
        {
            // signal thread loop to exit
            Exit();

            base.Stop();

            if (ExecuteEvent != null)
            {
                ExecuteEvent.Close();
                //executeEvent = null;
            }

            if (ExitEvent != null)
            {
                ExitEvent.Close();
                //exitEvent = null;
            }
        }

        protected void SignalExecuteCallback()
        {
            if (ExecuteEvent != null)
            {
                ExecuteEvent.Set();
            }
        }

        protected void Exit()
        {
            exiting = true;
            if (ExitEvent != null)
            {
                ExitEvent.Set();
            }
        }

        protected virtual bool PreExecuteWaitInit(AutoResetEvent executeEvent)
        {          
            return true;
        }

        protected override void InternalThreadProc()
        {
            while (! exiting)
            {                
                if (WaitHandles == null || WaitHandles.Count == 0 || ! WaitHandles.AreValid())
                {
                    return;
                }

                if (PreExecuteWaitInit(ExecuteEvent))
                {
                    int signalled = WaitHandles.WaitAny();
                    if (signalled == WaitHandles.IndexOf(ExecuteEvent))
                    {
                        base.InternalThreadProc();                   
                    }
                }
            }
        }          
    }
}

