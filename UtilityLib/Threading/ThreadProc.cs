using System;
using System.Threading;
using Libraries.UtilityLib.Disposeable;
using Libraries.UtilityLib.Logger;

namespace Libraries.UtilityLib
{
    public abstract class ThreadProc : DisposeableResource<ThreadProc>
    {
        protected Thread Thread;// { get; set; }

        protected abstract void Callback();    
        protected virtual void HandleThreadProcException(Exception e)
        {
            LocalStaticLogger.WriteLine(e.ToString());
        }

        public void Join()
        {
            if (Thread != null)
            {
                Thread.Join();
            }
        }

        public virtual bool Start()
        {
            Thread = new Thread(new ThreadStart(InternalThreadProc));
            if (Thread != null)
            {
                Thread.Name = "ThreadProc.InternalThreadProc()";
                Thread.Start();
                return Thread.IsAlive;
            };

            return false;
        }

        public virtual void Stop()
        {
            // wait for thread to finish
            Join();
            Thread = null;
        }

        protected override void FreeManagedResources()
        {
            Stop();
        }

        protected virtual void InternalThreadProc()
        {
            try
            {
                Callback();                   
            }
            catch (Exception e)
            {
                HandleThreadProcException(e);
            }        
        } 
    }
}

