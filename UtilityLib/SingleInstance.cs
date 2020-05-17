using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using Libraries.UtilityLib.Disposeable;
using Libraries.UtilityLib.Windows;

namespace Libraries.UtilityLib
{
    public class SingleInstance : ManagedResource
    {
        protected const string SHARED_HANDLES_NAME = "MONO_ENABLE_SHM";
        protected const string SHARED_HANDLES_VAL = "1";

        protected Mutex singleInstanceMutex;
        protected bool createdNew = false; 

        public string MutexName { get { return Assembly.GetCallingAssembly().FullName; } }

        public bool IsFirstInstance()
        {
            Environment.SetEnvironmentVariable(SHARED_HANDLES_NAME, SHARED_HANDLES_VAL, EnvironmentVariableTarget.Process);

            // lazy initialization
            if (singleInstanceMutex == null)
            {
                singleInstanceMutex = new Mutex(true, MutexName, out createdNew);
                if (singleInstanceMutex != null)
                {
                    Resource = singleInstanceMutex;
                }
            }

            return createdNew;
        }

        public bool BringFirstInstanceToFront()
        {            
            using (var current = Process.GetCurrentProcess())
            {
                foreach (var process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {
                        if (process.MainWindowHandle != IntPtr.Zero)
                        {
                            return Win32API.SetForegroundWindow(process.MainWindowHandle);
                        }                        
                    }
                }
            }

            return false;
        }

        private static bool IsApplicationRunningOnMono(string processName)
        { 
            var processFound = 0; 

            Process[] monoProcesses; 
            ProcessModuleCollection processModuleCollection; 

            //find all processes called 'mono', that's necessary because our app runs under the mono process! 
            monoProcesses = Process.GetProcessesByName("mono"); 

//            foreach (var p in monoProcesses)
//            {
//                processModuleCollection = p.Modules;            

            for (var i = 0; i <= monoProcesses.GetUpperBound(0); ++i) 
            { 
                processModuleCollection = monoProcesses[i].Modules; 

                for (var j = 0; j < processModuleCollection.Count; ++j) 
                { 
                    if (processModuleCollection[j].FileName.EndsWith(processName)) 
                    { 
                        processFound++; 
                    } 
                } 
            } 

            //we don't find the current process, but if there is already another one running, return true! 
            return (processFound == 1); 
        } 
    }
}
