using System;
using System.Net;
using System.Collections.Specialized;
using System.Threading;
using Libraries.UtilityLib.Callback;

namespace Libraries.UtilityLib.Http
{
    public class AsyncHttpRequest : HttpRequest
    {
        public event ResponseReceivedEventHandler ResponseReceived;

        public AsyncHttpRequest(Uri url)
            : base(url)
        {
        }

        public bool GetAsync(WebHeaderCollection headers = null)
        { 
            GetInvocation.GetArgs args = new GetInvocation.GetArgs(Url, headers);
            return new GetInvocation(args, ResponseReceived).Invoke();
        }

        public bool PostAsync(NameValueCollection parameters = null)
        { 
            PostInvocation.PostArgs args = new PostInvocation.PostArgs(Url, parameters);
            return new PostInvocation(args, ResponseReceived).Invoke();
        }

        public bool PostAsync(string username, string password)
        { 
            PostInvocation.PostArgs args = 
                new PostInvocation.PostArgs(Url, MakeCredentialCollection(username, password));
            return new PostInvocation(args, ResponseReceived).Invoke();
        }

        public bool GetAsync(string authHeaderVal)
        { 
            GetInvocation.GetArgs args = new GetInvocation.GetArgs(Url, MakeAuthHeaderCollection(authHeaderVal));
            return new GetInvocation(args, ResponseReceived).Invoke();
        }      
    }
}

