﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor
{
    public class CreateViewModel
    {
        #region Constructor
        public CreateViewModel()
        {
            Items = new List<ItemViewModel>();
        }

        #endregion Constructor

        #region Property
        
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

        [Display(Name = "Site")]
        public Guid SiteId { get; set; }

        [Display(Name = "Tank")]
        public Guid? TankId { get; set; }

        [Display(Name = "Pond")]
        public Guid? PondId { get; set; }

        public Boolean IsSensorSite { get; set; } = false;

        [Display(Name = "Spot GPS")]
        public String SpotGPS { get; set; }

        public IList<ItemViewModel> Items { get; set; }

        #endregion Property
    }
}