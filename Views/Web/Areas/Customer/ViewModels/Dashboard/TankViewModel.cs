using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
{
    public class TankViewModel
    {
        #region Constructor
        public TankViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Guid SiteId { get; set; }
        
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Decimal WaterVolumeCapacity { get; set; }

        public Decimal? WaterVolumeLast { get; set; }

        //public String WaterVolumePercentage
        //{
        //    get
        //    {
        //        if (WaterVolumeLast.HasValue)
        //        {
        //            return ((WaterVolumeLast.Value / WaterVolumeCapacity)).ToString("P2");
        //        }

        //        return "0 %";
        //    }
        //    private set { }
        //}

        public Decimal WaterVolumePercentage
        {
            get
            {
                if (WaterVolumeLast.HasValue)
                {
                    return (WaterVolumeLast.Value / WaterVolumeCapacity) * 100;
                }

                return 0;
            }
            private set { }
        }

        public DateTime? WaterVolumeLastMeasurement { get; set; }

        public Decimal? WaterTemperature { get; set; }
        public DateTime? WaterTemperatureEventDate { get; set; }

        public Decimal? WeatherTemperature { get; set; }
        public DateTime? WeatherTemperatureEventDate { get; set; }

        public Int32? Alarms { get; set; }

        public String Color { get; set; }

        #endregion Property

        #region Map

        public static List<TankViewModel> Map(List<Core.Entities.Tank> entities)
        {
            List<TankViewModel> vms = new List<TankViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(TankViewModel.Map(c)));
            }

            return vms;
        }

        public static TankViewModel Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, TankViewModel>();
            return Mapper.Map<Core.Entities.Tank, TankViewModel>(entity);
        }

        #endregion Map
    }
}