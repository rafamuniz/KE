using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Util.Notifications;
using Quartz;
using System;
using System.Linq;

namespace KarmicEnergy.Core.Jobs
{
    public class NotificationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create())
            {
                var notifications = KEUnitOfWork.NotificationRepository.Find(x => x.SentSuccessDate == null && x.ErrorMessage == null).ToList();

                foreach (var notification in notifications)
                {
                    try
                    {
                        if (notification.NotificationTypeId == (Int16)NotificationTypeEnum.Email)
                        {
                            EmailService.Send(notification.From, notification.Subject, notification.Message, notification.To);
                        }
                        else if (notification.NotificationTypeId == (Int16)NotificationTypeEnum.SMS)
                        {

                        }

                        notification.SentSuccessDate = DateTime.UtcNow;
                        KEUnitOfWork.NotificationRepository.Update(notification);
                    }
                    catch (Exception ex)
                    {
                        notification.ErrorMessage = ex.Message;
                    }
                    finally
                    {
                        KEUnitOfWork.Complete();
                    }
                }
            }
        }
    }
}
