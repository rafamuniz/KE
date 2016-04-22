using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.User
{
    public class CreateViewModel
    {
        #region Constructor
        public CreateViewModel()
        {
            Address = new AddressViewModel();
        }

        #endregion Constructor

        #region Property

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Username")]
        [EmailAddress]
        public String Username { get; set; }

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

        public Core.Entities.CustomerUser Map()
        {
            Mapper.CreateMap<CreateViewModel, Core.Entities.CustomerUser>().ForMember(x => x.Address, opt => opt.Ignore());
            return Mapper.Map<CreateViewModel, Core.Entities.CustomerUser>(this);
        }

        public static CreateViewModel Map(Core.Entities.Address entity)
        {
            Mapper.CreateMap<Core.Entities.Address, CreateViewModel>();
            return Mapper.Map<Core.Entities.Address, CreateViewModel>(entity);
        }

        public Core.Entities.Address MapAddress()
        {
            Mapper.CreateMap<AddressViewModel, Core.Entities.Address>();
            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address);
        }

        #endregion Map
    }
}