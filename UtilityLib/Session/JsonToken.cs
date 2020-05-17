using System;
using Libraries.UtilityLib.Http;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Libraries.UtilityLib.Session
{
    public class JsonToken : HttpToken
    {
        public JsonToken(string username, string password, Uri url)
                : base(username, password, url)
        {
        }       

        protected override string ParseResponse(Response response)
        {
            DataContractJsonSerializer serializer = 
                new DataContractJsonSerializer(typeof(JsonTokenObject));

            using (MemoryStream stream = new MemoryStream(response.Bytes))
            {
                JsonTokenObject jsonTokenObj = serializer.ReadObject(stream) as JsonTokenObject;
                return jsonTokenObj.Token;
            }       
        }

        // "{"token": "8bbc137025f892f5c77e17cf247cd9cbf8b31636"}"
        [DataContract]
        protected class JsonTokenObject
        {
            [DataMember(Name="token")]
            public string Token { get; set; }

//            public JsonTokenObject(string token)
//            {
//                Token = token;
//            }
        }
    }
}

