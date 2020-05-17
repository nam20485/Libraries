using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libraries.UtilityLib.Notification;

namespace Libraries.UtilityLib
{
    public abstract class Constants
    {
        public readonly static Uri CurrentDirectory = ".".FromFileSystemPath();        
    }
}
