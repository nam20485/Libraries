using System;
using Libraries.UtilityLib.JSON;
using Libraries.UtilityLib.Xml;

namespace Libraries.UtilityLib.JSON
{
    public abstract class LocalJSONFile<T> : LocalFile<T, JSONSerializer<T>>
        where T : LocalJSONFile<T>, new()
    {
    }
}

