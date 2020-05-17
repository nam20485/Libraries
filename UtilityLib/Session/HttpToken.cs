using System;
using System.Collections.Specialized;
using Libraries.UtilityLib.Http;
using System.Security.Policy;

namespace Libraries.UtilityLib.Session
{
    public abstract class HttpToken : Token
    {
        public Uri Url { get; protected set; }
        
        public HttpToken(string username, string password, Uri url)
                : base(username, password)
        {
            Url = url;
        }

        protected override Response FetchResource()
        {
            Response response = HttpRequest.Post(Url, Username, Password);
            return response;
        }

        protected override abstract string ParseResponse(Response response);
    }
}

