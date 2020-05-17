using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Libraries.UtilityLib.Json
{
    public class JsonDictionary<K, V> : DictionaryEx<K, V>
    {
        public readonly string Json;

        public JsonDictionary()
        {
            // argumentless ctor required for JavaScriptDeserializer!
        }

        public JsonDictionary(string json)
        {
            Json = json;
            Deserialize(Json);
        }

        public void Deserialize(string json)
        {
            var dictionary = JsonUtils.DeserializeMap<K, V>(json);
            foreach (var key in dictionary.Keys)
            {
                this[key] = dictionary[key];
            }
        }
    }
}

