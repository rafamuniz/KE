using System;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
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