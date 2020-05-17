using System;
using System.Threading;
using Libraries.UtilityLib.Logger;
using System.Windows.Forms;

namespace Libraries.UtilityLib.Callback
{
    public abstract class CallbackInvocation<T> : ArgumentlessInvocation
        where T : CallbackArguments
    {
        public readonly T Arguments;

        public CallbackInvocation(T arguments)
        {
            Arguments = arguments;
        }

        public override bool Invoke()
        {
            return Invoke(Arguments);
        }      

        protected override void CallbackInvoker(object state)
        {
            Callback(state as T);
            activeCallbackCount--;
            LocalStaticLogger.WriteLine("CallbackInvocation().CallbackInvoker(): activeCallbackCount= " + activeCallbackCount);
        }

        #region implemented abstract members of ArgumentlessInvocation

        protected override void Callback()
        {
        }      

        #endregion

        protected abstract void Callback(T args);
    }
}

