using System;
//using Libraries.UtilityLib.Http;

namespace Libraries.UtilityLib
{
    public delegate void ApiFetchedHandler(ApiFetchedEventArgs e);

    public class ApiFetchedEventArgs : EventArgs
    {
        public readonly string Api;
        public readonly Uri Url;
        public readonly Response Response;

        public ApiFetchedEventArgs(string api, Uri url, Response response)
        {
            Api = api;
            Url = url;
            Response = response;
        }
    }
}

