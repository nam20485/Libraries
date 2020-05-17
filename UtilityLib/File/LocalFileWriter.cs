using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Libraries.UtilityLib.File
{
    public class LocalFileWriter : FileWriter
    {
        public override void WriteToFile(string path, string s, bool append)
        {
            using (var writer = new StreamWriter(path, append))
            {
                // dispose calls flush and close
                WriteToFile(s, writer.BaseStream);
            }
        }
    }
}
