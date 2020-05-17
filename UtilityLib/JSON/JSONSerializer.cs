using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Libraries.UtilityLib.JSON
{
    public class JSONSerializer<T> : Serializer<T>
    {      
        static JSONSerializer()
        {
            extension = "json";
        }

        protected override void SerializeObject(T obj, Stream writer)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            if (serializer != null)
            {
                serializer.WriteObject(writer, obj);                             
            }
        }

        protected override T DeserializeObject(Stream reader)
        {
            T obj = default(T);

            var serializer = new DataContractJsonSerializer(typeof(T));
            if (serializer != null)
            {
                obj = (T) serializer.ReadObject(reader);
            }

            return obj;
        }        
    }
}

