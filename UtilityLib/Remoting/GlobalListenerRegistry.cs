using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace Libraries.UtilityLib.Remoting
{
    public class GlobalListenerRegistry : Singleton<ListenerRegistry>
    {
        public static string RemotingApplicationName
        {
            get { return Instance.RemotingApplicationName; }
            set { Instance.RemotingApplicationName = value; }
        }

        public static RemotingListener<TRemoteObj> RegisterType<TRemoteObj>(string uriEndpoint) where TRemoteObj : RemotingObject
        {
            return Instance.RegisterType<TRemoteObj>(uriEndpoint);
        }

        public static RemotingListener<TRemoteObj> RegisterType<TRemoteObj>(string uriEndpoint, WellKnownObjectMode wellKnownObjectMode) where TRemoteObj : RemotingObject
        {
            return Instance.RegisterType<TRemoteObj>(uriEndpoint, wellKnownObjectMode);
        }

        public static RemotingListener<TRemoteObj> RegisterType<TRemoteObj>(IChannel channel, string uriEndpoint, WellKnownObjectMode wellKnownObjectMode = RemotingListener<TRemoteObj>.DefaultWellKnownObjectMode) 
            where TRemoteObj : RemotingObject
        {
            return Instance.RegisterType<TRemoteObj>(channel, uriEndpoint, wellKnownObjectMode);
        }
    }
}

