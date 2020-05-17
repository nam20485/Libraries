using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Libraries.UtilityLib.Windows
{
    public static class DisplayDetection
    {
        public static class Screens
        {
            public static Screen GetScreen(Control control)
            {                                 
                // http://msdn.microsoft.com/en-us/library/system.windows.forms.screen.fromcontrol.aspx:
                // A Screen for the display that contains the largest region of the specified control. 
                // In multiple display environments where no display contains the control, the display 
                // closest to the specified control is returned.
                return Screen.FromControl(control);
            }

            public static int GetScreenNumber(Control control)
            {
                Screen screen = GetScreen(control);
                if (screen != null)
                {
                    for (int i = 0; i < Screen.AllScreens.Length; i++)
                    {
                        if (screen == Screen.AllScreens[i])
                        {
                            return i;
                        }
                    }
                }

                return -1;
            }

            public static int GetScreenCount()
            {
                return Screen.AllScreens.Length;
            }

            public static bool IsOnPrimaryScreen(Control control)
            {
                Screen screen = GetScreen(control);
                if (screen != null)
                {
                    return screen.Primary;
                }

                return false;
            }
        }      

        public static class SystemInfo
        {
            public static bool IsTerminalServerSession()
            {
                return SystemInformation.TerminalServerSession;
            }
        }

        public static class EnvironmentVariables
        {
            public static bool SessionNameStartsWithRdp()
            {
                bool sessionNameStartWithRdp = false;

                string sessionName = GetSessionName();
                if (!string.IsNullOrWhiteSpace(sessionName))
                {
                    // http://stackoverflow.com/questions/973802/detecting-remote-desktop-connection
                    // The system variable %sessionname% will return Console if its local or RDP* if its remote.
                    
                    sessionNameStartWithRdp = sessionName.StartsWith("RDP-");
                }

                return sessionNameStartWithRdp;
            }

            public static string GetSessionName()
            {
                // http://support.microsoft.com/kb/2509192
                // When connecting remotely with Remote Desktop Connection, the environment variables CLIENTNAME 
                // and SESSIONNAME are added to each process that is started. If you set the Folder Option 
                // "Launch folder windows in a separate process" and later launch an application from an additional 
                // Explorer window, the application will not see these additional environment variables.
                                 
                string sessionName = string.Empty;
                var e = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);
                if (e != null)
                {
                    sessionName = e["SESSIONNAME"] as string;
                }

                return sessionName;
            }
        }

        public static class SystemMetrics
        {
            public static bool SessionIsRemote()
            {
                int result = Win32API.GetSystemMetrics(Win32API.SystemMetric.SM_REMOTESESSION);
                return (result != 0);
            }        
        }     
    }
}
