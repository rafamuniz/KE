using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using KarmicEnergy.Web.ViewModels;
using Munizoft.Extensions;
using AutoMapper;
using Munizoft.Extensions;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Map
{
    public class SensorViewModel
    {
        #region Constructor
        public SensorViewModel()
        {
            Alarms = new List<AlarmViewModel>();
        }
        #endregion Constructor

        #region Property

        #region Sensor
        public Guid SiteId { get; set; }

        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Latitude { get; set; }
        public String Longitude { get; set; }

        #endregion Sensor

        #region Last Temperature
   
        public Guid? AmbientTemperatureLastEventId { get; set; }
        public Decimal? AmbientTemperatureLastEventValue { get; set; }
        public DateTime? AmbientTemperatureLastEventDate { get; set; }
        public String AmbientTemperatureLastEventUnit { get; set; }

        #endregion Last Temperature

        #region Last Voltage

        public Guid? VoltageLastEventId { get; set; }
        public Decimal? VoltageLastEventValue { get; set; }
        public DateTime? VoltageLastEventDate { get; set; }
        public String VoltageLastEventUnit { get; set; }

        #endregion Last Voltage

        #region Alarms
        public List<AlarmViewModel> Alarms { get; set; }
        public Int32 TotalAlarms
        {
            get
            {
                if (Alarms != null)
                    return Alarms.Count;
                return 0;
            }
            private set { }
        }

        public Boolean HasAlarmCritical
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.Critical).Any();
                return false;
            }
            private set { }
        }

        public Boolean HasAlarmHigh
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.High).Any();
                return false;
            }
            private set { }
        }

        public Boolean HasAlarmMedium
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.Medium).Any();
                return false;
            }
            private set { }
        }

        public Boolean HasAlarmLow
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.Low).Any();
                return false;
            }
            private set { }
        }

        public Boolean HasAlarmInfo
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.Info).Any();
                return false;
            }
            private set { }
        }
        #endregion Alarms

        #endregion Property      

        #region Map

        public static List<SensorViewModel> Map(IEnumerable<Core.Entities.Sensor> entities)
        {
            List<SensorViewModel> vms = new List<SensorViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(SensorViewModel.Map(c)));
            }

            return vms;
        }

        public static SensorViewModel Map(Core.Entities.Sensor entity)
        {
            var viewModel = Mapper.Map<Core.Entities.Sensor, SensorViewModel>(entity);
            return viewModel;
        }

        #endregion Map  
    }
}