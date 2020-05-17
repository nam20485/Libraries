using System;
using Libraries.UtilityLib.Callback;

namespace Libraries.UtilityLib.File
{
    public class WriteToFileInvocation : CallbackInvocation<WriteToFileInvocation.WriteToFileArgs>
    {
        public WriteToFileInvocation(WriteToFileArgs arguments)
            : base(arguments)
        {
        }

        #region implemented abstract members of CallbackInvocation

        protected override void Callback(WriteToFileArgs args)
        {
            if (args != null)
            {
                args.Writer.WriteToFile(args.Filename, args.Str, args.Append);
            }
        }

        #endregion
               
        public class WriteToFileArgs : CallbackArguments
        {
            public AsyncFileWriter Writer { get; protected set; }
            public string Filename { get; protected set; }
            public string Str { get; protected set; }
            public bool Append { get; protected set; }

            public WriteToFileArgs(string filename, string str, bool append, AsyncFileWriter writer)
            {
                Writer = writer;
                Filename = filename;
                Str = str;
                Append = append;
            }
        }
    }
}

