using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Libraries.UtilityLib.Logger;

namespace Libraries.UtilityLib.Reflection
{
    public class Reflection
    {
        public static Type[] GetInterfaceSubclassTypes(string assemblyFile, Type iface)
        {
            List<Type> types = new List<Type>();
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyFile);
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var i in type.GetInterfaces())
                    {
                        if (i == iface)
                        {
                            types.Add(type);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LocalStaticLogger.WriteLine(e.ToString());
            }
            return types.ToArray();
        }

        public static Type[] GetInheritedClasses<T>()
        {
            return GetInheritedClasses(typeof(T));
        }  

        public static Type[] GetInheritedClasses(Type baseType)
        {
            List<Type> inheritedTypes = new List<Type>();

            Assembly assembly = baseType.Assembly;
            if (assembly != null)
            {            
                Type[] allTypes = assembly.GetTypes();
                if (allTypes != null)
                {
                    foreach (var type in allTypes)
                    {
                        if (type.BaseType == baseType)
                        {
                            inheritedTypes.Add(type);
                        }
                    }
                }
            }

            return inheritedTypes.ToArray();
        }

        public static T[] GetInheritedClassInstances<T>()
        {
            return GetInheritedClassInstances<T>(typeof(T));
        }     

        public static object[] GetInheritedClassInstances(Type baseType)
        {
            return GetInheritedClassInstances<object>(baseType);
        }

        public static T[] GetInheritedClassInstances<T>(Type baseType)
        {
            List<T> instances = new List<T>();

            Type[] inheritedTypes = GetInheritedClasses(baseType);
            foreach (var inheritedType in inheritedTypes)
            {
                T instance = (T) Activator.CreateInstance(inheritedType);
                if (instance != null)
                {
                    instances.Add(instance);
                }
            }

            return instances.ToArray();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethodName()
        {
            return GetMethodNameByStackFrameIndex(0);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCallingMethodName()
        {
            return GetMethodNameByStackFrameIndex(1);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected static string GetMethodNameByStackFrameIndex(int index)
        {
            string name = default(string);

            StackTrace stackTrace = new StackTrace();
            if (stackTrace != null)
            {
                StackFrame currentFrame = stackTrace.GetFrame(index);
                if (currentFrame != null)
                {
                    return currentFrame.GetMethod().Name;
                }
            }

            return name;
        }
    }
}

