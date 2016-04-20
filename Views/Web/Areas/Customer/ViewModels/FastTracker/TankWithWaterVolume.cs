using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class TankWithWaterVolume
    {
        public TankWithWaterVolume()
        {
            WaterVolumeInfos = new List<WaterVolumeViewModel>();
        }

        #region Property

        public Guid SiteId { get; set; }

        public Guid TankId { get; set; }


        public String TankName { get; set; }

        public String UrlImageTankModel { get; set; }

        public Guid SensorItemId { get; set; }

        public Decimal WaterVolumeCapacity { get; set; }
        public Decimal WaterVolume { get; set; }
        public Decimal WaterVolumePerc
        {
            get
            {
                if (WaterVolumeCapacity != 0)
                {
                    return (WaterVolume / WaterVolumeCapacity);
                }
                return 0;
            }
        }

        public DateTime? EventDate { get; set; }

        public List<WaterVolumeViewModel> WaterVolumeInfos { get; set; }

        #endregion Property        
    }
}