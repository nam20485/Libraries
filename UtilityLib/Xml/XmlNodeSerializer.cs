using System;
using Libraries.UtilityLib.Xml;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;

namespace Libraries.UtilityLib
{
    public class XmlNodeSerializer<T> : XmlSerializer<T>
    {
        // T --> XmlNode
        protected XmlDocument SerializeNode(T obj)
        {
            using (MemoryStream s = new MemoryStream())
            {
                SerializeObject(obj, s);
                s.Flush();
                s.Position = 0;

                //using (StreamReader reader = new StreamReader(s))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(s);
                }
            }

            return null;
        }

        // XmlNode --> T
        protected T DeserializeNode(XmlNode node)
        {
            T obj = default(T);

            using (var xmlReader = new XmlNodeReader(node))
            {
                obj = DeserializeObject(xmlReader);
            }

            return obj;
        }      
    }
}

