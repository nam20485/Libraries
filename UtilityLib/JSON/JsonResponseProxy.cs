using System;
//using Libraries.UtilityLib.Session;
using Libraries.UtilityLib.Session;
using Libraries.UtilityLib.Http;
using System.Collections.Generic;

namespace Libraries.UtilityLib.Json
{
    public class JsonResponseProxy
    {
        protected readonly JsonApiRoot apiRoot = null;
        protected readonly AsyncSession<JsonToken> session = null;   

        protected Dictionary<string, Response> responses = new Dictionary<string, Response>();

        public JsonResponseProxy(JsonApiRoot apiRoot, AsyncSession<JsonToken> session)
        {
            this.apiRoot = apiRoot;
            this.session = session;
        }

        public Response FetchApi(string api)
        {
            if (session != null)
            {
                Response response = null;
                lock (responses)
                {
                    response = session.FetchResource(apiRoot[api]);
                    responses[api] = response;
                }
                return response;
            }
            return null;
        }

        public Response GetLastResponse(string api)
        {
            lock (responses)
            {
                if (responses.ContainsKey(api))
                {
                    return responses[api];
                }
            }
            return null;
        }

        public Response[] FetchAllApis()
        {
            List<Response> responses = new List<Response>();
            foreach (var entry in apiRoot.Entries)
            {
                responses.Add(FetchApi(entry.Key));
            }
            return responses.ToArray();
        }
    }
}

