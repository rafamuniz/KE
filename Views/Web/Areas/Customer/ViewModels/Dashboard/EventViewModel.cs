using System;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
{
    public class EventViewModel
    {
        #region Property

        public Guid Id{ get; set; }
        public String Value { get; set; }
        public DateTime EventDate { get; set; }
              
        #endregion Property        
    }
}