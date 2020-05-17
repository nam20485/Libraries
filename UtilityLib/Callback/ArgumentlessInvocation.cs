using System;
using System.Threading;
using Libraries.UtilityLib.Logger;
using Libraries.UtilityLib.Callback;

namespace Libraries.UtilityLib
{
    public abstract class ArgumentlessInvocation
    {
        protected static int activeCallbackCount = 0;

        public ArgumentlessInvocation()
        {
        }

        public virtual bool Invoke()
        {
            return Invoke(null);
        }

        protected bool Invoke(CallbackArguments arguments)
        {
            if (ThreadPool.QueueUserWorkItem(new WaitCallback(CallbackInvoker), arguments))
            {
                activeCallbackCount++;
                LocalStaticLogger.WriteLine("CallbackInvocation().Invoke(): activeCallbackCount= " + activeCallbackCount);               
                return true;
            }
            return false;
        }

        protected virtual void CallbackInvoker(object state)
        {
            Callback();
            activeCallbackCount--;
            LocalStaticLogger.WriteLine("CallbackInvocation().CallbackInvoker(): activeCallbackCount= " + activeCallbackCount);
        }

        protected abstract void Callback();
    }
}

