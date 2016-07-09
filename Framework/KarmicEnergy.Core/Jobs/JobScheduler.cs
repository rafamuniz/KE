using Quartz;
using Quartz.Impl;

namespace KarmicEnergy.Core.Jobs
{
    public class JobScheduler
    {
        public static void RegisterJobs()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            #region DataSync Job
            IJobDetail dataSyncJob = JobBuilder.Create<DataSyncJob>().Build();

            ITrigger dataSyncTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("DataSyncJob")
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(1)
                    .RepeatForever()
                    .WithRepeatCount(1))
                .Build();

            scheduler.ScheduleJob(dataSyncJob, dataSyncTrigger);
            #endregion DataSync Job

            #region Notification Job
            IJobDetail notificationJob = JobBuilder.Create<NotificationJob>().Build();

            ITrigger notificationTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("NotificationJob")      
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(5)
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
                    .WithIntervalInMinutes(10)
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
