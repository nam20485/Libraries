using System;
using Libraries.UtilityLib.Xml;
using Libraries.UtilityLib.Storage;

namespace Libraries.UtilityLib.Settings
{
    public abstract class LocalXmlSettings<T> : LocalSettings<T, XmlSerializer<T>>
        where T : LocalXmlSettings<T>, new()
    {
     
    }
}

