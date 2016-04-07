using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class WaterVolumeInfo
    {
        #region Property
      
        public Decimal WaterVolume { get; set; }
        public DateTime EventDate { get; set; }
              
        #endregion Property        
    }
}