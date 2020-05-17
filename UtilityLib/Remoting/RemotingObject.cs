using System;
using System.Runtime.Remoting;

namespace Libraries.UtilityLib.Remoting
{
    public abstract class RemotingObject : MarshalByRefObject
    {
        public const string UriEnpoint = "RemotingObject";

        public readonly bool Expires = true;

        public RemotingObject()
            : this(true)
        {
        }

        public RemotingObject(bool expires)
        {
            Expires = expires;
        }

        public override object InitializeLifetimeService()
        {
            if (! Expires)                              
            {
                return null;
            }
            return base.InitializeLifetimeService();
        }
    }
}

