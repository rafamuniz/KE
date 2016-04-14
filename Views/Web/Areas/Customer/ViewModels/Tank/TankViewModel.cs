using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class TankViewModel
    {
        public TankViewModel()
        {
            WaterVolumes = new List<WaterVolumeViewModel>();
            Voltages = new List<EventViewModel>();
        }

        #region Property

        public Guid SiteId { get; set; }

        public Guid TankId { get; set; }

        public String TankName { get; set; }

        public String UrlImageTankModel { get; set; }

        public Decimal WaterVolumeCapacity { get; set; }

        public List<WaterVolumeViewModel> WaterVolumes { get; set; }
        public List<EventViewModel> Voltages { get; set; }

        #endregion Property        
    }
}