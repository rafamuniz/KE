using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class TankWithWaterVolume
    {
        #region Property

        public Guid TankId { get; set; }
        public String TankName { get; set; }

        public String UrlImageTankModel { get; set; }

        public Decimal WaterVolumeCapacity { get; set; }
        public Decimal WaterVolume { get; set; }
        public Decimal WaterVolumePerc
        {
            get
            {
                return (WaterVolume / WaterVolumeCapacity) * 100;
            }
        }
        public DateTime CollectedDate { get; set; }

        #endregion Property        
    }
}