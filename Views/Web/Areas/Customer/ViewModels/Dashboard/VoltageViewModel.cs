using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
{
    public class VoltageViewModel
    {
        public VoltageViewModel()
        {
            Voltages = new List<EventViewModel>();
        }

        #region Property

        public List<EventViewModel> Voltages { get; set; }

        #endregion Property        
    }
}