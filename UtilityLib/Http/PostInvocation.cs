using System;
using System.Collections.Specialized;
using Libraries.UtilityLib.Http;

namespace Libraries.UtilityLib.Callback
{
    public class PostInvocation : 
        NotifyCallbackInvocation<PostInvocation.PostArgs, ResponseReceivedEventHandler>
    {
        public PostInvocation(PostArgs arguments, ResponseReceivedEventHandler invocationReturnedHandler)
            : base(arguments, invocationReturnedHandler)
        {
        }

        public class PostArgs : CallbackArguments
        {
            public readonly Uri Url;
            public readonly NameValueCollection Parameters;

            public PostArgs(Uri url, NameValueCollection parameters)
            {
                Url = url;
                Parameters = parameters;
            } 
        }

        #region implemented abstract members of Libraries.UtilityLib.Callback.CallbackInvocation
        protected override void Callback(PostArgs args)
        {
            if (args != null)
            {
                Response response = HttpRequest.Post(args.Url, args.Parameters);
                if (InvocationReturnedHandler != null)
                {
                    InvocationReturnedHandler(new ResponseReceivedEventArgs(args.Url, response));
                }
            }
        }
        #endregion

    }
}

