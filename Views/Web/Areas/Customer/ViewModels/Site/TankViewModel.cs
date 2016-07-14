using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site
{
    public class TankViewModel
    {
        #region Property
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Decimal? WaterVolumeCapacity { get; set; }

        public Decimal? WaterVolume { get; set; }

        public DateTime? WaterVolumeLastMeasurement { get; set; }

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

        public Decimal WaterVolumeRemaining
        {
            get { return WaterVolumeCapacity.Value - WaterVolume.Value; }
            set { }
        }

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
           
            return Mapper.Map<Core.Entities.Tank, TankViewModel>(entity);
        }

        #endregion Map
    }
}