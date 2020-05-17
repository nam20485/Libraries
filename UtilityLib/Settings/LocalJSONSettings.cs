using System;
using Libraries.UtilityLib.JSON;
using Libraries.UtilityLib.Storage;

namespace Libraries.UtilityLib.Settings
{
    public abstract class LocalJSONSettings<T> : LocalSettings<T, JSONSerializer<T>>
        where T : LocalJSONSettings<T>, new()
    {
     
    }
}

