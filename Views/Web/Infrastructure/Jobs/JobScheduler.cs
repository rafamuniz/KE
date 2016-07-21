using Quartz;
using Quartz.Impl;
using System;
using System.Configuration;

namespace KarmicEnergy.Web.Jobs
{
    public class JobScheduler
    {
        public static void RegisterJobs()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            #region DataSync Job
            var syncDataMin = Int32.Parse(ConfigurationManager.AppSettings["Sync:Data"].ToString());

            IJobDetail dataSyncJob = JobBuilder.Create<DataSyncJob>().Build();

            ITrigger dataSyncTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("DataSyncJob")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(syncDataMin)
                    .RepeatForever()
                    .WithRepeatCount(1))
                .Build();

            scheduler.ScheduleJob(dataSyncJob, dataSyncTrigger);
            #endregion DataSync Job

            #region Notification Job
            var syncNotificationMin = Int32.Parse(ConfigurationManager.AppSettings["Sync:Data"].ToString());

            IJobDetail notificationJob = JobBuilder.Create<NotificationJob>().Build();

            ITrigger notificationTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("NotificationJob")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(syncNotificationMin)
                    .RepeatForever()
                    .WithRepeatCount(1))
                .Build();

            scheduler.ScheduleJob(notificationJob, notificationTrigger);
            #endregion Notification Job

            #region Notification Template Job
            IJobDetail notificationTemplateJob = JobBuilder.Create<NotificationTemplateJob>().Build();

            ITrigger notificationTemplateTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("NotificationTemplateJob")
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(1)
                    .RepeatForever()
                    .WithRepeatCount(1))
                .Build();

            scheduler.ScheduleJob(notificationTemplateJob, notificationTemplateTrigger);
            #endregion Notification Template Job

            //.WithDailyTimeIntervalSchedule
            //  (s =>
            //     s.WithIntervalInHours(2)
            //    .OnEveryDay()
            //    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
            //  )
            //.Build();

            scheduler.Start();
        }
    }
}
