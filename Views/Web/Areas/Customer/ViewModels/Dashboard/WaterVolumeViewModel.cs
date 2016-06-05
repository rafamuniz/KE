using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
{
    public class WaterVolumeViewModel
    {
        #region Property
        public Guid Id { get; set; }
        public Decimal WaterVolume { get; set; }
        public DateTime EventDate { get; set; }
              
        #endregion Property        
    }
}