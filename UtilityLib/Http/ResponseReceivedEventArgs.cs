using System;
using Libraries.UtilityLib.Http;

namespace Libraries.UtilityLib
{
    public delegate void ResponseReceivedEventHandler(ResponseReceivedEventArgs e);

    public class ResponseReceivedEventArgs : EventArgs
    {
        public readonly Response Response = null;
        public readonly Uri Url = null;

        public ResponseReceivedEventArgs(Uri url, Response response)
        {
            Url = url;
            Response = response;
        }
    }
}

