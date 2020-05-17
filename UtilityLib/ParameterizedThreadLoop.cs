using System;

namespace Libraries.UtilityLib
{
    public abstract class ThreadLoopParameters
    {
    }

    public abstract class ParameterizedThreadLoop<TParams> : ThreadLoop
        where TParams : ThreadLoopParameters
    {
        protected TParams Params { get; set; } 

        public ParameterizedThreadLoop(TParams parameters)
        {
            Params = parameters;
        }

        public bool Start(TParams parameters)
        {
            Params = parameters;
            return Start();
        }
    }
}

