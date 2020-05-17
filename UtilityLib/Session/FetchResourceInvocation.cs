using System;
using Libraries.UtilityLib.Callback;
using Libraries.UtilityLib.Http;
using System.Security.Policy;

namespace Libraries.UtilityLib.Session
{
    public class FetchResourceInvocation
        : RoutineInvocation<FetchResourceInvocation.FetchResourceArgs, 
                            ResourceFetchedEventHandler,
                            FetchResourceInvocation.FetchResourceRoutine>
    {
        public delegate Response FetchResourceRoutine(Uri url);

        public FetchResourceInvocation(FetchResourceInvocation.FetchResourceArgs arguments, 
                                       ResourceFetchedEventHandler invocationReturnedHandler,
                                       FetchResourceRoutine routine)
            : base(arguments, invocationReturnedHandler, routine)
        {
        }

        public class FetchResourceArgs : CallbackArguments
        {
            public readonly Uri Url = null;

            public FetchResourceArgs(Uri url)
            {
                Url = url;
            }
        }

        #region implemented abstract members of Libraries.UtilityLib.Callback.CallbackInvocation
        protected override void Callback(FetchResourceArgs args)
        {
            if (args != null)
            {
                Response response = Routine(args.Url);
                if (response != null)
                {
                    if (InvocationReturnedHandler != null)
                    {
                        InvocationReturnedHandler(new ResourceFetchedEventArgs(args.Url, response));
                    }
                }
            }
        }
        #endregion

    }
}

