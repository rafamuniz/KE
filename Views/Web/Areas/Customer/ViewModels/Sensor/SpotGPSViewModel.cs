using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor
{
    public class SpotGPSViewModel
    {
        #region Property

        [Display(Name = "FindMeSpot")]
        public String FindMeSpotUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["FindMeSpot:Url"];
            }
        }

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