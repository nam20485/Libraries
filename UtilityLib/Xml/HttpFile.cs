using System;
using Libraries.UtilityLib.Storage;

namespace Libraries.UtilityLib.Xml
{
    public abstract class HttpFile<T, S> : FormattedFile<T, S, HttpStorage<T, S>>
        where T : HttpFile<T, S>, new()
        where S : Serializer<T>, new()
    {
      
    }
}

