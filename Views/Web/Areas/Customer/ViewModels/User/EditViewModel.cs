using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.User
{
    public class EditViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public String Email { get; set; }
                
        [Required]
        [Display(Name = "Role")]
        public String Role { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public String PasswordConfirm { get; set; }

        #region Map

        public static EditViewModel Map(Core.Entities.CustomerUser entity)
        {
            Mapper.CreateMap<Core.Entities.CustomerUser, EditViewModel>();
            return Mapper.Map<Core.Entities.CustomerUser, EditViewModel>(entity);
        }

        #endregion Map
    }
}