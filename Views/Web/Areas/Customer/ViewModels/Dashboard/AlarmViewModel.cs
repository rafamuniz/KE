using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
{
    public class AlarmViewModel
    {
        #region Constructor
        public AlarmViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }
        public Guid TriggerId { get; set; }

        public String Item { get; set; }

        public DateTime StartDate { get; set; }

        #endregion Property        
    }
}