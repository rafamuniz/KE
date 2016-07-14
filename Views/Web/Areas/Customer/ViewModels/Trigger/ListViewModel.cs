using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger
{
    public class ListViewModel
    {
        #region Constructor
        public ListViewModel()
        {
            Triggers = new List<TriggerViewModel>();
        }
        #endregion Constructor

        #region Property

        [Display(Name = "Site")]
        public Guid SiteId { get; set; }

        [Display(Name = "Pond")]
        public Guid? PondId { get; set; }

        [Display(Name = "Tank")]
        public Guid? TankId { get; set; }

        public List<TriggerViewModel> Triggers { get; set; }

        #endregion Property        
    }
}