using Munizoft.MVC.Helpers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor
{
    public class SpotGPSViewModel
    {
        #region Property
        [Display(Name = "Spot ID")]
        public String SpotId { get; set; }

        [Display(Name = "Name")]
        [MaxLength(128)]
        public String SpotName { get; set; }

        [Display(Name = "Battery")]
        public String Battery { get; set; }

        [Display(Name = "Last Contact")]
        public String LastContact { get; set; }

        [Display(Name = "Last Locator")]
        public String LastLocator { get; set; }

        [Display(Name = "Model")]
        public String Model { get; set; }

        #endregion Property
    }
}