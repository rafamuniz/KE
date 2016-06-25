using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site.Sensor
{
    public class SensorCreateViewModel
    {
        #region Constructor
        public SensorCreateViewModel()
        {
            Items = new List<SensorItemViewModel>();
        }

        #endregion Constructor

        #region Property

        [Display(Name = "Site")]
        public Guid SiteId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Reference")]
        [MaxLength(8)]
        [Required]
        public String Reference { get; set; }

        [Display(Name = "Status")]
        [DefaultValue("A")]
        [Required]
        public String Status { get; set; } = "A";

        [Display(Name = "Sensor Type")]
        [Required]
        public Int16 SensorTypeId { get; set; }

        [Display(Name = "Spot GPS")]
        public String SpotGPS { get; set; }

        public IList<SensorItemViewModel> Items { get; set; }

        #endregion Property
    }
}