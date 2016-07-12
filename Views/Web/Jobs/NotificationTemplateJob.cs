using KarmicEnergy.Core.Persistence;
using Quartz;
using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.IO;

namespace KarmicEnergy.Web.Jobs
{
    public class NotificationTemplateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create())
            {
                var notificationTemplates = KEUnitOfWork.NotificationTemplateRepository.GetAllActive();

                foreach (var notificationTemplate in notificationTemplates)
                {
                    var path = ConfigurationManager.AppSettings["Notification:PathTemplate"];
                    var pathfilename = String.Format("{0}\\{1}\\{2}.{3}", path, "Email", notificationTemplate.Name, "html");
                    if (File.Exists(pathfilename))
                    {
                        try
                        {
                            var message = File.ReadAllText(pathfilename);
                            notificationTemplate.Message = message;
                            KEUnitOfWork.NotificationTemplateRepository.Update(notificationTemplate);
                            KEUnitOfWork.Complete();
                        }
                        catch (DbEntityValidationException dex)
                        {
                            foreach (var eve in dex.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);

                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
        }
    }
}
