using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class DashboardTankViewModel
    {
        #region Property

        public Guid TankId { get; set; }

        [Display(Name = "Name")]
        public String TankName { get; set; }

        [Display(Name = "SiteId")]
        [Required]
        public Guid SiteId { get; set; }

        public String WaterVolume { get; set; }
        public DateTime WaterVolumeLastMeasurement { get; set; }

        public String AmbientTemperature { get; set; }
        public DateTime AmbientTemperatureLastMeasurement { get; set; }

        public String WaterTemperature { get; set; }
        public DateTime WaterTemperatureLastMeasurement { get; set; }

        public String Alarms { get; set; }

        public String Color { get; set; }

        #endregion Property

        #region Map

        public static List<DashboardTankViewModel> Map(List<Core.Entities.Tank> entities)
        {
            List<DashboardTankViewModel> vms = new List<DashboardTankViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(DashboardTankViewModel.Map(c)));
            }

            return vms;
        }

        public static DashboardTankViewModel Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, DashboardTankViewModel>();
            return Mapper.Map<Core.Entities.Tank, DashboardTankViewModel>(entity);
        }

        #endregion Map
    }
}