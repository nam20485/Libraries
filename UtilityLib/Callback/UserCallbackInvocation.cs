using System;

namespace Libraries.UtilityLib.Callback
{
    public abstract class UserCallbackInvocation<T, H> : NotifyCallbackInvocation<T, H>
        where T : CallbackArguments
    {
        public delegate void UserCallbackRoutine(T args);

        //public event UserCallback InvokeUserCallback;
        protected readonly UserCallbackRoutine UserCallback;

        public UserCallbackInvocation(T arguments, H invocationReturnedHandler)
            : base(arguments, invocationReturnedHandler)
        {
        }

        public UserCallbackInvocation(T arguments, H invocationReturnedHandler, UserCallbackRoutine userCallback)
            : this(arguments, invocationReturnedHandler)
        {
            UserCallback = userCallback;
        }       

        #region implemented abstract members of Libraries.UtilityLib.Callback.CallbackInvocation
        protected abstract override void Callback(T args);
        #endregion

        protected override void CallbackInvoker(object state)
        {
            if (UserCallback != null)
            {
                UserCallback(state as T);
            }
        }
    }
}

