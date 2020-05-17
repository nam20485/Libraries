using System;

namespace Libraries.UtilityLib.Remoting
{
    public abstract class NonExpiringRemotingObject : RemotingObject
    {
       public override object InitializeLifetimeService()
       {
            return null;
            //return base.InitializeLifetimeService();
       }
    }
}

