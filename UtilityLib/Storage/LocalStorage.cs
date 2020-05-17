using System;
using System.IO;

namespace Libraries.UtilityLib.Storage
{
    public class LocalStorage<T, S> : FileStorage<T, S>
        where S : Serializer<T>, new()
    {
        public override Stream GetReadStream(Uri path)
        {
            return new FileStream(path.ToFileSystemPath(), 
                                  FileMode.Open, 
                                  FileAccess.Read, 
                                  FileShare.Read);
        }        

        public override Stream GetWriteStream(Uri path)
        {
            return new FileStream(path.ToFileSystemPath(), 
                                  FileMode.Create, 
                                  FileAccess.Write, 
                                  FileShare.Read);
        }
     
        public override string[] GetFiles(Uri path, string name)
        {
            return Directory.GetFiles(path.ToFileSystemPath(), name);
        }    
    }
}

