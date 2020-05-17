using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Libraries.UtilityLib.File
{
    public abstract class FileWriter : AsyncFileWriter
    {
        //public override abstract void WriteToFile(string filename, string str, bool append);

        protected void WriteToFile(string s, Stream stream)
        {
            byte[] bytes = s.ToByteArray();
            stream.Write(bytes, 0, bytes.Length);
        }        
    }
}
