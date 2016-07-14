using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.User
{
    public class CreateViewModel
    {
        #region Contructor
        public CreateViewModel()
        {
            this.Address = new AddressViewModel();
        }
        #endregion Contructor

        #region Property
        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "UserName")]
        [Required]
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
        [Display(Name = "Role")]
        public String Role { get; set; }

        public AddressViewModel Address { get; set; }
        #endregion Property

        #region Map

        public Core.Entities.User Map()
        {
            return Mapper.Map<CreateViewModel, Core.Entities.User>(this);
        }

        public Core.Entities.Address MapAddress()
        {

            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address);
        }

        #endregion Map
    }
}