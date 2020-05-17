using System;
using Libraries.UtilityLib.Storage;

namespace Libraries.UtilityLib.Xml
{
    public abstract class LocalFile<T, S> : FormattedFile<T, S, LocalStorage<T, S>>
        where T : LocalFile<T, S>, new()
        where S : Serializer<T>, new()
    {
      
    }
}

