using System;

namespace Libraries.UtilityLib.Callback
{
    public class VoidVoidInvocation : RoutineInvocation<VoidVoidInvocation.VoidVoidArgs, 
                                                        VoidVoidInvocation.VoidVoidInvocationHandler, 
                                                        VoidVoidInvocation.VoidVoidRoutine>
    {
        public delegate void VoidVoidRoutine();

        public delegate void VoidVoidInvocationHandler();

        public VoidVoidInvocation(VoidVoidRoutine routine)
            : base(new VoidVoidArgs(), null, routine)
        {
        }

        public class VoidVoidArgs : CallbackArguments
        {
        }

        #region implemented abstract members of Libraries.UtilityLib.Callback.RoutineInvocation
        protected override void Callback(VoidVoidArgs args)
        {
            Routine();
        }
        #endregion

    }
}

