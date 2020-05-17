using System;
using System.IO;
using System.Collections.Generic;
using Libraries.UtilityLib;
using Libraries.UtilityLib.Logger;
using System.Collections.ObjectModel;

namespace Libraries.UtilityLib.Reflection
{
    public class InterfaceFactory<T> where T : class
    {
        protected readonly List<Type> subTypes = new List<Type>();

        public ReadOnlyCollection<Type> SubTypes
        {
            get { return subTypes.AsReadOnly(); }
        }

        public void LoadInterfaceSubClassTypes(string path)
        {
            subTypes.Clear();

            foreach (var file in Directory.GetFiles(path, "*.dll"))
            {
                subTypes.AddRange(Reflection.GetInterfaceSubclassTypes(Path.Combine(path, file), typeof(T)));
            }
        }

        public T CreateInstance(string typeName, object[] args)
        {
            foreach (var type in subTypes)
            {
                if (type.FullName == typeName)
                {
                    try
                    {
                        return Activator.CreateInstance(type, args) as T;                
                    }
                    catch( Exception e)
                    {
                        LocalStaticLogger.WriteLine(e.ToString());
                    }
                }
            }
            return null;
        }

        public T CreateInstance(string typeName)
        {
            return CreateInstance(typeName, new object[] { });
        }
    }
}

