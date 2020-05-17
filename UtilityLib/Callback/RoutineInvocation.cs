using System;

namespace Libraries.UtilityLib.Callback
{
    public abstract class RoutineInvocation<T, H, R> : NotifyCallbackInvocation<T, H>
        where T : CallbackArguments
    {
        protected readonly R Routine;

        public RoutineInvocation(T arguments, H invocationReturnedHandler, R routine)
            : base(arguments, invocationReturnedHandler)
        {
            Routine = routine;
        }

        #region implemented abstract members of Libraries.UtilityLib.Callback.CallbackInvocation
        protected abstract override void Callback(T args);
        #endregion

    }
}

