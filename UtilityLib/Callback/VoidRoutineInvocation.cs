using System;

namespace Libraries.UtilityLib.Callback
{
    public abstract class VoidRoutineInvocation<R> : ArgumentlessInvocation
    {
        public delegate void VoidVoidRoutine();   

        protected readonly R Routine;

        public VoidRoutineInvocation(R routine)
        {
            Routine = routine;
        }

        #region implemented abstract members of ArgumentlessInvocation

        protected override void Callback()
        {

        }

        #endregion
    }
}

