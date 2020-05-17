using System;
using Libraries.UtilityLib.Http;

namespace Libraries.UtilityLib.Session
{
    public abstract class Token
    {
        public string Username { get; protected set; }
        public string Password { get; protected set; }

        public event AuthenticatedEventHandler Authenticated;

        public Token(string username, string password)
        {
            Username = username;
            Password = password;
        }

        protected string value;
        public string Value
        { 
            get 
            {
                return FetchValue();
            }
            protected set
            {
                this.value = value;
            }
        }

        public bool IsValid { get { return (value != null && value.Length > 0); } }

        protected string FetchValue()
        {
            if (! IsValid)
            {
                var response = FetchResource();
                if (response != null)
                {
                    value = ParseResponse(response);
                    if (IsValid)
                    {
                        if (Authenticated != null)
                        {
                            Authenticated(new AuthenticatedEventArgs());
                        }
                    }
                }
            }

            return value;
        }          

        protected abstract Response FetchResource(); 
        protected abstract string ParseResponse(Response response);
    }
}


