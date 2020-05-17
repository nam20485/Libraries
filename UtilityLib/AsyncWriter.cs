using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Libraries.UtilityLib
{  
    public abstract class AsyncWriter<T>
    {
        public abstract void WriteObject(T obj, Stream s);

        public bool AsyncWrite(T obj, Stream stream)
        {
            return ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncWriteInvoker), new WriteArgs(obj, stream));
        }        

        protected void AsyncWriteInvoker(object state)
        {
            WriteArgs args = state as WriteArgs;
            if (args != null)
            {
                WriteObject(args.Object, args.Stream);
                //args.Stream.Close();
            }
        }

        protected class WriteArgs
        {
            public T Object { get; protected set; }
            public Stream Stream { get; protected set; }

            public WriteArgs(T obj, Stream stream)
            {
                Object = obj;
                Stream = stream;
            }
        }
    }
}
