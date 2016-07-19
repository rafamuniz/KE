using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Service;
using Munizoft.Extensions;
using Munizoft.Util.WinServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace KarmicEnergy.Services
{
    public partial class AlarmService : ServiceBase
    {
        private System.Timers.Timer timer = null;

        public AlarmService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Double interval = 5000;
            Double.TryParse(FileConfig.GetAppConfigValue("Interval"), out interval);

            timer = new System.Timers.Timer();
            timer.Interval = interval;
            timer.Elapsed += ProcessEvents_Tick;
            timer.Enabled = true;
            timer.AutoReset = false;
            Logger.WriteLog("Service started");
        }

        protected override void OnStop()
        {
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        private void ProcessEvents_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Enabled = false;
                KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();
                var events = KEUnitOfWork.SensorItemEventRepository.GetsToCheckAlarm().ToList();

                if (events.Any())
                {
                    foreach (var evt in events)
                    {
                        try
                        {
                            Logger.WriteLog(String.Format("Start - Check Event has alarm: {0} - {1}", evt.Id, evt.Value));
                            CheckAlarm(evt);
                            evt.CheckedAlarmDate = DateTime.UtcNow;
                            KEUnitOfWork.SensorItemEventRepository.Update(evt);
                            KEUnitOfWork.Complete();
                            Logger.WriteLog(String.Format("End - Check Event has alarm: {0} - {1}", evt.Id, evt.Value));
                        }
                        catch
                        {

                        }
                    }
                }

                Logger.WriteLog("Service has been done successfully");
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
            finally
            {
                timer.Enabled = true;
            }
        }

        private void CheckAlarm(SensorItemEvent sensorItemEvent)
        {
            KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();
            var triggers = KEUnitOfWork.TriggerRepository.Find(x => x.SensorItemId == sensorItemEvent.SensorItemId).ToList();

            foreach (var trigger in triggers)
            {
                try
                {
                    String value = sensorItemEvent.ConverterItemUnit();
                    if (trigger.IsAlarm(value))
                    {
                        if (trigger.HasAlarm) // Normalized
                        {
                            var alarms = KEUnitOfWork.AlarmRepository.Find(x => x.TriggerId == trigger.Id && x.EndDate == null);

                            foreach (var a in alarms)
                            {
                                a.EndDate = DateTime.UtcNow;
                            }

                            KEUnitOfWork.AlarmRepository.UpdateRange(alarms);
                        }

                        Alarm alarm = new Alarm()
                        {
                            TriggerId = trigger.Id,
                            SensorItemEventId = sensorItemEvent.Id,
                            Value = sensorItemEvent.Value
                        };

                        KEUnitOfWork.AlarmRepository.Add(alarm);
                        SendNotificationAlarm(trigger, sensorItemEvent, value);
                    }
                    else if (trigger.HasAlarm) // Normalized
                    {
                        var alarms = KEUnitOfWork.AlarmRepository.Find(x => x.TriggerId == trigger.Id && x.EndDate == null);

                        foreach (var a in alarms)
                        {
                            a.EndDate = DateTime.UtcNow;
                        }

                        KEUnitOfWork.AlarmRepository.UpdateRange(alarms);
                        SendNotificationAlarmNormalized(trigger, sensorItemEvent);
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(String.Format("ERROR - Event Value: {0} - {1} - Trigger: {2} - {3}", sensorItemEvent.Id, sensorItemEvent.Value, trigger.Id, ex.Message), KarmicEnergy.Service.LogTypeEnum.Error);
                    throw ex;
                }
            }

            KEUnitOfWork.Complete();
        }

        private void SendNotificationAlarm(Trigger trigger, SensorItemEvent sensorItemEvent, String value)
        {
            KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();
            var templateName = FileConfig.GetAppConfigValue("Email:TemplateAlarm");
            var template = KEUnitOfWork.NotificationTemplateRepository.Find(x => x.Name == templateName && x.NotificationTypeId == (Int16)NotificationTypeEnum.Email).SingleOrDefault();
            var triggerContacts = KEUnitOfWork.TriggerContactRepository.GetsByTrigger(trigger.Id);

            String message = MountEmailBody(trigger, sensorItemEvent, value, template);

            foreach (var triggerContact in triggerContacts)
            {
                String email = String.Empty;

                if (triggerContact.UserId.HasValue)
                {
                    var user = KEUnitOfWork.CustomerUserRepository.Get(triggerContact.UserId.Value);
                    email = user.Address.Email;
                }
                else
                {
                    var contact = KEUnitOfWork.ContactRepository.Get(triggerContact.ContactId.Value);
                    email = contact.Address.Email;
                }

                Notification notification = new Notification()
                {
                    To = email,
                    From = FileConfig.GetAppConfigValue("Email:From"),
                    NotificationTypeId = (Int16)NotificationTypeEnum.Email,
                    Subject = "Alarm",
                    Message = message
                };

                KEUnitOfWork.NotificationRepository.Add(notification);
            }

            KEUnitOfWork.Complete();
        }

        private static string MountEmailBody(Trigger trigger, SensorItemEvent sensorItemEvent, string value, NotificationTemplate template)
        {
            // Replace Data
            Dictionary<String, String> replacments = new Dictionary<string, string>();

            if (trigger.SensorItem.Sensor.PondId.HasValue)
            {
                replacments.Add("[PONDNAME]", trigger.SensorItem.Sensor.Pond.Name);
                replacments.Add("[SITENAME]", trigger.SensorItem.Sensor.Pond.Site.Name);
            }
            else
            {
                replacments.Add("[PONDNAME]", String.Empty);
            }

            if (trigger.SensorItem.Sensor.TankId.HasValue)
            {
                replacments.Add("[TANKNAME]", trigger.SensorItem.Sensor.Tank.Name);
                replacments.Add("[SITENAME]", trigger.SensorItem.Sensor.Tank.Site.Name);
            }
            else
            {
                replacments.Add("[TANKNAME]", String.Empty);
            }

            if (trigger.SensorItem.Sensor.SiteId.HasValue)
            {
                replacments.Add("[SITENAME]", trigger.SensorItem.Sensor.Site.Name);
            }

            replacments.Add("[SENSORNAME]", trigger.SensorItem.Sensor.Name);
            replacments.Add("[ITEMNAME]", trigger.SensorItem.Item.Name);
            replacments.Add("[VALUE]", String.Format("{0} {1}", value, trigger.SensorItem.Unit.Symbol));
            replacments.Add("[STARTDATE]", sensorItemEvent.EventDate.ToLocalTime().ToString());

            var message = template.Message.Replace(replacments);
            return message;
        }

        private void SendNotificationAlarmNormalized(Trigger trigger, SensorItemEvent sensorItemEvent)
        {
            KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();

            var triggerContacts = KEUnitOfWork.TriggerContactRepository.GetsByTrigger(trigger.Id);

            foreach (var triggerContact in triggerContacts)
            {
                String email = String.Empty;

                if (triggerContact.UserId.HasValue)
                {
                    var user = KEUnitOfWork.CustomerUserRepository.Get(triggerContact.UserId.Value);
                    email = user.Address.Email;
                }
                else
                {
                    var contact = KEUnitOfWork.ContactRepository.Get(triggerContact.ContactId.Value);
                    email = contact.Address.Email;
                }

                Notification notification = new Notification()
                {
                    To = email,
                    From = FileConfig.GetAppConfigValue("Email:From"),
                    NotificationTypeId = (Int16)NotificationTypeEnum.Email,
                    Subject = "Normalized"
                };

                KEUnitOfWork.NotificationRepository.Add(notification);
            }

            KEUnitOfWork.Complete();
        }
    }
}