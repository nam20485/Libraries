using System;
using System.Net;
using Libraries.UtilityLib.Http;

namespace Libraries.UtilityLib.Callback
{
    public class GetInvocation : NotifyCallbackInvocation<GetInvocation.GetArgs, ResponseReceivedEventHandler>
    {
        public GetInvocation(GetArgs arguments, ResponseReceivedEventHandler invocationReturnedHandler)
            : base(arguments, invocationReturnedHandler)
        {
        }

        public class GetArgs : CallbackArguments
        {
            public readonly Uri Url;
            public readonly WebHeaderCollection Headers;

            public GetArgs(Uri url, WebHeaderCollection headers)
            {
                Url = url;
                Headers = headers;
            }        
        }

        #region implemented abstract members of Libraries.UtilityLib.Callback.CallbackInvocation
        protected override void Callback(GetArgs args)
        {
            if (args != null)
            {
                Response response = HttpRequest.Get(args.Url, args.Headers);
                if (InvocationReturnedHandler != null)
                {
                    InvocationReturnedHandler(new ResponseReceivedEventArgs(args.Url, response));
                }
            }
        }
        #endregion

    }
}

