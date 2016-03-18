using AutoMapper;
using KarmicEnergy.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.Customer
{
    public class EditViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public String Email { get; set; }

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

        #endregion Map
    }
}