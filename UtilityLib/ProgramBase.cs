using System;
using Libraries.UtilityLib;
using System.Windows.Forms;
using Libraries.UtilityLib.Logger;
using System.Threading;

namespace Libraries.UtilityLib
{
    public class ProgramBase : Singleton<ProgramBase>
    {
        public virtual void Run()
        {
            // implement in derived class if you want your program to do something
        }

        public static int Run(string[] args)
        {
            return Instance.RunMain(args);
        }

        public int RunMain(string[] args)
        {
            Application.ThreadException += HandleThreadException;

            using (SingleInstance singleInstance = new SingleInstance())
            {
                if (singleInstance.IsFirstInstance())
                {
                    try
                    {
                        Run();
                    }
                    catch (Exception e)
                    {
                        LocalStaticLogger.WriteLine(e.ToString());
                    }
                }
                else
                {
                    singleInstance.BringFirstInstanceToFront();
                }
            }

            LocalStaticLogger.Stop();

            return 0;
        }

        static void HandleThreadException (object sender, ThreadExceptionEventArgs e)
        {
            LocalStaticLogger.WriteLine(e.ToString());
        }       
    }

}
