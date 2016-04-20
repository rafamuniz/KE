using Munizoft.MVC.Helpers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor
{
    public class CreateViewModel
    {
        #region Property

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Reference")]
        [MaxLength(8)]
        public String Reference { get; set; }

        [Display(Name = "Status")]
        [DefaultValue("A")]
        [Required]
        public String Status { get; set; } = "A";

        [Display(Name = "Sensor Type")]
        [Required]
        public Int16 SensorTypeId { get; set; }

        [Display(Name = "Tank")]
        [Required]
        public Guid TankId { get; set; }

        [Display(Name = "Spot GPS")]        
        public String SpotGPS { get; set; }

        [Display(Name = "Items")]
        public String[] Items { get; set; }

        public IEnumerable<ItemViewModel> AvailableItems { get; set; }
        public IEnumerable<ItemViewModel> SelectedItems { get; set; }

        #endregion Property
    }
}