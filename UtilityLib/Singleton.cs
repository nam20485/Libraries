using System;

namespace Libraries.UtilityLib
{
    public class Singleton<T> where T : new()
    {
        public static T Instance { get; protected set; }

        static Singleton()
        {
            Instance = new T();
        }
    }
}

