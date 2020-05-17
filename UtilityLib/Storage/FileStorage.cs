using System;
using System.IO;
using Libraries.UtilityLib.Logger;

namespace Libraries.UtilityLib.Storage
{
    public abstract class FileStorage<T, S> 
        where S : Serializer<T>, new()
    {
        public abstract Stream GetReadStream(Uri uri);
        public abstract Stream GetWriteStream(Uri uri);

        protected readonly S serializer = new S();

        public abstract string[] GetFiles(Uri path, string name); 

        public void Save(T obj, Uri path)
        {
            try
            {
                using (var writeStream = GetWriteStream(path))
                {
                    if (writeStream != null)
                    {
                        serializer.WriteObject(obj, writeStream);
                    }
                }
            }
            catch (Exception e)
            {
                LocalStaticLogger.WriteLine(e.ToString());
            }
        }

        public T Load(Uri filePath)
        {
            T file = default(T);
            using (var readStream = GetReadStream(filePath))
            {
                if (readStream != null)
                {
                    file = serializer.ReadObject(readStream);
                }
            }
            return file;
        }
    }
}

