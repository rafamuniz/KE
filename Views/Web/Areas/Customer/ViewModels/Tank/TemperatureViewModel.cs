using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class TemperatureViewModel
    {
        public TemperatureViewModel()
        {
            WaterTempertatures = new List<EventViewModel>();
            WeatherTempertatures = new List<EventViewModel>();
        }

        #region Property

        public List<EventViewModel> WaterTempertatures { get; set; }
        public List<EventViewModel> WeatherTempertatures { get; set; }

        #endregion Property        
    }
}