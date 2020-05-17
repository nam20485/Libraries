using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Libraries.UtilityLib.File;
using Libraries.UtilityLib.Windows;
using Libraries.UtilityLib.Threading;

namespace Libraries.UtilityLib.Logger
{
    public class Logger<T> : Worker<string> where T : FileWriter, new()
    {
        protected T fileWriter;

        [Flags]
        public enum LogType
        {
            ConsoleOut,
            ConsoleError,
            File
        }

        public string Filename   { get; protected set; }        
        public bool Timestamps   { get; protected set; }        
        public LogType LogTypes  { get; protected set; }
        public bool WriteAsynnc  { get; protected set; }

        protected const string seperator = "********************************************************************************";

        public Logger()
        {
            Timestamps = true;
            WriteAsynnc = false;
            LogTypes = LogType.ConsoleOut |LogType.ConsoleError|LogType.File;

            string filename = WinFormUtils.GetEntryAssemblyFilename(false);
            if (! string.IsNullOrEmpty(filename))
            {
                Filename = filename + ".log.txt";
            }
            else
            {
                Filename = "log.txt";
            }

            fileWriter = new T();

            Start();

            WriteLine(seperator);
            WriteLine("Logger started. Filename=[" + Filename + "]");
        }

        public void Write(string s)
        {
            string timestamp = string.Empty;
            if (Timestamps)
            {
                DateTime localNow = DateTime.Now.ToLocalTime();
                timestamp = string.Format("[{0}]", localNow.ToShortDateString() + " " + localNow.ToShortTimeString());
            }

            string message = string.Format("{0}{1}{2}", timestamp, Timestamps ? " " : string.Empty, s);
            ProduceWorkItem(message);
        }

        public void WriteLine(string s)
        {
            Write(s + Environment.NewLine);
        }

        public override void Stop()
        {
            WriteLine("Logger stopped.");
            WriteLine(seperator);

            base.Stop();
        }     

        protected void WriteLogMessage(string line)
        {
            if ((LogTypes & LogType.ConsoleOut) == LogType.ConsoleOut)
            {
                WriteToConsoleOut(line);
            }
    
            if ((LogTypes & LogType.ConsoleError) == LogType.ConsoleError)
            {
                WriteToConsoleError(line);
            }

            if ((LogTypes & LogType.File) == LogType.File)
            {
                WriteToFile(line);
            }
        }

        protected void WriteToFile(string line)
        {
            try
            {
                if (fileWriter != null)
                {
                    if (WriteAsynnc)
                    {
                        fileWriter.AsyncWriteToFile(Filename, line, true);
                    }
                    else
                    {
                        fileWriter.WriteToFile(Filename, line, true);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
        }

        protected void WriteToConsoleError(string line)
        {
            try
            {
                Console.Error.Write(line);
            }
            catch (Exception)
            {
                // do nothing, try writing to log file next
            }
        }

        protected void WriteToConsoleOut(string line)
        {
            try
            {
                Console.Out.Write(line);
            }
            catch (Exception)
            {
                // do nothing, try writing to log file next
            }
        }      

        #region implemented abstract members of Libraries.UtilityLib.ProducerConsumer
        protected override void HandleThreadProcException(Exception e)
        {
            WriteLogMessage(e.ToString());
        }

        protected override void ConsumeWorkItem(string workItem)
        {
            WriteLogMessage(workItem);
        }
        #endregion       
    }
}
