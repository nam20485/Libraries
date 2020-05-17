using System;
using System.Collections.Generic;

namespace Libraries.UtilityLib.Remoting
{
    public class ClientFactory : PeerRegistry
    {
        public RemotingClient<TRemoteObj> RegisterType<TRemoteObj>(string remoteUri) where TRemoteObj : RemotingObject, new()
        {
            var client = GetPeer<TRemoteObj>();
            if (client == null)
            {
                client = new RemotingClient<TRemoteObj>(remoteUri);
                client.Register();
                Peers[typeof(TRemoteObj)] = client;
            }
            return client;
        }      

        public RemotingClient<TRemoteObj> RegisterType<TRemoteObj>(string protocol, string host, int port, string applicationName, string endpoint) 
            where TRemoteObj : RemotingObject, new()
        {
            string remoteUri = RemotingPeer.BuildUri(protocol, host, port, applicationName, endpoint);
            return RegisterType<TRemoteObj>(remoteUri);
        }
    }
}

