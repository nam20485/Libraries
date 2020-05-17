using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libraries.UtilityLib.File;

namespace Libraries.UtilityLib.Logger
{
    public class StaticLogger<T> : Singleton<Logger<T>>
        where T : FileWriter, new()
    {
        public static void Write(string s)
        {
            Instance.Write(s);
        }

        public static void WriteLine(string s)
        {
            Instance.WriteLine(s);
        }

        public static void Stop()
        {
            Instance.Stop();
        }
    }
}
