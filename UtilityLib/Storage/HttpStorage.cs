using System;
using System.Net;
using System.IO;

namespace Libraries.UtilityLib.Storage
{
    public class HttpStorage<T, S> : FileStorage<T, S> 
        where S : Serializer<T>, new()
    {
       #region implemented abstract members of Libraries.UtilityLib.XmlSerializer
        public override Stream GetReadStream(Uri uri)
        {
            HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
            if (request != null)
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response != null)
                {
                    return response.GetResponseStream();
                }
            }
            return null;
        }

        public override Stream GetWriteStream(Uri uri)
        {
            HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "PUT";
                request.ContentType = "application/xml";
                return request.GetRequestStream();
            }
            return null;
        }

        public override string[] GetFiles(Uri path, string name)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

