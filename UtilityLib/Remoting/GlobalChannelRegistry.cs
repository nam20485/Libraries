using System;
using System.Runtime.Remoting.Channels;

namespace Libraries.UtilityLib.Remoting
{
    public class GlobalChannelRegistry : Singleton<ChannelRegistry>
    {
        public static void RegisterChannel(IChannel channel, bool ensureSecurity = false)
        {
            Instance.RegisterChannel(channel, ensureSecurity);
        }            

        public static void UnregisterChannel(IChannel channel)
        {
            Instance.UnregisterChannel(channel);
        }

        public static void UnregisterAllChannels()
        {
            Instance.UnregisterAllChannels();
        }

        public static void RegisterTcpChannel(int port, bool ensureSecurity = false, bool ignorePortInuse = false)
        {
            Instance.RegisterTcpChannel(port, ensureSecurity, ignorePortInuse);
        }

//        public static IChannel GetChannel(ChannelProtocol channelProtocol, int port)
//        {
//            return Instance.GetChannel(channelProtocol, port);
//        }
//
//        public static TChannel GetChannel<TChannel>(int port) where TChannel : class, IChannel
//        {
//            return Instance.GetChannel<TChannel>(port);
//        }
    }
}

