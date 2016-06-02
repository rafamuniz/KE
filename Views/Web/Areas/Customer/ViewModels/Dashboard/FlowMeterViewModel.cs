using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
{
    public class FlowMeterViewModel
    {
        #region Constructor
        public FlowMeterViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Decimal? RateFlow { get; set; }
        public DateTime? RateFlowLastMeasurement { get; set; }

        public Int32? Totalizer { get; set; }
        public DateTime? TotalizerLastMeasurement { get; set; }
        
        #endregion Property        
    }
}