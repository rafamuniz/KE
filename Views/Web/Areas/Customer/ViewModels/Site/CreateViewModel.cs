﻿using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site
{
    public class CreateViewModel
    {
        #region Constructor

        public CreateViewModel()
        {
            Address = new SiteAddressViewModel();
        }

        #endregion Constructor

        #region Property
        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Reference")]
        [Required]
        [MaxLength(8)]
        public String Reference { get; set; }

        [Display(Name = "IP")]
        [Required]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")]
        public String IPAddress { get; set; }

        [Display(Name = "Status")]
        [DefaultValue("A")]
        [Required]
        public String Status { get; set; } = "A";

        [Display(Name = "Latitude")]
        public String Latitude { get; set; }

        [Display(Name = "Longitude")]
        public String Longitude { get; set; }

        public SiteAddressViewModel Address { get; set; }

        #endregion Property

        #region Map

        public Core.Entities.Site Map()
        {
            return Mapper.Map<CreateViewModel, Core.Entities.Site>(this);
        }

        public static CreateViewModel Map(Core.Entities.Address entity)
        {
            return Mapper.Map<Core.Entities.Address, CreateViewModel>(entity);
        }

        public Core.Entities.Address MapAddress()
        {
            return Mapper.Map<SiteAddressViewModel, Core.Entities.Address>(this.Address);
        }

        #endregion Map
    }
}