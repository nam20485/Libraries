using System;
using Libraries.UtilityLib.Settings;
using Libraries.UtilityLib.Storage;

namespace Libraries.UtilityLib.Settings
{
    public abstract class LocalSettings<T, S> : MapSettings<T, S, LocalStorage<T, S>>
        where T : LocalSettings<T, S>, new()
        where S : Serializer<T>, new()
    {      
    }
}

