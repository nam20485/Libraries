using System;

namespace Libraries.UtilityLib.Remoting
{
    public class GlobalClientFactory : Singleton<ClientFactory>
    {
        public static RemotingClient<TRemoteObj> RegisterType<TRemoteObj>(string remoteUri) where TRemoteObj : RemotingObject, new()
        {
            return Instance.RegisterType<TRemoteObj>(remoteUri);
        }

        public RemotingClient<TRemoteObj> RegisterType<TRemoteObj>(string protocol, string host, int port, string applicationName, string endpoint) 
            where TRemoteObj : RemotingObject, new()
        {
            return Instance.RegisterType<TRemoteObj>(protocol, host, port, applicationName, endpoint);
        }
    }
}

