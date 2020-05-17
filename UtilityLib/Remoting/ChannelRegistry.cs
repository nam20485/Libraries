using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Ipc;
using System.Net.Sockets;

namespace Libraries.UtilityLib.Remoting
{
//    public enum ChannelProtocol
//    {
//        Tcp,
//        Http,
//        Ipc
//    }

    public class ChannelRegistry
    {
        protected readonly List<IChannel> Channels = new List<IChannel>();

        public void RegisterChannel(IChannel channel, bool ensureSecurity = false)
        {
            if (channel != null)
            {                    
                if (! Channels.Contains(channel))
                {
                    try
                    {
                        ChannelServices.RegisterChannel(channel, ensureSecurity);
                        Channels.Add(channel); 
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }    

        public void RegisterTcpChannel(int port, bool ensureSecurity = false, bool ignorePortInuse = false)
        {
            try
            {
                var tcpChannel = new TcpChannel(port);
                RegisterChannel(tcpChannel, ensureSecurity);
            }
            catch (SocketException se)
            {
                if (! (ignorePortInuse && se.SocketErrorCode == SocketError.AddressAlreadyInUse))
                {
                    throw se;
                }
            }
        }

//        public dynamic RegisterChannel(ChannelProtocol channelProtocol, int port, bool ensureSecurity)
//        {
//            ChannelInfo channelInfo = GetChannelInfo(channelProtocol, port);
//            if (channelInfo == null)
//            {
//                return CreateChannel(channelProtocol, port, ensureSecurity);
//            }
//
//            return null;
//        }            

        public void UnregisterChannel(IChannel channel)
        {
            ChannelServices.UnregisterChannel(channel);
            Channels.Remove(channel);
        }

        public void UnregisterAllChannels()
        {
            foreach (var channel in Channels.ToArray())
            {
                UnregisterChannel(channel);
            }
        }

//        protected dynamic CreateChannel(ChannelProtocol protocol, int port, bool ensureSecurity)
//        {
//            dynamic channel = null;
//
//            switch (protocol)
//            {
//                case ChannelProtocol.Tcp:
//                    channel = new TcpChannel(port);
//                    break;
//                case ChannelProtocol.Http:
//                    channel = new HttpChannel(port);
//                    break;
//                case ChannelProtocol.Ipc:
//                    channel = new IpcChannel(string.Format("{0}:{1}", "localhost", port.ToString()));
//                    break;
//            }
//
//            if (channel != null)
//            {
//                ChannelServices.RegisterChannel(channel, ensureSecurity);
//                ChannelInfos.Add(new ChannelInfo(protocol, port, channel));
//            }
//
//            return channel;
//        }

//        protected void UnregisterChannel(IChannel channel)
//        {
//            // unregister channelInfo.Channel
//        }

//        protected ChannelInfo GetChannelInfo(ChannelProtocol channelProtocol, int port)
//        {
//            switch (channelProtocol)
//            {
//                case ChannelProtocol.Tcp:
//                    return GetChannelInfo<TcpChannel>(port);
//                case ChannelProtocol.Http:
//                    return GetChannelInfo<HttpChannel>(port);
//                case ChannelProtocol.Ipc:
//                    return GetChannelInfo<IpcChannel>(port);
//            }
//            return null;
//        }

//        protected ChannelInfo GetChannelInfo<TChannel>(int port) where TChannel : class, IChannel
//        {
//            Type tChannel = typeof(TChannel);
//
//            foreach (var channelInfo in ChannelInfos)
//            {
//                if (channelInfo.Port == port &&
//                    channelInfo.Channel.GetType() == tChannel)
//                {
//                    return channelInfo;
//                }
//            }
//
//            return null;
//        }
//
//        public IChannel GetChannel(ChannelProtocol channelProtocol, int port)
//        {
//            var channelInfo = GetChannelInfo(channelProtocol, port);
//            if (channelInfo != null)
//            {
//                return channelInfo.Channel;
//            }
//            return null;
//        }
//
//        public TChannel GetChannel<TChannel>(int port) where TChannel : class, IChannel
//        {
//            var channelInfo = GetChannelInfo<TChannel>(port);
//            if (channelInfo != null)
//            {
//                return channelInfo.Channel;
//            }
//            return null;
//        }
//
//        protected class ChannelInfo
//        {
//            public readonly ChannelProtocol Protocol;
//            public readonly int Port;
//            public readonly dynamic Channel;            
//
//            public ChannelInfo(ChannelProtocol channelProtocol, int port, dynamic channel)
//            {
//                Protocol = channelProtocol;
//                Port = port;
//                Channel = channel;
//            }
//        }
    }
}

