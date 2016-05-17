using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SiteService
{
    public partial class KE_Sync_Sensors : ServiceBase
    {
        private System.Timers.Timer timer = null;
        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        List<ManualResetEvent> handles = null;

        public KE_Sync_Sensors()
        {
            AutoLog = false;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new System.Timers.Timer();
            timer.Interval = Double.Parse(ConfigurationManager.AppSettings["Interval"]);
            timer.Elapsed += SyncSensors_Tick;
            timer.Enabled = true;
            Logger.WriteLog("Service started");
            EventLog.WriteEntry("Service started");
        }

        protected override void OnStop()
        {
            //_shutdownEvent.Set();
            //if (!_thread.Join(3000))// give the thread 3 seconds to stop
            //{
            //    _thread.Abort();
            //}
            //ThreadPool.

            if (handles != null && handles.Any())
            {
                WaitHandle.WaitAll(handles.ToArray());

                foreach (var handle in handles)
                {
                    handle.Set();
                    //handle.Close();                    
                }
            }

            Logger.WriteLog("Service stopped");
            EventLog.WriteEntry("Service stopped");
        }

        private void SyncSensors_Tick(object sender, ElapsedEventArgs e)
        {
            KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();

            var sensors = KEUnitOfWork.SensorRepository.GetAllActive().ToList();

            if (sensors.Any())
            {
                List<ManualResetEvent> handles = new List<ManualResetEvent>();

                foreach (var sensor in sensors)
                {
                    // Create an MRE for each thread.
                    var handle = new ManualResetEvent(false);

                    // Store it for use below.
                    handles.Add(handle);

                    ThreadPool.QueueUserWorkItem(new WaitCallback(GetData), Tuple.Create(sensor, handle));

                    //Thread _thread = new Thread(GetData);
                    //_thread.Name = sensor.Name;
                    //_thread.IsBackground = true;
                    //_thread.Start();
                }

                WaitHandle.WaitAll(handles.ToArray());
            }

            Logger.WriteLog("Service has been done successfully");
            EventLog.WriteEntry("Service has been done successfully");
        }

        private void GetData(Object stateInfo)
        {
            var tuple = (Tuple<Sensor, ManualResetEvent>)stateInfo;
            Sensor sensor = tuple.Item1;

            try
            {
                String startMessage = String.Format("{0} start collection", sensor.Name);
                Logger.WriteLog(startMessage);
                EventLog.WriteEntry(startMessage);

                // eg: !01002*M65001~
                //cmdstr = "!" + tanks.Tables(0).Rows(tankindex).Item("tdefSensorID") + "*" + "M" + SiteID + "~"

                String command = String.Format("!{0}*M{1}~", sensor.Reference, "SITEID");
                SerialPort serialPort = new SerialPort();
                serialPort.WriteLine(command);

                String message = "No response";
                message = serialPort.ReadLine();

                if (message != "No response")
                {
                    SaveData(sensor, message);
                    EventLog.WriteEntry(String.Format("{0} responds: {1}", sensor.Name, command, EventLogEntryType.Information));
                }
                else
                {
                    EventLog.WriteEntry(String.Format("Error - {0} did not respond: {1}", sensor.Name, command, EventLogEntryType.Error));
                }

                String endMessage = String.Format("{0} finish collection", sensor.Name);
                Logger.WriteLog(endMessage);
                EventLog.WriteEntry(endMessage);
            }
            catch (Exception ex)
            {
                String errorMessage = String.Format("{0} error collection\n{1}", sensor.Name, ex.Message);
                Logger.WriteLog(errorMessage);
                EventLog.WriteEntry(errorMessage);

                Environment.Exit(1);
            }
            finally
            {
                // Signal that the work is done...even if an exception occurred.
                // Otherwise, PerformTimerOperation() will block forever.
                ManualResetEvent mreEvent = tuple.Item2;
                mreEvent.Set();
            }
        }

        private void SaveData(Sensor sensor, String response)
        {
            KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();

            //SensorItemEvent lastEvent = KEUnitOfWork.SensorItemEventRepository.Get();

            SensorItemEvent SensorItemEvent = new SensorItemEvent()
            {
                SensorItemId = Guid.NewGuid(),
                Value = "",
                CalculatedValue = ""
            };

            KEUnitOfWork.SensorItemEventRepository.Add(SensorItemEvent);
            KEUnitOfWork.Complete();
        }
    }
}
