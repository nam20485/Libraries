//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Win32.TaskScheduler;

//namespace Libraries.UtilityLib
//{
//    public class TaskSchedulerUtils
//    {
//        protected const string rootFolder = "\\";

//        public class TaskList : List<Task>   { }

//        public static TaskList GetTasksAroundLastRunTime(TimeSpan time, TimeSpan range)
//        {
//            TaskList found = new TaskList();

//            TimeSpan begin = time - range;
//            TimeSpan end = time + range;

//            foreach (var task in GetAllScheduledTasks())
//            {                
//                if (task.LastRunTime.TimeOfDay.OccursInRange(begin, end))
//                {
//                    found.Add(task);
//                }
//            }

//            return found;
//        }

//        public static TaskList GetAllScheduledTasks()
//        {
//            TaskList tasks = new TaskList();
//            GetTasksBelowRootFolder(ref tasks);
//            return tasks;
//        }     

//        public static void GetTasksBelowRootFolder(ref TaskList tasks)
//        {
//            GetTasksBelowFolder(ref tasks, rootFolder);
//        }

//        public static void GetTasksBelowFolder(ref TaskList tasks, string root)
//        {
//            using (var taskService = new TaskService())
//            {
//                TaskFolder rootFolder = taskService.GetFolder(root);
//                if (rootFolder != null)
//                {
//                    GetTasksBelowFolder(ref tasks, rootFolder);
//                }
//            }
//        }

//        protected static void GetTasksBelowFolder(ref TaskList tasks, TaskFolder root)
//        {
//            // get this folder's tasks
//            tasks.AddRange(root.GetTasks());

//            // subfolders'
//            foreach (var subFolder in root.SubFolders)
//            {
//                GetTasksBelowFolder(ref tasks, subFolder);
//            }
//        }
//    }
//}
