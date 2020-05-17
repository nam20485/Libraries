using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Libraries.UtilityLib.File
{
    public abstract class AsyncFileWriter
    {
        public abstract void WriteToFile(string filename, string str, bool append);

        public bool AsyncWriteToFile(string filename, string str, bool append)
        {
            return new WriteToFileInvocation(new WriteToFileInvocation.WriteToFileArgs(filename, str, append, this)).Invoke();
        }              
    }
}
