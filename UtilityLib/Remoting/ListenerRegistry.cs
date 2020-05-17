using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using  System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Ipc;
using System.Collections.Generic;
using System.Reflection;

namespace Libraries.UtilityLib.Remoting
{
    public class ListenerRegistry : PeerRegistry
    {
        public string RemotingApplicationName
        {
            get { return RemotingConfiguration.ApplicationName; }
            set { RemotingConfiguration.ApplicationName = value; }
        }

        public ListenerRegistry()
            : this(Assembly.GetEntryAssembly().GetName().Name)
        {
        }

        public ListenerRegistry(string applicationName)
        {
            RemotingApplicationName = applicationName;
        }

        public RemotingListener<TRemoteObj> RegisterType<TRemoteObj>(string uriEndpoint, WellKnownObjectMode wellKnownObjectMode = RemotingListener<TRemoteObj>.DefaultWellKnownObjectMode) 
            where TRemoteObj : RemotingObject
        {
            var listener = GetPeer<TRemoteObj>();
            if (listener == null)
            {
                listener = new RemotingListener<TRemoteObj>(uriEndpoint, wellKnownObjectMode);
                listener.Register();
                Peers[typeof(TRemoteObj)] = listener;
            }
            return listener;
        }     

        public RemotingListener<TRemoteObj> RegisterType<TRemoteObj>(IChannel channel, string uriEndpoint, WellKnownObjectMode wellKnownObjectMode = RemotingListener<TRemoteObj>.DefaultWellKnownObjectMode) 
            where TRemoteObj : RemotingObject
        {
            GlobalChannelRegistry.RegisterChannel(channel, false);
            return RegisterType<TRemoteObj>(uriEndpoint, wellKnownObjectMode);
        }     
    }
}


