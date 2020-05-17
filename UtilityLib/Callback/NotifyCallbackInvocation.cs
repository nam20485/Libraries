using System;
using Libraries.UtilityLib.Callback;

namespace Libraries.UtilityLib
{
    public abstract class NotifyCallbackInvocation<T, H> : CallbackInvocation<T>
        where T : CallbackArguments
    {
        protected readonly H InvocationReturnedHandler;

        public NotifyCallbackInvocation(T arguments, H invocationReturnedHandler)
            : base(arguments)
        {
            InvocationReturnedHandler = invocationReturnedHandler;
        }

        #region implemented abstract members of CallbackInvocation
        protected abstract override void Callback(T args);
        #endregion
    }
}

