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

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public String Email { get; set; }

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
        #endregion Property
    }
}