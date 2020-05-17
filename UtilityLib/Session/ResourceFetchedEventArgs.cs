using System;
using Libraries.UtilityLib.Http;

namespace Libraries.UtilityLib.Session
{
    public delegate void ResourceFetchedEventHandler(ResourceFetchedEventArgs e);

    public class ResourceFetchedEventArgs : EventArgs
    {
        public Uri Url { get; protected set; }
        public Response Response    { get; protected set; }

        public ResourceFetchedEventArgs(Uri url, Response response)
        {
            Url = url;
            Response = response;
        }
    }
}

