using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class GaugeViewModel
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
                if (WaterVolume.HasValue)
                {
                    return WaterVolume / WaterVolumeCapacity;
                }
                return null;
            }
            set { }
        }

        public Decimal? WaterVolumeRemaining
        {
            get
            {
                if (WaterVolume.HasValue)
                {
                    return WaterVolumeCapacity - WaterVolume;
                }

                return null;
            }
            set { }
        }

        public Decimal? WaterVolume { get; set; }
        public DateTime? WaterVolumeLastMeasurement { get; set; }

        public List<TriggerViewModel> Triggers { get; set; }

        #endregion Property

        #region Map

        public static List<GaugeViewModel> Map(List<Core.Entities.Tank> entities)
        {
            List<GaugeViewModel> vms = new List<GaugeViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(GaugeViewModel.Map(c)));
            }

            return vms;
        }

        public static GaugeViewModel Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, GaugeViewModel>();
            return Mapper.Map<Core.Entities.Tank, GaugeViewModel>(entity);
        }

        #endregion Map
    }
}