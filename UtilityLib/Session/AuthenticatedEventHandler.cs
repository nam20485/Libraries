using System;

namespace Libraries.UtilityLib.Session
{
    public delegate void AuthenticatedEventHandler(AuthenticatedEventArgs e);

    public class AuthenticatedEventArgs : EventArgs
    {
        public AuthenticatedEventArgs()
        {
        }
    }
}

