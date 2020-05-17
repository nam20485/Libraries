using System;
using Libraries.UtilityLib.Http;
using System.Net;

namespace Libraries.UtilityLib.Session
{
    public class HttpSession<T> : AsyncSession<T>
        where T : Token
    {
        protected const string AUTH_HEADER_VALUE_FORMAT = "Token {0}";

        public HttpSession(T token)
            : base(token)
        {
        }       

        public override Response FetchResource(Uri url)
        {
            if (Token != null)
            {
                string tokenVal = Token.Value;
                if (tokenVal != null)           // allow empty token if provided
                {
                    return HttpRequest.Get(url, MakeAuthHeaderValue(tokenVal));
                }
            }

            return null;
        }

        protected static string MakeAuthHeaderValue(string tokenValue)
        { 
            return string.Format(AUTH_HEADER_VALUE_FORMAT, tokenValue);
        }
    }
}

