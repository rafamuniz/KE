using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "EMAIL")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "PASSWORD")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}