using System;
using System.Runtime.Remoting.Channels;

namespace Libraries.UtilityLib.Remoting
{
    public abstract class RemotingPeer
    {       
        public abstract void Register();

        // "tcp://localhost:8082/HelloServiceApplication/MyUri"
        public static string BuildUri(string protocol, string host, int port, string applicationName, string endpoint)
        {
            string remoteUri = string.Format("{0}://{1}:{2}/{3}", protocol, host, port.ToString(), applicationName);
            return BuildUri(remoteUri, endpoint);
        }

        public static string BuildUri(string remoteUri, string endpoint)
        {
            return string.Format("{0}/{1}", remoteUri, endpoint);
        }
    }
}

