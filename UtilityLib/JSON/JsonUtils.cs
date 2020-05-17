using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Libraries.UtilityLib.Json
{
    public static class JsonUtils
    {
        public static JsonDictionary<K, V> DeserializeMap<K, V>(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<JsonDictionary<K, V>>(json);
        }

        public static string SerializeMap<K, V>(JsonDictionary<K, V> json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(json);
        }

        //{"preferences": "http://127.0.0.1:8000/greensuite_server/api/preferences/", "users": "http://127.0.0.1:8000/greensuite_server/api/users/", "settings": "http://127.0.0.1:8000/greensuite_server/api/settings/", "profiles": "http://127.0.0.1:8000/greensuite_server/api/profiles/", "bookings": "http://127.0.0.1:8000/greensuite_server/api/bookings/", "savings": "http://127.0.0.1:8000/greensuite_server/api/savings/", "greensuiteusers": "http://127.0.0.1:8000/greensuite_server/api/greensuiteusers/", "groups": "http://127.0.0.1:8000/greensuite_server/api/groups/", "rooms": "http://127.0.0.1:8000/greensuite_server/api/rooms/"}

//        public static JsonDictionary ParseStringMap(string json)
//        {
//            JsonDictionary dictionary = new JsonDictionary();
//
//            string bracesRemoved = json.Replace("{", string.Empty);
//            bracesRemoved = bracesRemoved.Replace("}", string.Empty);
//
//            string[] pairs = bracesRemoved.Split(',');
//            foreach (var pair in pairs)
//            {
//                var pair2 = pair.Replace("\"", string.Empty);
//                int firstColon = pair2.IndexOf(':');
//                string key = pair2.Substring(0, firstColon);
//                string val = pair2.Substring(firstColon + 2);
//                dictionary[key] = val;
//            }
//
//            return dictionary;
//        }
    }
}

