using AutoMapper;
using KarmicEnergy.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.Customer
{
    public class EditViewModel
    {
        #region Property
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(256)]
        public String Email { get; set; }

        [MaxLength(3)]
        public String PhoneNumberCountryCode { get; set; }

        [MaxLength(16)]
        public String PhoneNumber { get; set; }

        [MaxLength(3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public String MobileNumberCountryCode { get; set; }

        [MaxLength(16)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public String MobileNumber { get; set; }

        [MaxLength(256)]
        public String AddressLine1 { get; set; }

        [MaxLength(256)]
        public String AddressLine2 { get; set; }

        [MaxLength(128)]
        public String City { get; set; }

        [MaxLength(64)]
        public String State { get; set; }

        [MaxLength(64)]
        public String Country { get; set; }

        [MaxLength(16)]
        public String ZipCode { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(Core.Entities.Customer entity)
        {
            Mapper.CreateMap<Core.Entities.Customer, EditViewModel>();
            return Mapper.Map<Core.Entities.Customer, EditViewModel>(entity);
        }

        public static EditViewModel Map(ApplicationUser entity)
        {
            Mapper.CreateMap<ApplicationUser, EditViewModel>();
            return Mapper.Map<ApplicationUser, EditViewModel>(entity);
        }

        public static EditViewModel Map(Core.Entities.Contact entity)
        {
            Mapper.CreateMap<Core.Entities.Contact, EditViewModel>();
            return Mapper.Map<Core.Entities.Contact, EditViewModel>(entity);
        }

        public Core.Entities.Contact Map()
        {
            Mapper.CreateMap<EditViewModel, Core.Entities.Contact>();
            return Mapper.Map<EditViewModel, Core.Entities.Contact>(this);
        }

        #endregion Map
    }
}