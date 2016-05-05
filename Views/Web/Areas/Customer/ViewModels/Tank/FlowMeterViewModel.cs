using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class FlowMeterViewModel
    {
        #region Constructor
        public FlowMeterViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Guid TankId { get; set; }

        public String TankName { get; set; }

        public Decimal? RateFlow { get; set; }

        public Int32? Totalizer { get; set; }
        #endregion Property        
    }
}