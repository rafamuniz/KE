using System;
using System.Threading;

namespace KarmicEnergy.Web.App_Start
{
    public class ScheduleTask
    {
        #region Property

        private static DateTime whenTaskLastRan;

        #endregion Property

        public static void RegisterTasks()
        {
            //whenTaskLastRan = DateTime.Now;

            Thread thread = new Thread(
                new ThreadStart(SendNotifications));
            thread.IsBackground = true;
            thread.Name = "SendNotifications";
            thread.Start();
        }

        public static void SendNotifications()
        {

        }

        public static void ConfigureNotificationTemplates()
        {

        }
    }
}