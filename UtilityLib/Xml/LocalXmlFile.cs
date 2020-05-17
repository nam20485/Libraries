using System;
using Libraries.UtilityLib.Storage;

namespace Libraries.UtilityLib.Xml
{
    public abstract class LocalXmlFile<T> : LocalFile<T, XmlSerializer<T>>
        where T : LocalXmlFile<T>, new()
    {
    }
}