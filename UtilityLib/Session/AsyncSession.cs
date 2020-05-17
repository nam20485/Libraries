using System;
using Libraries.UtilityLib.Session;
using Libraries.UtilityLib.Http;
using System.Threading;
using Libraries.UtilityLib.Callback;

namespace Libraries.UtilityLib
{
    public abstract class AsyncSession<T> : Session<T>
        where T : Token
    {
        public event ResourceFetchedEventHandler ResourceFetched;
        public event AuthenticatedEventHandler Authenticated;

        public AsyncSession(T token)
            : base(token)
        {
            Token.Authenticated += HandleTokenAuthenticated;
        }

        protected void HandleTokenAuthenticated (AuthenticatedEventArgs e)
        {
            if (Authenticated != null)
            {
                Authenticated(new AuthenticatedEventArgs());
            }
        }

        public bool AuthenticateAsync()
        {
            return new VoidVoidInvocation(Authenticate).Invoke();
        }       

        public bool FetchResourceAsync(Uri url)
        {
            return new FetchResourceInvocation(
                new FetchResourceInvocation.FetchResourceArgs(url),
                ResourceFetched,
                FetchResource).Invoke();
        }       
    }
}

