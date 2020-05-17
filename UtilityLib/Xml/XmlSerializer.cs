using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Libraries.UtilityLib.Xml
{
    public class XmlSerializer<T> : Serializer<T>
    {       
        static XmlSerializer()
        {
            extension = "xml";
        }

        protected override void SerializeObject(T obj, Stream stream)
        {
           using (var xmlWriter = XmlWriter.Create(stream))
            {
                SerializeObject(obj, xmlWriter);
            }                                
        }

        protected void SerializeObject(T obj, XmlWriter xmlWriter)
        {
            var serializer = new DataContractSerializer(typeof(T));
            if (serializer != null)
            {                    
                serializer.WriteObject(xmlWriter, obj);
            }                                
        }

        protected override T DeserializeObject(Stream stream)
        {
            T obj = default(T);

            using (var xmlReader = XmlReader.Create(stream))
            {                       
                obj = DeserializeObject(xmlReader);                   
            }
            return obj;
        }        

        protected T DeserializeObject(XmlReader reader)
        {
            T obj = default(T);

            var serializer = new DataContractSerializer(typeof(T));
            if (serializer != null)
            {
                obj = (T) serializer.ReadObject(reader);                
            }

            return obj;
        }
    }
}
