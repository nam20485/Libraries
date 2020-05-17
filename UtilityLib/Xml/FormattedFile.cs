using System;
using Libraries.UtilityLib.Logger;
using System.Collections.Generic;
using Libraries.UtilityLib.Storage;
using Libraries.UtilityLib.Notification;

namespace Libraries.UtilityLib
{
    public abstract class FormattedFile<T, S, R> : NotifyPropertyChangedEx
        where T : FormattedFile<T, S, R>, new()
        where S : Serializer<T>,  new()
        where R : FileStorage<T, S>, new()
    {
        protected static string Extension
        {   
            get { return Serializer<T>.Extension; }
        }

        public static readonly T Empty = new T();

        //protected static readonly S serializer = new S();
        protected static readonly R storage = new R();

        public string Filename { get; protected set; } 

        public FormattedFile()
            : this("FormattedFile")
        {
        }

        protected FormattedFile(string filename)
        {
            Filename = filename;
        }

        protected static T Load(Uri filePath)
        {
            T file = default(T);

            try
            {
                file = storage.Load(filePath);
            }
            catch (Exception e)
            {
                file = FormattedFile<T, S, R>.Empty;
                LocalStaticLogger.WriteLine(e.ToString());
            }

            return file;
        }

        public static T Load(Uri path, string name)
        {
            T obj = Load(path.Append(name + "." + Extension));
            if (obj != null)
            {
                obj.Filename = name;
            }

            return obj;
        }         

        public virtual void Save()
        {
            Save(Constants.CurrentDirectory);
        }

        protected void Save(Uri path)
        {
            try
            {
                Uri uri = new Uri(path.ToFileSystemPath()+"."+Extension);
                storage.Save(this as T, uri);
            }
            catch (Exception e)
            {
                LocalStaticLogger.WriteLine(e.ToString());
            }
        }

//        public static void Save(Uri path, string name)
//        {
//           Save(path.Append(name + "." + Extension));
//        }         

        public static T[] LoadFilesWithName(Uri path, string name)
        {
            List<T> files = new List<T>();

            foreach (var filePath in storage.GetFiles(path, name))
            {
                var file = Load(filePath.FromFileSystemPath());
                if (file != null)
                {
                    files.Add(file);
                }
            }

            return files.ToArray();
        }

        public static T[] LoadFilesWithExtension(Uri path, string fileExtension)
        {
            return LoadFilesWithName(path, string.Format("*.{0}.{1}", fileExtension, FormattedFile<T, S, R>.Extension));
        }
    }
}

