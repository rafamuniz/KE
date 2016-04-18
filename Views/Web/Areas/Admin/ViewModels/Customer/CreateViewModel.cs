﻿using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.Customer
{
    public class CreateViewModel
    {
        #region Property
        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "UserName")]
        [Required]
        [EmailAddress]
        public String UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public String Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public String PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(256)]
        public String Email { get; set; }

        [MaxLength(3)]
        [Display(Name = "Code")]
        public String PhoneNumberCountryCode { get; set; } = "1";

        [MaxLength(16)]
        [Display(Name = "Phone Number")]
        public String PhoneNumber { get; set; }

        [Display(Name = "Code")]
        [MaxLength(3)]
        [Required(ErrorMessage = "{0} cannot be null or empty")]
        public String MobileNumberCountryCode { get; set; } = "1";

        [Display(Name = "Mobile Number")]
        [MaxLength(16)]
        [Required(ErrorMessage = "{0} cannot be null or empty")]
        public String MobileNumber { get; set; }

        [Display(Name = "Address Line 1")]
        [MaxLength(256)]
        public String AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        [MaxLength(256)]
        public String AddressLine2 { get; set; }

        [Display(Name = "City")]
        [MaxLength(128)]
        public String City { get; set; }

        [Display(Name = "State")]
        [MaxLength(64)]
        public String State { get; set; }

        [Display(Name = "Country")]
        [MaxLength(64)]
        public String Country { get; set; } = "United States";

        [Display(Name = "Zip Code")]
        [MaxLength(16)]
        public String ZipCode { get; set; }

        #endregion Property

        #region Map

        public static CreateViewModel Map(Core.Entities.Contact entity)
        {
            Mapper.CreateMap<Core.Entities.Contact, CreateViewModel>();
            return Mapper.Map<Core.Entities.Contact, CreateViewModel>(entity);
        }

        public Core.Entities.Contact Map()
        {
            Mapper.CreateMap<CreateViewModel, Core.Entities.Contact>();
            return Mapper.Map<CreateViewModel, Core.Entities.Contact>(this);
        }

        #endregion Map
    }
}