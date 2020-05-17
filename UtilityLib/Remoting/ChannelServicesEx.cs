using System;
using System.Runtime.Remoting.Channels;

namespace Libraries.UtilityLib.Remoting
{
    public static class ChannelServicesEx
    {
        public static void UnregisterAllChannels()
        {
            var registeredChannels = ChannelServices.RegisteredChannels;
            foreach (var registeredChannel in registeredChannels)
            {
                ChannelServices.UnregisterChannel(registeredChannel);
            }
        }
    }
}

