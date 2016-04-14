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

        [Display(Name = "SiteId")]
        [Required]
        public Guid SiteId { get; set; }

        public Guid TankId { get; set; }

        [Display(Name = "Name")]
        public String TankName { get; set; }

        public String ImageTankModelFilename { get; set; }

        public Decimal WaterVolumeCapacity { get; set; }

        public Decimal? WaterVolumePercentage
        {
            get
            {
                if (WaterVolume != default(Decimal))
                {
                    return WaterVolume / WaterVolumeCapacity;
                }
                return null;
            }
            set { }
        }

        public Decimal WaterVolumeRemaining
        {
            get { return WaterVolumeCapacity - WaterVolume; }
            set { }
        }

        public Decimal WaterVolume { get; set; }
        public DateTime? WaterVolumeLastMeasurement { get; set; }

        public Decimal AmbientTemperature { get; set; }
        public DateTime? AmbientTemperatureLastMeasurement { get; set; }

        public Decimal WaterTemperature { get; set; }
        public DateTime? WaterTemperatureLastMeasurement { get; set; }

        public Int32? Alarms { get; set; }

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