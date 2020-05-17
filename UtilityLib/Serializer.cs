using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Libraries.UtilityLib
{
    public abstract class Serializer<T>
    {
        protected static string extension = "";

        public static string Extension { get { return extension; } }      

        protected abstract void SerializeObject(T obj, Stream writer);
        protected abstract T DeserializeObject(Stream reader);

        public T ReadObject(Stream reader)
        {
           return (T) DeserializeObject(reader);
        }

        public void WriteObject(T obj, Stream writer)
        {
            SerializeObject(obj, writer);
        }   
    }
}
