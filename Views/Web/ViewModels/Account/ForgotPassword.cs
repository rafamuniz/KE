using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public String Username { get; set; }
    }
}