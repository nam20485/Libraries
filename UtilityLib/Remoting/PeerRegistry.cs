using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;

namespace Libraries.UtilityLib.Remoting
{
    public abstract class PeerRegistry
    {
        public readonly Dictionary<Type, dynamic> Peers = new Dictionary<Type, dynamic>();

        protected dynamic GetPeer<TRemoteObj>() where TRemoteObj : RemotingObject
        {
            return GetPeer(typeof(TRemoteObj));
        }

        protected dynamic GetPeer(Type t)
        {
            if (Peers.ContainsKey(t))
            {
                return Peers [t];
            }
            return null;
        }
    }
}

