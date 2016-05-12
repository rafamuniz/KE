using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.TankReport
{
    public class Report
    {
        #region Constructor
        public Report()
        {

        }
        #endregion Constructor

        #region Property

        public Guid SiteId { get; set; }

        public Guid TankId { get; set; }

        public String TankName { get; set; }

        public Decimal WaterVolumeCapacity { get; set; }

        public Decimal? WaterVolume { get; set; }
        public String WaterVolumePercentage
        {
            get
            {
                if (WaterVolume.HasValue)
                {
                    return ((WaterVolume.Value / WaterVolumeCapacity)).ToString("P2");
                }

                return "0 %";
            }
            private set { }
        }

        public DateTime? WaterVolumeEventDate { get; set; }

        public Decimal? WaterTemperature { get; set; }
        public DateTime? WaterTemperatureEventDate { get; set; }

        public Decimal? WeatherTemperature { get; set; }
        public DateTime? WeatherTemperatureEventDate { get; set; }

        public Int32? Alarms { get; set; }

        #endregion Property

        #region Map

        public static List<Report> Map(List<Core.Entities.Tank> entities)
        {
            List<Report> vms = new List<Report>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(Report.Map(c)));
            }

            return vms;
        }

        public static Report Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, Report>();
            return Mapper.Map<Core.Entities.Tank, Report>(entity);
        }

        #endregion Map
    }
}