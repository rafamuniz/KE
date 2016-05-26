using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Timers;

namespace SiteService
{
    public partial class SensorsSyncService : ServiceBase
    {
        private System.Timers.Timer timer = null;
        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        List<ManualResetEvent> handles = null;

        public SensorsSyncService()
        {
            AutoLog = false;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Double interval = 5000;
            Double.TryParse(GetAppConfigValue("Interval"), out interval);

            timer = new System.Timers.Timer();
            timer.Interval = interval;
            timer.Elapsed += SyncSensors_Tick;
            timer.Enabled = true;
            timer.AutoReset = false; // makes it fire only once
            Logger.WriteLog("Service started");
            //EventLog.WriteEntry("Service started");
        }

        public void OnDebug()
        {
            OnStart(null);
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
            //EventLog.WriteEntry("Service stopped");
        }

        private void SyncSensors_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Enabled = false;
                KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();

                var sensors = KEUnitOfWork.SensorRepository.GetAllActive().ToList();

                if (sensors.Any())
                {
                    List<ManualResetEvent> handles = new List<ManualResetEvent>();

                    foreach (var sensor in sensors)
                    {
                        GetData(sensor);
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

        /// <summary>
        /// Communicate with Serial Port and execute a command
        /// eg: !01002*M65001~
        /// cmdstr = "!" + tanks.Tables(0).Rows(tankindex).Item("tdefSensorID") + "*" + "M" + SiteID + "~"
        /// </summary>
        /// <param name="sensor"></param>
        //private void GetData(Object stateInfo)
        private void GetData(Sensor sensor)
        {
            //var tuple = (Tuple<Sensor, ManualResetEvent>)stateInfo;
            //Sensor sensor = tuple.Item1;
            Site site = null;
            SerialPort serialPort = null;
            String message = String.Empty;

            try
            {
                String startMessage = String.Format("{0} start collection", sensor.Name);
                Logger.WriteLog(startMessage);

                if (sensor.SiteId.HasValue)
                    site = sensor.Site;
                else if (sensor.TankId.HasValue)
                    site = sensor.Tank.Site;

                String command = String.Format("!{0}*M{1}~", sensor.Reference, site.Id);
                serialPort = CreatePortWithSettings();
                Logger.WriteLog(String.Format("Serial Port: {0} - {1} - {2} - {3} - {4} - {5}", serialPort.PortName, serialPort.BaudRate, serialPort.Parity, serialPort.DataBits, serialPort.StopBits, serialPort.ReadTimeout));


#if (!DEBUG)
                serialPort.Open();
                serialPort.WriteLine(command);

                message = "No response";
                message = serialPort.ReadLine();

#else
                message = "!65001*R274~";
#endif

                if (message != "No response")
                {
                    SaveData(sensor, message);
                    String messageResponse = String.Format("{0} responds: {1}", sensor.Name, command);
                    //EventLog.WriteEntry(messageResponse, EventLogEntryType.Information);
                    Logger.WriteLog(messageResponse);
                }
                else
                {
                    String messageResponse = String.Format("Error - {0} did not respond: {1}", sensor.Name, command);
                    //EventLog.WriteEntry(messageResponse, EventLogEntryType.Error);
                    Logger.WriteLog(messageResponse);
                }

                String endMessage = String.Format("{0} finish collection", sensor.Name);
                Logger.WriteLog(endMessage);
                EventLog.WriteEntry(endMessage);
            }
            catch (Exception ex)
            {
                String errorMessage = String.Format("{0} error collection - {1}", sensor.Name, ex.Message);
                Logger.WriteLog(errorMessage);
                //Environment.Exit(1);
            }
            finally
            {
#if (!DEBUG)
                if (serialPort != null && serialPort.IsOpen)
                    serialPort.Close();
#endif
                String startMessage = String.Format("{0} stop collection", sensor.Name);
                Logger.WriteLog(startMessage);

                // Signal that the work is done...even if an exception occurred.
                // Otherwise, PerformTimerOperation() will block forever.
                //ManualResetEvent mreEvent = tuple.Item2;
                //mreEvent.Set();
            }
        }

        /// <summary>
        /// Response format: !65001*LI01002R274A1899W1899V4935~
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="response"></param>
        private void SaveData(Sensor sensor, String response)
        {
            try
            {
                KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();

                String itemCode = String.Empty;
                String sensorValue = String.Empty;
                String sensorCalculatedValue = String.Empty;

                SensorItem sensorItem = KEUnitOfWork.SensorItemRepository.Find(x => x.Item.Code == itemCode && x.SensorId == sensor.Id).Single();

                SensorItemEvent lastEvent = KEUnitOfWork.SensorItemEventRepository.Find(x => x.SensorItemId == sensorItem.Id).OrderByDescending(o => o.RowVersion).Single();

                if (lastEvent == null || lastEvent.Value != sensorValue)
                {
                    if (itemCode == "R")
                    {
                        var tank = sensorItem.Sensor.Tank;
                        sensorCalculatedValue = tank.CalculateWaterRemaining(Decimal.Parse(sensorValue)).ToString();
                    }

                    SensorItemEvent SensorItemEvent = new SensorItemEvent()
                    {
                        SensorItemId = sensorItem.Id,
                        Value = sensorValue,
                        CalculatedValue = sensorCalculatedValue
                    };

                    KEUnitOfWork.SensorItemEventRepository.Add(SensorItemEvent);
                    KEUnitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Error: SaveData - " + ex.Message);
                EventLog.WriteEntry("Error: SaveData - " + ex.Message);
            }
        }

        private SerialPort CreatePortWithSettings()
        {
            SerialPort serialPort = new SerialPort();

            if (GetAppConfigValue("PortName") != null)
            {
                serialPort.PortName = GetAppConfigValue("PortName").ToUpper();
            }
            else
            {
                serialPort.PortName = "COM1";
            }

            if (GetAppConfigValue("BaudRate") != null)
            {
                Int32 baudRate = 9600;
                Int32.TryParse(GetAppConfigValue("BaudRate"), out baudRate);
                serialPort.BaudRate = baudRate;
            }
            else
            {
                serialPort.BaudRate = 9600;
            }

            if (GetAppConfigValue("Parity") != null)
            {
                var parity = GetAppConfigValue("Parity").ToUpper();
                switch (parity)
                {
                    case "EVEN":
                        serialPort.Parity = Parity.Even;
                        break;
                    case "MARK":
                        serialPort.Parity = Parity.Mark;
                        break;
                    case "ODD":
                        serialPort.Parity = Parity.Odd;
                        break;
                    case "SPACE":
                        serialPort.Parity = Parity.Space;
                        break;
                    case "NONE":
                        serialPort.Parity = Parity.None;
                        break;
                    default:
                        serialPort.Parity = Parity.None;
                        break;
                }
            }
            else
            {
                serialPort.Parity = Parity.None;
            }

            if (GetAppConfigValue("DataBits") != null)
            {
                Int32 dataBits = 8;
                Int32.TryParse(GetAppConfigValue("DataBits"), out dataBits);
                serialPort.DataBits = dataBits;
            }
            else
            {
                serialPort.DataBits = 8;
            }


            if (GetAppConfigValue("StopBits") != null)
            {
                var stopBits = GetAppConfigValue("StopBits");
                switch (stopBits)
                {
                    case "0":
                        serialPort.StopBits = StopBits.None;
                        break;
                    case "1":
                        serialPort.StopBits = StopBits.One;
                        break;
                    case "1.5":
                        serialPort.StopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        serialPort.StopBits = StopBits.Two;
                        break;
                    default:
                        serialPort.StopBits = StopBits.One;
                        break;
                }
            }
            else
            {
                serialPort.StopBits = StopBits.One;
            }

            if (GetAppConfigValue("ReadTimeout") != null)
            {
                Int32 readtimeout = 5000;
                Int32.TryParse(GetAppConfigValue("ReadTimeout"), out readtimeout);
                serialPort.ReadTimeout = readtimeout;
            }
            else
            {
                serialPort.ReadTimeout = 5000;
            }

            return serialPort;
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
