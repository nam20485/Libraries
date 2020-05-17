using System;
using Libraries.UtilityLib.Session;
using Libraries.UtilityLib.Http;
using System.Threading;

namespace Libraries.UtilityLib.Json
{
    public class AsyncJsonResponseProxy : JsonResponseProxy
    {
        public event ApiFetchedHandler ApiFetched;

        public AsyncJsonResponseProxy(JsonApiRoot apiRoot, AsyncSession<JsonToken> session)
            : base(apiRoot, session)
        {
            session.ResourceFetched += HandleSessionResourceFetched;
        }

        void HandleSessionResourceFetched(ResourceFetchedEventArgs e)
        {
            var api = apiRoot.GetEntryForValue(e.Url);
            if (api != null)
            {
                lock (responses)
                {
                    responses[api.Key] = e.Response;
                }

                if (ApiFetched != null)
                {
                    ApiFetched(new ApiFetchedEventArgs(api.Key, api.Value, e.Response));
                }
            }
        }

        public bool FetchApiAsync(string api)
        {
            if (session != null)
            {
                return session.FetchResourceAsync(apiRoot[api]);
            }
            return false;
        }

        public void FetchAllApisAsync()
        {
            foreach (var entry in apiRoot.Entries)
            {
                FetchApiAsync(entry.Key);               
            }
        }
    }
}

