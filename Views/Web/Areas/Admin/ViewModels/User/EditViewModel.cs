using AutoMapper;
using KarmicEnergy.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.User
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
        public String Email { get; set; }

        [Required]
        [Display(Name = "Role")]
        public String Role { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(ApplicationUser entity)
        {
            Mapper.CreateMap<ApplicationUser, EditViewModel>();
            return Mapper.Map<ApplicationUser, EditViewModel>(entity);
        }

        #endregion Map
    }
}