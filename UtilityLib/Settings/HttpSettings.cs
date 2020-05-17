using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libraries.UtilityLib.Storage;

namespace Libraries.UtilityLib.Settings
{
     public abstract class HttpSettings<T, S> : Settings<T, S, HttpStorage<T, S>>
        where T : HttpSettings<T, S>, new()
        where S : Serializer<T>, new()
    {
      
    }
}
