using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring
{
    public class ClearAlarmViewModel
    {
        #region Constructor
        public ClearAlarmViewModel()
        {

        }
        #endregion Constructor

        #region Property

        [Required]
        public Guid AlarmId { get; set; }

        [Required]
        [Display(Name = "Message")]
        public String Message { get; set; }

        public AlarmDetailViewModel Detail { get; set; }

        #endregion Property
    }
}