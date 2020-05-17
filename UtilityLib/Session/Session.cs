using System;
using Libraries.UtilityLib.Http;

namespace Libraries.UtilityLib.Session
{
    public abstract class Session<T> where T : Token
    {
        public T Token   { get; protected set; }

        public Session(T token)
        {
            Token = token;
        }

        public void Authenticate()
        {
#pragma warning disable 0219
            string value = null;
#pragma warning restore 0219
            if (Token != null)
            {
                value = Token.Value;

            }
            //return value;
        }

        public abstract Response FetchResource(Uri url);
    }
}

