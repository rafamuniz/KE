using KarmicEnergy.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            timer.Elapsed += CheckEvents_Tick;
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

        private void CheckEvents_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Enabled = false;
                KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();

                var events = KEUnitOfWork.SensorItemEventRepository.GetAllActive().ToList();

                if (sensors.Any())
                {
                    List<ManualResetEvent> handles = new List<ManualResetEvent>();

                    foreach (var evt in sensors)
                    {
                        //GetData(sensor);
                        // Create an MRE for each thread.
                        //var handle = new ManualResetEvent(false);

                        // Store it for use below.
                        //handles.Add(handle);

                        //ThreadPool.QueueUserWorkItem(new WaitCallback(GetData), Tuple.Create(sensor, handle));

                        //Thread _thread = new Thread(GetData);
                        //_thread.Name = sensor.Name;
                        //_thread.IsBackground = true;
                        //_thread.Start();
                    }

                    //WaitHandle.WaitAll(handles.ToArray());
                }

                Logger.WriteLog("Service has been done successfully");
                //EventLog.WriteEntry("Service has been done successfully");
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
                //EventLog.WriteEntry(ex.Message);
            }
            finally
            {
                timer.Enabled = true;
            }
        }

        private String GetAppConfigValue(String key)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetAssembly(typeof(ProjectInstaller)).Location);
            if (config.AppSettings.Settings[key] == null)
                return null;
            else
                return config.AppSettings.Settings[key].Value;
        }
    }
}
