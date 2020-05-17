using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.IO;
using Libraries.UtilityLib.Logger;

namespace Libraries.UtilityLib.Http
{
    public class HttpRequest
    {
        public Uri Url { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string AuthHeaderValue { get; protected set; }

        public HttpRequest(Uri url)
        {
            Url = url;
        }

        public HttpRequest(Uri url, string username, string password)
            : this(url)
        {
            Username = username;
            Password = password;
        }

        public HttpRequest(Uri url, string authHeaderValue)
            : this(url)
        {
            AuthHeaderValue = authHeaderValue;
        }

        public Response Get()                               { return Get(Url, AuthHeaderValue); }
        public Response Get(string authHeaderVal)           { return Get(Url, authHeaderVal); }
        public Response Get(WebHeaderCollection headers)    { return Get(Url, headers); }     

        public Response Post()                                  { return Post(Url, Username, Password); }
        public Response Post(string username, string password)  { return Post(Url, username, password); }
        public Response Post(NameValueCollection parameters)    { return Post(Url, parameters); }        

        public static Response Get(Uri url)
        {
            return Get(url, new WebHeaderCollection());
        }

        public static Response Get(Uri url, string authorizationHeader)
        {
            return Get(url, MakeAuthHeaderCollection(authorizationHeader));
        }

        public static Response Get(Uri url, WebHeaderCollection headers)
        {
            using (var client = new WebClient())
            {
                Response response = null;            
                try
                {
                    if (headers != null && headers.Count > 0)
                    {
                        client.Headers.Add(headers);
                    }
                    var bytes = client.DownloadData(url);
                    response = new Response(bytes);
                }
                catch (Exception e)
                {
                    LocalStaticLogger.WriteLine("HttpRequest.Get("+url+") - "+e.ToString());
                }
                return response;
            }
        }
       
        public static Response Post(Uri url, NameValueCollection parameters)
        {
            using (var client = new WebClient())
            {
                Response response = null;
                try
                {
                    byte[] bytes = client.UploadValues(url, parameters);
                    response = new Response(bytes);  
                }
                catch (Exception e)
                {
                    LocalStaticLogger.WriteLine("HttpRequest.Post("+url+") - "+e.ToString());
                } 
                return response;
            }
        }

        public static Response Post(Uri url, string username, string password)
        {
            return Post(url, MakeCredentialCollection(username, password));
        }

        protected static WebHeaderCollection MakeAuthHeaderCollection(string authHeaderValue)
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            if (authHeaderValue != null)
            {
                headers.Add(HttpRequestHeader.Authorization, authHeaderValue);
            }
            return headers;
        }

        protected static NameValueCollection MakeCredentialCollection(string username, string password)
        {
            NameValueCollection parameters = new NameValueCollection();
            if (username != null)
            {
                parameters ["username"] = username;
            }
            if (password != null)
            {
                parameters ["password"] = password;
            }
            return parameters;
        }
    }
}

