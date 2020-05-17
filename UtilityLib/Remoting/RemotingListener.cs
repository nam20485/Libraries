using System;
using System.Runtime.Remoting;
using System.Threading;

namespace Libraries.UtilityLib.Remoting
{
    public class RemotingListener<TRemoteObj> : RemotingPeer
        where TRemoteObj : RemotingObject
    { 
        public const WellKnownObjectMode DefaultWellKnownObjectMode = WellKnownObjectMode.Singleton;

        public readonly string UriEndpoint;
        public readonly WellKnownObjectMode WellKnownObjectMode;

        public RemotingListener(string uriEndpoint, WellKnownObjectMode wellKnownObjectMode = DefaultWellKnownObjectMode)
        {
            UriEndpoint = uriEndpoint;
            WellKnownObjectMode = wellKnownObjectMode;
        }

        public override void Register()
        {
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(TRemoteObj), UriEndpoint, WellKnownObjectMode);
        }      
    }
}

