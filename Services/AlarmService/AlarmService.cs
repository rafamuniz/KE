using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Service;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;

namespace AlarmService
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
            Double.TryParse(GetAppConfigValue("Interval"), out interval);

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
                        Logger.WriteLog(String.Format("Start - Check Event has alarm: {0} - {1}", evt.Id, evt.Value));
                        CheckAlarm(evt);
                        evt.CheckedAlarmDate = DateTime.UtcNow;
                        KEUnitOfWork.SensorItemEventRepository.Update(evt);
                        KEUnitOfWork.Complete();
                        Logger.WriteLog(String.Format("End - Check Event has alarm: {0} - {1}", evt.Id, evt.Value));
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

                    if (trigger.IsAlarm(sensorItemEvent.Value))
                    {
                        Alarm alarm = new Alarm()
                        {
                            TriggerId = trigger.Id,
                            SensorItemEventId = sensorItemEvent.Id,
                            Value = sensorItemEvent.Value
                        };

                        KEUnitOfWork.AlarmRepository.Add(alarm);
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(String.Format("ERROR - Event Value: {0} - {1} - Trigger: {2}", sensorItemEvent.Id, sensorItemEvent.Value, trigger.Id));
                }
            }

            KEUnitOfWork.Complete();
        }

        private void SendNotification(Trigger trigger, SensorItemEvent sensorItemEvent)
        {
            KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();

            var contacts = KEUnitOfWork.TriggerContactRepository.GetsByTrigger(trigger.Id);

            foreach (var contact in contacts)
            {
                //contact.
            }

            KEUnitOfWork.Complete();
        }

        private String GetAppConfigValue(String key)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetAssembly(typeof(Installer)).Location);
            if (config.AppSettings.Settings[key] == null)
                return null;
            else
                return config.AppSettings.Settings[key].Value;
        }
    }
}
